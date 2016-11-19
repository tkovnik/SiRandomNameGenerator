using SiRandomNameGenerator;
using SiRandomNameGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //PersonNameGenerator nameGen = new PersonNameGenerator();



            //string emso = nameGen.GeneratePIN(new DateTime(1980, 12, 5), SiRandomNameGenerator.Model.Sex.Female, 550);

            //Person p = nameGen.GetRandomPerson(false);

            //var lst = nameGen.GetRandomPersonList(20, Sex.Undefined, 10);

            PersonNameGenerator generator = new PersonNameGenerator();

            string name = generator.GetRandomName(Sex.Female);
            
        }
    }
}
