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
    class Enemy : Microsoft.Xna.Framework.Game
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public double Xspeed;
        public double Yspeed;
        public Boolean isEgg;
        public int eggTimer;
        public Boolean remove;

        public Enemy(Texture2D tex, Rectangle rect, double Xvelocity, double Yvelocity)
        {
            texture = tex;
            rectangle = rect;
            Xspeed = Xvelocity;
            Yspeed = Yvelocity;
            eggTimer = 0;
        }

        public void update()
        {
            if (isEgg)
            {
                eggTimer++;
                double eggSec = eggTimer / 60;
                if (eggSec >= 10)
                {
                    remove = true;
                    eggTimer = 0;
                    isEgg = false;
                }
                if (rectangle.X + rectangle.Width < 140 && rectangle.Y > 650 || rectangle.X > 690 && rectangle.Y > 650)
                {
                    remove = true;
                }
            }    
        }
    }
}



