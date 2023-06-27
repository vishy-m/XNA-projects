using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;


namespace space_invaders_pt1
{
    class Invade : Microsoft.Xna.Framework.Game
    {
        public Rectangle rect;
        public Texture2D text;
        public int xVelocity;
        int timer = 0;

        public Invade()
        {
            rect = new Rectangle(0, 0, 0, 0);
            text = null;
            xVelocity = 0;
        }
        public Invade(Rectangle r, Texture2D t, int v)
        {
            rect = r;
            text = t;
            xVelocity = v;
        }

        public void Update(GameTime gameTime)
        {
            rect.X += xVelocity;
        }
        
    }
}
