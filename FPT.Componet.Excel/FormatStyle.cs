using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FPT.Component.ExcelPlus
{
    public class BorderStyle
    {
        public Color BorderColor { get; set; }
        public BorderWeight Weight { get; set; }
        public LineStyle Style { get; set; }

        public BorderStyle()
        {
            Weight = BorderWeight.None;
            Style = LineStyle.None;
        }

        public BorderStyle(BorderStyle other)
        {
            BorderColor = Color.FromArgb(other.BorderColor.ToArgb());
            Weight = other.Weight;
        }
    }

    public class CellStyle
    {
        public string StyleName { get; set; }
        public FHorizontalAlignment HAlign { get; set; }
        public FVerticalAlignment VAlign { get; set; }
        public TextStyle TextStyle { get; set; }
        public CellBorder Border { get; set; }
        public Color BackGroundColor { get; set; }
        public bool WrapText { get; set; }
        public string NumberFormat { get; set; }

        public CellStyle()
        {
            HAlign = FHorizontalAlignment.Left;
            VAlign = FVerticalAlignment.Top;
            TextStyle = new TextStyle();
            Border = new CellBorder();
            StyleName = string.Empty;
            BackGroundColor = Color.Transparent;
            WrapText = false;
            NumberFormat = string.Empty;
        }
    }

    public class TextStyle
    {
        public string FontName { get; set; }
        public bool Bold { get; set; }
        public bool Italic { get; set; }
        public bool Underline { get; set; }
        public bool Strikeout { get; set; }
        public short Size { get; set; }
        public Color TextColor { get; set; }

        public TextStyle()
        {
            FontName = string.Empty;
            Bold = false;
            Italic = false;
            Underline = false;
            Strikeout = false;
            Size = 0;
            TextColor = Color.Black;
        }
    }

    public class CellBorder
    {
        public BorderStyle Top { get; set; }
        public BorderStyle Bottom { get; set; }
        public BorderStyle Left { get; set; }
        public BorderStyle Right { get; set; }
        public Color BorderColor
        {
            set
            {
                Top.BorderColor = value;
                Bottom.BorderColor = value;
                Left.BorderColor = value;
                Right.BorderColor = value;
            }
        }
        public BorderWeight Weight
        {
            set
            {
                Top.Weight = value;
                Bottom.Weight = value;
                Left.Weight = value;
                Right.Weight = value;
            }
        }
        public LineStyle Style
        {
            set
            {
                Top.Style = value;
                Bottom.Style = value;
                Left.Style = value;
                Right.Style = value;
            }
        }

        public CellBorder()
        {
            Top = new BorderStyle();
            Bottom = new BorderStyle();
            Left = new BorderStyle();
            Right = new BorderStyle();
        }

        public CellBorder(BorderStyle style)
        {
            Top = new BorderStyle(style);
            Bottom = new BorderStyle(style);
            Left = new BorderStyle(style);
            Right = new BorderStyle(style);
            BorderColor = Color.Black;
        }
    }

    public enum BorderWeight
    {
        None,
        Hair,
        Thin,
        Medium,
        Thick
    }

    public enum LineStyle
    {
        None = 0,
        Hair = 1,
        Dotted = 2,
        DashDot = 3,
        Thin = 4,
        DashDotDot = 5,
        Dashed = 6,
        MediumDashDotDot = 7,
        MediumDashed = 8,
        MediumDashDot = 9,
        Thick = 10,
        Medium = 11,
        Double = 12
    }

    public enum FHorizontalAlignment
    {
        General = 0,
        Left = 1,
        Center = 2,
        CenterContinuous = 3,
        Right = 4,
        Fill = 5,
        Distributed = 6,
        Justify = 7
    }

    public enum FVerticalAlignment
    {
        Top = 0,
        Center = 1,
        Bottom = 2,
        Distributed = 3,
        Justify = 4
    }
}
