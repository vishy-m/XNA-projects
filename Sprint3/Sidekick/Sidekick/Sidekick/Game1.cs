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

namespace Sidekick
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle smallR = new Rectangle(0, 275, 50, 50), bigR = new Rectangle(0, 50, 150, 100), stationaryR = new Rectangle(250, 250, 100, 100);
        Texture2D small, big, stationary;
        Color color = Color.White;
        Color color2 = Color.White;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 600;
            graphics.PreferredBackBufferHeight = 600;
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
            small = this.Content.Load<Texture2D>("SmallMovingObj");
            big = this.Content.Load<Texture2D>("BigMovingObj");
            stationary = this.Content.Load<Texture2D>("StationaryObj");
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
            smallR.X+=2;
            bigR.X+=2;
            if (smallR.X > 800)
                smallR.X = -75;
            if (bigR.X > 800)
                bigR.X = -175;

            if (shouldTurnRed(smallR, stationaryR))
                color = Color.Red;
            else
                color = Color.White;
            if (shouldTurnRed(bigR, stationaryR))
                color2 = Color.Red;
            else
                color2 = Color.White;
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
            spriteBatch.Draw(stationary, stationaryR, Color.White);
            spriteBatch.Draw(big, bigR, color2);
            spriteBatch.Draw(small, smallR, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Boolean shouldTurnRed(Rectangle rec1, Rectangle rec2)
        {
            if (rec1.X + rec1.Width> rec2.X && rec1.X < rec2.X + rec2.Width && rec1.Y >= rec2.Y && rec1.Y < rec2.Y + rec2.Height)
                return true;
            return false;
        }
    }
}
