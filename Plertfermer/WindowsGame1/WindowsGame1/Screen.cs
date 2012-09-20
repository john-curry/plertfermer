using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame1
{
    public class Screen
    {
        static Rectangle bounds;
        public static Rectangle Bounds { get { return bounds; } set { bounds = value; } }
        public static int Width { get { return bounds.Width; } }
        public static int Height { get { return bounds.Height; } }
        public static Texture2D background { get; set; }
    }
}
