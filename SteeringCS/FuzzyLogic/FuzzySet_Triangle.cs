using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.FuzzyLogic
{
    class FuzzySet_Triangle : FuzzySet
    {

        double peak, leftOffSet, rightOffSet;
        public FuzzySet_Triangle(double mid, double lft, double rgt) : base(mid)
        {
            peak = mid;
            leftOffSet = lft;
            rightOffSet = rgt;
        }

        public override double CalculateDOM(double val)
        {
            //prevent divide by zero
            if (rightOffSet.Equals(0.0) && peak.Equals(val) ||
                leftOffSet.Equals(0.0) && peak.Equals(val)) return 1.0;


            //find DOM if left of center
            if((val <= peak) && (val >= (peak - leftOffSet)))
            {
                double grad = 1.0 / leftOffSet;

                return grad * (val - (peak - leftOffSet));
            }

            //find DOM if right of center
            else if ((val <= peak) && (val >= (peak - leftOffSet)))
            {
                double grad = 1.0 / -rightOffSet;

                return grad * (val - peak)+ 1;
            }
            //out of range of this FLV
            else  return 0;
        }
    }
}
