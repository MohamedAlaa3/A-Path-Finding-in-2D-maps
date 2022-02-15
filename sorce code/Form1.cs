using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APathFindingin2DmapsProject
{
    public partial class Form1 : Form
    {
        public Random rnd = new Random();
        Bitmap off;
        public Graphics gf;
        Timer tt = new Timer();
        List<state> all = new List<state>();
        public string s2;
        bool static0 = true;
        int ct = 0, ct2 = 0, ct3 = -1;
        char k = 'a';
        int pathcost = 0;

        public int q = 0;
        state pnn2 = null;
        state item2 = null;
        state item3 = null;


        state pnn;

        class state
        {
            public int X, Y, isdraw, h, Tcost;
            public char id;
            public List<state> children = new List<state>();
            public Dictionary<state, int> cost = new Dictionary<state, int>();
            public List<state> path = new List<state>();
            public void draw(state s, Graphics gf, Pen p, state parent)
            {

                if (s.isdraw == 0)
                {
                    Pen p2 = new Pen(Color.Blue, 3);
                    gf.DrawEllipse(p2, s.X, s.Y, 30, 30);
                    s.isdraw = 1;
                    Font drawFont = new Font("Arial", 16);
                    string s2 = s.id.ToString();
                    gf.DrawString(s2, drawFon
                        t, Brushes.Red, s.X + 5, s.Y + 5);
                    //gf.FillEllipse(Brushes.Red, this.X, this.Y, 30, 30);
                    for (int i = 0; i < s.children.Count; i++)
                    {
                        //gf.DrawEllipse(p2, s.children[i].X, s.children[i].Y, 30, 30);
                        //gf.FillEllipse(Brushes.Red, s.children[i].X, s.children[i].Y, 30, 30);
                        //s2 = s.children[i].id.ToString();
                        //drawFont = new Font("Arial", 16);
                        //gf.DrawString(s2, drawFont, Brushes.Red, s.X + 5, s.Y + 5);


                        if (parent != s.children[i])
                        {
                            if (s.children[i].path.Contains(s))
                            {
                                Pen p3 = new Pen(Color.Green, 3);
                                s.path.Add(s.children[i]);
                                gf.DrawLine(p3, s.X + 10, s.Y + 3, s.children[i].X + 10, s.children[i].Y + 3);

                            }
                            else
                            {

                                gf.DrawLine(p, s.X + 10, s.Y + 3, s.children[i].X + 10, s.children[i].Y + 3);
                            }

                            this.draw(s.children[i], gf, p, s);
                        }

                    }
                }
                return;

            }
        }
        class car
        {
            public float X, Y;
            public bool run = true;
            public float c;
            public void cY()
            {

                c = -(500 - 0) / (500 - 0) * X + Y;
                Y = (500 - Y) / (500 - X) * X + c;

                if (Y >= float.MaxValue && Y <= float.MinValue)
                {
                    MessageBox.Show("fffffffff");
                }
                else
                {
                    // Y = 501;
                }
                if (Y < 0)
                {
                    Y /= -1;
                }
            }
        }
        car h = new car();
        state first;

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.KeyDown += Form1_KeyDown;

            tt.Tick += Tt_Tick;
            tt.Start();

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                pathcost = 0;
                foreach (var item in all)
                {
                    item.path.Clear();
                }
                s2 = "Depth First Search Technique (DFS)" + "\n";
                DFS();
                if (pathcost != 0)
                {
                    s2 += "Path Cost: ";
                    s2 += pathcost.ToString();
                }

            }
            if (e.KeyCode == Keys.B)
            {
                pathcost = 0;
                foreach (var item in all)
                {
                    item.path.Clear();
                }
                s2 = "Breadth First Search Technique (BFS)" + "\n";

                BFS();
                if (pathcost != 0)
                {
                    s2 += "Path Cost: ";
                    s2 += pathcost.ToString();
                }
            }
            if (e.KeyCode == Keys.U)
            {
                pathcost = 0;
                foreach (var item in all)
                {
                    item.path.Clear();
                }
                s2 = "Uniform Cost Search Technique (UC)" + "\n";

                UC();
                if (pathcost != 0)
                {
                    s2 += "Path Cost: ";
                    s2 += pathcost.ToString();
                }
            }
            if (e.KeyCode == Keys.A)
            {
                pathcost = 0;
                foreach (var item in all)
                {
                    item.path.Clear();
                }
                s2 = "A* Search Technique" + "\n";
                Astar();
                if (pathcost != 0)
                {
                    s2 += "Path Cost: ";
                    s2 += pathcost.ToString();
                }
            }
            if (e.KeyCode == Keys.G)
            {
                pathcost = 0;
                foreach (var item in all)
                {
                    item.path.Clear();
                }
                s2 = "Greedy Search Technique " + "\n";

                greedy();
                if (pathcost != 0)
                {
                    s2 += "Path Cost: ";
                    s2 += pathcost.ToString();
                }
            }
            if (e.KeyCode == Keys.S)
            {
                ct = 0;
                all.Clear();
                first = null;
                static0 = true;
                CreateActors();
            }
            if (e.KeyCode == Keys.R)
            {
                ct = 0;
                all.Clear();
                first = null;
                static0 = false;
                CreateActors();
            }
            if (e.KeyCode == Keys.K)
            {
                if (ct == 0)
                {
                    ct = 3;
                    while (true)
                    {
                        int i = rnd.Next(0, all.Count - 1);
                        int i2 = rnd.Next(0, all.Count - 1);
                        if (i != i2)
                        {
                            first = all[i];
                            all[i].id = 's';
                            all[i2].id = 'g';
                            break;
                        }
                    }
                }

            }
            if (e.KeyCode == Keys.Q)
            {
                k = 'a';
                all.Clear();
                first = null;
                q = 1;
            }
            if (e.KeyCode == Keys.Z)
            {
                all.Clear();
                first = null;
                q = 0;
            }
            if (e.KeyCode == Keys.Y)
            {
                ct = 0;
                q = 2;
            }
            if (e.KeyCode == Keys.M)
            {

            }

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(ClientSize.Width + "w  h" + ClientSize.Height);
            if (q == 1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (ct2 == 0)
                    {
                        all.Clear();
                        first = new state();
                        first.X = e.X;
                        first.Y = e.Y;
                        first.id = 'z';
                        all.Add(first);

                    }
                    else
                    {
                        pnn = new state();
                        pnn.X = e.X;
                        pnn.Y = e.Y;
                        if (k != 's' && k != 'm' && k != 'g' && k != 'z')
                        {
                            pnn.id = k;
                        }
                        else
                        {
                            pnn.id = ++k;
                        }
                        k++;
                        all.Add(pnn);
                    }
                    ct2++;
                }
                else
                {
                    foreach (var item in all)
                    {
                        if (e.X >= item.X &&
                           e.X <= item.X + 30 &&
                           e.Y >= item.Y &&
                           e.Y <= item.Y + 30)
                        {
                            //MessageBox.Show(" ");
                            item2 = item;
                            ct3++;
                        }
                    }
                    if (ct3 % 2 == 0)
                    {
                        item3 = item2;
                    }
                    else
                    {
                        if (item2 != null && item3 != null && item3 != item2)
                        {
                            if (!item2.children.Contains(item3))
                            {
                                item2.children.Add(item3);
                                item3.children.Add(item2);
                                item2.h = rnd.Next(2, 20);
                                item3.h = rnd.Next(2, 20);
                                item2.cost[item3] = rnd.Next(2, 20);
                                item3.cost[item2] = rnd.Next(2, 20);
                            }

                            item3 = null;
                            item2 = null;
                        }
                    }

                }
            }

            if (q == 0 || q == 2)
            {
                foreach (var item in all)
                {
                    if (e.X >= item.X &&
                       e.X <= item.X + 30 &&
                       e.Y >= item.Y &&
                       e.Y <= item.Y + 30)
                    {
                        if (e.Button == MouseButtons.Left)
                        {


                            if (ct == 0)
                            {
                                item.id = 's';
                                first = item;
                            }
                            if (ct == 1)
                            {
                                item.id = 'g';
                            }
                            if (ct == 2)
                            {
                                item.id = 'm';
                            }
                            ct++;
                        }
                        else
                        {
                            string a = "  The Cost";
                            foreach (var child in item.children)
                            {
                                a += " to   " + child.id.ToString() + "  is  " + item.cost[child].ToString() + "         ";
                            }
                            MessageBox.Show("H = " + item.h + " " + a);
                        }
                        //MessageBox.Show(item.id+"");
                    }
                }
            }

        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            h.cY();
            h.X++;

            DrawDubb(CreateGraphics());

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            //CreateActors();
            DrawScene(CreateGraphics());
        }

        void CreateActors()
        {
            s2 = "Select the Search technique";
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            if (static0 && q == 0)
            {
                ct = 3;
                first = new state();
                first.X = 500;
                first.Y = 75;
                first.id = 's';
                first.h = 0;
                all.Add(first);

                state pnn = new state();
                pnn.X = 20;
                pnn.Y = 145;
                pnn.id = 'a';
                pnn.h = 7;
                first.children.Add(pnn);
                pnn.children.Add(first);
                pnn.cost[first] = 1;
                first.cost[pnn] = 1;


                state pnn2 = new state();
                pnn2.X = 600;
                pnn2.Y = 120 + 25;
                pnn2.id = 'b';
                pnn2.h = 10;
                first.children.Add(pnn2);
                pnn2.children.Add(first);
                pnn2.cost[first] = 8;
                first.cost[pnn2] = 8;

                state pnn3 = new state();
                pnn3.X = 1100;
                pnn3.Y = 120 + 25;
                pnn3.id = 'c';
                pnn3.h = 5;
                first.children.Add(pnn3);
                pnn3.children.Add(first);
                pnn3.cost[first] = 4;
                first.cost[pnn3] = 4;

                state pnn4 = new state();
                pnn4.X = 100;
                pnn4.Y = 670 + 25;
                pnn4.id = 'd';
                pnn4.h = 4;
                pnn4.children.Add(pnn);
                pnn.children.Add(pnn4);
                pnn.cost[pnn4] = 5;
                pnn4.cost[pnn] = 5;

                state pnn5 = new state();
                pnn5.X = 450;
                pnn5.Y = 500 + 25;
                pnn5.id = 'h';
                pnn5.h = 6;
                pnn5.children.Add(pnn);
                pnn.children.Add(pnn5);
                pnn5.cost[pnn] = 12;
                pnn.cost[pnn5] = 12;

                pnn4.children.Add(pnn5);
                pnn5.children.Add(pnn4);
                pnn5.cost[pnn4] = 2;
                pnn4.cost[pnn5] = 2;

                pnn5.children.Add(pnn2);
                pnn2.children.Add(pnn5);
                pnn5.cost[pnn2] = 1;
                pnn2.cost[pnn5] = 1;

                state pnn6 = new state();
                pnn6.X = 590;
                pnn6.Y = 400 + 25;
                pnn6.id = 'm';
                pnn6.h = 0;
                pnn6.children.Add(pnn2);
                pnn2.children.Add(pnn6);
                pnn2.cost[pnn6] = 4;
                pnn6.cost[pnn2] = 8;

                state pnn7 = new state();
                pnn7.X = 700;
                pnn7.Y = 600 + 25;
                pnn7.id = 'j';
                pnn7.h = 8;
                pnn7.children.Add(pnn5);
                pnn5.children.Add(pnn7);
                pnn5.cost[pnn7] = 4;
                pnn7.cost[pnn5] = 4;

                pnn7.children.Add(pnn2);
                pnn2.children.Add(pnn7);
                pnn2.cost[pnn7] = 4;
                pnn7.cost[pnn2] = 4;

                state pnn8 = new state();
                pnn8.X = 1250;
                pnn8.Y = 650 + 25;
                pnn8.id = 'g';
                pnn8.h = 0;
                pnn8.children.Add(pnn7);
                pnn7.children.Add(pnn8);
                pnn7.cost[pnn8] = 2;
                pnn8.cost[pnn7] = 2;

                state pnn9 = new state();
                pnn9.X = 1280;
                pnn9.Y = 300 + 25;
                pnn9.id = 'e';
                pnn9.h = 7;
                pnn9.children.Add(pnn3);
                pnn3.children.Add(pnn9);
                pnn3.cost[pnn9] = 2;
                pnn9.cost[pnn3] = 2;

                state pnn10 = new state();
                pnn10.X = 1100;
                pnn10.Y = 300 + 25;
                pnn10.id = 'f';
                pnn10.h = 9;
                pnn10.children.Add(pnn3);
                pnn3.children.Add(pnn10);
                pnn10.cost[pnn3] = 6;
                pnn3.cost[pnn10] = 6;

                pnn10.children.Add(pnn9);
                pnn9.children.Add(pnn10);
                pnn9.cost[pnn10] = 10;
                pnn10.cost[pnn9] = 10;
                pnn10.children.Add(pnn8);
                pnn8.children.Add(pnn10);
                pnn10.cost[pnn8] = 2;
                pnn8.cost[pnn10] = 2;

                all.Add(pnn);
                all.Add(pnn2);
                all.Add(pnn3);
                all.Add(pnn4);
                all.Add(pnn5);
                all.Add(pnn6);
                all.Add(pnn7);
                all.Add(pnn8);
                all.Add(pnn9);
                all.Add(pnn10);



            }
            if (!static0 && q == 0)
            {
                first = new state();
                first.X = rnd.Next(0, ClientSize.Width / 100) * 100;
                first.Y = rnd.Next(2, ClientSize.Height / 100) * 100;
                first.id = 'z';
                first.h = 0;
                all.Add(first);
                char id = 'a';
                state pnn;
                int Nstate = rnd.Next(8, 12), Nchildren;
                for (int i = 0; i < Nstate; i++)
                {
                    pnn = new state();
                    pnn.X = rnd.Next(0, ClientSize.Width / 100) * 100;
                    pnn.Y = rnd.Next(2, ClientSize.Height / 100) * 100;
                    pnn.h = rnd.Next(1, 25);
                    if (id != 'g' && id != 'm')
                    {
                        pnn.id = id;
                    }
                    else
                    {
                        pnn.id = ++id;
                    }
                    id++;
                    if (i == 0)
                    {
                        first.children.Add(pnn);
                        pnn.children.Add(first);
                        int cost = rnd.Next(1, 30);
                        pnn.cost[first] = cost;
                        first.cost[pnn] = cost;

                    }
                    Nchildren = rnd.Next(2, 4);
                    if (all.Count < Nchildren)
                    {
                        Nchildren = all.Count();
                    }
                    for (; Nchildren != 1;)
                    {
                        int l = rnd.Next(0, all.Count() - 1);
                        if (!pnn.children.Contains(all[l]))
                        {
                            int cost = rnd.Next(1, 30);
                            pnn.children.Add(all[l]);
                            all[l].children.Add(pnn);
                            pnn.cost[all[l]] = cost;
                            all[l].cost[pnn] = cost;
                            Nchildren--;
                        }

                    }
                    all.Add(pnn);


                }


            }

            /*off = new Bitmap(ClientSize.Width, ClientSize.Height);
            Bitmap img = new Bitmap("1.bmp");
            img.MakeTransparent(img.GetPixel(0, 0));
            m.imgLst.Add(img);*/
        }

        void DrawScene(Graphics gf)
        {
            gf.Clear(Color.Black);
            Font drawFont = new Font("Arial", 30);
            gf.DrawString(s2, drawFont, Brushes.WhiteSmoke, ClientSize.Width / 2 - 200, 10);
            Pen p = new Pen(Color.Yellow, 2);

            for (int i = 0; i < all.Count; i++)
            {
                all[i].isdraw = 0;

            }
            if (first != null)
                first.draw(first, gf, p, null);
            foreach (var item in all)
            {
                if (item.isdraw == 0)
                {
                    item.draw(item, gf, p, null);

                }
            }
            for (int i = 0; i < all.Count; i++)
            {
                all[i].isdraw = 0;
            }
            Pen p2 = new Pen(Color.Blue, 3);
            // h.X = 0;
            //h.Y = 0;
            // gf.DrawLine(p2, 0, 0, 500, 500);
            try
            {
                //gf.DrawEllipse(p2, h.X, h.Y, 30, 30);

            }
            catch (Exception)
            {
                MessageBox.Show(h.X + "x    y" + h.Y + "c" + h.c);
                throw;

            }
            //Pen p2= new Pen(Color.Blue, 3);
            //gf.DrawEllipse(p, 65, 65, 65, 65);
            //gf.FillEllipse(Brushes.Red, 65, 65, 65, 65);



            /*
            gf.FillRectangle(Brushes.Red, 0, this.Height / 2 - 20, this.Width / 2 - 30, 15);//x,y,width,hight
            Pen p = new Pen(Color.Red, 3);// ,width
            gf.DrawLine(p, 60, 60, 60, 0);//x1,y1,x2,y2*/

        }
        void DFS()
        {
            int f = 0;
            foreach (var item in all)
            {
                if (item.id == 's')
                {
                    f = 1;
                }
            }
            if (f == 0)
            {
                MessageBox.Show("Please Select the pickup location");
            }
            int f2 = 0;
            foreach (var item in all)
            {
                if (item.id == 'g')
                {
                    f2 = 1;
                }
            }
            if (f2 == 0)
            {
                MessageBox.Show("Please Select the destination location");
            }
            if (first != null && f == 1 && f2 == 1)
            {
                Dictionary<state, state> maps = new Dictionary<state, state>();
                state a;
                List<state> open = new List<state>();
                List<state> closed = new List<state>();
                state curr = null;
                open.Add(first);
                for (int m = 1; true; m++)
                {
                    if (open.Count == 0 || open[0].id == 'g' || open[0].id == 'm')
                    {
                        if (open.Count != 0)
                        {
                            curr = open[0];
                        }
                        //MessageBox.Show("beak");
                        break;
                    }
                    a = open[0];
                    open.RemoveAt(0);

                    List<state> sorted = a.children.OrderBy(x => x.id).ToList();
                    sorted.Reverse();
                    for (int i = 0; i < sorted.Count; i++)
                    {
                        int f1 = 0;

                        foreach (state item in open)
                        {
                            if (sorted[i] == item)
                            {
                                f1++;
                            }
                        }
                        foreach (state item in closed)
                        {
                            if (sorted[i] == item)
                            {
                                f1++;
                            }
                        }
                        if (f1 == 0)
                        {
                            maps[sorted[i]] = a;
                            open.Insert(0, sorted[i]);


                        }
                    }
                    closed.Add(a);
                    //foreach (var item in open)
                    //{
                    //    //MessageBox.Show(item.id + "");
                    //}
                }


                //foreach (var item in open)
                //{
                //    MessageBox.Show(item.id + "");

                //}
                while (true)
                {

                    //MessageBox.Show(curr.id + "");
                    if (curr == null)
                        break;
                    if (curr == first)
                    {
                        break;

                    }
                    state state = maps[curr];
                    pathcost += curr.cost[maps[curr]];
                    curr.path.Add(maps[curr]);
                    state.path.Add(curr);
                    curr = maps[curr];

                }
            }
        }
        void BFS()
        {
            int f = 0;
            foreach (var item in all)
            {
                if (item.id == 's')
                {
                    f = 1;
                }
            }
            if (f == 0)
            {
                MessageBox.Show("Please Select the pickup location");
            }
            int f2 = 0;
            foreach (var item in all)
            {
                if (item.id == 'g')
                {
                    f2 = 1;
                }
            }
            if (f2 == 0)
            {
                MessageBox.Show("Please Select the destination location");
            }
            if (first != null && f == 1 && f2 == 1)
            {
                Dictionary<state, state> maps = new Dictionary<state, state>();
                state a;
                List<state> open = new List<state>();
                List<state> closed = new List<state>();
                state curr = null;
                open.Add(first);
                for (; true;)
                {
                    if (open.Count == 0 || open[0].id == 'g' || open[0].id == 'm')
                    {
                        if (open.Count != 0)
                        {
                            curr = open[0];
                            //MessageBox.Show("break");
                        }

                        //MessageBox.Show("beak");
                        break;
                    }
                    a = open[0];
                    open.RemoveAt(0);

                    List<state> sorted = a.children.OrderBy(x => x.id).ToList();
                    //sorted.Reverse();
                    for (int i = 0; i < sorted.Count; i++)
                    {
                        int f1 = 0;

                        foreach (state item in open)
                        {
                            if (sorted[i] == item)
                            {
                                f1++;
                            }
                        }
                        foreach (state item in closed)
                        {
                            if (sorted[i] == item)
                            {
                                f1++;
                            }
                        }
                        if (f1 == 0)
                        {
                            maps[sorted[i]] = a;
                            open.Add(sorted[i]);


                        }
                    }
                    closed.Add(a);
                    //foreach (var item in open)
                    //{
                    //    //MessageBox.Show(item.id + "");
                    //}
                }


                //foreach (var item in open)
                //{
                //    MessageBox.Show(item.id + "");

                //}
                while (true)
                {
                    //MessageBox.Show(curr.id + "");
                    if (curr == first || curr == null)
                        break;
                    state state = maps[curr];
                    pathcost += curr.cost[maps[curr]];

                    curr.path.Add(maps[curr]);
                    state.path.Add(curr);
                    curr = maps[curr];
                }
            }
        }
        void UC()
        {
            int f = 0;
            foreach (var item in all)
            {
                if (item.id == 's')
                {
                    f = 1;
                }
            }
            if (f == 0)
            {
                MessageBox.Show("Please Select the pickup location");
            }
            int f2 = 0;
            foreach (var item in all)
            {
                if (item.id == 'g')
                {
                    f2 = 1;
                }
            }
            if (f2 == 0)
            {
                MessageBox.Show("Please Select the destination location");
            }
            if (first != null && f == 1 && f2 == 1)
            {
                Dictionary<state, state> maps = new Dictionary<state, state>();
                state a;
                List<state> open = new List<state>();
                Dictionary<state, int> opencost = new Dictionary<state, int>();
                List<state> closed = new List<state>();
                state curr = null;
                open.Add(first);
                for (; true;)
                {
                    if (open[0].id == 'g' || open[0].id == 'm' || open.Count == 0)
                    {
                        if (open.Count != 0)
                        {
                            curr = open[0];
                            //MessageBox.Show("break");
                        }
                        //MessageBox.Show("break");
                        break;
                    }
                    a = open[0];
                    open.RemoveAt(0);
                    if (a.id == 's')
                    {
                        opencost[a] = a.Tcost;//this line make g=18
                    }//List<state> sorted = a.children.OrderBy(x => x.h).ToList();
                     //sorted.Reverse();
                    for (int i = 0; i < a.children.Count; i++)
                    {
                        a.children[i].Tcost = a.cost[a.children[i]] + opencost[a];
                        int f1 = 0;
                        foreach (state item in closed)
                        {
                            if (a.children[i] == item)
                            {
                                f1++;
                            }
                        }
                        int k = 0;
                        foreach (state item in open)
                        {

                            if (a.children[i] == item)
                            {

                                if (a.children[i].Tcost < opencost[item])
                                {
                                    maps[a.children[i]] = a;
                                    opencost[a.children[i]] = a.children[i].Tcost;
                                    open = open.OrderBy(x => opencost[x]).ToList();

                                }
                                f1++;
                            }
                            k++;
                        }

                        if (f1 == 0)
                        {
                            maps[a.children[i]] = a;
                            open.Add(a.children[i]);
                            opencost[a.children[i]] = a.children[i].Tcost;
                            open = open.OrderBy(x => opencost[x]).ToList();

                        }
                    }
                    //foreach (var item in open)
                    //{
                    //    //MessageBox.Show(item.id + "  " + opencost[item] + "");
                    //}
                    //MessageBox.Show("done");





                    closed.Add(a);
                    //foreach (var item in open)
                    //{
                    //    //MessageBox.Show(item.id + "");
                    //}
                }


                //foreach (var item in open)
                //{
                //    MessageBox.Show(item.id + "");

                //}
                while (true)
                {
                    //MessageBox.Show(curr.id + "");
                    if (curr == first || curr == null)
                        break;
                    state state = maps[curr];
                    pathcost += curr.cost[maps[curr]];
                    curr.path.Add(maps[curr]);
                    state.path.Add(curr);
                    curr = maps[curr];
                }
            }
        }
        void Astar()
        {
            int f = 0;
            foreach (var item in all)
            {
                if (item.id == 's')
                {
                    f = 1;
                }
            }
            if (f == 0)
            {
                MessageBox.Show("Please Select the pickup location");
            }
            int f2 = 0;
            foreach (var item in all)
            {
                if (item.id == 'g')
                {
                    f2 = 1;
                }
            }
            if (f2 == 0)
            {
                MessageBox.Show("Please Select the destination location");
            }
            if (first != null && f == 1 && f2 == 1)
            {
                Dictionary<state, state> maps = new Dictionary<state, state>();
                state a;
                List<state> open = new List<state>();
                Dictionary<state, int> opencost = new Dictionary<state, int>();
                List<state> closed = new List<state>();
                int q = 0;
                state curr = null;
                open.Add(first);
                for (; true;)
                {
                    if (open.Count == 0 || open[0].id == 'g' || open[0].id == 'm')
                    {
                        if (open.Count != 0)
                        {
                            curr = open[0];
                            //MessageBox.Show("break");
                        }
                        break;
                    }
                    a = open[0];
                    open.RemoveAt(0);
                    if (a.id == 's')
                    {
                        opencost[a] = a.Tcost;//this line make g=18
                    }//List<state> sorted = a.children.OrderBy(x => x.h).ToList();
                     //sorted.Reverse();
                    for (int i = 0; i < a.children.Count; i++)
                    {
                        a.children[i].Tcost = a.cost[a.children[i]] + opencost[a] + a.children[i].h - a.h;
                        int f1 = 0;
                        foreach (state item in closed)
                        {
                            if (a.children[i] == item)
                            {
                                f1++;
                            }
                        }
                        int k = 0;
                        foreach (state item in open)
                        {

                            if (a.children[i] == item)
                            {

                                if (a.children[i].Tcost < opencost[item])
                                {

                                    opencost[a.children[i]] = a.children[i].Tcost;
                                    open = open.OrderBy(x => opencost[x]).ToList();

                                }
                                f1++;
                            }
                            k++;
                        }

                        if (f1 == 0)
                        {
                            maps[a.children[i]] = a;
                            open.Add(a.children[i]);
                            opencost[a.children[i]] = a.children[i].Tcost;
                            open = open.OrderBy(x => opencost[x]).ToList();

                        }
                    }
                    //foreach (var item in open)
                    //{
                    //    MessageBox.Show(item.id + "  " + opencost[item] + "");
                    //}
                    //MessageBox.Show("done");





                    closed.Add(a);
                    //foreach (var item in open)
                    //{
                    //    //MessageBox.Show(item.id + "");
                    //}
                }


                //foreach (var item in open)
                //{
                //    MessageBox.Show(item.id + "");

                //}
                while (true)
                {
                    //MessageBox.Show(curr.id + "");
                    ;
                    if (curr == first || curr == null)
                        break;
                    state state = maps[curr];
                    pathcost += curr.cost[maps[curr]];
                    curr.path.Add(maps[curr]);
                    state.path.Add(curr);
                    curr = maps[curr];
                }
            }
        }
        void greedy()
        {
            int f = 0;
            foreach (var item in all)
            {
                if (item.id == 's')
                {
                    f = 1;
                }
            }
            if (f == 0)
            {
                MessageBox.Show("Please Select the pickup location");
            }
            int f2 = 0;
            foreach (var item in all)
            {
                if (item.id == 'g')
                {
                    f2 = 1;
                }
            }
            if (f2 == 0)
            {
                MessageBox.Show("Please Select the destination location");
            }
            if (first != null && f == 1 && f2 == 1)
            {
                Dictionary<state, state> maps = new Dictionary<state, state>();
                state a;
                List<state> open = new List<state>();
                Dictionary<state, int> opencost = new Dictionary<state, int>();
                List<state> closed = new List<state>();
                state curr = null;
                open.Add(first);
                for (; true;)
                {
                    if (open[0].id == 'g' || open[0].id == 'm' || open.Count == 0)
                    {
                        if (open.Count != 0)
                        {
                            curr = open[0];
                            //MessageBox.Show("break");
                        }                        //MessageBox.Show("break");
                        break;
                    }
                    a = open[0];
                    open.RemoveAt(0);
                    if (a.id == 's')
                    {
                        opencost[a] = a.Tcost;//this line make g=18
                    }//List<state> sorted = a.children.OrderBy(x => x.h).ToList();
                     //sorted.Reverse();
                    for (int i = 0; i < a.children.Count; i++)
                    {
                        a.children[i].Tcost = a.children[i].h;
                        int f1 = 0;
                        foreach (state item in closed)
                        {
                            if (a.children[i] == item)
                            {
                                f1++;
                            }
                        }
                        int k = 0;
                        foreach (state item in open)
                        {

                            if (a.children[i] == item)
                            {

                                if (a.children[i].Tcost < opencost[item])
                                {

                                    opencost[a.children[i]] = a.children[i].Tcost;
                                    open = open.OrderBy(x => opencost[x]).ToList();

                                }
                                f1++;
                            }
                            k++;
                        }

                        if (f1 == 0)
                        {
                            maps[a.children[i]] = a;
                            open.Add(a.children[i]);
                            opencost[a.children[i]] = a.children[i].Tcost;
                            open = open.OrderBy(x => opencost[x]).ToList();

                        }
                    }
                    //foreach (var item in open)
                    //{
                    //    MessageBox.Show(item.id + "  " + opencost[item] + "");
                    //}
                    //MessageBox.Show("done");





                    closed.Add(a);
                    //foreach (var item in open)
                    //{
                    //    //MessageBox.Show(item.id + "");
                    //}
                }


                //foreach (var item in open)
                //{
                //    MessageBox.Show(item.id + "");

                //}
                while (true)
                {
                    //MessageBox.Show(curr.id + "");
                    if (curr == first)
                        break;
                    state state = maps[curr];
                    pathcost += curr.cost[maps[curr]];
                    curr.path.Add(maps[curr]);
                    state.path.Add(curr);
                    curr = maps[curr];
                }
            }
        }



        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }


    }
}
