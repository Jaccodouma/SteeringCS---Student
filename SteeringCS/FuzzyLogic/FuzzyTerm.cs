using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.FuzzyLogic
{
    class FuzzyTerm
    {
        public List<FuzzyTerm> terms;

        virtual public FuzzyTerm Clone() { return null; }

        virtual public double GetDOM() { return 0; }

        virtual public void ClearDOM() { return; }

        virtual public void ORwithDOM(double val) { return; }
    }
}
