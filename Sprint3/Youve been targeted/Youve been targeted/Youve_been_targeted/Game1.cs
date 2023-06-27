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

namespace Youve_been_targeted
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle tankR = new Rectangle(250, 250, 100, 100), bulletR = new Rectangle(225, 225, 25, 25), trigger;
        Texture2D tank, bullet;
        float rotation = 0;
        int dx = 0, dy = 0, updates = 0, xpos = 0, ypos = 0, mx = 0, my = 0;
        Boolean fired = false;
        double hyp = 0;
        MouseState oldMouse = Mouse.GetState();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 500;
            graphics.PreferredBackBufferWidth = 500;
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
            IsMouseVisible = true;
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
            tank = this.Content.Load<Texture2D>("Tank");
            bullet = this.Content.Load<Texture2D>("White Square");
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
            MouseState mouse = Mouse.GetState();
            int xDiff = mouse.X - tankR.X;
            int yDiff = mouse.Y - tankR.Y;
            float deg = (float)Math.Atan2(yDiff, xDiff);
            rotation = deg + MathHelper.ToRadians(90);
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released && ! fired)
            {
                dx = mouse.X - bulletR.X;
                dy = mouse.Y - bulletR.Y;
                hyp = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                updates = (int)hyp / 10;
                xpos = dx / updates;
                ypos = dy / updates;
                mx = mouse.X;
                my = mouse.Y;
                trigger = new Rectangle(mouse.X - 25, mouse.Y - 25, 50, 50);
                fired = true;
            }
            if (fired)
            {
                bulletR.X += xpos;
                bulletR.Y += ypos;
            }
            if (isOverlapping(bulletR, trigger))
            {
                fired = false;
                bulletR.X = 225;
                bulletR.Y = 225;
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
            spriteBatch.Draw(bullet, bulletR, Color.Red);
            spriteBatch.Draw(tank, tankR, null, Color.White, rotation, new Vector2(tankR.X / 2, tankR.Y / 2), SpriteEffects.None, 0);
            //spriteBatch.Draw()
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
