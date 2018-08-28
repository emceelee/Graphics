using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;

namespace NodePhysics.Forces
{
    public abstract class InteractionForce : Force
    {
        protected Node _source;
        protected Vector _displacement;

        public InteractionForce(Node target, Node source) : base (target)
        {
            _source = source;
        }

        public abstract InteractionForce Reverse();

        public Node Source { get { return _source; } }

        protected Vector CalculateDisplacement()
        {
            double y = Target.Position.Y - Source.Position.Y;
            double x = Target.Position.X - Source.Position.X;

            var vector = new Vector(x, y);
            
            return vector;
        }

        public override void Calculate()
        {
            base.Calculate();
            _displacement = CalculateDisplacement();
        }
    }
}
