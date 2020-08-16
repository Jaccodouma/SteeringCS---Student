using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.FuzzyLogic
{
    class FuzzySet_LeftShoulder : FuzzySet
    {
        double peakPoint, leftOffSet, rightOffSet;
        public FuzzySet_LeftShoulder(double peak, double lftOS, double rgtOS) : base(((peak + rgtOS) + peak) / 2)
        {
            this.peakPoint = peak;
            leftOffSet = lftOS;
            rightOffSet = rgtOS;
        }

        public override double CalculateDOM(double val)
        {
            //prevent divide by zero
            if (rightOffSet.Equals(0.0) && val.Equals(peakPoint)) return 1.0;

            //find DOM if left of center
            if (val < peakPoint)
            {
                return 1.0;
            }

            //find DOM if right of center
            else if ((val <= peakPoint) && (val >= (peakPoint - leftOffSet)))
            {
                double grad = 1.0 / -rightOffSet;

                return grad * (val - peakPoint) + 1;
            }
            //out of range of this FLV
            else return 0;
        }
    }
}
