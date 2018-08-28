using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

using NodePhysics.Logging;
using NodePhysics;

namespace FormApp
{
    public class GraphicsLogger : IWorkspaceLogger
    {
        Graphics _graphics;
        BufferedGraphics _bufferedGraphics;
        BufferedGraphicsContext _context;
        Form _form;

        private List<Color> _colors = new List<Color>();
        private List<Brush> _brushes = new List<Brush>();
        private Color _relationshipColor  = Color.White;

        private int _width;
        private int _height;

        public Graphics Graphics { get { return _bufferedGraphics.Graphics; } }

        public GraphicsLogger(Graphics graphics, Form form, int width, int height)
        {
            _graphics = graphics;
            _context = BufferedGraphicsManager.Current;
            _form = form;
            _width = width;
            _height = height;

            _context.MaximumBuffer = new Size(width + 1, height + 1);
            _bufferedGraphics = _context.Allocate(graphics, new Rectangle(0, 0, _width, height));
        }

        public void Log(Workspace ws)
        {
            Graphics.FillRectangle(Brushes.Black, 0, 0, _width, _height);
            Graphics.DrawString("Working...", new Font("Arial", 8), Brushes.White, 10, 10);

            //DrawEllipses();
            DrawWorkspace(ws);

            _bufferedGraphics.Render(Graphics.FromHwnd(_form.Handle));
        }

        public void Resize(int width, int height)
        {

            _width = width;
            _height = height;

            _context.MaximumBuffer = new Size(_width + 1, _height + 1);
            if (_bufferedGraphics != null)
            {
                _bufferedGraphics.Dispose();
                _bufferedGraphics = null;
            }

            _bufferedGraphics = _context.Allocate(_graphics,
                new Rectangle(0, 0, _width, _height));

            _bufferedGraphics.Graphics.FillRectangle(Brushes.Black, 0, 0, _width, _height);
            _bufferedGraphics.Render(Graphics.FromHwnd(_form.Handle));
        }

        private void DrawEllipses()
        {
            // Draw randomly positioned and colored ellipses.
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                int px = rnd.Next(20, _width - 40);
                int py = rnd.Next(20, _height - 40);
                _bufferedGraphics.Graphics.DrawEllipse(new Pen(Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255)), 1),
                    px, py, px + rnd.Next(0, _width - 20), py + rnd.Next(0, _height - py - 20));
            }
        }

        private void DrawWorkspace(Workspace ws)
        {
            DrawNodes(ws);
            DrawRelationships(ws);
        }

        private void DrawNodes(Workspace ws)
        {
            var nodes = ws.Nodes;
            Random r = new Random();

            for (int i = 0; i < nodes.Count; ++i)
            {
                var node = nodes[i];

                if(i >= _colors.Count)
                {
                    _colors.Add(Color.FromArgb(255, r.Next(256), r.Next(256), r.Next(256)));
                    _brushes.Add(new SolidBrush(_colors[i]));
                }

                this.Graphics.FillEllipse(_brushes[i], (int) node.Position.X - node.Radius, (int) node.Position.Y - node.Radius, 2 * node.Radius, 2 * node.Radius);
            }
        }

        private void DrawRelationships(Workspace ws)
        {
            var relationships = ws.Relationships;

            foreach (Relationship r in relationships)
            {
                double distance = r.From.Position.Subtract(r.To.Position).Magnitude;
                int scale = (int) Math.Ceiling(distance / 100.0);

                Pen pen = new Pen(_relationshipColor, scale);
                this.Graphics.DrawLine(pen, new Point((int) r.From.Position.X, (int)r.From.Position.Y), new Point((int)r.To.Position.X, (int)r.To.Position.Y));
            }
        }
    }
}
