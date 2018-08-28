using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NodePhysics.Forces;

namespace NodePhysics
{
    public class Node
    {
        #region Class Variables
        private const double deceleration = 0.9;

        private int _radius;
        private int _mass;
        private Vector _position;
        private Vector _speed;
        private double _maxSpeed = 15;

        #endregion

        #region Constructors
        
        public Node(double x = 0, double y = 0, int radius = 1)
        {
            if(radius < 1)
            {
                radius = 1;
            }

            _position = new Vector(x, y);
            _radius = radius;
            InitializeDefaults();
        }

        #endregion

        #region Public Properties

        public Vector Position { get { return _position; } }
        public Vector Speed { get { return _speed; } }
        public int Mass { get { return _mass; } }
        public int Radius { get { return _radius; } }

        #endregion

        public void Move()
        {
            LimitSpeed();
            UpdatePosition();
        }

        private Vector CalculateAcceleration(Force force)
        {
            //calculate acceleration due to singular force
            Vector f = force.Value;
            double magnitude = f.Magnitude / _mass;

            return f.Scale(magnitude);
        }

        public void Accelerate(Force force)
        {
            Vector acceleration = CalculateAcceleration(force);
            _speed = _speed.Add(acceleration);
        }

        private void LimitSpeed()
        {
            if(_speed.Magnitude > _maxSpeed)
            {
                _speed = _speed.Scale(_maxSpeed);
            }
        }

        private void UpdatePosition()
        {
            _position = _position.Add(_speed);
        }

        public void Decelerate()
        {
            double magnitude = deceleration * _speed.Magnitude;
            _speed = _speed.Scale(magnitude);
        }

        private void InitializeDefaults()
        {
            _mass = _radius * _radius;
            _speed = new Vector();
        }
    }
}
