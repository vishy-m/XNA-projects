using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        Texture2D ballTex;
        Texture2D spriteSheet;
        
        int ballSpeedX;
        int ballSpeedY;
        int x, y, w, h;
        int screenWidth, screenHeight;
        int paddleSpeed;
        int leftGamesWon, rightGamesWon;
        int leftScore, rightScore;
        int resetTimer;
        int maxSpeed, minSpeed;
        int maxSpin, minSpin;
        int spinAmount, spinTimer;
        int newMaxSpin = 2, newMinSpin = -2;

        float spin;

        Rectangle ballRect;
        Rectangle top;
        Rectangle bottom;
        Rectangle left;
        Rectangle right;

        Rectangle[] sprites = new Rectangle[7];
        Rectangle[] dests = new Rectangle[3];
        Rectangle[] wSquares;

        KeyboardState oldKB;

        Boolean onPaddle;
        Boolean hitLeft;
        Boolean hitRight;

        Vector2 leftGamesWonPosition;
        Vector2 rightGamesWonPosition;
        Vector2 leftScorePosition;
        Vector2 rightScorePosition;

        Random random;

        Color lColor;
        Color rColor;

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
            screenWidth = graphics.GraphicsDevice.Viewport.Width;
            screenHeight = graphics.GraphicsDevice.Viewport.Height;

            top = new Rectangle(0, 0, screenWidth, 1);
            bottom = new Rectangle(0, screenHeight - 1, screenWidth, 1);
            left = new Rectangle(0, 0, 1, screenHeight);
            right = new Rectangle(screenWidth - 1, 0, 1, screenHeight);
            ballRect = new Rectangle(50, 50, 20, 20);

            ballSpeedX = 7;
            ballSpeedY = 8;
            paddleSpeed = 4;
            leftScore = 0;
            rightScore = 0;
            resetTimer = 0;
            maxSpeed = 8;
            minSpeed = 4;
            rightGamesWon = 0;
            leftGamesWon = 0;
            maxSpin = 18;
            minSpin = -18;
            
            spinTimer = 0;

            dests[0] = new Rectangle(0, 0, screenWidth, screenHeight);
            dests[1] = new Rectangle(50, (screenHeight / 2) - 64, 32, 128);
            dests[2] = new Rectangle(screenWidth - 50, (screenHeight / 2) - 64, 32, 128);
            

            wSquares = new Rectangle[screenHeight / 20];

            int yPos = 2;
            for (int i = 0; i < wSquares.Length; i++)
            {
                wSquares[i] = new Rectangle((screenWidth / 2) - 5, yPos, 10, 6);
                yPos += 20;
            }

            oldKB = Keyboard.GetState();

            onPaddle = false;

            hitLeft = false;
            hitRight = false;

            leftGamesWonPosition = new Vector2((screenWidth / 2) - 95, 400);
            rightGamesWonPosition = new Vector2((screenWidth / 2) + 50, 400);
            leftScorePosition = new Vector2((screenWidth / 2) - 95, 0);
            rightScorePosition = new Vector2((screenWidth / 2) + 50, 0);

            random = new Random();

            lColor = Color.CornflowerBlue;
            rColor = Color.LightGreen;

            spin = 0f;

            spinAmount = random.Next(maxSpin - minSpin) - minSpin;

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
            ballTex = Content.Load<Texture2D>("orange ping pong ball");
            font = this.Content.Load<SpriteFont>("SpriteFont1");
            spriteSheet = this.Content.Load<Texture2D>("spriteSheet");
            ReadFileAsInt(@"Content/spriteInfo.txt");
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
            ballRect.X += ballSpeedX;
            ballRect.Y += ballSpeedY;
            spin += MathHelper.ToRadians(spinAmount);

            if (spin > MathHelper.ToRadians(1080) || spin < MathHelper.ToRadians(-1080))
                spin = 0;

            if (ballRect.Intersects(top))
            {
                ballSpeedY *= -1;
                int spinA = random.Next(newMaxSpin - newMinSpin) - newMinSpin;
                if (spinA < 0)
                {
                    if (ballSpeedX + spinA > minSpeed)
                        ballSpeedX += spinA;
                }
                else
                {
                    if (ballSpeedX + spinA < maxSpeed)
                        ballSpeedX += spinA;
                }
            }
            if (ballRect.Intersects(bottom))
            {
                ballSpeedY *= -1;
                //ballRect.X += spinAmount;
                //spinAmount = random.Next(maxSpin - minSpin) - minSpin;
                int spinA = random.Next(newMaxSpin - newMinSpin) - newMinSpin;
                if (spinA < 0)
                {
                    if (ballSpeedX + spinA > minSpeed)
                        ballSpeedX += spinA;
                }
                else
                {
                    if (ballSpeedX + spinA < maxSpeed)
                        ballSpeedX += spinA;
                }
            }
            if (ballRect.Intersects(left) || ballRect.Intersects(dests[1]) && !onPaddle)
            {
                ballSpeedX *= -1;
                if (ballRect.Intersects(left))
                {
                    hitLeft = true;
                }
                //int spinA = random.Next(newMaxSpin - newMinSpin) - newMinSpin;
                //if (spinA < 0)
                //{
                //    if (ballSpeedY + spinA > minSpeed)
                //        ballSpeedY += spinA;
                //}
                //else
                //{
                //    if (ballSpeedY + spinA < maxSpeed)
                //        ballSpeedY += spinA;
                //}

                //ballRect.Y += spinAmount;
                //spinAmount = random.Next(maxSpin - minSpin) - minSpin;
            }
            if (ballRect.Intersects(right) || ballRect.Intersects(dests[2]) && !onPaddle)
            {
                ballSpeedX *= -1;
                if (ballRect.Intersects(right))
                {
                    hitRight = true;
                }
                //int spinA = random.Next(newMaxSpin - newMinSpin) - newMinSpin;
                //if (spinA < 0)
                //{
                //    if (ballSpeedY + spinA > minSpeed)
                //        ballSpeedY += spinA;
                //}
                //else
                //{
                //    if (ballSpeedY + spinA < maxSpeed)
                //        ballSpeedY += spinA;
                //}
                //spinAmount = random.Next(maxSpin - minSpin) - minSpin;
                //ballRect.Y += spinAmount;
                //spinAmount = random.Next(maxSpin - minSpin) - minSpin;
            }
            


            onPaddle = (ballRect.Intersects(dests[1]) || ballRect.Intersects(dests[2]));

            if (hitLeft || hitRight)
            {
                if (resetTimer == 0)
                {
                    if (hitLeft)
                        rightScore++;
                    if (hitRight)
                        leftScore++;
                    ballRect.X = (screenWidth / 2) - (ballRect.Width / 2);
                    ballRect.Y = (screenHeight / 2) - (ballRect.Height / 2);
                    //spin = 0;
                }

                resetTimer++;

                if (resetTimer > 120)
                {
                    random = new Random();
                    resetTimer = 0;
                    int ballSpeed = random.Next(maxSpeed - minSpeed) + minSpeed;
                    ballSpeedX = ballSpeed;
                    ballSpeed = random.Next(maxSpeed - minSpeed) + minSpeed;
                    ballSpeedY = ballSpeed;
                    hitLeft = false;
                    hitRight = false;
                }
                else
                {
                    ballSpeedX = 0;
                    ballSpeedY = 0;
                    spin = 0;
                }
            }

            if (rightScore == 11 && rightScore - 2 >= leftScore)
            {
                rightScore = 0;
                leftScore = 0;
                rightGamesWon++;
            }
            else if(leftScore == 11 && leftScore - 2 >= rightScore)
            {
                leftGamesWon++;
                rightScore = 0;
                leftScore = 0;
            }
            else
            {
                if (leftScore - 2 == rightScore && leftScore > 9 && rightScore > 9)
                {
                    leftGamesWon++;
                    rightScore = 0;
                    leftScore = 0;
                }
                else if (rightScore - 2 == leftScore && leftScore > 9 && rightScore > 9)
                {
                    rightGamesWon++;
                    rightScore = 0;
                    leftScore = 0;
                }
            }

            if (kb.IsKeyDown(Keys.W) && dests[1].Y > 0)
            {
                dests[1].Y -= paddleSpeed;
            }
            if (kb.IsKeyDown(Keys.S) && dests[1].Y + dests[1].Height < screenHeight)
            {
                dests[1].Y += paddleSpeed;
            }
            if (kb.IsKeyDown(Keys.Up) && dests[2].Y > 0)
            {
                dests[2].Y -= paddleSpeed;
            }
            if (kb.IsKeyDown(Keys.Down) && dests[2].Y + dests[2].Height < screenHeight)
            {
                dests[2].Y += paddleSpeed;
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
            spriteBatch.Draw(spriteSheet, dests[0], sprites[0], Color.White);
            for (int i = 0; i < wSquares.Length; i++)
                spriteBatch.Draw(spriteSheet, wSquares[i], sprites[6], Color.White);

            spriteBatch.Draw(spriteSheet, dests[1], sprites[2], Color.White);
            spriteBatch.Draw(spriteSheet, dests[2], sprites[3], Color.White);

            spriteBatch.Draw(ballTex, top, Color.White);
            spriteBatch.Draw(ballTex, bottom, Color.White);
            spriteBatch.Draw(ballTex, left, Color.White);
            spriteBatch.Draw(ballTex, right, Color.White);

            spriteBatch.Draw(spriteSheet, ballRect, sprites[5], Color.White);

            spriteBatch.DrawString(font, "" + leftScore, leftScorePosition, lColor);
            spriteBatch.DrawString(font, "" + rightScore, rightScorePosition, rColor);
            spriteBatch.DrawString(font, "" + leftGamesWon, leftGamesWonPosition, lColor);
            spriteBatch.DrawString(font, "" + rightGamesWon, rightGamesWonPosition, rColor);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void ReadFileAsInt(String path)
        {
            try
            {
                using(StreamReader reader = new StreamReader(path))
                {
                    int i = 0;

                    while (!reader.EndOfStream)
                    {
                        String line = reader.ReadLine(); 
                        String[] parts = line.Split(' ');

                        x = Convert.ToInt32(parts[3]);
                        y = Convert.ToInt32(parts[4]);
                        w = Convert.ToInt32(parts[5]);
                        h = Convert.ToInt32(parts[6]);

                        sprites[i] = new Rectangle(x, y, w, h);

                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("the file could not be found");
                Console.WriteLine(e.Message);
            }
        }
    }
}
