using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.FuzzyLogic
{
    class FuzzySet_RightShoulder : FuzzySet
    {
        double peakPoint, leftOffSet, rightOffSet;
        public FuzzySet_RightShoulder(double peak, double lftOS, double rgtOS) : base(((peak + rgtOS) + peak) / 2)
        {
            this.peakPoint = peak;
            leftOffSet = lftOS;
            rightOffSet = rgtOS;
        }

        public override double CalculateDOM(double val)
        {
            //prevent divide by zero
            if (leftOffSet.Equals(0.0) && val.Equals(peakPoint)) return 1.0;

            //find DOM if left of center
            if ((val <= peakPoint) && (val > (peakPoint - leftOffSet)))
            {
                double grad = 1.0 / leftOffSet;

                return grad * (val - (peakPoint - leftOffSet));
            }

            //find DOM if right of center
            else if (val > peakPoint)
            {
                return 1.0;
            }
            //out of range of this FLV
            else return 0;
        }
    }
}
