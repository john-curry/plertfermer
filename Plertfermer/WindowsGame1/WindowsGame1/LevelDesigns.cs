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
    abstract class Level
    {
        public Level NextLevel;

        public Level LastLevel;

        protected Smith smith;

        public List<Block> blks;

        protected Texture2D texture;

        protected Rectangle Bounds;

        abstract public void Draw(SpriteBatch sBatch);

        abstract public void Update();

        public abstract bool isNextLevel();

        public abstract bool isLastLevel();

        public abstract void LoadNext();

        public abstract void LoadLast();

        public static Level getLevel(Smith smith, Rectangle bounds, Texture2D texture) { return null; }

    }

    //====================================================================================================
    //                          Level One
    //====================================================================================================
    
    class L1 : Level
    {
        private static L1 SL1 = null;


        private L1(Smith smith, Rectangle Bounds, Texture2D texture)
        {
            this.smith = smith;
            this.Bounds = Bounds;
            this.texture = texture;

            blks = new List<Block>();


            blks.Add(Create.Pltm(1, 0, Bounds.Height - 10));

            
           
            
        }

        public override void LoadNext()
        {
            this.NextLevel = L2.getLevel(smith, Bounds, texture);
            smith.prect.X = 3;
            this.LastLevel = null;
        }

        public override void LoadLast()
        {
            
        }
        
        public static new Level getLevel(Smith smith, Rectangle bounds, Texture2D texture)
        {
            if (SL1 == null)
            {
                SL1 = new L1(smith, bounds, texture);
            }
            return SL1;
            
        }

        public override void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(Screen.background, Screen.Bounds, Color.White);

            blks.ForEach(delegate(Block b) { b.Draw(sBatch); });

            
        }

        public override void Update()
        {
            blks.ForEach(delegate(Block b) { b.Update(blks); });
        }

        public override bool isNextLevel()
        {
            return (smith.prect.X + smith.prect.Width) >= Bounds.Width;
        }

        public override bool isLastLevel()
        {
            return false;
        }
    }

    //====================================================================================================
    //                          Level Two
    //====================================================================================================
    
    class L2 : Level
    {
        private static L2 SL2 = null;

        private L2(Smith smith, Rectangle Bounds, Texture2D texture)
        {
            this.smith = smith;
            this.Bounds = Bounds;
            this.texture = texture;

            blks = new List<Block>();

            blks.Add(Create.Pltm(4, 0, Screen.Bounds.Height - 10));
            blks.Add(Create.Ldr(2, Screen.Width / 3, Screen.Height / 2));
            blks.Add(Create.Ldr(3, (int)((1 - .2) * Screen.Width), 0));
            blks.Add(Create.Ldr(2, (int)((1 - .5) * Screen.Width), 0));
            blks.Add(new Platform((int)((1 - .5) * Screen.Width),Screen.Height / 2, (int)( 0.8 * Screen.Width),Screen.Height / 2, Color.White,Create.TGround));
        }

        public override void LoadLast()
        {
            this.NextLevel = null;
            smith.prect.X = Bounds.Width - (smith.prect.Width + 2);
            this.LastLevel = L1.getLevel(smith, Bounds, texture);
        }

        public override void LoadNext()
        {
            NextLevel = L3.getLevel(smith, Bounds, texture);
            smith.prect.X = 1;
        }


        public override void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(Screen.background, Screen.Bounds, Color.White);
            blks.ForEach(delegate(Block b) { b.Draw(sBatch); });
        }

        public override void Update()
        {
            blks.ForEach(delegate(Block b) { b.Update(blks); });
        }

        public static new Level getLevel(Smith smith, Rectangle bounds, Texture2D texture)
        {
            if (SL2 == null)
            {
                SL2 = new L2(smith, bounds, texture);
            }
            return SL2;
            

        }

        public override bool isNextLevel()
        {
            return smith.prect.X >= Screen.Bounds.Width;
        }

        public override bool isLastLevel()
        {
            return (smith.prect.X <= 0);
        }
    }

    //====================================================================================================
    //                          Level Three
    //====================================================================================================
    
    class L3 : Level
    {
        private static L3 SL3 = null;

        private L3(Smith smith, Rectangle Bounds, Texture2D texture)
        {
            this.smith = smith;
            this.Bounds = Bounds;
            this.texture = texture;

            blks = new List<Block>();
            blks.Add(Create.Pltm(3, 0, (int)((1 - .4) * Screen.Bounds.Height)));
            blks.Add(Create.Pltm(3, Screen.Bounds.Width - Create.SmallWidth, Screen.Bounds.Height / 4));
            blks.Add(Create.Ldr(1, Screen.Bounds.Width - Create.SmallWidth, Screen.Bounds.Height / 4)); 
            blks.Add(Create.Pltm(3,(int)(.5 * Screen.Width), (int)Screen.Height / 2));
            
        }

        public override void LoadLast()
        {
            
            smith.prect.X = Bounds.Width - (smith.prect.Width + 2);
            this.LastLevel = L2.getLevel(smith, Bounds, texture);
        }

        public override void LoadNext()
        {
            smith.prect.X = 1;
            this.NextLevel = L4.getLevel(smith,Bounds,texture);
        }


        public override void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(Screen.background, Screen.Bounds, Color.White);
            blks.ForEach(delegate(Block b) { b.Draw(sBatch); });
        }

        public override void Update()
        {
            blks.ForEach(delegate(Block b) { b.Update(blks); });
        }

        public static new Level getLevel(Smith smith, Rectangle bounds, Texture2D texture)
        {
            if (SL3 == null)
            {
                SL3 = new L3(smith, bounds, texture);
            }
            return SL3;
            

        }

        public override bool isNextLevel()
        {
            return smith.prect.X >= Screen.Bounds.Width;
        }

        public override bool isLastLevel()
        {
            return (smith.prect.X <= 0);
        }
    }
    //====================================================================================================
    //                          Level Four
    //====================================================================================================

    class L4 : Level
    {
        private static L4 SL4 = null;

        private L4(Smith smith, Rectangle Bounds, Texture2D texture)
        {
            this.smith = smith;
            this.Bounds = Bounds;
            this.texture = texture;

            blks = new List<Block>();

            blks.Add(Create.Pltm(4 , 0, Screen.Bounds.Width / 4));
            blks.Add(Create.Pltm(3,(int)((1 - .1) * Screen.Width), Screen.Height - 10));
            blks.Add(Create.DeathBlock(Screen.Width / 3, (1 - .3) * Screen.Height, Screen.Width / 3,Screen.Height / 2));
            blks.Add(Create.DeathBlock( 2 * Screen.Width / 3, 0, Screen.Width, Screen.Height / 2));
        }

        public override void LoadLast()
        {

            smith.prect.X = Bounds.Width - (smith.prect.Width + 2);
            this.LastLevel = L3.getLevel(smith, Bounds, texture);
        }

        public override void LoadNext()
        {
            this.NextLevel = L5.getLevel(smith,Bounds,texture);
            smith.prect.X = 1;
        }


        public override void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(Screen.background, Screen.Bounds, Color.White);
            blks.ForEach(delegate(Block b) { b.Draw(sBatch); });
        }

        public override void Update()
        {
            blks.ForEach(delegate(Block b) { b.Update(blks); });
        }

        public static new Level getLevel(Smith smith, Rectangle bounds, Texture2D texture)
        {
            if (SL4 == null)
            {
                SL4 = new L4(smith, bounds, texture);
            }
            return SL4;


        }

        public override bool isNextLevel()
        {
            return smith.prect.X + smith.Width >= Screen.Bounds.Width;
        }

        public override bool isLastLevel()
        {
            return (smith.prect.X <= 0);
        }
    }
    //====================================================================================================
    //                          AfterLife (aka gameover level)
    //====================================================================================================

    class AfterLife : Level
    {
        private static AfterLife SL = null;
        
        SpriteFont sf;

        private AfterLife(Smith smith, Rectangle Bounds, Texture2D texture, SpriteFont sf)
        {
            this.smith = smith;
            this.Bounds = Bounds;
            this.texture = texture;
            this.sf = sf;
            blks = new List<Block>();

            
          
        }



        public override void LoadLast()
        {

        }

        public override void LoadNext()
        {
            smith.Restart();
            this.NextLevel = L1.getLevel(smith,Bounds,texture);
        }


        public override void Draw(SpriteBatch sBatch)
        {
            foreach (Block b in blks)
            {
                b.Draw(sBatch);
            }

            sBatch.DrawString(sf, "BITCH YOU DEAD", new Vector2(Screen.Width / 2), Color.Black);
        }

        public override void Update()
        {
            foreach (Block b in blks)
            {
                b.Update(blks);
            }
        }

        public static Level getLevel(Smith smith, Rectangle bounds, Texture2D texture, SpriteFont sf)
        {
            if (SL == null)
            {
                SL = new AfterLife(smith, bounds, texture, sf);
            }
            return SL;


        }

        public override bool isNextLevel()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Enter);
        }

        public override bool isLastLevel()
        {
            return false;
        }
    }

    //====================================================================================================
    //                          Level 5
    //====================================================================================================

    class L5 : Level
    {
        private static L5 SL5 = null;

        

        private L5(Smith smith, Rectangle Bounds, Texture2D texture)
        {
            this.smith = smith;
            this.Bounds = Bounds;
            this.texture = texture;
         
            blks = new List<Block>();

            blks.Add(Create.Pltm(1,0,Screen.Height - 10));
            blks.Add(Create.PushBlock(1, Screen.Width / 2, Screen.Height - Create.SmallBox));

        }



        public override void LoadLast()
        {
            smith.prect.X = Screen.Width - smith.Width - 1;
            this.LastLevel = L4.getLevel(smith, Bounds, texture);
        }

        public override void LoadNext()
        {

        }


        public override void Draw(SpriteBatch sBatch)
        {
            sBatch.Draw(Screen.background, Screen.Bounds, Color.White);
            blks.ForEach(delegate(Block b) { b.Draw(sBatch); });
        }

        public override void Update()
        {
            
            blks.ForEach(delegate(Block b) { b.Update(blks); });
        }

        public new static Level getLevel(Smith smith, Rectangle bounds, Texture2D texture)
        {
            if (SL5 == null)
            {
                SL5 = new L5(smith, bounds, texture);
            }
            return SL5;


        }

        public override bool isNextLevel()
        { 
            return false;
        }

        public override bool isLastLevel()
        {
            return smith.X <= 0;
        }
    }

    //====================================================================================================
    //                          YouWon (aka "fuck you you beat my game" level)
    //====================================================================================================

    class Won : Level
    {
        private static Won  SL = null;

        SpriteFont sf;

        private Won(Smith smith, Rectangle Bounds, Texture2D texture, SpriteFont sf)
        {
            this.smith = smith;
            this.Bounds = Bounds;
            this.texture = texture;
            this.sf = sf;
            blks = new List<Block>();



        }



        public override void LoadLast()
        {

        }

        public override void LoadNext()
        {
            smith.Restart();
            this.NextLevel = L1.getLevel(smith, Bounds, texture);
        }


        public override void Draw(SpriteBatch sBatch)
        {
            foreach (Block b in blks)
            {
                b.Draw(sBatch);
            }

            sBatch.DrawString(sf, "fuck you you beat my game", new Vector2(Screen.Width / 2), Color.Black);
        }

        public override void Update()
        {
            foreach (Block b in blks)
            {
                b.Update(blks);
            }
        }

        public static Level getLevel(Smith smith, Rectangle bounds, Texture2D texture, SpriteFont sf)
        {
            if (SL == null)
            {
                SL = new Won(smith, bounds, texture, sf);
            }
            return SL;


        }

        public override bool isNextLevel()
        {
            return Keyboard.GetState().IsKeyDown(Keys.Enter);
        }

        public override bool isLastLevel()
        {
            return false;
        }
    }

}
