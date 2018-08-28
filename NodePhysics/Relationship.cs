using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodePhysics
{
    public class Relationship
    {
        private Node _fromNode;
        private Node _toNode;
        private List<Node> _nodes = new List<Node>();

        public Relationship(Node fromNode, Node toNode)
        {
            _fromNode = fromNode;
            _toNode = toNode;

            _nodes.Add(fromNode);
            _nodes.Add(toNode);
        }

        public Node From { get { return _fromNode; } }
        public Node To { get { return _toNode; } }

        public Relationship Reverse()
        {
            return new Relationship(To, From);
        }

        public List<Node> Nodes { get { return _nodes; } }

        public override bool Equals(object obj)
        {
            Relationship r = obj as Relationship;

            if(r is null)
            {
                return false;
            }

            return (r.From == this.From && r.To == this.To);
        }
    }
}
