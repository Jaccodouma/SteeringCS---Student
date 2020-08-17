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
        protected Dictionary<string, FuzzyVariable> variables = new Dictionary<string, FuzzyVariable>();
        protected List<FuzzyRule> rules = new List<FuzzyRule>();

        //creates an empty fuzzyvariable
        public FuzzyVariable CreateFLV(string name)
        {
            variables.Add(name, new FuzzyVariable());
            return variables[name];
        }

        //add rule to fuzzy module
        public void Addrule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            rules.Add(new FuzzyRule(antecedent, consequence));
        }

        public void Fuzzify(string FLV, double value)
        {
            if (variables.ContainsKey(FLV))
            {
                variables[FLV].Fuzzify(value);
            }
        }

        public double DeFuzzify(string key)
        {
            if (variables.ContainsKey(key))
            {
                foreach (FuzzyRule r in rules)
                {
                    r.SetConfidenceOfConsequentToZero();
                }

                foreach (FuzzyRule r in rules)
                {
                    r.Calculate();
                }

                return variables[key].DefuzzifyMaxAV();
            }
            else throw new KeyNotFoundException();
        }
    }
}
