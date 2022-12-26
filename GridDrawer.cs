using System.Collections.Generic;
using System.Drawing;
using Image = System.Drawing.Image;

namespace photocutter
{
    public class GridDrawer
    {
        public Data _DT = new Data();

        


        Pen pen = new Pen(Color.Black);
        Point location = new Point(0, 0);
        Size size = new Size(0, 0);
        bool sheasrule = false;





        public GridDrawer(Data DT, Bitmap image)
        {
            on_load(DT, image);
        }

        public GridDrawer(Data DT, Image image)
        {
            on_load(DT, (Bitmap)image);
        }

        private void on_load(Data DT, Bitmap image)
        {
            _DT = DT;
            _DT.OriginalImage = new Bitmap(image);
            _DT.Working_bitmap = new Bitmap(image);
        }

        public void Draw_rectangles_auto()
        {



            // პანელ 2
            location = new Point(0, 0);

            _DT.rects.Clear();
            for (int i = 0; i < _DT.Y_Times; i++)
            {
                for (int j = 0; j < _DT.X_Times; j++)
                {
                    _DT.rects.Add(new Rectangle(location, size));
                    location = new Point(location.X + _DT.dashoreba + size.Width, location.Y);
                }
                location = new Point(0, location.Y + size.Height + _DT.dashoreba);
            }

            using (Graphics gg = Graphics.FromImage(_DT.Working_bitmap))
            {
                gg.DrawImage(_DT.OriginalImage, 0, 0);
                foreach (var rectangle in _DT.rects)
                {

                    gg.DrawRectangle(pen, rectangle);
                }
                gg.Save();
            }
        }

        public void Draw_rectangles(Point startlocation)
        {



            location = startlocation;
            size = new Size((int)_DT.numericUpDown8, (int)_DT.numericUpDown7);

            if (_DT.checkBox2)
            {
                using (Graphics g = Graphics.FromImage(_DT.Working_bitmap))
                {
                    g.Clear(Color.White);
                }

            }
            else
            {
                using (Graphics g = Graphics.FromImage(_DT.Working_bitmap))
                {
                    g.DrawImage(_DT.OriginalImage, 0, 0);
                }

            }



            _DT.rects.Clear();
            for (int i = 0; i < _DT.numericUpDown4; i++)
            {
                for (int j = 0; j < _DT.numericUpDown5; j++)
                {
                    sheasrule = true;

                    if (location.X < 0)
                    {
                        location.X = 0;
                    }
                    else if (location.X + size.Width > _DT.OriginalImage.Width)
                    {
                        size.Width = _DT.OriginalImage.Width - location.X;
                    }

                    if (location.Y < 0)
                    {
                        location.Y = 0;
                    }
                    else if (location.Y + size.Height > _DT.OriginalImage.Height)
                    {
                        size.Height = _DT.OriginalImage.Height - location.Y;
                    }
                    if (size.Width <= 0 || size.Height <= 0)
                    {
                        sheasrule = false;
                    }
                    if (sheasrule)
                        _DT.rects.Add(new Rectangle(location, size));

                    location = new Point(location.X + (int)_DT.numericUpDown6 + size.Width, location.Y);
                }
                location = new Point(startlocation.X, location.Y + size.Height + (int)_DT.numericUpDown6);
            }

            using (Graphics gg = Graphics.FromImage(_DT.Working_bitmap))
            {
                foreach (Rectangle rect in _DT.rects)
                {
                    if (_DT.checkBox2)
                    {
                        gg.DrawImage(_DT.OriginalImage, rect, rect, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        gg.DrawRectangle(pen, rect);
                    }
                }
            }
        }
    }
}
