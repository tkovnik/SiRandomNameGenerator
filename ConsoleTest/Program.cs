using SiRandomNameGeneratorNETStandard;
using SiRandomNameGeneratorNETStandard.Model;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonNameGenerator nameGen = new PersonNameGenerator();



            string emso = nameGen.GeneratePIN(new DateTime(1980, 12, 5), SiRandomNameGeneratorNETStandard.Model.Sex.Female, 550);

            Person p = nameGen.GetRandomPerson(false);

            var lst = nameGen.GetRandomPersonList(20, Sex.Undefined, 10);

        }
    }
}
