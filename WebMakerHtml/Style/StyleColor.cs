using System;
using System.Drawing;

namespace WebMaker.Html.Style
{
    /// <summary>
    /// Třída reprezentující CSS barvu
    /// </summary>
    public class StyleColor
    {
        private const string rgbaNotation = "rgba({0}, {1}, {2}, {3})";
        private const string rgbNotation = "rgb({0}, {1}, {2})";

        /// <summary>
        /// Vytvoří instanci třídy style color
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        /// <param name="a">Alpha</param>
        public StyleColor(byte r, byte g, byte b, byte? a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Vytvoří instanci třídy style color
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Green</param>
        /// <param name="b">Blue</param>
        public StyleColor(byte r, byte g, byte b) : this(r, g, b, null)
        {
        }

        /// <summary>
        /// Vytvoří instanci třídy style color
        /// </summary>
        /// <param name="color">Barva</param>
        public StyleColor(Color color) : this(color.R, color.G, color.B, color.A)
        {
        }

        /// <summary>
        /// Vytvoří instanci třídy style color
        /// </summary>
        /// <param name="gray">Odstín šedé</param>
        public StyleColor(byte gray) : this(gray, gray, gray)
        {
        }
        /// <summary>
        /// Černá
        /// </summary>
        public static StyleColor Black { get; } = new StyleColor(0);
        /// <summary>
        /// Modrá
        /// </summary>
        public static StyleColor Blue { get; } = new StyleColor(0, 0, 255);
        /// <summary>
        /// Modrozelená
        /// </summary>
        public static StyleColor Cyan { get; } = new StyleColor(0, 255, 255);
        /// <summary>
        /// Zelená
        /// </summary>
        public static StyleColor Green { get; } = new StyleColor(0, 255, 0);
        /// <summary>
        /// Magenta
        /// </summary>
        public static StyleColor Magenta { get; } = new StyleColor(255, 0, 255);
        /// <summary>
        /// Červená
        /// </summary>
        public static StyleColor Red { get; } = new StyleColor(255, 0, 0);
        /// <summary>
        /// Bílá
        /// </summary>
        public static StyleColor White { get; } = new StyleColor(255);
        /// <summary>
        /// Žlutá
        /// </summary>
        public static StyleColor Yellow { get; } = new StyleColor(255, 255, 0);

        /// <summary>
        /// Alpha
        /// </summary>
        public byte? A { get; set; }
        /// <summary>
        /// Blue
        /// </summary>
        public byte B { get; set; }

        /// <summary>
        /// Alpha převedená na hodnotu mezi 0 a 1
        /// </summary>
        public float? FloatA
        {
            get
            {
                if (A is null)
                {
                    return null;
                }
                else
                {
                    return (float?)Math.Round((float)A / byte.MaxValue, 2);
                }
            }
            set
            {
                if (value is null)
                {
                    A = null;
                }
                else
                {
                    A = (byte)(value * byte.MaxValue);
                }
            }
        }
        /// <summary>
        /// Green
        /// </summary>
        public byte G { get; set; }
        /// <summary>
        /// Red
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// Sečte dvě barvy pomocí průměru
        /// </summary>
        /// <param name="a">První barva</param>
        /// <param name="b">Druhá barva</param>
        /// <returns>Součet barev pomocí průměru</returns>
        public static StyleColor operator +(StyleColor a, StyleColor b)
        {
            return new StyleColor(Average(a.R, b.R), Average(a.G, b.G), Average(a.B, b.B));
        }

        /// <summary>
        /// Převede barvu na řetězec
        /// </summary>
        /// <returns>Barva v RGB notaci</returns>
        public override string ToString()
        {
            if (A is null)
            {
                return string.Format(rgbNotation, R, G, B);
            }
            else
            {
                return string.Format(rgbaNotation, R, G, B, FloatA);
            }
        }

        private static byte Average(byte a, byte b)
        {
            return (byte)((a + b) / 2);
        }
    }
}