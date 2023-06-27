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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Invade[] invaders = new Invade[10];
        int sHeight;
        int sWidth;
        Rectangle collider;
        Rectangle collider2;
        Rectangle sourceHDown = new Rectangle(0, 0, 50, 38);
        Rectangle sourceHUp = new Rectangle(50, 0, 50, 28);
        Rectangle[] sources = new Rectangle[2];
        int index = 0;
        int timer = 0;
        SpriteFont font;
        Texture2D invaderT;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            sHeight = GraphicsDevice.Viewport.Height;
            sWidth = GraphicsDevice.Viewport.Width;
            collider = new Rectangle(900, 0, 50, sHeight);
            collider2 = new Rectangle(50, 0, 50, sHeight);
            for (int i = 0; i < invaders.Length; i++)
            {
                Rectangle invaderR = new Rectangle(130 + (i * 50), 30, 50, 50);
                invaderT = this.Content.Load<Texture2D>("invaders");
                invaders[i] = new Invade(invaderR, invaderT, 5);
            }
            sources[0] = sourceHDown;
            sources[1] = sourceHUp;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = this.Content.Load<SpriteFont>("SpriteFont1");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            timer++;
            if (timer % 10 == 0)
                index++;
            if (index >= 2)
                index = 0;
            for (int i = 0; i < invaders.Length; i++)
                invaders[i].Update(gameTime);
            if (invaders[invaders.Length - 1].rect.Intersects(collider) || invaders[0].rect.Intersects(collider2))
                hasCollided(sHeight);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            //spriteBatch.DrawString(font, "" + invaderT.Width + " " + invaderT.Height, new Vector2(0, 0), Color.White);
            for (int i = 0; i < invaders.Length; i++)
            {
                spriteBatch.Draw(invaders[i].text, invaders[i].rect, sources[index], Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }


        public void hasCollided(int screenHeight)
        {
            for (int i = 0; i < invaders.Length; i++)
            {
                invaders[i].xVelocity *= -1;
                if (invaders[i].rect.Y + 55 < screenHeight - 55)
                {
                    invaders[i].rect.Y += 55;
                }
            }
        }
    }
}
