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

        public FuzzySet AddLeftShoulderSet(string name, double minbound, double peak, double maxbound)
        {
            FuzzySet_LeftShoulder leftShoulder = new FuzzySet_LeftShoulder(peak, minbound, maxbound);
            membersets.Add(name, leftShoulder);
            AdjustRangeToFit(minbound, maxbound);
            return leftShoulder;
        }

        public FuzzySet AddRightShoulderSet(string name, double minbound, double peak, double maxbound)
        {
            FuzzySet_RightShoulder rightShoulder = new FuzzySet_RightShoulder(peak, minbound, maxbound);
            membersets.Add(name, rightShoulder);
            AdjustRangeToFit(minbound, maxbound);
            return rightShoulder;
        }

        public FuzzySet AddTriangle(string name, double minbound, double peak, double maxbound)
        {
            FuzzySet_Triangle triangle = new FuzzySet_Triangle(peak, minbound, maxbound);
            membersets.Add(name, triangle);
            AdjustRangeToFit(minbound, maxbound);
            return triangle;
        }
    }
}
