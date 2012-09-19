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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D white;
        Texture2D stexture;


        Smith smith;

        Vector2 startpos;

        SpriteFont sf;
        SpriteFont EndFont;
        Level CurrentLevel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 800;
            graphics.IsFullScreen = true;
            
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            Screen.Bounds = Window.ClientBounds;
            Create.setBounds(Window.ClientBounds);
            
            
            int xstart = Screen.Bounds.Width / 2;
            int ystart = Screen.Bounds.Height / 2;
            startpos = new Vector2(xstart, ystart);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // sprites
            
            white = Content.Load<Texture2D>("White");

            Screen.background = Content.Load<Texture2D>("bgnd");
            
            stexture = Content.Load<Texture2D>("smith");
            
            Create.TLadder = Content.Load<Texture2D>("ladder");
            Create.TGround = Content.Load<Texture2D>("ground");
            Create.TDeath = Content.Load<Texture2D>("death");
            
            // fonts
            sf = Content.Load<SpriteFont>("Font");

            EndFont = Content.Load<SpriteFont>("Font2");
            
            smith = new Smith(stexture, startpos);
            
            AfterLife.getLevel(smith, Screen.Bounds, white, EndFont);
            
            Create.t = white;

            CurrentLevel = L1.getLevel(smith, Window.ClientBounds, white);
            CurrentLevel.LoadNext();
      
        }
        
        protected override void UnloadContent()
        {
           
        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if ((Keyboard.GetState().IsKeyDown(Keys.Escape)))
                this.Exit();

            smith.Update(CurrentLevel.blks);

            CurrentLevel.Update();
            
            

            if (CurrentLevel.isNextLevel())
            {
                CurrentLevel.LoadNext();
                CurrentLevel = CurrentLevel.NextLevel;
                
            }

            if (CurrentLevel.isLastLevel())
            {
                CurrentLevel.LoadLast();
                CurrentLevel = CurrentLevel.LastLevel;
            }
            
            if (smith.prect.Y +  (smith.prect.Height) > Window.ClientBounds.Height + smith.Height)
            {
                Console.WriteLine("Smith is Dead");
                CurrentLevel = L1.getLevel(smith, Window.ClientBounds, white);
                smith.Die();
            }

            foreach (Block b in CurrentLevel.blks)
            {
                if (smith.prect.Intersects(b.Bounds) && b is DeathBlock)
                {
                    Console.WriteLine("Smith is Dead");
                    CurrentLevel = L1.getLevel(smith, Window.ClientBounds, white);
                    smith.Die();
                }
            }

            if (smith.lives <= 0)
            {
                CurrentLevel = AfterLife.getLevel(smith, Screen.Bounds, white, sf);
                smith.action = new Dead(smith.action);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);
            spriteBatch.Begin();

            CurrentLevel.Draw(spriteBatch);

            smith.Draw(spriteBatch, sf);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
