using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodePhysics.Forces
{
    //Force source nodes upward.
    public class SortingForce : InteractionForce
    {
        private double _weight = 1;
        private Relationship _relationship;

        public SortingForce(Node target, Node source, Relationship relationship) : base(target, source)
        {
            _relationship = relationship;

            var nodes = relationship.Nodes;

            if (!nodes.Contains(target))
            {
                throw new ArgumentException("Target node is not part of relationship");
            }
            if (!nodes.Contains(source))
            {
                throw new ArgumentException("Source node is not part of relationship");
            }
        }

        public override InteractionForce Reverse()
        {
            return new SortingForce(Source, Target, _relationship);
        }

        public override void Calculate()
        {
            base.Calculate();

            double radians = _displacement.Radians;
            //Target -> force down (positive sign)
            //Source -> force up (negative sign)
            //Node pair rotates clockwise when combined with AttractingForce

            int sign = 1;
            if(Target == _relationship.From)
            {
                sign = -1;
            }

            double magnitude = sign * _weight * Math.Abs(Math.Cos(radians));

            _value = new Vector(0, magnitude);
        }
    }
}
