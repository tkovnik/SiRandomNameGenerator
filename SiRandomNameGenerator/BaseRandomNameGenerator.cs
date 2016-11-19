using System;
using System.Collections.Generic;

namespace SiRandomNameGenerator
{
    public abstract class BaseRandomNameGenerator
    {
        protected readonly Random _Rand;

        public BaseRandomNameGenerator(Random rand)
        {
            if (rand == null)
                throw new ArgumentNullException(nameof(rand));

            _Rand = rand;
        }

        protected string[] ReadResource(string resource)
        {
            List<string> lst = new List<string>();

            foreach (var item in resource.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                lst.Add(item);
            }

            return lst.ToArray();
        }
    }
}
