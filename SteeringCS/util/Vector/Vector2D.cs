using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
   
    public class Vector2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D() : this(0,0)
        {
        }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2D(Vector2D v)
        {
            X = v.X;
            Y = v.Y;
        }

        public double Length()
        {
            return Math.Sqrt(this.LengthSquared());
        }

        public double LengthSquared()
        {
            return (Math.Pow(this.X, 2) + Math.Pow(this.Y, 2));
        }

        public Vector2D Add(Vector2D v)
        {
            this.X += v.X;
            this.Y += v.Y;
            return this;
        }

        public Vector2D Sub(Vector2D v)
        {
            this.X -= v.X;
            this.Y -= v.Y;
            return this;
        }

        public Vector2D Multiply(double value)
        {
            this.X *= value;
            this.Y *= value;
            return this;
        }

        public Vector2D Divide(double value)
        {
            if (value != 0)
            {
                this.X /= value;
                this.Y /= value;
            }
            return this;
        }

        public static Vector2D operator +(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vector2D operator -(Vector2D v1, Vector2D v2)
        {
            return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }
        public static Vector2D operator *(Vector2D v, double multiplier)
        {
            return new Vector2D(v.X * multiplier, v.Y * multiplier);
        }
        public static Vector2D operator /(Vector2D v, double divider)
        {
            return new Vector2D(v.X / divider, v.Y / divider);
        }

        public Vector2D Normalize()
        {
            double length = this.Length();
            if (length > 0)
            {
                this.X = this.X / length;
                this.Y = this.Y / length;
            }
            return this; 
        }

        public Vector2D Truncate(double max)
        {
            if (Length() > max)
            {
                Normalize();
                Multiply(max);
            }
            return this;
        }

        public Vector2D Rotate(float degrees)
        {
            double DegreesToRadians = Math.PI / 180;
            double sin = Math.Sin((double)degrees * DegreesToRadians);
            double cos = Math.Cos((double)degrees * DegreesToRadians);

            double x = this.X;
            double y = this.Y;

            this.X = (x * cos) - (y * sin);
            this.Y = (y * cos) + (x * sin);

            return this;
        }

        public double VectorDistance(Vector2D vector)
        {
            double newX, newY, returnLength;
            newX = vector.X - this.X;
            newY = vector.Y - this.Y;
            returnLength = new Vector2D(newX, newY).Length();
            return returnLength;
        }

        public double VectorDistanceSq(Vector2D vector)
        {
            double newX, newY, returnLength;
            newX = vector.X - this.X;
            newY = vector.Y - this.Y;
            returnLength = new Vector2D(newX, newY).LengthSquared();
            return returnLength;
        }

        public Vector2D Clone()
        {
            return new Vector2D(this.X, this.Y);
        }
        
        public override string ToString()
        {
            return String.Format("({0},{1})", X, Y);
        }
    }


}
