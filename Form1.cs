using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms; 
namespace photocutter
{
    public partial class Form1 : Form
    {
     
        Point MouseLastLocation;
        /// <summary>
        /// Open File Dialog
        /// </summary>
        OpenFileDialog _OFD { get; set; }

        /// <summary>
        /// Save File Dialog
        /// </summary>
        SaveFileDialog _savefile { get; set; }

        Data DT = new Data();

        GridDrawer drawer { get; set; }
        
        
        Size size = new Size(0, 0);

        Pen pen = new Pen(Color.Black);


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (_savefile == null)
            {
                _savefile = savedialog() as SaveFileDialog;
            }

            if (_OFD == null)
            {
                _OFD = dialog() as OpenFileDialog;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_OFD.ShowDialog() == DialogResult.OK)
            {
                drawer = new GridDrawer(DT, new Bitmap(_OFD.FileName));
              
                pictureBox1.Image = (Image)DT.OriginalImage;
                pictureBox1.Size = DT.OriginalImage.Size;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                label2.Text = DT.OriginalImage.Size.ToString();
                label4.Text = _OFD.FileName;
                
                pictureBox1.Image = (Image)DT.Working_bitmap;
                
            }
        }

        /// <summary>
        /// Open File Dialog Parameters
        /// </summary>
        /// <returns></returns>
        private static object dialog()
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = "item.png";
            OFD.InitialDirectory = @"c:\";
            OFD.FileName = "item";
            OFD.Filter = "Png (*.png)|*.png";
            OFD.FilterIndex = 2;
            OFD.RestoreDirectory = true;
            return OFD;
        }
        private static object savedialog()
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = "unknown";
            savefile.Filter = "Text files (*.png)|*.png|All files (*.*)|*.*";
            return savefile;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pictureBox1.Size = new Size(600, 800);
            }
            else
            {
                pictureBox1.Size = DT.OriginalImage.Size;
            }

            drawer.Draw_rectangles_auto();
            pictureBox1.Refresh();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point Location = e.Location;
            if (e.Button == MouseButtons.Left)
            {
                label2.Text = e.Location.ToString();
    

                if (radioButton2.Checked)
                {
                    drawrectangles(Location);
                    MouseLastLocation = Location;
                }

            }
        }


        private void button3_Click(object sender, EventArgs e)
        {



            Point location = new Point(0, 0);


            if (_savefile.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < DT.Y_Times; i++)
                {

                    for (int j = 0; j < DT.X_Times; j++)
                    {
                        string name = $"{_savefile.FileName}{i}{j}.png";
                        Rectangle rectangle = new Rectangle(location, size);
                    
                        DT.OriginalImage.Clone(rectangle, PixelFormat.Format32bppArgb).Save(name, ImageFormat.Png);

                        location = new Point(location.X + DT.dashoreba + size.Width, location.Y);
                    }
                    location = new Point(0, location.Y + size.Height + DT.dashoreba);
                }


            }


        }


        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                panelAuto.Visible = true;
                panelmanual.Visible = false;
            }
            else
            {
                panelAuto.Visible = false;
                panelmanual.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Point zero = new Point(0, 0);
            drawrectangles(zero);
            MouseLastLocation = new Point(0, 0);
        }


        private void drawrectangles(Point startlocation)
        {
            drawer.Draw_rectangles(startlocation);
            pictureBox1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Point location = MouseLastLocation;
            Size size = new Size((int)numericUpDown8.Value, (int)numericUpDown7.Value);


            if (_savefile.ShowDialog() == DialogResult.OK)
            {

                for (int i = 0; i < (int)numericUpDown4.Value; i++)
                {
                    for (int j = 0; j < (int)numericUpDown5.Value; j++)
                    {
                        bool sheasrule = true;
                        if (location.X < 0)
                        {
                            location.X = 0;
                        }
                        else if (location.X + size.Width > DT.OriginalImage.Width)
                        {
                            size.Width = DT.OriginalImage.Width - location.X;
                        }

                        if (location.Y < 0)
                        {
                            location.Y = 0;
                        }
                        else if (location.Y + size.Height > DT.OriginalImage.Height)
                        {
                            size.Height = DT.OriginalImage.Height - location.Y;
                        }

                        if (size.Width <= 0 || size.Height <= 0)
                        {
                            sheasrule = false;
                        }
                        if (sheasrule)
                        {
                            string name = _savefile.FileName.Remove(_savefile.FileName.Length - 6, 4);
                            name = name.Insert(_savefile.FileName.Length - 4, $"{i}{j}.png");
                            //string name = savefile.FileName;

                            Rectangle rectangle = new Rectangle(location, size);
                            Bitmap btmm = DT.OriginalImage.Clone(rectangle, PixelFormat.Format32bppArgb);
                            btmm.Save(name, ImageFormat.Png);
                        }


                        location = new Point(location.X + (int)numericUpDown6.Value + size.Width, location.Y);
                    }
                    location = new Point(MouseLastLocation.X, location.Y + size.Height + (int)numericUpDown6.Value);
                }

            }
        }

        public void refresh_numbericUpDowns(object sender, EventArgs e)
        {
            DT.numericUpDown4 = (int)numericUpDown4.Value;
            DT.numericUpDown5 = (int)numericUpDown5.Value;
            DT.numericUpDown6 = (int)numericUpDown6.Value;
            DT.numericUpDown7 = (int)numericUpDown7.Value;
            DT.numericUpDown8 = (int)numericUpDown8.Value;
            DT.checkBox2 = checkBox2.Checked;
            DT.X_Times = (int)numericUpDown1.Value;
            DT.Y_Times = (int)numericUpDown2.Value;
            DT.dashoreba = (int)numericUpDown3.Value;
            size = new Size(DT.OriginalImage.Width / DT.X_Times - DT.dashoreba, DT.OriginalImage.Height / DT.Y_Times - DT.dashoreba);
        }
    }
}
