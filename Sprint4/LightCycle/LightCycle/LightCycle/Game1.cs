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

namespace LightCycle
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        KeyboardState oldKb = Keyboard.GetState();

        Boolean end;
        Boolean start = false;

        int timer;
        int num;
        int sH;
        int sW;
        int xV, yV, xV2, yV2;
        int timer2;
        int winBike;

        Color color;
        Color endColor;

        Rectangle blueB;
        Rectangle orangeB;
        Rectangle map;
        Rectangle endR;

        Texture2D bike;
        Texture2D grid;
        Texture2D blank;

        Boolean gameStart;

        float rotationB;
        float rotationO;

        List<Rectangle> orangeWall;
        List<Rectangle> BlueWall;
        List<Vector2> bluePos;
        List<Vector2> orangePos;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
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
            sH = GraphicsDevice.Viewport.Height;
            sW = GraphicsDevice.Viewport.Width;

            timer = 0;
            num = 1;
            xV = 3;
            yV = -3;
            xV2 = -3;
            yV2 = 3;
            timer2 = 0;
            winBike = -1;

            color = new Color();
            endColor = new Color();

            blueB = new Rectangle(0, 0, 100, 100);
            orangeB = new Rectangle(0, 0, 100, 100);
            endR = new Rectangle(200, 200, 400, 400);

            rotationB = MathHelper.ToRadians(90);
            rotationO = MathHelper.ToRadians(270);

            orangeWall = new List<Rectangle>();
            BlueWall = new List<Rectangle>();
            bluePos = new List<Vector2>();
            orangePos = new List<Vector2>();

            blueB = new Rectangle(50, 350, 100, 100);
            orangeB = new Rectangle(650, 350, 100, 100);
            map = new Rectangle(0, 0, sW, sH);

            end = false;
            start = false;
            gameStart = false;

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
            bike = this.Content.Load<Texture2D>("bike");
            //grid = this.Content.Load<Texture2D>("tronGrid");
            blank = this.Content.Load<Texture2D>("white");
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
            if (!start)
                timer++;
            else
                timer--;
            if (kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space) && !start && !end)
            {
                start = true;
                timer = 180;
                gameStart = true;
                Random random = new Random();
                blueB = new Rectangle(random.Next(100, 200), random.Next(sH - 200, sH - 100), 50, 50);
                orangeB = new Rectangle(random.Next(sW - 200, sW - 100), random.Next(100, 200), 50, 50);
                BlueWall.Add(blueB);
                orangeWall.Add(orangeB);
                bluePos.Add(new Vector2(blueB.X, blueB.Y));
                orangePos.Add(new Vector2(orangeB.X, orangeB.Y));
            }
            if (timer % 30 == 0 && !start)
                num *= -1;
            if (num == 1)
                color = Color.White;
            else
                color = Color.Black;
            if (start && timer == 0)
                gameStart = false;

            if (start && !gameStart)
            {
                timer2++;
                blueControls(kb);
                orangeControls(kb);
                BlueWall[0] = new Rectangle(blueB.X, blueB.Y, blueB.Width, blueB.Height);
                orangeWall[0] = new Rectangle(orangeB.X, orangeB.Y, orangeB.Width, orangeB.Height);

                if (timer2 > 15)
                {
                    if (BlueWall.Count < 10)
                    {
                        orangeWall.Add(new Rectangle(0, 0, orangeB.Width, orangeB.Height));
                        BlueWall.Add(new Rectangle(0, 0, blueB.Width, blueB.Height));
                        orangePos.Add(new Vector2(orangeB.X, orangeB.Y));
                        bluePos.Add(new Vector2(blueB.X, blueB.Y));
                    }
                    followBlue();
                    followOrange();
                    timer2 = 0;
                }
                if (checkCollision() != -1)
                {
                    end = true;
                    timer2 = 0;
                    winBike = checkCollision();

                    blueB = new Rectangle(50, 350, 100, 100);
                    orangeB = new Rectangle(650, 350, 100, 100);

                    if (winBike == 1)
                    {
                        endR = new Rectangle(25, blueB.Y - 25, 150, 150);
                        endColor = Color.LightCyan;
                    }
                    else
                    {
                        endR = new Rectangle(orangeB.X - 25, orangeB.Y - 25, 150, 150);
                        endColor = Color.Orange;
                    }

                }
            }

            if (end)
            {
                start = false;
                gameStart = false;
            }
            if (kb.IsKeyDown(Keys.Y) && !oldKb.IsKeyDown(Keys.Y) && end)
            {
                Initialize();
            }
            if (kb.IsKeyDown(Keys.N) && !oldKb.IsKeyDown(Keys.N) && end)
            {
                this.Exit();
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
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (!start && !end)
            {
                spriteBatch.DrawString(font, "Welcome to Tron", new Vector2(285, 370), Color.White);
                spriteBatch.DrawString(font, "Press Space", new Vector2(330, 450), color);
                spriteBatch.Draw(bike, blueB, Color.LightCyan);
                spriteBatch.Draw(bike, orangeB, Color.Orange);
                spriteBatch.DrawString(font, "A,D", new Vector2(75, 475), Color.White);
                spriteBatch.DrawString(font, "Left,Right", new Vector2(630, 475), Color.White);
            }
            else if (start)
            {
                if (gameStart)
                    spriteBatch.DrawString(font, "" + ((timer / 60) + 1), new Vector2(375, 370), Color.White);
                spriteBatch.Draw(bike, blueB, new Rectangle(0, 0, 670, 1192), Color.LightCyan, rotationB, new Vector2(blueB.Center.X, blueB.Center.Y), SpriteEffects.None, 0);
                spriteBatch.Draw(bike, orangeB, new Rectangle(0, 0, 670, 1192), Color.Orange, rotationO, new Vector2(orangeB.Center.X, orangeB.Center.Y), SpriteEffects.None, 0);
                for (int i = 0; i < BlueWall.Count; i++)
                {
                    spriteBatch.Draw(blank, BlueWall[i], new Color(Color.LightCyan.R, Color.LightCyan.G, Color.LightCyan.B, Color.LightCyan.A - 100));
                    spriteBatch.Draw(blank, orangeWall[i], new Color(Color.Orange.R, Color.Orange.G, Color.Orange.B, Color.Orange.A - 100));
                }
            }
            else if (end)
            {
                spriteBatch.Draw(blank, endR, endColor);
                if (winBike == 1)
                    spriteBatch.DrawString(font, "Winner is Blue", new Vector2(300, 370), Color.LightCyan);
                else
                    spriteBatch.DrawString(font, "Winner is Orange", new Vector2(300, 370), Color.Orange);
                spriteBatch.DrawString(font, "Play again (y/n)", new Vector2(310, 450), color);

                spriteBatch.Draw(bike, blueB, Color.LightCyan);
                spriteBatch.Draw(bike, orangeB, Color.Orange);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


        public void blueControls(KeyboardState kb)
        {
            if (rotationB == MathHelper.ToRadians(90))
            {
                if (kb.IsKeyDown(Keys.A) && !oldKb.IsKeyDown(Keys.A) && start)
                {
                    rotationB = MathHelper.ToRadians(0);
                    xV = 0;
                    yV = -3;
                }
                else if (kb.IsKeyDown(Keys.D) && !oldKb.IsKeyDown(Keys.D) && start)
                {
                    rotationB = MathHelper.ToRadians(180);
                    xV = 0;
                    yV = 3;
                }
            }
            else if (rotationB == MathHelper.ToRadians(0) || rotationB == MathHelper.ToRadians(180))
            {
                if (kb.IsKeyDown(Keys.A) && !oldKb.IsKeyDown(Keys.A) && start)
                {
                    rotationB = MathHelper.ToRadians(270);
                    xV = -3;
                    yV = 0;
                }
                else if (kb.IsKeyDown(Keys.D) && !oldKb.IsKeyDown(Keys.D) && start)
                {
                    rotationB = MathHelper.ToRadians(90);
                    xV = 3;
                    yV = 0;
                }
            }
            else if (rotationB == MathHelper.ToRadians(270))
            {
                if (kb.IsKeyDown(Keys.A) && !oldKb.IsKeyDown(Keys.A) && start)
                {
                    rotationB = MathHelper.ToRadians(180);
                    xV = 0;
                    yV = 3;
                }
                else if (kb.IsKeyDown(Keys.D) && !oldKb.IsKeyDown(Keys.D) && start)
                {
                    rotationB = MathHelper.ToRadians(0);
                    xV = 0;
                    yV = -3;
                }
            }
            blueB.X += xV;
            blueB.Y += yV;

        }

        public void orangeControls(KeyboardState kb)
        {
            if (rotationO == MathHelper.ToRadians(90))
            {
                if (kb.IsKeyDown(Keys.Left) && !oldKb.IsKeyDown(Keys.Left) && start)
                {
                    rotationO = MathHelper.ToRadians(0);
                    xV2 = 0;
                    yV2 = -3;
                }
                else if (kb.IsKeyDown(Keys.Right) && !oldKb.IsKeyDown(Keys.Right) && start)
                {
                    rotationO = MathHelper.ToRadians(180);
                    xV2 = 0;
                    yV2 = 3;
                }
            }
            else if (rotationO == MathHelper.ToRadians(0) || rotationO == MathHelper.ToRadians(180))
            {
                if (kb.IsKeyDown(Keys.Left) && !oldKb.IsKeyDown(Keys.Left) && start)
                {
                    rotationO = MathHelper.ToRadians(270);
                    xV2 = -3;
                    yV2 = 0;
                }
                else if (kb.IsKeyDown(Keys.Right) && !oldKb.IsKeyDown(Keys.Right) && start)
                {
                    rotationO = MathHelper.ToRadians(90);
                    xV2 = 3;
                    yV2 = 0;
                }
            }
            else if (rotationO == MathHelper.ToRadians(270))
            {
                if (kb.IsKeyDown(Keys.Left) && !oldKb.IsKeyDown(Keys.Left) && start)
                {
                    rotationO = MathHelper.ToRadians(180);
                    xV2 = 0;
                    yV2 = 3;
                }
                else if (kb.IsKeyDown(Keys.Right) && !oldKb.IsKeyDown(Keys.Right) && start)
                {
                    rotationO = MathHelper.ToRadians(0);
                    xV2 = 0;
                    yV2 = -3;
                }
            }
            orangeB.X += xV2;
            orangeB.Y += yV2;
        }

        public void followBlue()
        {
            for (int i = 0; i < BlueWall.Count; i++)
            {
                bluePos[i] = new Vector2(BlueWall[i].X, BlueWall[i].Y);
            }
            for (int i = BlueWall.Count - 1; i > 0; i--)
            {
                int x = (int)bluePos[i - 1].X;
                int y = (int)bluePos[i - 1].Y;
                BlueWall[i] = new Rectangle(x, y, BlueWall[i].Width, BlueWall[i].Height);
            }
        }

        public void followOrange()
        {
            for (int i = 0; i < orangeWall.Count; i++)
            {
                orangePos[i] = new Vector2(orangeWall[i].X, orangeWall[i].Y);
            }

            for (int i = 1; i < orangeWall.Count; i++)
            {
                int x = (int)orangePos[i - 1].X;
                int y = (int)orangePos[i - 1].Y;
                orangeWall[i] = new Rectangle(x, y, orangeWall[i].Width, orangeWall[i].Height);
            }
        }

        public int checkCollision()
        {
            for (int i = 1; i < BlueWall.Count; i++)
            {
                if (blueB.Intersects(orangeWall[i]))
                    return 2;
                if (orangeB.Intersects(BlueWall[i]))
                    return 1;
            }
            for (int i = 5; i < BlueWall.Count; i++)
            {
                if (blueB.Intersects(BlueWall[i]))
                    return 2;
                if (orangeB.Intersects(orangeWall[i]))
                    return 1;
            }

            if (blueB.X < 0 || blueB.X + blueB.Width > sW || blueB.Y < 0 || blueB.Y + blueB.Height > sH)
                return 2;
            if (orangeB.X < 0 || orangeB.X + orangeB.Width > sW || orangeB.Y < 0 || orangeB.Y + orangeB.Height > sH)
                return 1;

            return -1;
        }
    }
}
