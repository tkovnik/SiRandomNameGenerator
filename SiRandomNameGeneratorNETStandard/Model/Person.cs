using System;

namespace SiRandomNameGeneratorNETStandard.Model
{
    public class Person
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        //EMŠO
        public string PIN { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime? DateOfDeath { get; set; }
        public Sex Sex { get; set; }
    }

    public enum Sex
    {
        Male = 1,
        Female = 2,
        Undefined = 3
    }
}
