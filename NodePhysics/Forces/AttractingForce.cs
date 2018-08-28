using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodePhysics.Forces
{
    //Attract nodes with a defined relationship
    public class AttractingForce : InteractionForce
    {
        private double _weight = 0.0001;
        private double _radius = 100;

        public AttractingForce(Node target, Node source) : base(target, source) { }

        public override InteractionForce Reverse()
        {
            return new AttractingForce(Source, Target);
        }

        public override void Calculate()
        {
            base.Calculate();

            double delta = _displacement.Magnitude - _radius;
            double magnitude = 0;

            if(delta > 0)
            {
                magnitude = _weight * Math.Pow(delta, 2);
            }

            _value = _displacement.Scale(-magnitude);
        }
    }
}
