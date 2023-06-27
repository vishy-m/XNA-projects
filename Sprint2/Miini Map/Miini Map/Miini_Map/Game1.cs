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

namespace Miini_Map
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle miniR = new Rectangle(0, 0, 100, 100);
        Rectangle bigR = new Rectangle(-250, -250, 1000, 1000);
        Texture2D mapT;
        Rectangle playerR = new Rectangle(250, 250, 100, 100);
        Rectangle miniPR = new Rectangle(50, 50, 10, 10);
        Rectangle biggestR = new Rectangle(0, 0, 10000, 10000);
        Texture2D player;
        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
            
            this.Window.AllowUserResizing = true;
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
            player = this.Content.Load<Texture2D>("white");
            mapT = this.Content.Load<Texture2D>("map");
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
            GamePadState pad1 = GamePad.GetState(PlayerIndex.One);
            KeyboardState kb = Keyboard.GetState();
            //if (pad1.ThumbSticks.Left.X > 0)
            //{
            //    miniPR.X += 1;
            //    playerR.X += 10;
            //}
            //else if (pad1.ThumbSticks.Left.X < 0)
            //{
            //    miniPR.X -= 1;
            //    playerR.X -= 10;
            //}
            //if (pad1.ThumbSticks.Left.Y > 0)
            //{
            //    miniPR.Y += 1;
            //    playerR.Y += 10;
            //}
            //else if (pad1.ThumbSticks.Left.Y < 0)
            //{
            //    miniPR.X -= 1;
            //    playerR.X -= 10;
            //}

            if (kb.IsKeyDown(Keys.Right))
            {
                miniPR.X++;
                bigR.X -= 10;
            }
            else if (kb.IsKeyDown(Keys.Left))
            {
                miniPR.X--;
                bigR.X += 10;
            }
            if (kb.IsKeyDown(Keys.Down))
            {
                miniPR.Y++;
                bigR.Y -= 10;
            }
            else if (kb.IsKeyDown(Keys.Up))
            {
                miniPR.Y--;
                bigR.Y += 10;
            }

            if (bigR.X < -650)
            {
                bigR.X += 10;
                miniPR.X--;
            }
            else if (bigR.X > 250)
            {
                bigR.X -= 10;
                miniPR.X++;
            }
            else if (bigR.Y < -650)
            {
                bigR.Y += 10;
                miniPR.Y--;
            }
            else if (bigR.Y > 250)
            {
                bigR.Y -= 10;
                miniPR.Y++;
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
            spriteBatch.Draw(player, biggestR, Color.DarkGreen);
            spriteBatch.Draw(mapT, bigR, Color.White);
            spriteBatch.Draw(mapT, miniR, Color.White);
            spriteBatch.Draw(player, playerR, Color.Red);
            spriteBatch.Draw(player, miniPR, Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
