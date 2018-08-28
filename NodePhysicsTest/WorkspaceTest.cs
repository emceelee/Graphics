using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Logging;
using NodePhysics;

namespace NodePhysicsTest
{
    [TestClass]
    public class WorkspaceTest
    {
        [TestMethod]
        public void WorkspaceTest_1()
        {
            ILogger logger = new ConsoleLogger();
            Workspace ws = new Workspace(logger);

            ws.AddNode(new Node(0, 90, 1));
            ws.AddNode(new Node(10, 20, 2));
            ws.AddNode(new Node(20, 40, 1));
            ws.AddNode(new Node(30, 60, 4));
            ws.AddNode(new Node(40, 100, 1));
            ws.AddNode(new Node(50, 80, 5));
            ws.AddNode(new Node(60, 0, 1));
            ws.AddNode(new Node(70, 50, 5));
            ws.AddNode(new Node(80, 10, 1));
            ws.AddNode(new Node(90, 30, 100));
            ws.AddNode(new Node(100, 70, 3));
            
            ws.AddRelationship(new Relationship(ws.Nodes[0], ws.Nodes[1]));
            ws.AddRelationship(new Relationship(ws.Nodes[0], ws.Nodes[2]));
            ws.AddRelationship(new Relationship(ws.Nodes[0], ws.Nodes[3]));
            ws.AddRelationship(new Relationship(ws.Nodes[0], ws.Nodes[7]));
            ws.AddRelationship(new Relationship(ws.Nodes[1], ws.Nodes[2]));
            ws.AddRelationship(new Relationship(ws.Nodes[1], ws.Nodes[5]));
            ws.AddRelationship(new Relationship(ws.Nodes[1], ws.Nodes[7]));
            ws.AddRelationship(new Relationship(ws.Nodes[2], ws.Nodes[3]));
            ws.AddRelationship(new Relationship(ws.Nodes[2], ws.Nodes[9]));
            ws.AddRelationship(new Relationship(ws.Nodes[2], ws.Nodes[10]));
            ws.AddRelationship(new Relationship(ws.Nodes[3], ws.Nodes[5]));
            ws.AddRelationship(new Relationship(ws.Nodes[3], ws.Nodes[6]));
            ws.AddRelationship(new Relationship(ws.Nodes[4], ws.Nodes[8]));
            ws.AddRelationship(new Relationship(ws.Nodes[4], ws.Nodes[9]));
            ws.AddRelationship(new Relationship(ws.Nodes[5], ws.Nodes[6]));
            ws.AddRelationship(new Relationship(ws.Nodes[5], ws.Nodes[10]));
            ws.AddRelationship(new Relationship(ws.Nodes[6], ws.Nodes[10]));
            ws.AddRelationship(new Relationship(ws.Nodes[7], ws.Nodes[8]));
            ws.AddRelationship(new Relationship(ws.Nodes[9], ws.Nodes[10]));

            ws.Iterate(500);
        }
    }
}
