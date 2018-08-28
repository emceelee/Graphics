using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodePhysics.Forces
{
    public class ResistanceForce : Force
    {
        private double factor = 0.02;

        public ResistanceForce(Node target) : base(target) { }

        public override void Calculate()
        {
            base.Calculate();

            //decelerate by X% of Node speed
            double acceleration = factor * Target.Speed.Magnitude;
            double magnitude = Target.Mass * acceleration;

            //decelerate in opposite direction of current speed
            _value = new Vector(-magnitude * Math.Cos(Target.Speed.Radians), -magnitude * Math.Sin(Target.Speed.Radians));
        }

    }
}
