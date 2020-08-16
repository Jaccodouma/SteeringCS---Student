using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.FuzzyLogic
{
    class FuzzyRule
    {
        private FuzzyTerm antecedent, consequence;

        public FuzzyRule(FuzzyTerm ant, FuzzyTerm con)
        {
            this.antecedent = ant.Clone();
            this.consequence = con.Clone();
        }

        public void SetConfidenceOfConsequentToZero()
        {
            this.consequence.ClearDOM();
        }

        public void Calculate()
        {
            Console.WriteLine("Consequence1: " + consequence.GetDOM() + " class:" + consequence.GetType().Name);
            Console.WriteLine("Antecedent1: " + antecedent.GetDOM() + " class:" + consequence.GetType().Name);
            this.consequence.ORwithDOM(antecedent.GetDOM());
            Console.WriteLine("Consequence2: " + consequence.GetDOM() + " class:" + consequence.GetType().Name);
            Console.WriteLine("Antecedent2: " + antecedent.GetDOM() + " class:" + consequence.GetType().Name);
        }
    }
}
