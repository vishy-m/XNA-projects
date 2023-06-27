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

namespace RocketMan
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle shipR = new Rectangle(100, 100, 100, 100);
        Texture2D shipT;
        int velocity = 1;
        int time = 0;
        int degrees = 0;
        Boolean isGoingX = false;
        Boolean isGoingY = false;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            shipT = this.Content.Load<Texture2D>("ship");
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
            time++;
            GamePadState pad1 = GamePad.GetState(PlayerIndex.One);
            KeyboardState kb = Keyboard.GetState();
            //if (kb.IsKeyDown(Keys.Right))
            //{
            //    shipR.X += velocity;
            //    isGoingX = true;
            //    if (!isGoingY)
            //    {
            //        degrees = 90;
            //    }
            //}
            //else if (kb.IsKeyDown(Keys.Left))
            //{
            //    shipR.X -= velocity;
            //    isGoingX = true;
            //    if (!isGoingY)
            //    {
            //        degrees = 270;
            //    }
            //}
            //else
            //    isGoingX = false;
            //if (kb.IsKeyDown(Keys.Down))
            //{
            //    shipR.Y += velocity;
            //    isGoingY = true;
            //    if (!isGoingX)
            //    {
            //        degrees = 180;
            //    }
            //}
            //else if (kb.IsKeyDown(Keys.Up))
            //{
            //    shipR.Y -= velocity;
            //    isGoingY = true;
            //    if (!isGoingX)
            //    {
            //        degrees = 0;
            //    }
            //}
            //else
            //    isGoingY = false;

            //if (kb.IsKeyDown(Keys.W) && time > 10)
            //{
            //    velocity++;
            //    time = 0;
            //}
            //else if (kb.IsKeyDown(Keys.S) && time > 10)
            //{
            //    velocity--;
            //    time = 0;
            //}


            if (pad1.ThumbSticks.Right.X > 0)
            {
                shipR.X += velocity;
                isGoingX = true;
                if (!isGoingY)
                {
                    degrees = 90;
                }
            }
            else if (pad1.ThumbSticks.Right.X < 0)
            {
                shipR.X -= velocity;
                isGoingX = true;
                if (!isGoingY)
                {
                    degrees = 270;
                }
            }
            else
                isGoingX = false;
            if (pad1.ThumbSticks.Right.Y > 0)
            {
                shipR.Y += velocity;
                isGoingY = true;
                if (!isGoingX)
                {
                    degrees = 180;
                }
            }
            else if (pad1.ThumbSticks.Right.Y < 0)
            {
                shipR.Y -= velocity;
                isGoingY = true;
                if (!isGoingX)
                {
                    degrees = 0;
                }
            }
            else
                isGoingY = false;

            if (pad1.ThumbSticks.Left.Y < 0 && time > 10)
            {
                velocity++;
                time = 0;
            }
            else if (pad1.ThumbSticks.Left.Y > 0 && time > 10)
            {
                velocity--;
                time = 0;
            }
            if (velocity < 0)
                velocity--;
            if (isGoingY && isGoingX)
            {
                if (pad1.ThumbSticks.Right.X > 0 && pad1.ThumbSticks.Left.Y > 0)
                    degrees = 135;
                if (pad1.ThumbSticks.Right.X < 0 && pad1.ThumbSticks.Left.Y > 0)
                    degrees = 225;
                if (pad1.ThumbSticks.Right.X > 0 && pad1.ThumbSticks.Left.Y < 0)
                    degrees = 315;
                if (pad1.ThumbSticks.Right.X < 0 && pad1.ThumbSticks.Left.Y < 0)
                    degrees = 45;
            }


            if (shipR.X < 0)
                shipR.X = GraphicsDevice.Viewport.Width;
            if (shipR.X > GraphicsDevice.Viewport.Width)
                shipR.X = 0;
            if (shipR.Y < 0)
                shipR.Y = GraphicsDevice.Viewport.Height;
            if (shipR.Y > GraphicsDevice.Viewport.Height)
                shipR.Y = 0;
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
            spriteBatch.Draw(shipT, shipR, Color.White);
            spriteBatch.DrawString(font, "Spaceship velocity: " + velocity, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, "Ship heading: " + degrees + " degrees", new Vector2(0, 30), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
