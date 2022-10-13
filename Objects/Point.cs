using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace Lab_5.Objects
{
    class Point: BaseObject
    {
        public int timer;
        public int count;
        public Point(float x, float y, float angle) : base(x, y, angle)
        {
            timer = 400;
            count = 35;
        }
        public Action<Point> wanteremove;
        public override void Render(Graphics g)
        {

            this.timer--;
            //int count = 35;

            if (this.timer < 0)
            {
                wanteremove?.Invoke(this);
                this.count = 35;
            }
            else
            {
                Random rnd = new Random();
                int value = rnd.Next(20, 50);
                g.FillEllipse(new SolidBrush(Color.Green), -this.timer / 16, -this.timer / 16, this.timer / 8, this.timer / 8);

            }

            g.DrawString(
                $"{timer}",
                new Font("Verdana", 8), // шрифт и размер
                new SolidBrush(Color.Green), // цвет шрифта
                -this.timer / 16 - 10, -this.timer / 16 - 10 // точка в которой нарисовать текст
                );


        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-this.timer / 16, -this.timer / 16, this.timer / 8, this.timer / 8);
            return path;
        }
    }
}
