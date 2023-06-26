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

namespace Museum
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle heroRect;
        Rectangle ironRect;
        Rectangle paintRect;
        Rectangle museumRect;
        Texture2D heroTexture;
        Texture2D ironTexture;
        Texture2D paintTexture;
        Texture2D musuemTexture;
        int count = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            count = 0;
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
            ironRect = new Rectangle(50, 100, 300, 500);
            heroRect = new Rectangle(300, 250, 100, 200);
            paintRect = new Rectangle(225, 150, 100, 200);
            museumRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
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
            paintTexture = this.Content.Load<Texture2D>("painting");
            heroTexture = this.Content.Load<Texture2D>("herobrineArms");
            ironTexture = this.Content.Load<Texture2D>("IronGolemArms");
            musuemTexture = this.Content.Load<Texture2D>("museum");
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
            if (paintRect.X != 550)
            {
                heroRect.X++;
                ironRect.X++;
                paintRect.X++;
            }
            else 
            {

                if (count < 15)
                {
                    paintRect.Y = 125;
                    if (count == 0)
                    {
                        ironRect.Y -= 25;
                        heroRect.Y -= 25;
                    }
                    count++;
                     
                }
                else if (count == 15)
                {
                    ironRect.Y += 25;
                    heroRect.Y += 25;
                    count++;
                }
                else
                {
                    heroTexture = this.Content.Load<Texture2D>("herobrine");
                    ironTexture = this.Content.Load<Texture2D>("IronGolem");
                    heroRect.X--;
                    ironRect.X--;
                }
                
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(musuemTexture, museumRect, Color.White);

            if (count < 15)
            {
                spriteBatch.Draw(ironTexture, ironRect, Color.White);
                spriteBatch.Draw(heroTexture, heroRect, Color.White);
                spriteBatch.Draw(paintTexture, paintRect, Color.White);
            }
            else if (count < 150)
            {
                spriteBatch.Draw(paintTexture, paintRect, Color.White);
                spriteBatch.Draw(ironTexture, ironRect, Color.White);
                spriteBatch.Draw(heroTexture, heroRect, Color.White);
            }
            else
            {
                spriteBatch.Draw(paintTexture, paintRect, Color.White);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
