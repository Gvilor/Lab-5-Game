using Lab_5.Objects;
using Point = Lab_5.Objects.Point;

namespace Lab_5
{
    public partial class Form1 : Form
    {
       
        List<BaseObject> objects = new(); // �������� ������ 
        Player player; //������� ���� ��� ������
        Marker marker;
        Point point;
       

        public Form1()
        {
            InitializeComponent();

            int countPoint = 0;
            label2.Text = $"����: {countPoint}";

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0); //������� ��������� ������ ������ � ������ ������ 
                                                                         // �������� ������� �� �����������
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] ����� ��������� � {obj}\n" + txtLog.Text;
            };

            // ������� ������� �� ����������� � ��������
            player.OnMarKerOverlap += (m) =>
            {
                objects.Remove(marker);

                marker = null;
            };
            player.OnMarKerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };
            player.OnPointKerOverlap += (p) =>
            {
                countPoint++;
                label2.Text = $"����: {countPoint}";
               
                p.timer = 400;
                Random rnd = new Random();
                p.X = rnd.Next(30, pbMain.Width - 226);
                p.Y = rnd.Next(30, pbMain.Height - 226);

            };

          
            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            Random rnd = new Random();
            point = new Point(0, 0, 0);
            point.X = rnd.Next(30, pbMain.Width - 226);
            point.Y = rnd.Next(30, pbMain.Height - 226);
            point.wanteremove += p =>
            {
                Random rnd = new Random();
                p.X = rnd.Next(30, pbMain.Width - 226);
                p.Y = rnd.Next(30, pbMain.Height - 226);
                p.timer = 400;
                //point.
            };



            objects.Add(player);
            objects.Add(marker);
            objects.Add(point);
            objects.Add(point);
        }

        private void updatePlayer()
        {
            // ��� ��������� �������� �� marker �� �������
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;

                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.5f;
                player.vY += dy * 0.5f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }
            // ���������� ������
            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // �������� ������� ������ � ������� ������� ��������
            player.X += player.vX;
            player.Y += player.vY;

            
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
          

            // ����������� ���������� pbMain
            // ��� ������� ����� pbMain_Paint �� �����
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            // ��� ������� �������� ������� �� ����� ���� �� ��� �� ������
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // � ������� �� ������ ��������� � objects
            }

            // � ��� ��� � ��������
            marker.X = e.X;
            marker.Y = e.Y;
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            updatePlayer();

            // ������������� �����������
            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                }
            }

            // �������� �������
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
          
        }
    }
}