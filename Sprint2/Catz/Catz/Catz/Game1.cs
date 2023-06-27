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

namespace Catz
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle catRect = new Rectangle(100, 100, 100, 100);
        Rectangle mouseRect = new Rectangle(200, 200, 100, 100);
        Texture2D catPic;
        Texture2D mousePic;
        int timer = 0;
        int seconds;
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
            catPic = this.Content.Load<Texture2D>("cat");
            mousePic = this.Content.Load<Texture2D>("mouse");
            font = this.Content.Load<SpriteFont>("spriteFont1");
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
            seconds = timer / 60;
            KeyboardState kb = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kb.IsKeyDown(Keys.Escape))
                this.Exit();

            if (kb.IsKeyDown(Keys.Up))
                mouseRect.Y -= 2;
            if (kb.IsKeyDown(Keys.Down))
                mouseRect.Y += 2;
            if (kb.IsKeyDown(Keys.Left))
                mouseRect.X--;
            if (kb.IsKeyDown(Keys.Right))
                mouseRect.X++;

            if (kb.IsKeyDown(Keys.W))
                catRect.Y--;
            if (kb.IsKeyDown(Keys.A))
                catRect.X -= 2;
            if (kb.IsKeyDown(Keys.D))
                catRect.X += 2;
            if (kb.IsKeyDown(Keys.S))
                catRect.Y++; 

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
            spriteBatch.Draw(mousePic, mouseRect, Color.White);
            spriteBatch.Draw(catPic, catRect, Color.White);
            spriteBatch.DrawString(font, "" + seconds, new Vector2(300, 300), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
