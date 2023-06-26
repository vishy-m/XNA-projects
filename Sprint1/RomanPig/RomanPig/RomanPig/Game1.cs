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


namespace RomanPig
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle pigRect;
        Rectangle pigRect2;
        Texture2D pigText;
        Texture2D pigText2;
        String text;
        int timer;
        Vector2 pigLoc;
        Vector2 englishLoc;
        SpriteFont font;
        Boolean breakLoop;
        int count;
        int seconds;
        Rectangle boxRect;
        Texture2D boxTexture;

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
            pigRect = new Rectangle(100, 300, 150, 100);
            pigRect2 = new Rectangle(500, 300, 115, 100);
            pigLoc = new Vector2(500, 250);
            englishLoc = new Vector2(100, 250);
            boxRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            text = "hello";
            timer = 0;
            breakLoop = false;
            
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
            pigText = this.Content.Load<Texture2D>("pig1");
            pigText2 = this.Content.Load<Texture2D>("pig2");
            font = this.Content.Load<SpriteFont>("SpriteFont1");
            boxTexture = new Texture2D(GraphicsDevice, 1, 1);
            boxTexture.SetData(new[] { Color.Black });
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
            timer++;
            seconds = timer / 60;
            if (seconds < 4)
                text = "hello";
            else if (seconds < 10)
                text = "I am a pig";
            else if (seconds < 17)
                text = "Have a great day";
            else if (seconds < 25)
                text = "ok bye";
            else if (seconds < 30)
                text = ".";
            else if (seconds < 38)
                text = "why are you still here";
            else if (seconds < 46)
                text = "You need to get out of here";
            else if (seconds < 54)
                text = "you shouldn't be here";
            else if (seconds < 55)
                text = "...... He";
            else if (seconds < 56)
                text = "...... He i";
            else if (seconds < 57)
                text = "...... He is";
            else if (seconds < 58)
                text = "...... He is c ";
            else if (seconds < 59)
                text = "...... He is co";
            else if (seconds < 60)
                text = "...... He is com";
            else if (seconds < 61)
                text = "...... He is comi";
            else if (seconds < 62)
                text = "...... He is comin";
            else if (seconds < 63)
                text = "...... He is coming";
            else if (seconds < 70)
                text = "...... He is coming";
            else if (seconds < 71)
                text = "R";
            else if (seconds < 72)
                text = "Ru";
            else if (seconds < 80)
                text = "RUN";


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
            if (seconds < 80)
            {
                spriteBatch.Draw(pigText, pigRect, Color.White);
                spriteBatch.Draw(pigText2, pigRect2, Color.White);
                spriteBatch.DrawString(font, translate(text), pigLoc, Color.White);
                spriteBatch.DrawString(font, text, englishLoc, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public String translate(String s)
        {
            String translated = "";
            char[] vowels = { 'a', 'A', 'e', 'E', 'i', 'I', 'o', 'O', 'u', 'U' };
            String[] words = s.Split(' ');
            
            for (int k = 0; k < words.Length ; k++)
            {
                for (int i = 0; i < words[k].Length; i++)
                {
                    for (int j = 0; j < vowels.Length; j++)
                    {
                        if (words[k][0] == vowels[j])
                        {
                            translated += words[k] + "way";
                            breakLoop = true;
                            break;
                        }
                        else if (words[k][i] == vowels[j])
                        {
                            translated += words[k].Substring(i) + words[k].Substring(0, i) + "ay";
                            breakLoop = true;
                            break;
                        }
                    }
                    if (breakLoop == true)
                    {
                        breakLoop = false;
                        break;
                    }
                    
                    
                }
                translated += " ";
            }

            return translated;
        }
    }
}
