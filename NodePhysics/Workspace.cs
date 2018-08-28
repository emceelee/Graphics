using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NodePhysics.Forces;
using NodePhysics.Logging;
using Logging;

namespace NodePhysics
{
    public class Workspace
    {
        private ILogger _logger;
        private IWorkspaceLogger _wsLogger;
        private int _iteration = 0;

        public int Iteration
        {
            get { return _iteration; }
        }
        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<Relationship> Relationships { get; set; } = new List<Relationship>();
        public List<Force> Forces { get; set; } = new List<Force>();
        
        public Workspace(ILogger logger = null, IWorkspaceLogger wsLogger = null)
        {
            _logger = logger;
            _wsLogger = wsLogger;
        }

        public bool AddNode(Node node)
        {
            if(!Nodes.Contains(node))
            {
                Nodes.Add(node);
                return true;
            }

            return false;
        }

        public bool AddRelationship(Relationship relationship)
        {
            if(!Relationships.Contains(relationship))
            {
                if(Nodes.Contains(relationship.From) && Nodes.Contains(relationship.To))
                {
                    Relationships.Add(relationship);
                }
            }

            return false;
        }

        private void DetermineForces()
        {
            DetermineResistanceForces();
            DetermineRepellingForces();
            DetermineAttractingForces();
            DetermineSortingForces();
        }

        public void Iterate(int count, int sleep = 0)
        {
            DetermineForces();

            Log(_iteration);
            for(int i=0; i < count; ++i)
            {
                EnactForces();
                UpdateNodes();

                Log(++_iteration);

                System.Threading.Thread.Sleep(sleep);
            }

            Forces.Clear();
        }

        public void Log(int iteration)
        {
            StringBuilder sb = new StringBuilder($"Iteration {iteration}");

            foreach (Node node in Nodes)
            {
                sb.Append($",{node.Position}");
            }

            _logger?.Log(sb.ToString());
            _wsLogger?.Log(this);

        }

        private void EnactForces()
        {
            foreach (Force force in Forces)
            {
                force.Act();
            }
        }

        private void UpdateNodes()
        {
            foreach (Node node in Nodes)
            {
                node.Move();
            }
        }

        private void DetermineResistanceForces()
        {
            _logger?.Log("DetermineResistanceForces");

            //Repelling Forces
            foreach (Node node in Nodes)
            {
                Forces.Add(new ResistanceForce(node));
            }
        }

        private void DetermineRepellingForces()
        {
            _logger?.Log("DetermineRepellingForces");

            //Repelling Forces
            foreach (Node node1 in Nodes)
            {
                foreach (Node node2 in Nodes)
                {
                    if (node1 != node2)
                    {
                        Forces.Add(new RepellingForce(node1, node2));
                    }
                }
            }
        }

        private void DetermineAttractingForces()
        {
            _logger?.Log("DetermineAttractingForces");

            //Attracting Forces
            foreach (Relationship relationship in Relationships)
            {
                Forces.Add(new AttractingForce(relationship.From, relationship.To));
                Forces.Add(new AttractingForce(relationship.To, relationship.From));
            }
        }

        private void DetermineSortingForces()
        {
            _logger?.Log("DetermineSortingForces");
            //Sorting Forces
            foreach (Relationship relationship in Relationships)
            {
                Forces.Add(new SortingForce(relationship.From, relationship.To, relationship));
                Forces.Add(new SortingForce(relationship.To, relationship.From, relationship));
            }
        }
    }
}
