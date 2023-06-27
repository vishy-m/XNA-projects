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

namespace Bigger
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState olfKb = Keyboard.GetState();
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            this.Window.AllowUserResizing = true;
            IsMouseVisible = true;
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
            KeyboardState kb = Keyboard.GetState();

            if (kb.IsKeyDown(Keys.D1) && !olfKb.IsKeyDown(Keys.D1))
            {
                graphics.PreferredBackBufferWidth = 400;
                graphics.PreferredBackBufferHeight = 200;
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();
            }
            else if (kb.IsKeyDown(Keys.D2) && !olfKb.IsKeyDown(Keys.D2))
            {
                graphics.PreferredBackBufferWidth = 600;
                graphics.PreferredBackBufferHeight = 400;
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();
            }
            else if (kb.IsKeyDown(Keys.D3) && !olfKb.IsKeyDown(Keys.D3))
            {
                graphics.IsFullScreen = true; 
                graphics.ApplyChanges();
            }
            if (kb.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            this.Window.AllowUserResizing = true;
            olfKb = kb;
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

            base.Draw(gameTime);
        }
    }
}
