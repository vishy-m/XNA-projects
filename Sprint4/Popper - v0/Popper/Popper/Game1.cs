using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Popper
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle window;
        Texture2D unpoppedTex;
        Texture2D poppedTex;

        List<Rectangle> kernels;
        List<Vector2> velocities;
        List<Texture2D> images;


        int x;
        int y;
        int gameTimer;

        List<int> timers;

        Random random = new Random();
        
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
            int screenWidth = graphics.GraphicsDevice.Viewport.Width;
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;
            window = new Rectangle(0, 0, screenWidth, screenHeight);

            kernels = new List<Rectangle>();
            velocities = new List<Vector2>();
            images = new List<Texture2D>();
            timers = new List<int>();

            gameTimer = 1;

            kernels.Add(new Rectangle(70, 50, 15, 15));
            velocities.Add(new Vector2(2, 3));
            timers.Add(0);

            kernels.Add(new Rectangle(70, 110, 15, 15));
            velocities.Add(new Vector2(2, 3));
            timers.Add(0);

            
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
            unpoppedTex = Content.Load<Texture2D>("unpopped");
            poppedTex = Content.Load<Texture2D>("popped");

            images.Add(unpoppedTex);
            images.Add(unpoppedTex);
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
            gameTimer++;
            int seconds = gameTimer / 60;
            
            for (int i = 0; i < kernels.Count; i++)
            {
                x = kernels[i].X + (int)velocities[i].X;
                y = kernels[i].Y + (int)velocities[i].Y;
                kernels[i] = new Rectangle(x, y, kernels[i].Width, kernels[i].Height);
                if (kernels[i].Y + kernels[i].Height >= window.Bottom || kernels[i].Y <= window.Top)
                {
                    velocities[i] = new Vector2(velocities[i].X, velocities[i].Y * -1);
                }
                if (kernels[i].X + kernels[i].Width >= window.Right || kernels[i].X <= window.Left)
                {
                    velocities[i] = new Vector2(velocities[i].X * -1, velocities[i].Y);
                }

                for (int r = 0; r < kernels.Count; r++)
                {
                    if (kernels[i] == kernels[r])
                        continue;
                    if (kernels[i].Intersects(kernels[r]) && timers[r] == 0 && timers[i] == 0)
                    {
                        timers[r] = 45;
                        timers[i] = 45;
                        images[i] = poppedTex;
                        images[r] = poppedTex;
                    }
                }
            }

            for (int k = kernels.Count - 1; k > -1; k--)
            {
                if (timers[k] == 1)
                {
                    kernels.RemoveAt(k);
                    timers.RemoveAt(k);
                    velocities.RemoveAt(k);
                    images.RemoveAt(k);
                }
                else if (timers[k] > 1)
                    timers[k]--;  
            }
            if (seconds > random.Next(2, 4))
            {
                kernels.Add(new Rectangle(random.Next(window.Left + 100, window.Right - 100), random.Next(window.Top + 100, window.Bottom - 100), 15, 15));
                int xv;
                int yv;
                do
                {
                    xv = random.Next(-3, 3);
                    yv = random.Next(-3, 3);
                }
                while (xv == 0 || yv == 0);

                velocities.Add(new Vector2(xv, yv));
                timers.Add(0);
                images.Add(unpoppedTex);
                gameTimer = 0;

            }



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Beige);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            for (int i = 0; i < kernels.Count; i++)
            {
                spriteBatch.Draw(images[i], kernels[i], Color.White);
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
