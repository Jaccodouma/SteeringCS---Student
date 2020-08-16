using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.FuzzyLogic
{
    class FuzzySet
    {
        //degree of membership
        double dom;

        //max value of fuzzyset. if a set is triangular this will be the peak of the triangle
        double representative_value;

        public FuzzySet(double repVal)
        {
            this.representative_value = repVal;
            this.dom = 0.0;
        }

        virtual public double CalculateDOM(double val)
        {
            return 0;
        }

        public void ORwithDOM(double val)
        {
            if (val > dom) this.dom = val;
            Console.WriteLine("dom: " + this.dom);
        }

        public double GetRepresentativeVal()
        {
            return this.representative_value;
        }

        public void ClearDOM()
        {
            this.dom = 0;
        }

        public double GetDOM()
        {
            return this.dom;
        }

        public void SetDOM(double val)
        {
            if (val <= 1 && val >= 0)
            {
                this.dom = val;
            }
            else throw new Exception("invalid value");
        }
    }
}
