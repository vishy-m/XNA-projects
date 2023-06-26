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

namespace Mavs
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle netRect;
        Rectangle ballRect;
        Rectangle courtRect;
        Texture2D netTexture;
        Texture2D ballTexture;
        Texture2D courtTexture;
        SpriteFont spriteFont;
        int timer;
        String time;
        double xVelocity;
        double yVelocity;
        double gravity;
        double xT;
        double yT;
        int seconds;
        int timer2;
        float alpha;

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
            //courtRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            ballRect = new Rectangle(0, 300, 50, 50);
            netRect = new Rectangle(345, 160, 150, 150);
            timer = 0;
            time = "";
            alpha = MathHelper.ToRadians(45f);
            timer2 = 0;
            gravity = 9.8;
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
            netTexture = this.Content.Load<Texture2D>("hoops");
            ballTexture = this.Content.Load<Texture2D>("Basketball");
            //courtTexture = this.Content.Load<Texture2D>("basketballCourt");
            spriteFont = this.Content.Load<SpriteFont>("SpriteFont1");
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

            if (seconds < 1)
                time = "5";
            else if (seconds < 2)
                time = "4";
            else if (seconds < 3)
                time = "3";
            else if (seconds < 4)
                time = "2";
            else if (seconds < 5)
                time = "1";
            else if (seconds < 6)
                time = "0";
            else
            {
                time = "";
                timer2++;
                seconds = timer2 / 30;
                xVelocity = 70 * Math.Cos(alpha);
                yVelocity = 70 * Math.Sin(alpha);

                
                //xT = ballRect.X * seconds;
                //yT = ballRect.Y * seconds;
                xT = xVelocity * seconds;

                yT = yVelocity * seconds - gravity * Math.Pow(seconds, 2) / 2;
                ballRect.X = (int)xT;
                ballRect.Y = -((int)yT) + 300;
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
            //spriteBatch.Draw(courtTexture, courtRect, Color.White);
            spriteBatch.Draw(netTexture, netRect, Color.White);
            spriteBatch.Draw(ballTexture, ballRect, Color.White);
            
            spriteBatch.DrawString(spriteFont, time, new Vector2(200, 150), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
