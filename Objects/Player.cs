using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5.Objects
{
    class Player : BaseObject
    {
        public Action<Marker> OnMarKerOverlap;
        public Action<Point> OnPointKerOverlap;

        public float vX, vY;

        public Player(float x, float y, float angle) : base(x, y, angle)
        {

        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(
                new SolidBrush(Color.DeepSkyBlue),
                -15, -15,
                30, 30
                ); //рисуем кружок с синим фоном 

            g.DrawEllipse(
                new Pen(Color.Black, 2),
                -15, -15,
                30, 30
                ); //очерчиваем кружочку рамку

            g.DrawLine(new Pen(Color.Black, 2), 0, 0, 25, 0); //рисуем паолучку указывающую направление игрока
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }

        public override void Overlap(BaseObject obj)
        {

            base.Overlap(obj);

            if (obj is Marker)
            {
                OnMarKerOverlap(obj as Marker);
            }
            if (obj is Point)
            {
                OnPointKerOverlap(obj as Point);
            }
        }
    }
}
