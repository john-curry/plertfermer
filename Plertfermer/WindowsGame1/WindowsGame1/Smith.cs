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
    class Smith
    {
        #region dimensions
        public Rectangle prect;
        public int X { get { return prect.X; } set { prect.X = value; } }
        public int Y { get { return prect.Y; } set { prect.Y = value; } }

        protected int width;
        public int Width { get { return width; } }
        protected int height;
        public int Height { get { return height; } }
    #endregion

        #region speed
        int speed;
        public int Speed { get { return speed; } }
       
        // Used for speed calculations
        int timetocrossscreen = 250;
        #endregion

        #region accspeed

        int acctimetoscross = 110;
        int accspeed;
        public int AccSpeed { get { return accspeed; } }

        #endregion

        #region lives
        int Lives = 5;
        public int lives { get { return Lives; } }
        int distfromtop = Screen.Height / 20;
        int distfromside = Screen.Width / 20;
        int lifebarwidth = Screen.Width / 5;
        int lifebarheight = Screen.Height / 10;

        


        Rectangle LifeBar;
        Rectangle LifeLeft;
        #endregion

        Texture2D pl;
        public State action;
        
        public Smith(Texture2D pl, Vector2 pos)
        {
            this.pl = pl;
            this.width = (int)(Screen.Bounds.Width / 40);
            this.height = 2 * width;
            this.prect = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            
            this.action = new Start(this);

            this.speed = Screen.Bounds.Width / timetocrossscreen;
            this.accspeed = Screen.Bounds.Width / acctimetoscross;

            this.LifeBar = new Rectangle(distfromside, distfromtop, lifebarwidth, lifebarheight);
            this.LifeLeft = new Rectangle(distfromside, distfromtop, lifebarwidth, lifebarheight);
            
        }

        public void Update(List<Block> blocks)
        {
            action.Update();

            if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) AccMove(); else Move(); 

            action.ManageState(blocks);
        }

        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                prect.X -= speed;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                prect.X += speed;
        }

        private void AccMove()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                prect.X -= accspeed;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                prect.X += accspeed;
        }

        public void Draw(SpriteBatch sBatch, SpriteFont sf)
        {
      
            sBatch.Draw(pl, prect, Color.White);
            sBatch.Draw(pl, LifeBar, Color.Green);
            sBatch.Draw(pl, LifeLeft ,Color.HotPink);
            sBatch.DrawString(sf,"Lives: " + Lives, new Vector2(distfromside,distfromtop),Color.White);
        }

        public void Restart()
        {

            Lives = 5;
            prect.X = 0;
            prect.Y = 0;
            action = new Start(this);
        }

        public void Die()
        {
            if (Lives > 0)
            {
                Lives--;
            }

            prect.X = 0;
            prect.Y = 0;
            Console.WriteLine("smith has " + Lives);
        }



        public bool onTop(Rectangle platform)
        {
            return ((prect.X + prect.Width >= platform.X && (prect.X + prect.Width < platform.X + platform.Width))
                    || ((prect.X <= (platform.X + platform.Width)) && (prect.X >= platform.X)))
                    && (prect.Y + prect.Height >= platform.Y)
                    && (prect.Y <= platform.Y);
        }


    }
}
