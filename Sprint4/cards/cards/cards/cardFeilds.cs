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

namespace cards
{
    public class cardFeilds : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Rectangle diamond;
        public Rectangle spade;
        public Texture2D spadeT;
        public Texture2D diamondT;
        //Game1 game = new Game1();
        public cardFeilds()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            diamond = new Rectangle(100, 200, 100, 100);
            spade = new Rectangle(300, 200, 100, 100);
        }


    }
}
