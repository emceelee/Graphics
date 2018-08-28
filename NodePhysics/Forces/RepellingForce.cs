using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodePhysics.Forces
{
    //Repel nodes that are too close
    public class RepellingForce : InteractionForce
    {
        private double _weight = 0.01;
        private double _radius = 100;

        public RepellingForce(Node target, Node source) : base(target, source) { }

        public override InteractionForce Reverse()
        {
            return new RepellingForce(Source, Target);
        }

        public override void Calculate()
        {
            base.Calculate();

            double delta = _radius - _displacement.Magnitude;
            double magnitude = 0;

            if (delta > 0)
            {
                magnitude = _weight * Math.Pow(delta, 2);
            }

            _value = _displacement.Scale(magnitude);
        }
    }
}
