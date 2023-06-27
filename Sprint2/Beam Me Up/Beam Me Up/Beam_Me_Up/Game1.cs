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

namespace Beam_Me_Up
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        String o1 = "Up Arrow";
        String o2 = "Down Arrow";
        String o3 = "Right Arrow";
        String o4 = "Left Arrow";
        String o5 = "Space Bar";
        KeyboardState oldKB;
        SoundEffect c1;
        SoundEffect c2;
        SoundEffect c3;
        SoundEffect c4;
        SoundEffect c5;
        Rectangle[] rects = { new Rectangle(0, 300, 100, 100), new Rectangle(175, 300, 100, 100), new Rectangle(350, 300, 100, 100), new Rectangle(525, 300, 100, 100), new Rectangle(700, 300, 100, 100) };
        Texture2D soundT, starT;
        Rectangle starR = new Rectangle(250, 50, 300, 200);
        SpriteFont font;


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
            oldKB = Keyboard.GetState();
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
            c1 = this.Content.Load<SoundEffect>("o1");
            c2 = this.Content.Load<SoundEffect>("o2");
            c3 = this.Content.Load<SoundEffect>("o3");
            c4 = this.Content.Load<SoundEffect>("o4");
            c5 = this.Content.Load<SoundEffect>("o5");
            soundT = this.Content.Load<Texture2D>("Sound");
            starT = this.Content.Load<Texture2D>("star");
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


            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Up) && !oldKB.IsKeyDown(Keys.Up))
                c1.Play();
            if (kb.IsKeyDown(Keys.Down) && !oldKB.IsKeyDown(Keys.Down))
                c2.Play();
            if (kb.IsKeyDown(Keys.Right) && !oldKB.IsKeyDown(Keys.Right))
                c3.Play();
            if (kb.IsKeyDown(Keys.Left) && !oldKB.IsKeyDown(Keys.Left))
                c4.Play();
            if (kb.IsKeyDown(Keys.Space) && !oldKB.IsKeyDown(Keys.Space))
                c5.Play();

            oldKB = kb;
                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(starT, starR, Color.White);
            for (int i = 0; i < rects.Length; i++)
            {
                spriteBatch.Draw(soundT, rects[i], Color.White);
            }
            spriteBatch.DrawString(font, o1, new Vector2(0, 275), Color.White);
            spriteBatch.DrawString(font, o2, new Vector2(175, 275), Color.White);
            spriteBatch.DrawString(font, o3, new Vector2(350, 275), Color.White);
            spriteBatch.DrawString(font, o4, new Vector2(525, 275), Color.White);
            spriteBatch.DrawString(font, o5, new Vector2(700, 275), Color.White);
            spriteBatch.End();
            //spriteBatch.DrawString();
            base.Draw(gameTime);
        }
    }
}
