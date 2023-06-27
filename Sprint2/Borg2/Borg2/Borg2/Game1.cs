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

namespace Borg2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle borgR = new Rectangle(450, 200, 100, 100);
        Rectangle photonR;
        Rectangle starBaseR = new Rectangle(455, 455, 100, 100);
        Rectangle bPhotonR = new Rectangle(-100, -100, 50, 50);
        Random random = new Random();
        Texture2D borgT;
        Texture2D photonT;
        Texture2D starBaseT;
        Rectangle[] lightR = { new Rectangle(460, 500, 10, 10), new Rectangle(540, 500, 10, 10), new Rectangle(500, 460, 10, 10), new Rectangle(500, 540, 10, 10) };
        Texture2D lightT;
        Boolean isFired = false, isAtEdge = false;
        Color[] colors = {Color.Red, Color.Red, Color.Green, Color.Red};
        Rectangle[] energyBar = new Rectangle[9];
        Rectangle[] propelBar = new Rectangle[9];
        Rectangle box = new Rectangle(0, 0, 560, 70);
        Rectangle[] lsuBar = new Rectangle[100];
        Rectangle explosion = new Rectangle(-100, -100, 100, 100);
        Boolean borgCanFire = false;
        int energyDrawn = 0;
        int lSUCharge = 100;
        int timer = 0;
        int timer2 = 0;
        int place = 3;
        int laneTimer = 0;
        int propelEnergy = 1;
        int lane = 0;
        int explosionTimer = 0;
        int explosionCharge = 1;
        Color color = Color.Purple;
        int phasorPow = 0;
        Texture2D explosionT;
        Boolean torpedoFire = false;
        Boolean phasorFire = false;
        SpriteFont font;
        Boolean explode = false;
        int score = 0;
        Boolean isShot = false;
        KeyboardState oldKB = Keyboard.GetState();
        MouseState oldMouse = Mouse.GetState();
        Vector2[] vectors = { new Vector2(300, 250), new Vector2(700, 250)};
        int dissapearTimer = 0;
        int dissapearSeconds = 0;
        int lSeconds = 0;
        SoundEffect baseSFX;
        SoundEffect torpedoSFX;
        SoundEffect borgSFX;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
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
            for (int i = 0; i < lsuBar.Length; i++)
                lsuBar[i] = new Rectangle(10, 985 - (5 * i), 20, 5);
            for (int i = 0; i < energyBar.Length; i++)
                energyBar[i] = new Rectangle(50, 960 - (i * 40), 30, 30);
            for (int i = 0; i < propelBar.Length; i++)
                propelBar[i] = new Rectangle(100, 960 - (i * 40), 30, 30);

            IsMouseVisible = true;
            photonR = lightR[3];
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
            borgT = this.Content.Load<Texture2D>("BorgCube");
            photonT = this.Content.Load<Texture2D>("Photon Torpedo");
            starBaseT = this.Content.Load<Texture2D>("StarBase");
            lightT = this.Content.Load<Texture2D>("white");
            font = this.Content.Load<SpriteFont>("SpriteFont1");
            explosionT = this.Content.Load<Texture2D>("explosion");
            borgSFX = this.Content.Load<SoundEffect>("borgShot");
            torpedoSFX = this.Content.Load<SoundEffect>("torpedo");
            baseSFX = this.Content.Load<SoundEffect>("base");

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
            MouseState mouse = Mouse.GetState();
            GamePadState pad1 = GamePad.GetState(PlayerIndex.One);
            timer++;
            int seconds = timer / 60;
            timer2++;
            dissapearTimer++;
            dissapearSeconds = dissapearTimer / 60;

            if (pad1.Buttons.A == ButtonState.Pressed)
                phasorPow = 20;
            if (pad1.Buttons.B == ButtonState.Pressed)
                phasorPow = 50;
            if (pad1.Buttons.X == ButtonState.Pressed)
                phasorPow = 75;
            if (pad1.Buttons.Y == ButtonState.Pressed)
                phasorPow = 100;

            if (kb.IsKeyDown(Keys.NumPad0) && oldKB.IsKeyDown(Keys.NumPad0))
                energyDrawn = 0;
            else if (kb.IsKeyDown(Keys.NumPad1) && oldKB.IsKeyDown(Keys.NumPad1))
                energyDrawn = 1;
            else if (kb.IsKeyDown(Keys.NumPad2) && oldKB.IsKeyDown(Keys.NumPad2))
                energyDrawn = 2;
            else if (kb.IsKeyDown(Keys.NumPad3) && oldKB.IsKeyDown(Keys.NumPad3))
                energyDrawn = 3;
            else if (kb.IsKeyDown(Keys.NumPad4) && oldKB.IsKeyDown(Keys.NumPad4))
                energyDrawn = 4;
            else if (kb.IsKeyDown(Keys.NumPad5) && oldKB.IsKeyDown(Keys.NumPad5))
                energyDrawn = 5;
            else if (kb.IsKeyDown(Keys.NumPad6) && oldKB.IsKeyDown(Keys.NumPad6))
                energyDrawn = 6;
            else if (kb.IsKeyDown(Keys.NumPad7) && oldKB.IsKeyDown(Keys.NumPad7))
                energyDrawn = 7;
            else if (kb.IsKeyDown(Keys.NumPad8) && oldKB.IsKeyDown(Keys.NumPad8))
                energyDrawn = 8;
            else if (kb.IsKeyDown(Keys.NumPad9) && oldKB.IsKeyDown(Keys.NumPad9))
                energyDrawn = 9;
            if (kb.IsKeyDown(Keys.D1) && oldKB.IsKeyDown(Keys.D1))
                propelEnergy = 1;
            else if (kb.IsKeyDown(Keys.D2) && oldKB.IsKeyDown(Keys.D2))
                propelEnergy = 2;
            else if (kb.IsKeyDown(Keys.D3) && oldKB.IsKeyDown(Keys.D3))
                propelEnergy = 3;
            else if (kb.IsKeyDown(Keys.D4) && oldKB.IsKeyDown(Keys.D4))
                propelEnergy = 4;
            else if (kb.IsKeyDown(Keys.D5) && oldKB.IsKeyDown(Keys.D5))
                propelEnergy = 5;
            else if (kb.IsKeyDown(Keys.D6) && oldKB.IsKeyDown(Keys.D6))
                propelEnergy = 6;
            else if (kb.IsKeyDown(Keys.D7) && oldKB.IsKeyDown(Keys.D7))
                propelEnergy = 7;
            else if (kb.IsKeyDown(Keys.D8) && oldKB.IsKeyDown(Keys.D8))
                propelEnergy = 8;
            else if (kb.IsKeyDown(Keys.D9) && oldKB.IsKeyDown(Keys.D9))
                propelEnergy = 9;

            if (pad1.DPad.Right == ButtonState.Pressed && propelEnergy < 9)
                propelEnergy++;
            else if (pad1.DPad.Left == ButtonState.Pressed && propelEnergy > 1)
                propelEnergy--;
            else if (pad1.DPad.Up == ButtonState.Pressed && explosionCharge < 9)
                explosionCharge++;
            else if (pad1.DPad.Down == ButtonState.Pressed && propelEnergy > 1)
                explosionCharge--;


            if (kb.IsKeyDown(Keys.Right) && !oldKB.IsKeyDown(Keys.Right) && !isFired || pad1.ThumbSticks.Left.X < 0 && !isFired)
            {
                for (int i = 0; i < colors.Length; i++)
                    colors[i] = Color.Red;
                colors[1] = Color.Green;
            }
            else if (kb.IsKeyDown(Keys.Left) && !oldKB.IsKeyDown(Keys.Left) && !isFired || pad1.ThumbSticks.Left.X > 0 && !isFired)
            {
                for (int i = 0; i < colors.Length; i++)
                    colors[i] = Color.Red;
                colors[0] = Color.Green;

            }
            else if (kb.IsKeyDown(Keys.Up) && !oldKB.IsKeyDown(Keys.Up) && !isFired || pad1.ThumbSticks.Left.Y < 0 && !isFired)
            {
                for (int i = 0; i < colors.Length; i++)
                    colors[i] = Color.Red;
                colors[2] = Color.Green;
            }
            else if (kb.IsKeyDown(Keys.Down) && !oldKB.IsKeyDown(Keys.Down) && !isFired || pad1.ThumbSticks.Left.Y > 0 && !isFired)
            {
                for (int i = 0; i < colors.Length; i++)
                    colors[i] = Color.Red;
                colors[3] = Color.Green;
            }
            if (colors[0] == Color.Green)
                place = 0;
            if (colors[1] == Color.Green)
                place = 1;
            if (colors[2] == Color.Green)
                place = 2;
            if (colors[3] == Color.Green)
                place = 3;
            if (!isFired)
                colors[place] = Color.Green;
            if (kb.IsKeyDown(Keys.Space) && oldKB.IsKeyDown(Keys.Space) && !isFired || mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released && !isFired || pad1.Triggers.Left > 0 && !isFired || pad1.Triggers.Right > 0 && !isFired)
            {
                if (pad1.Triggers.Left > 0)
                {
                    photonT = this.Content.Load<Texture2D>("Torpedo Mark XXV");
                    torpedoFire = true;
                }
                if (pad1.Triggers.Right > 0)
                {
                    photonT = this.Content.Load<Texture2D>("white");
                    phasorFire = true;
                }
                
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released && !isFired)
                {
                    if (mouse.X < 500 && mouse.Y < 500)
                    {
                        if (mouse.X < vectors[0].X)
                        {
                            for (int i = 0; i < colors.Length; i++)
                                colors[i] = Color.Red;
                            colors[0] = Color.Green;
                            place = 0;
                        }
                        else
                        {
                            for (int i = 0; i < colors.Length; i++)
                                colors[i] = Color.Red;
                            colors[2] = Color.Green;
                            place = 2;
                        }
                    }
                    else if (mouse.X > 500 && mouse.Y < 500)
                    {
                        if (mouse.X > vectors[1].X)
                        {
                            for (int i = 0; i < colors.Length; i++)
                                colors[i] = Color.Red;
                            colors[1] = Color.Green;
                            place = 1;
                        }
                        else
                        {
                            for (int i = 0; i < colors.Length; i++)
                                colors[i] = Color.Red;
                            colors[2] = Color.Green;
                            place = 2;
                        }
                    }
                    else if (mouse.X > 500 && mouse.Y > 500)
                    {
                        if (mouse.X > vectors[1].X)
                        {
                            for (int i = 0; i < colors.Length; i++)
                                colors[i] = Color.Red;
                            colors[1] = Color.Green;
                            place = 1;
                        }
                        else
                        {
                            for (int i = 0; i < colors.Length; i++)
                                colors[i] = Color.Red;
                            colors[3] = Color.Green;
                            place = 3;
                        }
                    }
                    else if (mouse.X < 500 && mouse.Y > 500)
                    {

                        if (mouse.X < vectors[0].X)
                        {
                            for (int i = 0; i < colors.Length; i++)
                                colors[i] = Color.Red;
                            colors[0] = Color.Green;
                            place = 0;
                        }
                        else
                        {
                            for (int i = 0; i < colors.Length; i++)
                                colors[i] = Color.Red;
                            colors[3] = Color.Green;
                            place = 3;
                        }
                    }
                }
                for (int i = 0; i < colors.Length; i++)
                {
                    if (colors[i] == Color.Green)
                    {
                        photonR.X = lightR[i].X;
                        photonR.Y = lightR[i].Y;
                    }
                }
                if (energyDrawn > lSUCharge)
                    energyDrawn = 0;
                photonR.Width = (energyDrawn * 10) + 10;
                photonR.Height = (energyDrawn * 10) + 10;
                if (place == 0 && energyDrawn != 0 || place == 1 && energyDrawn != 0)
                    photonR.Y -= photonR.Width / 2;
                if (place == 2 && energyDrawn != 0 || place == 3 && energyDrawn != 0)
                    photonR.X -= photonR.Width / 2;
                lSUCharge -= energyDrawn;
                explosion.Width = (explosionCharge * 10) + 20;
                explosion.Height = (explosionCharge * 10) + 20;
                isFired = true;

                if (torpedoFire)
                    torpedoSFX.Play();
                else if (isFired)
                    baseSFX.Play();
                explosionTimer = 0;
            }
            if (isFired)
            {
                for (int i = 0; i < colors.Length; i++)
                    colors[i] = Color.Red;
                if (place == 0)
                {
                    photonR.X -= 10;
                    if (photonR.X <= borgR.X + 100)
                    {
                        isShot = true;
                        isFired = false;
                        score++;
                    }
                }
                if (place == 1)
                {
                    photonR.X += 10;
                    if (photonR.X >= borgR.X)
                    {
                        isShot = true;
                        isFired = false;
                        score++;
                    }
                }
                if (place == 2)
                {
                    photonR.Y -= 10;
                    if (photonR.Y <= borgR.Y + 100)
                    {
                        isShot = true;
                        isFired = false;
                        score++;
                    }
                }
                if (place == 3)
                {
                    photonR.Y += 10;
                    if (photonR.Y >= borgR.Y)
                    {
                        isShot = true;
                        isFired = false;
                        score++;
                    }
                }
            }
            if (photonR.X < 500 - (propelEnergy * 50) && !phasorFire || photonR.X > 500 + (propelEnergy * 50) && !phasorFire || photonR.Y < 500 - (propelEnergy * 50) && !phasorFire|| photonR.Y > 500 + (propelEnergy * 50) && !phasorFire)
            {
                isFired = false;
                if (torpedoFire)
                {
                    photonT = this.Content.Load<Texture2D>("Torpedo Mark XXV");
                    if (place == 0)
                    {
                        explosion.X = photonR.X;
                        explosion.Y = photonR.Y - (explosion.Height / 2);
                    }
                    else if (place == 1)
                    {
                        explosion.X = photonR.X - explosion.Width;
                        explosion.Y = photonR.Y - (explosion.Height / 2);

                    }
                    else if (place == 2)
                    {
                        explosion.X = photonR.X - (explosion.Width / 2);
                        explosion.Y = photonR.Y;
                    }
                    else if (place == 3)
                    {
                        explosion.X = photonR.X - (explosion.Width / 2);
                        explosion.Y = photonR.Y - explosion.Height;
                    }
                }
                
                explode = true;
            }
            if (phasorFire)
            {
                if (photonR.X < 0 || photonR.X > 1000 || photonR.Y < 0 || photonR.Y > 1000)
                {
                    phasorFire = false;
                }
            }
            else if (!phasorFire && !explode)
                photonT = this.Content.Load<Texture2D>("Photon Torpedo");
            if (explode)
                explosionTimer++;
            if (explosionTimer > 30)
            {
                torpedoFire = false;
                photonT = this.Content.Load<Texture2D>("Photon Torpedo");
                explode = false;
            }
                

            if (seconds > 0 && lSUCharge <= 97)
            {
                lSUCharge += 3;
                timer = 0;
            }
            if (seconds > 0 && lSUCharge == 99)
                lSUCharge++;
            laneTimer++;
            lSeconds = laneTimer / 60;

            if (!borgCanFire)
            {
                lane = random.Next(4);
            }
            
            if (lane == 0 && !borgCanFire && dissapearSeconds > random.Next(5))
            {
                borgR.X = random.Next(300);
                borgR.Y = 450;
                laneTimer = 0;
                bPhotonR.X = borgR.X;
                bPhotonR.Y = borgR.Y + 25;
                borgSFX.Play();
                borgCanFire = true;
                isShot = false;

            }
            else if (lane == 1&& !borgCanFire && dissapearSeconds > random.Next(5))
            {
                borgR.X = random.Next(300) + 600;
                borgR.Y = 450;
                laneTimer = 0;
                bPhotonR.X = borgR.X;
                bPhotonR.Y = borgR.Y + 25;
                borgCanFire = true;
                isShot = false;
                borgSFX.Play();

            }
            else if (lane == 2 &&!borgCanFire && dissapearSeconds > random.Next(5))
            {
                borgR.Y = random.Next(300);
                borgR.X = 450;
                laneTimer = 0;
                bPhotonR.X = borgR.X + 25;
                bPhotonR.Y = borgR.Y;
                borgCanFire = true;
                isShot = false;
                borgSFX.Play();

            }
            else if (lane == 3 &&!borgCanFire && dissapearSeconds > random.Next(5))
            {
                
                borgR.Y = random.Next(300) + 600;
                borgR.X = 450;
                laneTimer = 0;
                bPhotonR.X = borgR.X + 25;
                bPhotonR.Y = borgR.Y;
                borgCanFire = true;
                isShot = false;
                borgSFX.Play();

            }

            if (borgCanFire)
            {
                if (lane == 0)
                {
                    bPhotonR.X += 10;
                    if (bPhotonR.X > 500)
                    {
                        borgCanFire = false;
                        dissapearTimer = 0;
                    }
                }
                else if (lane == 1)
                {
                    bPhotonR.X -= 10;
                    if (bPhotonR.X < 500)
                    {
                        borgCanFire = false;
                        dissapearTimer = 0;
                    }
                } 
                else if (lane == 2)
                {
                    bPhotonR.Y += 10;
                    if (bPhotonR.Y > 500)
                    {
                        borgCanFire = false;
                        dissapearTimer = 0;
                    }
                }   
                else if (lane == 3)
                {
                    bPhotonR.Y -= 10;
                    if (bPhotonR.Y < 500)
                    {
                        borgCanFire = false;
                        dissapearTimer = 0;
                    }
                        
                }
                
                   
            }

            

            oldKB = kb;
            oldMouse = mouse;
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
            spriteBatch.Draw(photonT, bPhotonR, Color.White);
            if (! isShot)
                spriteBatch.Draw(borgT, borgR, Color.White);
                
            spriteBatch.Draw(starBaseT, starBaseR, Color.White);
            //spriteBatch.Draw(torpedoT, torpedoR, Color.White);
            if (explode)
                spriteBatch.Draw(explosionT, explosion, Color.White); 
            if (phasorFire && phasorPow <= 20)
                spriteBatch.Draw(explosionT, explosion, Color.Red);
            if (phasorFire && phasorPow <= 50)
                spriteBatch.Draw(explosionT, explosion, Color.Purple);
            if (phasorFire && phasorPow <= 75)
                spriteBatch.Draw(explosionT, explosion, Color.Blue);
            if (phasorFire && phasorPow <= 100)
                spriteBatch.Draw(explosionT, explosion, Color.Green);

            if (isFired)
                spriteBatch.Draw(photonT, photonR, Color.White);
            for (int i = 0; i < lightR.Length; i++)
                spriteBatch.Draw(lightT, lightR[i], colors[i]);
            //spriteBatch.Draw(lightT, box, Color.Gray);

            for (int i = 0; i < propelBar.Length; i++)
                spriteBatch.Draw(lightT, propelBar[i], Color.Gray);
            for (int i = 0; i < propelEnergy; i++)
                spriteBatch.Draw(lightT, propelBar[i], Color.White);

            for (int i = 0; i < energyBar.Length; i++)
                spriteBatch.Draw(lightT, energyBar[i], Color.Gray);
            for (int i = 0; i < energyDrawn; i++)
                spriteBatch.Draw(lightT, energyBar[i], Color.White);

            for (int i = 0; i < lsuBar.Length; i++)
                spriteBatch.Draw(lightT, lsuBar[i], Color.Gray);
            for (int i = 0; i < lSUCharge; i++)
                spriteBatch.Draw(lightT, lsuBar[i], Color.White);

            spriteBatch.DrawString(font, "LSU:\n" + lSUCharge, new Vector2(5, 440), Color.Black);
            spriteBatch.DrawString(font, "EC:\n" + energyDrawn, new Vector2(55, 590), Color.Black);
            spriteBatch.DrawString(font, "PE:\n" + propelEnergy, new Vector2(105, 590), Color.Black);
            spriteBatch.DrawString(font, "" + score, new Vector2(0, 0), Color.Black);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
