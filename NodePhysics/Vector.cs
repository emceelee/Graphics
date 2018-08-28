using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodePhysics
{
    public struct Vector
    {
        #region Class Variables

        private double _x;
        private double _y;

        #endregion

        #region Constructors
        public Vector(double x = 0, double y = 0)
        {
            _x = x;
            _y = y;
        }

        #endregion

        #region Public Properties

        public double X { get { return _x; } }
        public double Y { get { return _y; } }

        public double Magnitude
        {
            get { return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)); }
        }

        public double Radians
        {
            get
            {
                double input = 0;

                if (X == 0)
                {
                    if (Y > 0)
                    {
                        input = Double.PositiveInfinity;
                    }
                    else if (Y < 0)
                    {
                        input = Double.NegativeInfinity;
                    }
                }
                else
                {
                    input = Y / X;
                }

                double radians = Math.Atan(input);

                if (IsQuadrant2Or3(X))
                {
                    radians += Math.PI;
                }
                if (radians < 0)
                {
                    radians += 2 * Math.PI;
                }

                return radians;
            }
        }

        #endregion

        #region Private Methods

        private bool IsQuadrant2Or3(double X)
        {
            return X < 0;
        }

        #endregion

        #region Overridden

        public override string ToString()
        {
            //return $"({Math.Round(X, 4)},{Math.Round(Y, 4)})";
            return $"{Math.Round(X, 4)},{Math.Round(Y, 4)}";
        }

        #endregion

        #region Public Methods
        public Vector Scale(double magnitude)
        {
            double radians = Radians;
            return new Vector(magnitude * Math.Cos(radians), magnitude * Math.Sin(radians));
        }

        public Vector Add(Vector vector)
        {
            double x = X + vector.X;
            double y = Y + vector.Y;

            return new Vector(x, y);
        }

        public Vector Subtract(Vector vector)
        {
            double x = X - vector.X;
            double y = Y - vector.Y;

            return new Vector(x, y);
        }

        #endregion
    }
}
