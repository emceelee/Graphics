using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodePhysics.Forces
{
    public class Force
    {
        //force acts on a target
        protected Node _target;

        //force vector - Directional
        protected Vector _value;

        public Force(Node target, int X = 0, int Y = 0)
        {
            _target = target;

            _value = new Vector(X, Y);
        }

        public Node Target { get { return _target; } }

        public Vector Value { get { return _value; } }

        //Calculate the force vector
        public virtual void Calculate() { }

        //Act on target
        public void Act()
        {
            Calculate();
            Target.Accelerate(this);
        }
        
    }
}
