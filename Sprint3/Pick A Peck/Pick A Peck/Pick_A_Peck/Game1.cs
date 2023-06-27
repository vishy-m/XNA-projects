using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pick_A_Peck
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        List<string> lines;

        int xI, yI, wI, hI;
        int timer = 0;
        int newWidth = 0;
        int newHeight = 0;
        int screenHeight;
        int screenWidth;
        int place = 0;

        Rectangle[] rects = new Rectangle[5];
        Rectangle[] dests = new Rectangle[5];
        Rectangle[] bigRects = new Rectangle[5];

        Vector2[] vectors = new Vector2[5];

        //Rectangle sprite = new Rectangle(200, 200, 100, 100);

        Texture2D box;

        KeyboardState oldKB = Keyboard.GetState();
        Boolean numTyped = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
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
            lines = new List<string>();

            screenHeight = GraphicsDevice.Viewport.Height;
            screenWidth = GraphicsDevice.Viewport.Width;
            
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
            font = this.Content.Load<SpriteFont>("SpriteFont1");
            box = this.Content.Load<Texture2D>("SpriteSheet");
            ReadIntegersOfFile(@"Content/rData.txt");
            

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

            if (timer == 0)
            {
                dests[0] = new Rectangle(0, 0, rects[0].Width, rects[0].Height);
                for (int k = 1; k < rects.Length; k++)
                {
                    dests[k] = new Rectangle((dests[k - 1].X + dests[k - 1].Width) + 5, 0, rects[k].Width, rects[k].Height);
                }
                for (int k = 0; k < rects.Length; k++)
                {
                    bigRects[k] = new Rectangle((screenWidth / 2) - rects[k].Width, (screenHeight / 2) - rects[k].Height, rects[k].Width * 2, rects[k].Height * 2);
                }
                timer++;
            }

            if (kb.IsKeyDown(Keys.D1) && ! oldKB.IsKeyDown(Keys.D1))
            {
                place = 0;
                numTyped = true;
            }
            if (kb.IsKeyDown(Keys.D2) && !oldKB.IsKeyDown(Keys.D2))
            {
                place = 1;
                numTyped = true;
            }
            if (kb.IsKeyDown(Keys.D3) && !oldKB.IsKeyDown(Keys.D3))
            {
                place = 2;
                numTyped = true;
            }
            if (kb.IsKeyDown(Keys.D4) && !oldKB.IsKeyDown(Keys.D4))
            {
                place = 3;
                numTyped = true;
            }
            if (kb.IsKeyDown(Keys.D5) && !oldKB.IsKeyDown(Keys.D5))
            {
                place = 4;
                numTyped = true;
            }

            if (kb.IsKeyDown(Keys.R) && !oldKB.IsKeyDown(Keys.R))
            {
                numTyped = false;
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
            if (!numTyped)
                for (int r = 0; r < rects.Length; r++)
                {
                    spriteBatch.Draw(box, dests[r], rects[r], Color.White);
                    DrawNums();
                }
                    
            else
            {
                spriteBatch.Draw(box, bigRects[place], rects[place], Color.White);
                spriteBatch.DrawString(font, "Press R to return", new Vector2(0, 0), Color.Black);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void DrawNums()
        {
            
            for (int i = 0; i < rects.Length; i++)
            {
                Vector2 position = new Vector2(dests[i].X + (rects[i].Width / 2) - (rects[i].Width / 4), dests[i].Y + dests[i].Height + 5);
                spriteBatch.DrawString(font, "#" + (i + 1), position, Color.Black);
            }
                
        }
        private void ReadIntegersOfFile(string path)
        {
            try
            {
                int i = 0;
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(' ');
                        xI = Convert.ToInt32(parts[2]);
                        yI = Convert.ToInt32(parts[3]);
                        wI = Convert.ToInt32(parts[4]);
                        hI = Convert.ToInt32(parts[5]);
                        rects[i] = new Rectangle(xI, yI, wI, hI);
                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read");
                Console.WriteLine(e.Message);
            }
        }
    }
}
