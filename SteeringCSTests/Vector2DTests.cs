using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SteeringCS;

namespace SteeringCSTests
{
    [TestClass]
    public class Vector2DTests
    {
        [TestMethod]
        public void Length_returnsExpectedValue()
        {
            // Test some pythagorean triangles
            List<double[]> tris = new List<double[]>();
            tris.Add(new double[] { 3, 4, 5 });
            tris.Add(new double[] { 5, 12, 13 });
            tris.Add(new double[] { 8, 15, 17 });
            tris.Add(new double[] { 7, 24, 25 });

            tris.Add(new double[] { 20, 21, 29 });
            tris.Add(new double[] { 12, 35, 37 });
            tris.Add(new double[] { 9, 40, 41 });
            tris.Add(new double[] { 28, 45, 53 });

            tris.Add(new double[] { 11, 60, 61 });
            tris.Add(new double[] { 16, 63, 65 });
            tris.Add(new double[] { 33, 56, 65 });
            tris.Add(new double[] { 48, 55, 73 });

            tris.Add(new double[] { 13, 84, 85 });
            tris.Add(new double[] { 36, 77, 85 });
            tris.Add(new double[] { 39, 80, 89 });
            tris.Add(new double[] { 65, 72, 97 });

            foreach(double[] tri in tris)
            {
                Vector2D v = new Vector2D(tri[0], tri[1]);
                Assert.AreEqual(tri[2], v.Length());
            }
        }

        [TestMethod] 
        public void LengthSquared_returnsExpectedValue()
        {
            // Test some pythagorean triangles
            List<double[]> tris = new List<double[]>();
            tris.Add(new double[] {  3,  4,  5 });
            tris.Add(new double[] {  5, 12, 13 });
            tris.Add(new double[] {  8, 15, 17 });
            tris.Add(new double[] {  7, 24, 25 });

            tris.Add(new double[] { 20, 21, 29 });
            tris.Add(new double[] { 12, 35, 37 });
            tris.Add(new double[] {  9, 40, 41 });
            tris.Add(new double[] { 28, 45, 53 });

            tris.Add(new double[] { 11, 60, 61 });
            tris.Add(new double[] { 16, 63, 65 });
            tris.Add(new double[] { 33, 56, 65 });
            tris.Add(new double[] { 48, 55, 73 });

            tris.Add(new double[] { 13, 84, 85 });
            tris.Add(new double[] { 36, 77, 85 });
            tris.Add(new double[] { 39, 80, 89 });
            tris.Add(new double[] { 65, 72, 97 });

            foreach (double[] tri in tris)
            {
                Vector2D v = new Vector2D(tri[0], tri[1]);
                Assert.AreEqual(Math.Pow(tri[2], 2), v.LengthSquared());
            }
        }

        [TestMethod]
        public void Add_returnsExpectedValue()
        {
            List<double[]> vs = new List<double[]>();
            vs.Add(new double[] { 2,2,1,1,3,3 });
            vs.Add(new double[] { 4,4,-1,-1,3,3 });
            vs.Add(new double[] { 3, 5, -5, -2, -2, 3 });

            foreach (double[] v in vs)
            {
                Vector2D v0 = new Vector2D(v[0], v[1]);
                v0.Add(new Vector2D(v[2], v[3]));
                Assert.AreEqual(v[4], v0.X);
                Assert.AreEqual(v[5], v0.Y);
            }
        }

        [TestMethod]
        public void Sub_returnsExpectedValue()
        {
            List<double[]> vs = new List<double[]>();
            vs.Add(new double[] { 2, 2, 1, 1, 1, 1 });
            vs.Add(new double[] { 4, 4, -1, -1, 5, 5 });
            vs.Add(new double[] { 3, 5, -5, -2, 8, 7 });

            foreach (double[] v in vs)
            {
                Vector2D v0 = new Vector2D(v[0], v[1]);
                v0.Sub(new Vector2D(v[2], v[3]));
                Assert.AreEqual(v[4], v0.X);
                Assert.AreEqual(v[5], v0.Y);
            }
        }

        [TestMethod]
        public void Multipy_returnsExpectedValue()
        {
            List<double[]> test = new List<double[]>();
            test.Add(new double[] { 2,2,5,10,10});
            test.Add(new double[] { 1,4,2,2,8});
            test.Add(new double[] { -2,-4,2,-4,-8});
            test.Add(new double[] { 3,-5,-3,-9,15});
            //test.Add(new double[] { });

            foreach (double[] t in test)
            {
                Vector2D v = new Vector2D(t[0], t[1]);
                v.Multiply(t[2]);
                Assert.AreEqual(t[3], v.X);
                Assert.AreEqual(t[4], v.Y);
            }
        }

        [TestMethod]
        public void Divide_returnsExpectedValue()
        {
            List<double[]> test = new List<double[]>();
            test.Add(new double[] {4,6,2,2,3});
            test.Add(new double[] {4,6,-2,-2,-3});
            test.Add(new double[] {6,6,0,6,6});
            //test.Add(new double[] { });

            foreach(double[] t in test)
            {
                Vector2D v = new Vector2D(t[0], t[1]);
                v.Divide(t[2]);
                Assert.AreEqual(t[3], v.X);
                Assert.AreEqual(t[4], v.Y);
            }
        }

        [TestMethod]
        public void Normalize_lengthEquals1()
        {
            List<double[]> test = new List<double[]>();
            test.Add(new double[] {3,5});
            test.Add(new double[] {-1,-4});
            test.Add(new double[] {-5,3});
            test.Add(new double[] {7,-17});

            foreach (double[] t in test)
            {
                Vector2D v = new Vector2D(t[0], t[1]);
                Assert.AreEqual((double) 1, (double) v.Normalize().Length(), 0.00000001);
            }
        }

        [TestMethod]
        public void Truncate_LengthEqualsExpectedValue()
        {
            List<double[]> test = new List<double[]>();
            test.Add(new double[] { 3, 5, 2});
            test.Add(new double[] { -1, -4, 0.124 });
            test.Add(new double[] { -5, 3, 1 });
            test.Add(new double[] { 7, -17, 8 });

            foreach (double[] t in test)
            {
                Vector2D v = new Vector2D(t[0], t[1]);
                Assert.AreEqual((double)t[2], (double)v.Truncate(t[2]).Length(), 0.00000001);
            }
        }

    }
}
