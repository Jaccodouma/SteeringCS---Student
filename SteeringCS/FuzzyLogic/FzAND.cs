﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.FuzzyLogic
{
    class FzAND : FuzzyTerm
    {
        public FzAND(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
        {
            terms = new List<FuzzyTerm>();
            this.terms.Add(op1.Clone());
            this.terms.Add(op2.Clone());
            this.terms.Add(op3.Clone());
            this.terms.Add(op4.Clone());
            Console.WriteLine("check");
        }
        public FzAND(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
        {
            terms = new List<FuzzyTerm>();
            this.terms.Add(op1.Clone());
            this.terms.Add(op2.Clone());
            this.terms.Add(op3.Clone());
            Console.WriteLine("check");
        }
        public FzAND(FuzzyTerm op1, FuzzyTerm op2)
        {
            terms = new List<FuzzyTerm>();
            this.terms.Add(op1.Clone());
            this.terms.Add(op2.Clone());
            Console.WriteLine("check");
        }

        public FzAND(FuzzyTerm term)
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
            return new FzAND(this);
        }

        public override double GetDOM()
        {
            double smallest = double.MaxValue;
            foreach (FuzzyTerm t in terms)
            {
                if (t.GetDOM() < smallest) smallest = t.GetDOM();
            }
            return smallest;
        }

        public override void ORwithDOM(double val)
        {
            foreach (FuzzyTerm t in terms)
            {
                t.ORwithDOM(val);
            }
        }
    }
}
