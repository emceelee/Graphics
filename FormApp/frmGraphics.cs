using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NodePhysics;
using NodePhysics.Logging;

namespace FormApp
{
    public partial class frmGraphics : Form
    {
        Graphics graphics;
        GraphicsLogger logger;
        Workspace workspace;

        public frmGraphics()
        {
            InitializeComponent();
        }

        private void frmGraphics_Load(object sender, EventArgs e)
        {
            graphics = this.CreateGraphics();
            logger = new GraphicsLogger(graphics, this, this.Width, this.Height);

            Initialize();
        }

        private void Initialize()
        {

            //SetupWorkspace();
            //SetupWorkspaceTree();
            //SetupWorkspaceSimple();
            SetupWorkspaceRandom();
        }

        private void SetupWorkspace()
        {
            workspace = new Workspace(null, logger);

            SetupNodesStandard(workspace);

            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[1]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[2]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[3]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[7]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[1], workspace.Nodes[2]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[1], workspace.Nodes[5]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[1], workspace.Nodes[7]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[2], workspace.Nodes[3]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[2], workspace.Nodes[9]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[2], workspace.Nodes[10]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[3], workspace.Nodes[5]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[3], workspace.Nodes[6]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[4], workspace.Nodes[8]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[4], workspace.Nodes[9]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[5], workspace.Nodes[6]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[5], workspace.Nodes[10]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[6], workspace.Nodes[10]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[7], workspace.Nodes[8]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[9], workspace.Nodes[10]));

        }

        private void SetupWorkspaceTree()
        {
            workspace = new Workspace(null, logger);

            SetupNodesStandard(workspace);

            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[1]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[2]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[3]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[4]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[5]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[6]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[7]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[8]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[9]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[10]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[11]));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[12]));

        }

        private void SetupWorkspaceSimple()
        {
            workspace = new Workspace(null, logger);

            workspace.AddNode(new Node(400, 400, 4));
            workspace.AddNode(new Node(500, 400, 4));
            workspace.AddRelationship(new Relationship(workspace.Nodes[0], workspace.Nodes[1]));
        }

        private void SetupWorkspaceRandom()
        {
            workspace = new Workspace(null, logger);

            Random rand = new Random();
            int size = 10;

            for(int i=0; i<size; ++i)
            {
                workspace.AddNode(new Node(rand.Next(Width), rand.Next(Height), 2+rand.Next(19)));
            }

            //double power = 1.1;
            double power = 1+rand.NextDouble();

            int maxRelationships = (int)Math.Pow(size, power);

            for (int i=0; i < maxRelationships; ++i)
            {
                Relationship relationship = new Relationship(workspace.Nodes[rand.Next(size)], workspace.Nodes[rand.Next(size)]);

                if (workspace.Relationships.Contains(relationship) || workspace.Relationships.Contains(relationship.Reverse()))
                {
                    continue;
                }

                workspace.AddRelationship(relationship);
            }
        }

        private void SetupNodesStandard(Workspace workspace)
        {
            workspace.AddNode(new Node(300, 30, 2));
            workspace.AddNode(new Node(400, 120, 4));
            workspace.AddNode(new Node(500, 200, 6));
            workspace.AddNode(new Node(250, 700, 8));
            workspace.AddNode(new Node(500, 600, 10));
            workspace.AddNode(new Node(100, 500, 12));
            workspace.AddNode(new Node(600, 150, 11));
            workspace.AddNode(new Node(700, 250, 9));
            workspace.AddNode(new Node(550, 750, 7));
            workspace.AddNode(new Node(200, 300, 5));
            workspace.AddNode(new Node(300, 250, 3));
            workspace.AddNode(new Node(400, 300, 5));
            workspace.AddNode(new Node(650, 750, 4));
        }

        private void Iterate()
        {
            workspace.Iterate(1000,2);

            graphics.DrawString("Done", new Font("Arial", 8), Brushes.Green, 10, 22);
        }

        private void frmGraphics_Click(object sender, EventArgs e)
        {
            Initialize();
            Iterate();
        }

        private void frmGraphics_Resize(object sender, EventArgs e)
        {
            Initialize();
            logger.Resize(Width, Height);
            Iterate();
        }
    }
}
