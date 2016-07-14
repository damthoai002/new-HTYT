using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPT.Component.ExcelPlus
{
    public class ColorPalette
    {
        public List<int> ColorCollection
        {
            get { return colorCollection; }
        }

        private int indexCount;
        private List<int> colorCollection;

        public ColorPalette(int capacity)
        {
            indexCount = capacity;
            colorCollection = new List<int>();
        }
        public void AddColor(System.Drawing.Color color, bool force)
        {
            if (ColorCollection.Count < indexCount)
            {
                if (!ColorCollection.Contains(color.ToArgb()))
                    ColorCollection.Add(color.ToArgb());
            }
            else if (force)
            {
                ColorCollection.Sort();
                int tmp = ColorCollection.Find(x => x >= color.ToArgb());
                ColorCollection[tmp] = color.ToArgb();
            }
        }

        public short GetColorIndex(System.Drawing.Color color)
        {
            ColorCollection.Sort();
            return (short)ColorCollection.Find(x => x >= color.ToArgb());
        }
    }
}
