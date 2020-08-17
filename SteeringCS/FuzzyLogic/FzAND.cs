using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.FuzzyLogic
{
    class FzAND : FuzzyTerm
    {
        private List<FuzzyTerm> terms = new List<FuzzyTerm>();

        public FzAND(FuzzyTerm op1, FuzzyTerm op2) 
        {
            terms.Add(op1);
            terms.Add(op2);
            Console.WriteLine("check");
        }

        public FzAND(List<FuzzyTerm> terms)
        {
            foreach(FuzzyTerm t in terms)
            {
                this.terms.Add(t);
            }
        }

        public override void ClearDOM()
        {
            foreach (FuzzyTerm t in terms)
            {
                t.ClearDOM();
            }
        }

        public override FuzzyTerm Clone()
        {
            return new FzAND(terms);
        }

        public override double GetDOM()
        {
            double smallest = double.MaxValue;
            foreach(FuzzyTerm t in terms)
            {
                if (t.GetDOM() < smallest) smallest = t.GetDOM();
            }
            return smallest;
        }

        public override void ORwithDOM(double val)
        {
            foreach(FuzzyTerm t in terms)
            {
                t.ORwithDOM(val);
            }
        }
    }
}
