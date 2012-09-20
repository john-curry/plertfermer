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
    public abstract class Block
    {
        protected Rectangle bounds;
        public Rectangle Bounds
        {
            get { return bounds; }
        }

        protected Color c;
        protected Texture2D t;

        abstract public void Update(List<Block> blocks);

        abstract public void Draw(SpriteBatch sb);
    }
    //================================================================================
    //=========================Platform===============================================
    //================================================================================
    public class Platform : Block
    {
        public Platform(int x, int y, int width, int height, Color c, Texture2D t)
        {
            this.bounds = new Rectangle(x, y, width, height);
            this.c = c;
            this.t = t;
        }

        public override void Update(List<Block> blocks)
        {
            
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(t, Bounds, c);
        }
    }

    //================================================================================
    //=========================Anti-Platform==========================================
    //================================================================================
    public class AntiPlatform : Block
    {
        public AntiPlatform(int x, int y, int width, int height, Color c, Texture2D t)
        {
            this.bounds = new Rectangle(x, y, width, height);
            this.c = c;
            this.t = t;
        }

        public override void Update(List<Block> blocks)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(t, Bounds, c);
        }
    }
    //================================================================================
    //=========================DeathBlock=============================================
    //================================================================================
    public class DeathBlock : Block
    {
        public DeathBlock(int x, int y, int width, int height, Color c, Texture2D t)
        {
            this.bounds = new Rectangle(x, y, width, height);
            this.c = c;
            this.t = t;
        }

        public override void Update(List<Block> blocks)
        {

        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(t, Bounds, c);
        }
    }
    //================================================================================
    //=========================PushBlock==============================================
    //================================================================================
    public class PushBlock : Block
    {

    
        int fallingspeed;
        int timetofall = 160;
        public PushBlock(int x, int y, int width, int height, Color c, Texture2D t)
        {
            this.bounds = new Rectangle(x, y, width, height);
            this.c = c;
            this.t = t;
          

            fallingspeed = Screen.Height / timetofall;
        }

        public override void Update(List<Block> blocks)
        {
            if(!blocks.Exists(delegate(Block b) { return onTop(b.Bounds) && (b is Platform || b is PushBlock) && !Object.ReferenceEquals(b,this); }))
                bounds.Y += fallingspeed;

        }

        public override void Draw(SpriteBatch sb) 
        {
            sb.Draw(t, Bounds, c);
        }

        public void Move(string xdir, string ydir, int dx, int dy)
        {
            // xdir and ydir only recongizes "left" and "right"
            if (xdir == "left")
                this.bounds.X -= dx;
            else if (xdir == "right")
                this.bounds.X += dx;
            else
                ;

            if (ydir == "up")
                this.bounds.Y -= dy;
            else if (ydir == "down")
                this.bounds.Y += dy;
            else
                ;
        }

        public bool onTop(Rectangle platform)
        {
            return ((bounds.X + bounds.Width >= platform.X && (bounds.X + bounds.Width < platform.X + platform.Width))
                    || ((bounds.X <= (platform.X + platform.Width)) && (bounds.X >= platform.X)))
                    && (bounds.Y + bounds.Height >= platform.Y)
                    && (bounds.Y <= platform.Y);
        }
    }
    //================================================================================
    //=========================Ladder=================================================
    //================================================================================
    public class Ladder : Block
    {
        public Ladder(int x, int y, int width, int height, Color c, Texture2D t)
        {
            this.t = t;
            this.c = c;
            this.bounds = new Rectangle(x, y, width, height);
        }

        public override void Update(List<Block> blocks)
        {
            
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(t, Bounds, c);
        }
    }
    //================================================================================
    //=========================UpMover================================================
    //================================================================================
    public class UpMover : Block
    {
        private System.Timers.Timer timer;
        
        enum Dir { Up, Down }

        private Dir dir;

        private int speed;

        public UpMover(int x, int y, int width, int height, int distance, int speed, Color c, Texture2D t, bool isUp)
        {
            this.bounds = new Rectangle(x, y, width, height);
            this.c = c;
            this.t = t;
            this.speed = speed;

            int time = distance / speed;

            if (isUp)
                dir = Dir.Up;
            else
                dir = Dir.Down;

            timer = new System.Timers.Timer(time);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

            
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Dir.Up == dir)
                dir = Dir.Down;
            if (Dir.Down == dir)
                dir = Dir.Up;
        }

        public override void Update(List<Block> blocks)
        {
            if (Dir.Up == dir)
                bounds.Y -= speed;
            else
                bounds.Y += speed;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(t, Bounds, c);
        }

    }
}
