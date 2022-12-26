using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photocutter
{
    public class Data
    {
        public Bitmap OriginalImage { get; set; }
        public Bitmap Working_bitmap { get; set; }
     
        public List<Rectangle> rects = new List<Rectangle>();

        public int numericUpDown8 = 0,
        numericUpDown7 = 0,
        numericUpDown4 = 0,
        numericUpDown5 = 0,
        numericUpDown6 = 0,
        X_Times = 0, 
        Y_Times = 0, 
        dashoreba = 0;

        public bool checkBox2 = false;
        
    }
}
