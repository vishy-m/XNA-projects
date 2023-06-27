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

namespace Just_a_piece
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle sprite = new Rectangle(200, 200, 100, 100);
        Texture2D spriteT;
        int spriteNum = 0;
        int i = 0;
        int spriteNum2 = 1;
        Rectangle sprite1 = new Rectangle(0, 0, 100, 100);
        Rectangle sprite2 = new Rectangle(100, 0, 100, 100);
        Rectangle sprite3 = new Rectangle(200, 0, 100, 100);
        Rectangle sprite4 = new Rectangle(300, 0, 100, 100);
        Rectangle[] sprites = new Rectangle[5];
        Rectangle sprite5 = new Rectangle(400, 0, 100, 100);
        int timer = 0;
        SpriteFont font;

        KeyboardState oldKb = Keyboard.GetState();

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
            sprites[0] = sprite1;
            sprites[1] = sprite2;
            sprites[2] = sprite3;
            sprites[3] = sprite4;
            sprites[4] = sprite5;
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
            spriteT = this.Content.Load<Texture2D>("SpriteSheet-1");
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
            timer++;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Right) && !oldKb.IsKeyDown(Keys.Right) && i < 4 && timer > 10)
            {
                i++;
                timer = 0;
            }
            else if (kb.IsKeyDown(Keys.Left) && !oldKb.IsKeyDown(Keys.Left) && i > 0 && timer > 10)
            {
                i--;
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
            //spriteBatch.Draw(spriteT, new Vector2(150, 150), sprite, Color.White, spriteNum,Vector2.Zero, spriteNum2, SpriteEffects.None, 1);
            spriteBatch.Draw(spriteT, sprite, sprites[i], Color.White);
            spriteBatch.DrawString(font, "sprite #" + (i + 1), new Vector2(200, 325), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
