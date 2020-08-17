using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.FuzzyLogic
{
    class FzOR : FuzzyTerm
    {
        public FzOR(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
        {
            terms = new List<FuzzyTerm>();
            this.terms.Add(op1.Clone());
            this.terms.Add(op2.Clone());
            this.terms.Add(op3.Clone());
            this.terms.Add(op4.Clone());
            Console.WriteLine("check");
        }
        public FzOR(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
        {
            terms = new List<FuzzyTerm>();
            this.terms.Add(op1.Clone());
            this.terms.Add(op2.Clone());
            this.terms.Add(op3.Clone());
            Console.WriteLine("check");
        }
        public FzOR(FuzzyTerm op1, FuzzyTerm op2)
        {
            terms = new List<FuzzyTerm>();
            this.terms.Add(op1.Clone());
            this.terms.Add(op2.Clone());
        }
        public FzOR(FuzzyTerm term)
        {
            terms = new List<FuzzyTerm>();
            foreach (FuzzyTerm t in term.terms)
            {
                this.terms.Add(t.Clone());
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
            return new FzOR(this);
        }

        public override double GetDOM()
        {
            double largest = double.MinValue;
            foreach(FuzzyTerm t in terms)
            {
                if (t.GetDOM() > largest) largest = t.GetDOM();
            }
            return largest;
        }

        public override void ORwithDOM(double val)
        {
            base.ORwithDOM(val);
        }
    }
}
