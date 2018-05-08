using SiRandomNameGeneratorNETStandard.Asset;
using SiRandomNameGeneratorNETStandard.Model;
using System;
using System.Collections.Generic;

namespace SiRandomNameGeneratorNETStandard
{
    public class PersonNameGenerator : BaseRandomNameGenerator
    {
        #region Fields

        private string[] _AvailableMaleNames;
        private string[] _AvailableFemaleNames;
        private string[] _AvailableLastNames;

        //cca 115 years
        private int MAX_LIVED_DAYS = 41900;

        #endregion

        #region Constructors

        public PersonNameGenerator()
            : this(new Random())
        {

        }

        public PersonNameGenerator(Random rand)
            : base(rand)
        {
            InitializeMaleNames();
            InitializeFemaleNames();
            InitializeLastNames();
        }

        #endregion

        #region Simple Randomizer

        public string GetRandomMaleName()
        {
            return _AvailableMaleNames[_Rand.Next(0, _AvailableMaleNames.Length)];
        }

        public string GetRandomFemaleName()
        {
            return _AvailableFemaleNames[_Rand.Next(0, _AvailableFemaleNames.Length)];
        }

        public string GetRandomLastName()
        {
            return _AvailableLastNames[_Rand.Next(0, _AvailableLastNames.Length)];
        }

        public Sex GetRandomSex()
        {
            return (Sex)_Rand.Next(1, 3);
        }

        public string GetRandomName(Sex sex = Sex.Undefined)
        {
            if (sex == Sex.Undefined)
                sex = GetRandomSex();

            if (sex == Sex.Male)
                return GetRandomMaleName();
            else
                return GetRandomFemaleName();
        }

        public string GetRandomNameAndLastName(Sex sex = Sex.Undefined)
        {
            return GetRandomName(sex) + " " + GetRandomLastName();
        }

        public IEnumerable<string> GetRandomNameList(int count = 10, Sex sex = Sex.Undefined)
        {
            List<string> lst = new List<string>();

            for (int i = 1; i <= count; i++)
            {
                lst.Add(GetRandomName(sex));
            }

            return lst;
        }

        public IEnumerable<string> GetRandomNameAndLastNameList(int count = 10, Sex sex = Sex.Undefined)
        {
            List<string> lst = new List<string>();

            for (int i = 1; i <= count; i++)
            {
                lst.Add(GetRandomNameAndLastName(sex));
            }

            return lst;
        }

        #endregion

        #region Person randomizer

        public Person GetRandomPerson(bool isAlive = true, Sex sex = Sex.Undefined)
        {
            Person p = new Person();

            if (sex == Sex.Undefined)
                p.Sex = GetRandomSex();
            else
                p.Sex = sex;

            p.FirstName = GetRandomName(p.Sex);
            p.LastName = GetRandomLastName();
            
            p.DateOfBirth = GetInitialDateOfBirthDate().AddDays(_Rand.Next(0, MAX_LIVED_DAYS));
            p.PIN = GeneratePIN(p.DateOfBirth, p.Sex);

            if(!isAlive)
                p.DateOfDeath = p.DateOfBirth.AddDays(_Rand.Next(1, (DateTime.Now - p.DateOfBirth).Days));

            return p;
        }

        /// <summary>
        /// Method returns list of random persons
        /// </summary>
        /// <param name="count">Number of generated Persons</param>
        /// <param name="sex">Sex of generated Persons (Default is undefined)</param>
        /// <param name="deceasedRatio">Ratio of deceased persons (default value is 10%)</param>
        /// <returns>List of persons</returns>
        public IEnumerable<Person> GetRandomPersonList(int count = 10, Sex sex = Sex.Undefined, int deceasedRatio = 10)
        {
            List<Person> lst = new List<Person>();

            int maxDeceasedPerson = (count * deceasedRatio) / 100;
            int deceasedPersonCount = 0;

            for (int i = 0; i < count; i++)
            {
                Person p = null;

                if(deceasedPersonCount <= maxDeceasedPerson && maxDeceasedPerson > 0)
                {
                    if(_Rand.Next(1, 3) == 1)
                    {
                        p = GetRandomPerson(true, sex);
                    }
                    else
                    {
                        p = GetRandomPerson(false, sex);
                        deceasedPersonCount++;
                    }
                }
                else
                {
                    p = GetRandomPerson(true, sex);
                }

                lst.Add(p);
            }

            return lst;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method generates pin (EMŠO) based on sex and date of birth
        /// see: http://www.uradni-list.si/1/objava.jsp?urlid=19998&stevilka=345
        /// </summary>
        /// <param name="seqNum">sequence number of person</param>
        /// <returns>13 digit PIN</returns>
        public string GeneratePIN(DateTime dateOfBirth, Sex sex, int? seqNum = null)
        {
            //at this point we need walid sex and valid date of birth
            if (sex == Sex.Undefined)
                throw new ArgumentException(nameof(sex));
            if (dateOfBirth == DateTime.MinValue || dateOfBirth == DateTime.MaxValue)
                throw new ArgumentException(nameof(dateOfBirth));

            string rawPid = dateOfBirth.ToString("ddMM") +
                dateOfBirth.Year.ToString().Substring(1) +
                "50";

            int seqNumL = seqNum != null ? seqNum.Value : ((sex == Sex.Male) ? _Rand.Next(0, 499) : _Rand.Next(500, 999));
            rawPid += seqNumL.ToString("D3");

            int[] factor = new int[12] { 7, 6, 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };

            int pinSum = 0;
            for (int i = 0; i < factor.Length; i++)
            {
                pinSum += int.Parse(rawPid[i].ToString()) * factor[i];
            }

            int control = 0;
            int rem = pinSum % 11;

            string PIN = "";

            if(rem == 1)
            {
                //we have to get new number
                PIN = GeneratePIN(dateOfBirth, sex, seqNumL + 1);
            }
            else
            {
                control = 11 - rem;
                PIN = rawPid + control.ToString();
            }

            return PIN;
        }

        #endregion

        #region Init Methods

        private void InitializeMaleNames()
        {
            //lazy load
            if (_AvailableMaleNames == null)
                _AvailableMaleNames = ReadResource(Res.Name_Men);
        }

        private void InitializeFemaleNames()
        {
            if (_AvailableFemaleNames == null)
                _AvailableFemaleNames = ReadResource(Res.Name_Women);
        }

        private void InitializeLastNames()
        {
            if (_AvailableLastNames == null)
                _AvailableLastNames = ReadResource(Res.LastNames);
        }

        #endregion

        #region Helper Methods

        private DateTime GetInitialDateOfBirthDate()
        {
            return DateTime.Now.AddDays(-MAX_LIVED_DAYS);
        }

        #endregion
    }
}
