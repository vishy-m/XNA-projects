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

namespace Huff_n_Puff
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle[] boy = new Rectangle[10];
        //Rectangle[] boyR = new Rectangle[5];
        Rectangle[] feathers = new Rectangle[4];
        int i = 0, f = 0;
        Rectangle dest = new Rectangle(250, 200, 100, 100);
        Rectangle feather = new Rectangle(225, 50, 50, 50);
        Texture2D spriteSheet;
        SpriteFont font;
        List<String> lines;
        int rTimer = 0, fTimer = 0, timer = 0, lTimer = 0, spaceTimer = 0, stringTimer = 0, gameTimer = 0;
        int set = 0;
        Boolean goingRight = false, goingLeft = false, spacePressed = false, featherPuffed = false, wind = false;
        int xI, yI, wI, hI, score = 0, speed, highScore;
        int num = 0, feaherSpeed = 1;
        
        
        Random random = new Random();
        KeyboardState oldKB = Keyboard.GetState();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 500;
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
            speed = 2;
            dest = boy[i];
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
            spriteSheet = this.Content.Load<Texture2D>("HuffNPuff");
            ReadIntegersInFile(@"Content/HuffNPuff.txt");
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
            if (set == 0)
            {
                dest.Y = 400;
                feather = new Rectangle(225, 50, 50, 50);
                dest.Height = 100;
                dest.Width = 100;
                feaherSpeed = 1;
                wind = false;
                score = 0;
                gameTimer = 0;
                set++;
            }
                
            KeyboardState kb = Keyboard.GetState();
            timer++;
            feather.Y+= feaherSpeed;
            fTimer++;
            gameTimer++;
            if (gameTimer % 1200 == 0)
            {
                feaherSpeed++;
                wind = true;
            }
            
            if (score >= highScore)
                highScore = score;
            if (fTimer % 20 == 0)
            {
                int rOrL = random.Next(2);
                if (rOrL == 0)
                    feather.X -= random.Next(50) + 1;
                else if (rOrL == 1)
                    feather.X += random.Next(50) + 1;
            }
            if (kb.IsKeyDown(Keys.Right))
            {
                dest.X+=speed;
                goingRight = true;        
            }
            else
                goingRight = false;
            if (kb.IsKeyDown(Keys.Left))
            {
                dest.X-=speed;
                goingLeft = true;
            }
            else
                goingLeft = false;
            if (kb.IsKeyDown(Keys.Right) && kb.IsKeyDown(Keys.Left))
                goingRight = goingLeft = false;
            if (kb.IsKeyDown(Keys.Space))
            {
                spacePressed = true;
            }

            if (kb.IsKeyDown(Keys.R) && !oldKB.IsKeyDown(Keys.R) || feather.Y + feather.Height >= GraphicsDevice.Viewport.Height)
            {
                set = 0;
            }

            if (goingRight && !goingLeft)
            {
                if (rTimer == 0)
                    i = 5;
                rTimer++;
                if (i < 9 && rTimer % 20 == 0)
                    i++;
                else if (i > 8)
                    i = 5;
                lTimer = 0;
            }
            if (goingLeft && !goingRight )
            {
                if (lTimer == 0)
                    i = 0;
                lTimer++;

                if (i < 4 && lTimer % 20 == 0)
                    i++;
                else if (i > 3)
                    i = 0;
                rTimer = 0;
            }
            if (!goingLeft && !goingRight)
            {
                if (lTimer == 0)
                    i = 5;
                else
                    i = 0;
            }
            if (spacePressed && goingLeft)
                i = 4;
            else if (spacePressed && goingRight || spacePressed && !goingRight && !goingLeft)
                i = 9;

            if (spacePressed)
            {
                spaceTimer++;
                if (feather.Intersects(dest))
                {
                    featherPuffed = true;
                    if (num == 0)
                    {
                        score++;
                        num++;
                    }
                }
                if (spaceTimer > 60)
                {
                    spacePressed = false;
                    num = 0;
                    spaceTimer = 0;
                }
            }
            if (featherPuffed)
            {
                if (feather.Y >= 50)
                {
                    feather.Y -= feaherSpeed + 1;
                }
                else
                    featherPuffed = false;
            }
            if (dest.X > GraphicsDevice.Viewport.Width)
            {
                dest.X = 0 - (dest.Width / 2);
            }
            if (dest.X < 0 - (dest.Width / 2))
            {
                dest.X = GraphicsDevice.Viewport.Width;
            }

            if (feather.X > GraphicsDevice.Viewport.Width - feather.Width)
            {
                feather.X = 0;
            }
            if (feather.X < 0)
            {
                feather.X = GraphicsDevice.Viewport.Width - feather.Width;
            }
            oldKB = kb; ;
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
            spriteBatch.Draw(spriteSheet, dest, boy[i], Color.White);
            spriteBatch.Draw(spriteSheet, feather, feathers[f], Color.White);
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(font, "High Score: " + highScore, new Vector2(0, 30), Color.White);
            spriteBatch.DrawString(font, "Press R to restart", new Vector2(0, 60), Color.White);
            if (wind)
            {
                stringTimer++;
                spriteBatch.DrawString(font, "Gravity increased", new Vector2(0, 90), Color.White);
                if (stringTimer > 240)
                {
                    wind = false;
                    stringTimer = 0;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void ReadIntegersInFile(string path)
        {
            
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    int c = 0;
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(' ');
                        if (c < 5)
                        {
                            xI = Convert.ToInt32(parts[6]);
                            yI = Convert.ToInt32(parts[7]);
                            wI = Convert.ToInt32(parts[8]);
                            hI = Convert.ToInt32(parts[9]);
                            boy[c] = new Rectangle(xI, yI, wI, hI);
                        }
                        else if (c < 10)
                        {
                            xI = Convert.ToInt32(parts[6]);
                            yI = Convert.ToInt32(parts[7]);
                            wI = Convert.ToInt32(parts[8]);
                            hI = Convert.ToInt32(parts[9]);
                            boy[c] = new Rectangle(xI, yI, wI, hI);
                        }
                        else if (c > 9)
                        {
                            xI = Convert.ToInt32(parts[3]);
                            yI = Convert.ToInt32(parts[4]);
                            wI = Convert.ToInt32(parts[5]);
                            hI = Convert.ToInt32(parts[6]);
                            feathers[c - 10] = new Rectangle(xI, yI, wI, hI);
                        }
                        
                        c++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("the file could not be read");
                Console.WriteLine(e.Message);
            }
        }
    }
}
