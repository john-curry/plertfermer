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
    public class Create
    {
        // This class will be a factory for creating platforms on the level as it suck ball to program manually
        // Three Different Types:
        //      1. Long: Whole Screen Length
        //      2. Medium: Half Screen Length
        //      3. Small: 1/4 Screen Length

        static int ladderWidth;
        public static int LadderWidth { get { return ladderWidth; } }
        
        public static int SmallWidth { get { return Screen.Bounds.Width / 4 ; } }
        public static int MediumWidth { get { return Screen.Bounds.Width / 2; } }
        public static int LongWidth { get { return Screen.Bounds.Width; } }

        static Rectangle Bounds;

        static int SmallHeight;

        static int smallBox;
        public static int SmallBox { get { return smallBox = Screen.Width / 40; } }

        static int bigBox;
        public static int BigBox { get { return smallBox = Screen.Height / 25; } }

        static Color dflt = Color.White;
        static Color dfltldr = Color.Brown;
        static Color dfltdeath = Color.White;
        static Color dfltpush = Color.DeepPink;
        
        
        static public Texture2D TLadder { set; get; }
        static public Texture2D TGround { get; set; }
        static public Texture2D TDeath { get; set; } 
        
        static Texture2D T;
        public static Texture2D t 
        { 
            set { T = value; }
            get { return T; }
        }

        public static void setBounds(Rectangle r)
        {
            Bounds = r;
            SmallHeight = Bounds.Height / 30;
            ladderWidth = Bounds.Width / 30;
        }

        public static Platform Pltm(int id, int x, int y)
        {
            if(id == 1)
                return new Platform(x, y, Bounds.Width, SmallHeight, dflt, TGround);
            if(id == 2)
                return new Platform(x, y, Bounds.Width / 2, SmallHeight, dflt, TGround);
            if (id == 3)
                return new Platform(x, y, Bounds.Width / 4, SmallHeight, dflt, TGround);
            if (id == 4)
                return new Platform(x, y, Bounds.Width / 6, SmallHeight, dflt, TGround);
            else
                return null;
        }

        public static Platform Pltm(int id, double x, double y)
        {
            if (id == 1)
                return new Platform((int)x, (int)y, Bounds.Width, SmallHeight, dflt, TGround);
            if (id == 2)
                return new Platform((int)x, (int)y, Bounds.Width / 2, SmallHeight, dflt, TGround);
            if (id == 3)
                return new Platform((int)x, (int)y, Bounds.Width / 4, SmallHeight, dflt, TGround);
            if (id == 4)
                return new Platform((int)x, (int)y, Bounds.Width / 6, SmallHeight, dflt, TGround);
            else
                return null;
        }

        public static Platform Pltm(int id, int x, int y, Color c)
        {
            if (id == 1)
                return new Platform(x, y, Bounds.Width, SmallHeight, c, TGround);
            if (id == 2)
                return new Platform(x, y, Bounds.Width / 2, SmallHeight, c, TGround);
            if (id == 3)
                return new Platform(x, y, Bounds.Width / 4, SmallHeight, c, TGround);

            else
                return null;
        }

        public static Ladder Ldr(int id, int x, int y)
        {
            
            if (id == 1)
                return new Ladder(x, y, LadderWidth, Bounds.Height, dfltldr, TLadder);
            if (id == 2)
                return new Ladder(x, y, LadderWidth, Bounds.Height / 2, dfltldr, TLadder);
            if (id == 3)
                return new Ladder(x, y, LadderWidth, Bounds.Height / 4, dfltldr, TLadder);
            else
                return null;
        }

        public static DeathBlock DeathBlock(double x, double y, double width, double height)
        {
            return new DeathBlock((int)x, (int)y, (int)width, (int)height, dfltdeath, TDeath);
        }

        public static PushBlock PushBlock(int id, double x, double y)
        {
            if(id == 1)
                return new PushBlock((int)x, (int)y, SmallBox, SmallBox, dfltpush, t);
            if (id == 2)
                return new PushBlock((int)x, (int)y, BigBox, BigBox, dfltpush, t);
            else
                return null;
        }
    }
}
