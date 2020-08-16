using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.FuzzyLogic
{
    class FuzzyVariable
    {
        private Dictionary<string, FuzzySet> membersets = new Dictionary<string, FuzzySet>();
        double minRange, maxRange;

        public FuzzyVariable()
        {
            this.minRange = 0;
            this.maxRange = 0;
        }

        private void AdjustRangeToFit(double min, double max)
        {
            if (this.minRange > min) this.minRange = min;
            if (this.maxRange < max) this.maxRange = max;
        }

        public FzSet AddLeftShoulderSet(string name, double minbound, double peak, double maxbound)
        {
            FuzzySet_LeftShoulder leftShoulder = new FuzzySet_LeftShoulder(peak, minbound, maxbound);
            membersets.Add(name, leftShoulder);
            AdjustRangeToFit(minbound, maxbound);
            return new FzSet(leftShoulder);
        }

        public FzSet AddRightShoulderSet(string name, double minbound, double peak, double maxbound)
        {
            FuzzySet_RightShoulder rightShoulder = new FuzzySet_RightShoulder(peak, minbound, maxbound);
            membersets.Add(name, rightShoulder);
            AdjustRangeToFit(minbound, maxbound);
            return new FzSet(rightShoulder);
        }

        public FzSet AddTriangle(string name, double minbound, double peak, double maxbound)
        {
            FuzzySet_Triangle triangle = new FuzzySet_Triangle(peak, minbound, maxbound);
            membersets.Add(name, triangle);
            AdjustRangeToFit(minbound, maxbound);
            return new FzSet(triangle);
        }

        public void Fuzzify(double val)
        {
            if (val >= minRange && val <= maxRange)
            {
                foreach (KeyValuePair<string, FuzzySet> k in membersets)
                {
                    k.Value.ClearDOM();
                }
                foreach (KeyValuePair<string, FuzzySet> k in membersets)
                {
                    k.Value.SetDOM(k.Value.CalculateDOM(val));
                }
            }
            else throw new IndexOutOfRangeException("fuzzify value is out of bounce");
        }

        public double DefuzzifyMaxAV()
        {
            double bottom = 0.0;
            double top = 0.0;

            foreach(KeyValuePair<string, FuzzySet> k in membersets)
            {
                bottom += k.Value.GetDOM();
                top += k.Value.GetRepresentativeVal() * k.Value.GetDOM();
            }

            if (bottom.Equals(0)) return 0.0;
            return top/bottom;
        }
    }
}
