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

namespace BreadCrumbs
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle[] rects = new Rectangle[11];
        Texture2D text;
        //Vector2 pos;
        Vector2[] velocities = new Vector2[11];
        KeyboardState oldKb = Keyboard.GetState();
        int index = 1;
        int timer = 0;
        int xv = 3;
        int yv = 2;
        int k = 1;

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
            rects[0] = new Rectangle(195, 195, 110, 110);
            velocities[0] = new Vector2(rects[0].X, rects[0].Y);
            //pos = new Vector2(rects[0].X, rects[0].Y);
            IsMouseVisible = true;
            for (int i = 1; i < rects.Length; i++)
            {
                
                velocities[i] = new Vector2(rects[0].X, rects[0].Y);
                rects[i] = new Rectangle((int)velocities[i].X, (int) velocities[i].Y, 110 - (10 * i), 110 - (10 * i));
            }
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
            text = this.Content.Load<Texture2D>("color");
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
            if (kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space))
                k *= -1;
            if (k == 1)
            {
                timer++;

                for (int i = 0; i < rects.Length; i++)
                {
                    velocities[i] = new Vector2(rects[i].X, rects[i].Y);
                }
                rects[0].X += xv;
                rects[0].Y += yv;
                
                if (timer > 10)
                {
                    for (int i = 1; i < rects.Length; i++)
                    {
                        rects[i].X = (int)velocities[i - 1].X;
                        rects[i].Y = (int)velocities[i - 1].Y;
                    }
                    timer = 0;
                }
                

                if (rects[0].X < 0 || rects[0].X + rects[0].Width > GraphicsDevice.Viewport.Width)
                    xv *= -1;
                if (rects[0].Y < 0 || rects[0].Y + rects[0].Width > GraphicsDevice.Viewport.Height)
                    yv *= -1;
            }

            
            
            oldKb = kb;
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
            for (int i = 1; i < rects.Length; i++)
            {
                spriteBatch.Draw(text, rects[i], new Color(Color.LightBlue.R, Color.LightBlue.G, Color.LightBlue.B, Color.LightBlue.A - 100));
            }
            spriteBatch.Draw(text, rects[0], Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
