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

namespace School_home_School
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D redTexture;
        Texture2D blueTexture;
        Texture2D goldTexture;
        Texture2D homeTexture;
        Texture2D schoolTexture;
        Texture2D schoolSTexture;
        Rectangle homeRect;
        Rectangle schoolRect;
        Rectangle schoolSRect;
        Rectangle redRect;
        Rectangle blueRect;
        Rectangle goldRect;
        Rectangle leftR;
        Rectangle middleR;
        Rectangle rightR;
        int recTimer;
        int imageTimer;
        Rectangle[] locations;
        SpriteFont font;
        Vector2 schoolV;
        Vector2 homeV;
        Vector2 schoolSV;


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
            blueRect = new Rectangle(75, 100, 150, 150);
            redRect = new Rectangle(325, 250, 150, 150);
            goldRect = new Rectangle(575, 100, 150, 150);

            leftR = new Rectangle(blueRect.X, blueRect.Y, 150, 150);
            middleR = new Rectangle(redRect.X, redRect.Y, 150, 150);
            rightR = new Rectangle(goldRect.X, goldRect.Y, 150, 150);

            schoolRect = new Rectangle(leftR.X + 10, leftR.Y + 10, 130, 130);
            schoolSRect = new Rectangle(rightR.X + 10, rightR.Y + 10, 130, 130);
            homeRect = new Rectangle(middleR.X + 10, middleR.Y + 10, 130, 130);

            schoolV = new Vector2(leftR.X, leftR.Y + 150);
            homeV = new Vector2(middleR.X, middleR.Y + 150);
            schoolSV = new Vector2(rightR.X, rightR.Y + 150);


            recTimer = 0;
            imageTimer = 0;
            locations = new Rectangle[3];
            locations[0] = leftR;
            locations[1] = middleR;
            locations[2] = rightR;
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
            redTexture = new Texture2D(GraphicsDevice, 1, 1);
            redTexture.SetData(new[] { Color.Red });
            blueTexture = new Texture2D(GraphicsDevice, 1, 1);
            blueTexture.SetData(new[] { Color.Blue });
            goldTexture = new Texture2D(GraphicsDevice, 1, 1);
            goldTexture.SetData(new[] { Color.Gold });

            schoolTexture = this.Content.Load<Texture2D>("school");
            schoolSTexture = this.Content.Load<Texture2D>("schoolSlide");
            homeTexture = this.Content.Load<Texture2D>("home");
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
            imageTimer++;
            recTimer++;
            int iSeconds = imageTimer / 60;
            int rSeconds = recTimer / 60;
            if (rSeconds < 4)
            {
                blueRect = locations[0];
                redRect = locations[1];
                goldRect = locations[2];
            }
            else if (rSeconds < 8)
             { 
                blueRect = locations[1];
                redRect = locations[2];
                goldRect = locations[0];
            }
            else if (rSeconds < 12)
            {
                blueRect = locations[2];
                redRect = locations[0];
                goldRect = locations[1];
            }
            else 
                recTimer = 0;

            if (iSeconds < 7)
            {
                schoolRect.X = locations[0].X + 10;
                schoolRect.Y = locations[0].Y + 10;
                homeRect.X = locations[1].X + 10;
                homeRect.Y = locations[1].Y + 10;
                schoolSRect.X = locations[2].X + 10;
                schoolSRect.Y = locations[2].Y + 10;

                schoolV = new Vector2(leftR.X, leftR.Y + 150);
                homeV = new Vector2(middleR.X, middleR.Y + 150);
                schoolSV = new Vector2(rightR.X, rightR.Y + 150);

            }
            else if (iSeconds < 14)
            {
                schoolRect.X = locations[2].X + 10;
                schoolRect.Y = locations[2].Y + 10;
                homeRect.X = locations[0].X + 10;
                homeRect.Y = locations[0].Y + 10;
                schoolSRect.X = locations[1].X + 10;
                schoolSRect.Y = locations[1].Y + 10;

                schoolV = new Vector2(rightR.X, rightR.Y + 150);
                homeV = new Vector2(leftR.X, leftR.Y + 150);
                schoolSV = new Vector2(middleR.X, middleR.Y + 150);
            }
            else if (iSeconds < 21)
            {
                schoolRect.X = locations[1].X + 10;
                schoolRect.Y = locations[1].Y + 10;
                homeRect.X = locations[2].X + 10;
                homeRect.Y = locations[2].Y + 10;
                schoolSRect.X = locations[0].X + 10;
                schoolSRect.Y = locations[0].Y + 10;

                schoolV = new Vector2(middleR.X, middleR.Y + 150);
                homeV = new Vector2(rightR.X, rightR.Y + 150);
                schoolSV = new Vector2(leftR.X, leftR.Y + 150);
            }
            else
                imageTimer = 0;
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
            spriteBatch.Draw(redTexture, redRect, Color.Red);
            spriteBatch.Draw(blueTexture, blueRect, Color.Blue);
            spriteBatch.Draw(goldTexture, goldRect, Color.Gold);
            spriteBatch.Draw(schoolTexture, schoolRect, Color.White);
            spriteBatch.Draw(schoolSTexture, schoolSRect, Color.White);
            spriteBatch.Draw(homeTexture, homeRect, Color.White);
            spriteBatch.DrawString(font,  "Classroom \nslide", schoolV, Color.White);
            spriteBatch.DrawString(font, "Home \nslide", homeV, Color.White);
            spriteBatch.DrawString(font, "School \nslide", schoolSV, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
