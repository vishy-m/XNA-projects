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

namespace Joust
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;

        Rectangle menuTitle;
        Texture2D menuText;

        Rectangle lava;
        Texture2D lavaText;

        Rectangle tempEnemyRect;

        Texture2D whiteText;
        Rectangle temp;
        int xVelocity, yVelocity;

        Rectangle bird;

        Rectangle birdRect;

        Player player;
        Rectangle sRect = new Rectangle(0, 0, 15, 23);
        List<Enemy> enemies;

        KeyboardState oldKb;


        Texture2D spriteSheet;
        Texture2D spriteSheet2;
        Rectangle[] platforms;

        int enemyX;
        int enemyY;
        int playerX;
        int playerY;
        int removetimer;

        List<int> spawnXVals;
        List<int> spawnYVals;

        double enemyXVel;

        Random rnd;

        int timer;

        int birdX;
        int birdY;
        int count;
        int place = 1;

        Color color;

        enum GAMESTATE
        {
            menu, rules, wave1, wave2, wave3, gameLost, gameWon
        }

        GAMESTATE gState;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Window.AllowUserResizing = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 800;
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
            gState = GAMESTATE.menu;
            rnd = new Random();
            player = new Player(new Rectangle(0, 50, 50, 50), 5, 1, 3, 0);
            oldKb = Keyboard.GetState();
            menuTitle = new Rectangle(0, 0, 800, 700);
            IsMouseVisible = true;
            lava = new Rectangle(0, 750, 800, 50);
            platforms = new Rectangle[9];
            IsMouseVisible = true;
            platforms[0] = new Rectangle(0, 200, 100, 40);
            platforms[1] = new Rectangle(600, 200, 200, 40);
            platforms[2] = new Rectangle(200, 300, 300, 40);
            platforms[3] = new Rectangle(0, 400, 200, 40);
            platforms[4] = new Rectangle(500, 375, 200, 40);
            platforms[5] = new Rectangle(600, 425, 200, 40);
            platforms[6] = new Rectangle(250, 525, 200, 40);
            platforms[7] = new Rectangle(150, 700, 500, 20);
            platforms[8] = new Rectangle(140, 711, 550, 200);
            enemies = new List<Enemy>();
            tempEnemyRect = new Rectangle(0, 0, 0, 0);
            enemyXVel = 0;
            yVelocity = 1;

            enemyX = 0;
            enemyY = 48;
            playerX = 0;
            playerY = 0;
            removetimer = 0;

            spawnXVals = new List<int>();
            spawnYVals = new List<int>();

            spawnXVals.Add(325);
            spawnYVals.Add(250);

            spawnXVals.Add(50);
            spawnYVals.Add(350);

            spawnXVals.Add(575);
            spawnYVals.Add(325);

            bird = new Rectangle(0, 120, 23, 24);

            birdY = 600;

            bird = new Rectangle(0, 120, 23, 24);
            birdRect = new Rectangle(200, 400, 50, 50);

            color = Color.White;

            count = 0;

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
            menuText = this.Content.Load<Texture2D>("joustMenuName");
            lavaText = this.Content.Load<Texture2D>("lava");
            spriteSheet = this.Content.Load<Texture2D>("joust character+enemies (1)");
            spriteSheet2 = this.Content.Load<Texture2D>("platforms, characters, and other objects");
            whiteText = this.Content.Load<Texture2D>("white");

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

            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            timer++;

            if (gState == GAMESTATE.wave1 || gState == GAMESTATE.wave2 || gState == GAMESTATE.wave3)
            {
                count = 0;
                if (gState == GAMESTATE.wave1)
                {
                    birdRect.X -= rnd.Next(2, 4);
                }
                if (gState == GAMESTATE.wave2)
                {
                    birdRect.X -= rnd.Next(4, 6);
                }
                if (gState == GAMESTATE.wave3)
                {
                    birdRect.X -= rnd.Next(6, 8);
                }
                if (birdRect.X > 800)
                {
                    birdRect.X = 0;

                }
                if (birdRect.X < 0)
                {
                    birdRect.X = 800;
                    if (gState != GAMESTATE.wave1)
                        birdRect.Y = rnd.Next(100, 400);
                }
                if (player.rectangle.Intersects(birdRect) && !(player.rectangle.Y > birdRect.Y))
                {
                    player.livesRemaining--;
                    int sL = rnd.Next(0, 3);
                    player.rectangle.X = spawnXVals[sL];
                    player.rectangle.Y = spawnYVals[sL];
                    player.points -= 1;
                }

                if (player.livesRemaining == 0)
                {
                    gState = GAMESTATE.gameLost;
                }
                if (player.rectangle.Intersects(lava))

                {
                    int sL = rnd.Next(0, 3);
                    player.rectangle.X = spawnXVals[sL];
                    player.rectangle.Y = spawnYVals[sL];
                    player.livesRemaining -= 1;

                }
                enemyChecks();


            }
            if (kb.IsKeyDown(Keys.Z) && !oldKb.IsKeyDown(Keys.Z) && gState == GAMESTATE.rules)
            {
                gState = GAMESTATE.wave1;
                for (int i = 0; i < 3; i++)
                {
                    int r = rnd.Next(0, 100);
                    Console.WriteLine(r);
                    if (r > 50)
                    {
                        temp.X = 0;
                        xVelocity = rnd.Next(1, 4);
                    }
                    else
                    {
                        temp.X = 760;
                    }
                    temp.Y = rnd.Next(0, 600);
                    temp.Width = 50;
                    temp.Height = 50;
                    xVelocity = -1 * rnd.Next(1, 4);
                    Console.WriteLine(temp + " " + xVelocity);
                    enemies.Add(new Enemy(spriteSheet, temp, xVelocity, yVelocity));

                }
            }


            if (enemies.Count == 0)
            {
                if (gState == GAMESTATE.wave1)
                {
                    gState = GAMESTATE.wave2;
                    for (int i = 0; i < 7; i++)
                    {
                        if (rnd.Next(0, 100) > 50)
                        {
                            temp.X = 0;
                            xVelocity = rnd.Next(4, 8);

                        }
                        else
                        {
                            temp.X = 760;
                        }
                        temp.Y = rnd.Next(0, 800);
                        temp.Width = 50;
                        temp.Height = 50;
                        xVelocity = -1 * rnd.Next(4, 8);
                        enemies.Add(new Enemy(spriteSheet, temp, xVelocity, yVelocity));
                    }
                }
                else if (gState == GAMESTATE.wave2)
                {
                    gState = GAMESTATE.wave3;
                    for (int i = 0; i < 12; i++)
                    {
                        if (rnd.Next(0, 100) > 50)
                        {
                            temp.X = 0;
                            xVelocity = rnd.Next(6, 8);

                        }
                        else
                        {
                            temp.X = 760;
                        }
                        temp.Y = rnd.Next(0, 800);
                        temp.Width = 50;
                        temp.Height = 50;
                        xVelocity = -1 * rnd.Next(6, 8);
                        enemies.Add(new Enemy(spriteSheet, temp, xVelocity, yVelocity));
                    }
                }
                else if (gState == GAMESTATE.wave3)
                {
                    gState = GAMESTATE.gameWon;
                }
            }
            if (gState == GAMESTATE.wave1 || gState == GAMESTATE.wave2 || gState == GAMESTATE.wave3)
            {
                playerChecks(kb);
            }

            if (gState == GAMESTATE.menu)
            {
                count++;
                if (count % 60 == 0)
                {
                    place *= -1;
                    if (place < 0)
                        color = Color.White;
                    else
                        color = Color.Black;
                }
            }

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
            KeyboardState kb = Keyboard.GetState();
            spriteBatch.Begin();
            if (gState == GAMESTATE.menu)
            {
                spriteBatch.Draw(menuText, menuTitle, Color.White);
                spriteBatch.DrawString(font, "Press Enter to continue", new Vector2(300, 700), color);
                if (kb.IsKeyDown(Keys.Enter) && !oldKb.IsKeyDown(Keys.Enter))
                {
                    gState = GAMESTATE.rules;
                }

            }
            else if (gState == GAMESTATE.rules)
            {
                spriteBatch.DrawString(font, "In this game you will operate a knight riding an ostrich!\n\nThe objective is to defeat 3 waves of enemy buzzard that become more difficult\n\nUse the arrow keys to move left and right\n\nUse the space bar to fly higher than the enemy\n\nIf you attack the enemy and are higher up you win the joust\n\nBeat all 3 waves to beat the game and beware of the unbeatable bird!!!\nGood Luck!\n\nPress Z to start the game", new Vector2(0, 0), Color.White);
                if (kb.IsKeyDown(Keys.Z) && !oldKb.IsKeyDown(Keys.Z))
                {
                    gState = GAMESTATE.wave1;
                    int sL = rnd.Next(0, 3);
                    player.rectangle.X = spawnXVals[sL];
                    player.rectangle.Y = spawnYVals[sL];
                }
            }
            else if (gState == GAMESTATE.wave1 || gState == GAMESTATE.wave2 || gState == GAMESTATE.wave3)
            {
                //spriteBatch.Draw(spriteSheet, new Rectangle(birdX, birdY, 50, 50), bird, Color.White);
                spriteBatch.DrawString(font, "Player Lives: " + player.livesRemaining, new Vector2(600, 100), Color.White);
                spriteBatch.Draw(spriteSheet, player.rectangle, sRect, Color.White);
                spriteBatch.Draw(whiteText, lava, Color.Red);
                spriteBatch.Draw(spriteSheet2, platforms[0], new Rectangle(0, 0, 75, 20), Color.White);
                spriteBatch.Draw(spriteSheet2, platforms[1], new Rectangle(75, 0, 75, 20), Color.White);
                spriteBatch.Draw(spriteSheet2, platforms[2], new Rectangle(175, 0, 190, 20), Color.White);
                spriteBatch.Draw(spriteSheet2, platforms[3], new Rectangle(375, 0, 150, 20), Color.White);
                spriteBatch.Draw(spriteSheet2, platforms[4], new Rectangle(0, 25, 125, 20), Color.White);
                spriteBatch.Draw(spriteSheet2, platforms[5], new Rectangle(130, 25, 75, 20), Color.White);
                spriteBatch.Draw(spriteSheet2, platforms[6], new Rectangle(225, 25, 135, 20), Color.White);
                spriteBatch.Draw(spriteSheet2, platforms[7], new Rectangle(0, 60, 400, 15), Color.White);
                spriteBatch.Draw(spriteSheet2, platforms[8], new Rectangle(0, 0, 75, 20), Color.Orange);
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i].isEgg)
                        spriteBatch.Draw(spriteSheet, enemies[i].rectangle, new Rectangle(118, 34, 10, 18), Color.White);
                    else
                        spriteBatch.Draw(spriteSheet, enemies[i].rectangle, new Rectangle(enemyX, enemyY, 15, 23), Color.White);
                }
                spriteBatch.Draw(spriteSheet, birdRect, bird, Color.White);
                spriteBatch.DrawString(font, "Player Score: " + player.points, new Vector2(0, 0), Color.White);

            }
            if (gState == GAMESTATE.wave1)
            {
                spriteBatch.DrawString(font, "Wave 1", new Vector2(350, 200), Color.White);
            }
            else if (gState == GAMESTATE.wave2)
            {
                spriteBatch.DrawString(font, "Wave 2", new Vector2(390, 200), Color.White);
            }
            else if (gState == GAMESTATE.wave3)
            {
                spriteBatch.DrawString(font, "Wave 3", new Vector2(390, 200), Color.White);
            }
            else if (gState == GAMESTATE.gameWon)
            {
                spriteBatch.DrawString(font, "CONGRATULATIONS! You have beat the game. Your score was " + player.points + "\nPress S to play again!", new Vector2(0, 200), Color.White);
                if (kb.IsKeyDown(Keys.S))
                {
                    gState = GAMESTATE.menu;
                    player = new Player(new Rectangle(0, 50, 50, 50), 5, 1, 3, 0);
                    enemies.RemoveRange(0, enemies.Count);
                }
            }
            else if (gState == GAMESTATE.gameLost)
            {
                spriteBatch.DrawString(font, "You have lost the game. Your score was " + player.points + "\nPress S to play again and hopefully do better!", new Vector2(0, 200), Color.White);
                if (kb.IsKeyDown(Keys.S) && !oldKb.IsKeyDown(Keys.S))
                {
                    gState = GAMESTATE.menu;
                    player = new Player(new Rectangle(0, 50, 50, 50), 5, 1, 3, 0);
                    enemies.RemoveRange(0, enemies.Count);

                }
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
        public void playerChecks(KeyboardState kb)
        {
            player.update(platforms);

            if (player.rectangle.Intersects(platforms[0]) && player.rectangle.Bottom < platforms[0].Bottom)
            {
                if (timer % 10 == 0)
                {
                    if (sRect.X < 64)
                    {
                        sRect.X += 16;
                    }
                    else if (sRect.X >= 64)
                    {
                        sRect.X = 0;
                    }
                }
                player.Yspeed = 0;

            }
            else if (player.rectangle.Intersects(platforms[1]) && player.rectangle.Bottom < platforms[1].Bottom)
            {
                if (timer % 10 == 0)
                {
                    if (sRect.X < 64)
                    {
                        sRect.X += 16;
                    }
                    else if (sRect.X >= 64)
                    {
                        sRect.X = 0;
                    }
                }
                player.Yspeed = 0;
            }
            else if (player.rectangle.Intersects(platforms[2]) && player.rectangle.Bottom < platforms[2].Bottom)
            {
                if (timer % 10 == 0)
                {
                    if (sRect.X < 64)
                    {
                        sRect.X += 16;
                    }
                    else if (sRect.X >= 64)
                    {
                        sRect.X = 0;
                    }
                }
                player.Yspeed = 0;
            }
            else if (player.rectangle.Intersects(platforms[3]) && player.rectangle.Bottom < platforms[3].Bottom)
            {
                if (timer % 10 == 0)
                {
                    if (sRect.X < 64)
                    {
                        sRect.X += 16;
                    }
                    else if (sRect.X >= 64)
                    {
                        sRect.X = 0;
                    }
                }
                player.Yspeed = 0;
            }
            else if (player.rectangle.Intersects(platforms[4]) && player.rectangle.Bottom < platforms[4].Bottom)
            {
                if (timer % 10 == 0)
                {
                    if (sRect.X < 64)
                    {
                        sRect.X += 16;
                    }
                    else if (sRect.X >= 64)
                    {
                        sRect.X = 0;
                    }
                }
                player.Yspeed = 0;
            }
            else if (player.rectangle.Intersects(platforms[5]) && player.rectangle.Bottom < platforms[5].Bottom)
            {
                if (timer % 10 == 0)
                {
                    if (sRect.X < 64)
                    {
                        sRect.X += 16;
                    }
                    else if (sRect.X >= 64)
                    {
                        sRect.X = 0;
                    }
                }
                player.Yspeed = 0;
            }
            else if (player.rectangle.Intersects(platforms[6]) && player.rectangle.Bottom < platforms[6].Bottom)
            {
                if (timer % 10 == 0)
                {
                    if (sRect.X < 64)
                    {
                        sRect.X += 16;
                    }
                    else if (sRect.X >= 64)
                    {
                        sRect.X = 0;
                    }
                }
                player.Yspeed = 0;
            }
            else if (player.rectangle.Intersects(platforms[7]) && player.rectangle.Bottom < platforms[7].Bottom)
            {
                if (timer % 10 == 0)
                {
                    if (sRect.X < 64)
                    {
                        sRect.X += 16;
                    }
                    else if (sRect.X >= 64)
                    {
                        sRect.X = 0;
                    }
                }
                player.Yspeed = 0;
            }
            else if (player.rectangle.Intersects(platforms[8]) && player.rectangle.Bottom < platforms[8].Bottom)
            {
                if (timer % 10 == 0)
                {
                    if (sRect.X < 64)
                    {
                        sRect.X += 16;
                    }
                    else if (sRect.X >= 64)
                    {
                        sRect.X = 0;
                    }
                }
                player.Yspeed = 0;
            }
            else
            {
                sRect.X = 64;
                if (timer % 60 == 0)
                {
                    if (sRect.X < 112)
                    {
                        sRect.X += 16;
                    }
                    else if (sRect.X >= 112)
                    {
                        sRect.X = 64;
                    }
                }
                player.Yspeed = 4;
            }


            if (kb.IsKeyDown(Keys.Space) && !oldKb.IsKeyDown(Keys.Space))
            {
                player.rectangle.Y -= 10;
                player.rectangle.Y -= 4;
            }

            player.rectangle.Y += (int)player.Yspeed;
        }
        public void enemyChecks()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].update();
                if (enemies[i].rectangle.Intersects(player.rectangle) && !enemies[i].isEgg)
                {
                    if (player.rectangle.Top <= enemies[i].rectangle.Top)
                    {
                        //enemies.RemoveAt(i);
                        player.points += 1;
                        enemies[i].isEgg = true;
                        xVelocity *= -1;
                    }
                    else
                    {
                        player.livesRemaining -= 1;
                        int sL = rnd.Next(0, 3);
                        player.rectangle.X = spawnXVals[sL];
                        player.rectangle.Y = spawnYVals[sL];
                        player.points -= 1;
                    }
                }
                else
                {
                    if (enemies[i].remove)
                    {
                        enemies.RemoveAt(i);
                        continue;
                    }
                    if (enemies[i].isEgg)
                    {
                        removetimer++;
                        if (removetimer > 60)
                        {
                            if (enemies[i].rectangle.Intersects(player.rectangle) && enemies[i].isEgg)
                            {
                                player.points++;
                                enemies.RemoveAt(i);
                                removetimer = 0;
                                continue;
                            }
                        }

                    }
                    if (!enemies[i].isEgg)
                        enemies[i].rectangle.X += xVelocity;

                    if (enemies[i].rectangle.X > 800)
                    {
                        enemies[i].rectangle.X = 0;
                    }
                    if (enemies[i].rectangle.X < 0)
                    {
                        enemies[i].rectangle.X = 800;
                    }
                    if (enemies[i].rectangle.Intersects(platforms[0]) && enemies[i].rectangle.Bottom < platforms[0].Bottom)
                    {
                        yVelocity = 0;
                        if (timer % 10 == 0 && !enemies[i].isEgg)
                        {
                            if (enemyX < 64)
                            {
                                enemyX += 16;
                            }
                            else if (enemyX >= 64)
                            {
                                enemyX = 0;
                            }
                        }

                    }
                    else if (enemies[i].rectangle.Intersects(platforms[1]) && enemies[i].rectangle.Bottom < platforms[1].Bottom)
                    {
                        yVelocity = 0;
                        if (timer % 10 == 0 && !enemies[i].isEgg)
                        {
                            if (enemyX < 64)
                            {
                                enemyX += 16;
                            }
                            else if (enemyX >= 64)
                            {
                                enemyX = 0;
                            }
                        }
                    }
                    else if (enemies[i].rectangle.Intersects(platforms[2]) && enemies[i].rectangle.Bottom < platforms[2].Bottom)
                    {
                        yVelocity = 0;
                        if (timer % 10 == 0 && !enemies[i].isEgg)
                        {
                            if (enemyX < 64)
                            {
                                enemyX += 16;
                            }
                            else if (enemyX >= 64)
                            {
                                enemyX = 0;
                            }
                        }
                    }
                    else if (enemies[i].rectangle.Intersects(platforms[3]) && enemies[i].rectangle.Bottom < platforms[3].Bottom)
                    {
                        yVelocity = 0;
                        if (timer % 10 == 0 && !enemies[i].isEgg)
                        {
                            if (enemyX < 64)
                            {
                                enemyX += 16;
                            }
                            else if (enemyX >= 64)
                            {
                                enemyX = 0;
                            }
                        }
                    }
                    else if (enemies[i].rectangle.Intersects(platforms[4]) && enemies[i].rectangle.Bottom < platforms[4].Bottom)
                    {
                        yVelocity = 0;
                        if (timer % 10 == 0 && !enemies[i].isEgg)
                        {
                            if (enemyX < 64)
                            {
                                enemyX += 16;
                            }
                            else if (enemyX >= 64)
                            {
                                enemyX = 0;
                            }
                        }
                    }
                    else if (enemies[i].rectangle.Intersects(platforms[5]) && enemies[i].rectangle.Bottom < platforms[5].Bottom)
                    {
                        yVelocity = 0;
                        if (timer % 10 == 0 && !enemies[i].isEgg)
                        {
                            if (enemyX < 64)
                            {
                                enemyX += 16;
                            }
                            else if (enemyX >= 64)
                            {
                                enemyX = 0;
                            }
                        }
                    }
                    else if (enemies[i].rectangle.Intersects(platforms[6]) && enemies[i].rectangle.Bottom < platforms[6].Bottom)
                    {
                        yVelocity = 0;
                        if (timer % 10 == 0 && !enemies[i].isEgg)
                        {
                            if (enemyX < 64)
                            {
                                enemyX += 16;
                            }
                            else if (enemyX >= 64)
                            {
                                enemyX = 0;
                            }
                        }
                    }
                    else if (enemies[i].rectangle.Intersects(platforms[7]) && enemies[i].rectangle.Bottom < platforms[7].Bottom)
                    {
                        yVelocity = 0;
                        if (timer % 10 == 0 && !enemies[i].isEgg)
                        {
                            if (enemyX < 64)
                            {
                                enemyX += 16;
                            }
                            else if (enemyX >= 64)
                            {
                                enemyX = 0;
                            }
                        }
                    }
                    else if (enemies[i].rectangle.Intersects(platforms[8]) && enemies[i].rectangle.Bottom < platforms[8].Bottom)
                    {
                        yVelocity = 0;
                        if (timer % 10 == 0 && !enemies[i].isEgg)
                        {
                            if (enemyX < 64)
                            {
                                enemyX += 16;
                            }
                            else if (enemyX >= 64)
                            {
                                enemyX = 0;
                            }
                        }
                    }
                    else
                    {
                        yVelocity = 1;
                    }

                    if (enemies[i].rectangle.Y > 650)
                    {
                        enemies[i].rectangle.Y = rnd.Next(0, 500);
                    }
                    enemies[i].rectangle.Y += yVelocity;
                    
                }
            }
        }
    }
}
    

