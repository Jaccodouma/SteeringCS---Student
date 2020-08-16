using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.FuzzyLogic
{
    class FzSet : FuzzyTerm
    {
        private FuzzySet set;
        public FzSet(FuzzySet set)
        {
            this.set = set;
        }

        public override void ClearDOM()
        {
            this.set.ClearDOM();
        }

        public override FuzzyTerm Clone()
        {
            return new FzSet(this.set);
        }

        public override double GetDOM()
        {
            return this.set.GetDOM();
        }

        public override void ORwithDOM(double val)
        {
            this.set.ORwithDOM(val);
        }
    }
}
