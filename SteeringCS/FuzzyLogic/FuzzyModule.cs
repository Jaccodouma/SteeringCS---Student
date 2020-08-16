using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.FuzzyLogic
{
    class FuzzyModule
    {
        protected Dictionary<string, FuzzyVariable> variables;
        protected List<FuzzyRule> rules;

        //creates an empty fuzzyvariable
        public FuzzyVariable CreateFLV(string name)
        {
            return new FuzzyVariable();
        }

        //add rule to fuzzy module
        public void Addrule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            rules.Add(new FuzzyRule());
        }

        public void Fuzzify(string FLV, double value)
        {
            return;
        }

        public double DeFuzzify(string key)
        {
            return 0;
        }
    }
}
