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

namespace Rock_and_Hard_Place
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle placeR = new Rectangle(300, 300, 250, 250), rockR = new Rectangle(0, 0, 100, 100);
        Texture2D place, rock;
        Color color = Color.White;
        KeyboardState oldKb = Keyboard.GetState();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 850;
            graphics.PreferredBackBufferHeight = 850;
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
        /// LoadContent will be called once per game and is the place to]
        /// load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            rock = this.Content.Load<Texture2D>("Rock");
            place = this.Content.Load<Texture2D>("Hard Place");
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
            if (kb.IsKeyDown(Keys.Right))
                rockR.X += 10;
            if (kb.IsKeyDown(Keys.Left))
                rockR.X -= 10;
            if (kb.IsKeyDown(Keys.Up))
                rockR.Y -= 10;
            if (kb.IsKeyDown(Keys.Down))
                rockR.Y += 10;
            // stuck in box, goes to one edge, comes out of other.
            int rightEdge = GraphicsDevice.Viewport.Width;
            int downEdge = GraphicsDevice.Viewport.Height;
            if (rockR.X < -100)
                rockR.X = 500;
            else if (rockR.X > rightEdge)
                rockR.X = -100;
            else if (rockR.Y < -100)
                rockR.Y = 500;
            else if (rockR.Y > downEdge)
                rockR.Y = -100;
            if (isOverlapping(rockR, placeR))
                color = Color.Red;
            else
                color = Color.White;
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
            spriteBatch.Draw(place, placeR, Color.White);
            spriteBatch.Draw(rock, rockR, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public Boolean isOverlapping(Rectangle rec1, Rectangle rec2)
        {
            int rockRight = rec1.X + rec1.Width;
            int rockLeft = rec1.X;
            int rockTop = rec1.Y;
            int rockBottom = rec1.Y + rec1.Height;

            int placeRight = rec2.X + rec2.Width;
            int placeLeft = rec2.X;
            int placeTop = rec2.Y;
            int placeBottom = rec2.Y + rec2.Height;

            if (rockRight >= placeLeft && rockLeft <= placeRight && rockBottom >= placeTop && rockTop <= placeBottom)
                return true;
            return false;
        }
    }
}
