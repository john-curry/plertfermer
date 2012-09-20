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
    abstract class State
    {
        public Smith smith;
        public abstract void Update();
        public abstract void ManageState(List<Block> blocks);
       

    }

    class Start : State
    {
        public Start(Smith smith)
        {
            this.smith = smith;
        }

        public override void Update()
        {
            
        }

        public override void ManageState(List<Block> blocks)
        {
            smith.action = new Falling(this);
        }
        
    }

    class Falling : State
    {
        int fallspeed;
        int timetofall = 180;
        
        public Falling(State state)
        {
            Console.WriteLine("Falling");
            this.smith = state.smith;
            fallspeed = Screen.Width / timetofall;

        }

        public override void Update()
        {
            
            smith.prect.Y += fallspeed;

        }

        public override void ManageState(List<Block> blocks)
        { 
            if(blocks.Exists(delegate(Block b) { return b.Bounds.Intersects(smith.prect) && b is Ladder; }))
                smith.action = new Climbing(this);

            if(blocks.Exists(delegate(Block b) { return smith.onTop(b.Bounds) && ( b is Platform || b is PushBlock ); }))
                smith.action = new Moving(this);
        }
    }

    class Moving : State
    {

        

        public Moving(State state)
        {
            this.smith = state.smith;
            
        }

        public override void Update()
        {


        }

        public override void ManageState(List<Block> blocks)
        {

            
            foreach (Block b in blocks)
            {
                if (smith.prect.Intersects(b.Bounds) && Keyboard.GetState().IsKeyDown(Keys.A) && b is PushBlock)
                {
                    Console.WriteLine("moving block left");
                    (b as PushBlock).Move("left", null, smith.Speed, 0);
                }
                if (smith.prect.Intersects(b.Bounds) && Keyboard.GetState().IsKeyDown(Keys.D) && b is PushBlock)
                {
                    Console.WriteLine("moving block right");
                    (b as PushBlock).Move("right", null, smith.Speed, 0);
                }
            }

          
            
            foreach (Block b in blocks)
            {
                if (b.Bounds.Intersects(smith.prect) && b is Ladder)
                    smith.action = new Climbing(this);
     
                if (b.Bounds.Intersects(smith.prect) && b is AntiPlatform)
                    smith.action = new OnAntiPlatform(this);

            }
            
            if (!blocks.Exists(delegate(Block b) { return b is Platform && smith.onTop(b.Bounds); }))
                smith.action = new Falling(this);
           
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                smith.action = new Jumping(this);
            

           
            
        }
    }

    class Jumping : State
    {
        int jmpht;
        int jumpspeed;
        int ystrt;
        const int timeonjump = 15;
        const double jumphtfractionofscreen = 1 / 6.1;
 
        public Jumping(State state)
        {
            this.smith = state.smith;
            ystrt = smith.prect.Y;
            jmpht = (int)(Screen.Bounds.Height * jumphtfractionofscreen) ;
            jumpspeed = jmpht / timeonjump;
        }

        public override void Update()
        {
            smith.prect.Y -= jumpspeed;
        }

        public override void ManageState(List<Block> blocks)
        {
            if (smith.prect.Y <= ystrt - jmpht)
            {
                smith.action = new Falling(this);
            }
        }
    }

    class Climbing : State
    {
        const int climbspeed = 5;

        public Climbing(State state)
        {
            Console.WriteLine("Climbing");
            this.smith = state.smith;
            
        }

        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                smith.prect.Y -= climbspeed;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                smith.prect.Y += climbspeed;
        }

        public override void ManageState(List<Block> blocks)
        {
            if(!blocks.Exists(delegate(Block b) { return b.Bounds.Intersects(smith.prect) && b is Ladder; }))
                smith.action = new Falling(this);

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                smith.action = new Jumping(this);
        }
    }

    class OnAntiPlatform : State
    {
        int grav;
        const int timefromtoptobottom = 20;

        public OnAntiPlatform(State state)
        {
            Console.WriteLine("OnAntiPlatfrom");
            
            this.smith = state.smith;
         

            grav = Screen.Bounds.Height / timefromtoptobottom;
        }

        public override void Update()
        {
            smith.prect.Y += grav;

        }

        public override void ManageState(List<Block> blocks)
        {
            if (blocks.Exists(delegate(Block b) { return b is Platform && smith.onTop(b.Bounds); }))
                smith.action = new Moving(this);
            if (!blocks.Exists(delegate(Block b) { return (b is Platform || b is PushBlock) && b.Bounds.Intersects(smith.prect); }))
                smith.action = new Falling(this);


        }
    }
    class Dead : State
    {
        public Dead(State state)
        {
            this.smith = state.smith;
            smith.prect.X = Screen.Width / 2;
            smith.prect.Y = Screen.Height / 2;
        }

        public override void Update()
        {

        }

        public override void ManageState(List<Block> blocks)
        {
            
        }

    }
}
