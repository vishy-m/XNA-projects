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

namespace Avatar
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle[] rects = new Rectangle[12];
        Texture2D[] textures = new Texture2D[12];
        Rectangle selectBoxR;
        Texture2D selectBoxT;
        int rectTrack = 0;
        Boolean startPressed = false;
        Rectangle bigRect = new Rectangle(0, 0, 500, 500);
        int timer = 0;


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
            for (int i = 0; i < 4; i++)
            {
                rects[i] = new Rectangle(i * 200, 50, 100, 100);
            }
            for (int i = 4; i < 8; i++)
            {
                rects[i] = new Rectangle((i - 4) * 200, 200, 100, 100);
            }
            for (int i = 8; i < 12; i++)
            {
                rects[i] = new Rectangle((i - 8) * 200, 350, 100, 100);
            }
            selectBoxR = rects[0];
            
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
            for (int i = 0; i < textures.Length; i++)
            {
                textures[i] = this.Content.Load<Texture2D>("i" + i);
            }
            selectBoxT = this.Content.Load<Texture2D>("white");
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
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                //this.Exit();

            // TODO: Add your update logic here
            GamePadState pad1 = GamePad.GetState(PlayerIndex.One);
            timer++;
            if (pad1.DPad.Left == ButtonState.Pressed && timer > 10)
            {
                if (selectBoxR == rects[0])
                {
                    rectTrack = 11;
                    selectBoxR = rects[rectTrack];
                    
                }
                else
                {
                    rectTrack--;
                    selectBoxR = rects[rectTrack];
                }
                timer = 0;
            }
            else if (pad1.DPad.Right == ButtonState.Pressed && timer > 10)
            {
                if (selectBoxR == rects[11])
                {
                    rectTrack = 0;
                    selectBoxR = rects[rectTrack];
                }
                else
                {
                    rectTrack++;
                    selectBoxR = rects[rectTrack];
                    
                }
                timer = 0;

            }
            else if (pad1.Buttons.Start == ButtonState.Pressed && timer > 10)
            {

                startPressed = true;
                timer = 0;
            }
            else if (pad1.Buttons.Back == ButtonState.Pressed && timer > 10)
            {
                startPressed = false;
                selectBoxR = rects[rectTrack];
                timer = 0;
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
            
            if (!startPressed)
            {
                spriteBatch.Draw(selectBoxT, selectBoxR, Color.Black);
                for (int i = 0; i < rects.Length; i++)
                {
                    spriteBatch.Draw(textures[i], rects[i], Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(textures[rectTrack], bigRect, Color.White);
            }
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
