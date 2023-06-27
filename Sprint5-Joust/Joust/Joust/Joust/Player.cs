using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Joust
{
    class Player : Microsoft.Xna.Framework.Game
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public double Xspeed;
        public double Yspeed;
        public int livesRemaining;
        public int points;
        Boolean isOnFloor = false;
        int timer = 0;
        public KeyboardState oldKb = Keyboard.GetState();
        public Player( Rectangle rect, double Xvelocity, double Yvelocity, int livesLeft, int score)
        {
            
            rectangle = rect;
            Xspeed = Xvelocity;
            Yspeed = Yvelocity;
            livesRemaining = livesLeft;
            points = score;
        }

        public void update(Rectangle[] platforms)
        {
            KeyboardState kb = Keyboard.GetState();
            timer++;
            controls(kb);
            checks(platforms);
            oldKb = kb;
        }


        public void controls(KeyboardState kb)
        {
            if (kb.IsKeyDown(Keys.Left))
            {
                rectangle.X -= (int)Xspeed;
            }
            if (kb.IsKeyDown(Keys.Right))
            {
                rectangle.X += (int)Xspeed;
            }
            if (kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space))
            {
                rectangle.Y -= (int)Yspeed;
            }
        }

        public void checks(Rectangle[] platforms)
        {


            if (rectangle.X < 0 - rectangle.Width)
            {
                rectangle.X = 800;
            }
            if (rectangle.X > 800)
            {
                rectangle.X = 0 - rectangle.Width;
            }
            int count = 0;
            for (int i = 0; i < platforms.Length; i++)
            {
                if (rectangle.Intersects(platforms[i]))
                {
                    isOnFloor = true;
                }
                else
                    count++;
            }
            if (count == platforms.Length)
            {
                isOnFloor = false;
            }
            if (!isOnFloor)
            {
                if (timer % 30 == 0)
                    rectangle.Y += (int)Yspeed - 1;
            }
        }
    }


}




