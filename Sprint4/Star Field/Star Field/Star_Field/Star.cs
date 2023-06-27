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

namespace Star_Field
{
    public class Star : Microsoft.Xna.Framework.Game
    {
        public Rectangle star;
        public Texture2D texture;
        public Color color;
        Vector2 position;
        int xVelocity, yVelocity;
        Random random = new Random();

        public Star(int screenheight, int screenWidth)
        {
            position = new Vector2(random.Next(screenWidth - 200) + 100, random.Next(screenheight - 200) + 100);
            color = new Color(random.Next(256), random.Next(256), random.Next(256));
            do
            {
                xVelocity = random.Next(-5, 5);
                yVelocity = random.Next(-5, 5);
            }
            while (xVelocity == 0 || yVelocity == 0);
            star = new Rectangle((int)position.X, (int)position.Y, random.Next(300), random.Next(300));
        }

        public void setT(Texture2D text)
        {
            texture = text;
        }

        public void Update(GameTime gameTime)
        {
            star.X += xVelocity;
            star.Y += yVelocity;
            base.Update(gameTime);
        }
    }
}
