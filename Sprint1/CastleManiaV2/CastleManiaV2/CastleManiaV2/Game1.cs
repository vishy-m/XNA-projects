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

namespace CastleManiaV2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle cityRect;
        Rectangle dCityRect;
        Rectangle ghostKnightRect;
        Rectangle kingRect;
        Rectangle knightRect;
        Rectangle dKnightRect;
        Rectangle sKingRect;
        Rectangle gKnight2Rect;
        Rectangle swordRect;
        Rectangle plainsRect;
        Rectangle kSwordRect;

        Texture2D cityText;
        Texture2D dCityText;
        Texture2D ghostKnightText;
        Texture2D kingText;
        Texture2D knightText;
        Texture2D dKnightText;
        Texture2D sKingText;
        Texture2D gKnight2Text;
        Texture2D swordText;
        Texture2D plainsText;
        Texture2D kSwordText;

        int timer;
        int seconds;

        SpriteFont font;
        SpriteFont narratorFont;

        String kingWords;
        String knightWords;

        Vector2 knightTextLoc;
        Vector2 kingTextLoc;

        Boolean ghostIsAtEdge;
        Boolean ghostIsAtEdge2;
        Boolean sceneOver1;
        Boolean sceneOver2;
        Boolean SceneOverBattle;
        Boolean kingOutOfScene;
        Boolean kinginPos;
        Boolean knightInPos;

        Color clearColor;



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

            cityRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            dCityRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            ghostKnightRect = new Rectangle(-200, 300, 100, 100);
            kingRect = new Rectangle(350, 200, 100, 100);
            knightRect = new Rectangle(75, 250, 250, 250);
            dKnightRect = new Rectangle(400, 300, 200, 200);
            sKingRect = new Rectangle(360, 175, 100, 100);
            gKnight2Rect = new Rectangle(-800, 325, 150, 150);
            swordRect = new Rectangle(100, 100, 200, 200);
            plainsRect = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            kSwordRect = new Rectangle(0, 0, 100, 100);

            timer = 0;
            seconds = 0;

            clearColor = Color.Black;

            knightTextLoc = new Vector2(100, 100);
            kingTextLoc = new Vector2(200, 200);

            kingWords = "";
            knightWords = "";

            ghostIsAtEdge = false;
            ghostIsAtEdge2 = false;
            sceneOver1 = true;
            sceneOver2 = false;
            SceneOverBattle = false;
            kinginPos = false;
            kingOutOfScene = false;
            knightInPos = false;

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
            cityText = this.Content.Load<Texture2D>("throneRoom");
            dCityText = this.Content.Load<Texture2D>("DarkThroneRoom");
            ghostKnightText = this.Content.Load<Texture2D>("GhostKnight");
            kingText = this.Content.Load<Texture2D>("King");
            knightText = this.Content.Load<Texture2D>("Knight");
            dKnightText = this.Content.Load<Texture2D>("Knight2");
            sKingText = this.Content.Load<Texture2D>("skullKing");
            gKnight2Text = this.Content.Load<Texture2D>("GKnight2");
            plainsText = this.Content.Load<Texture2D>("plains");
            swordText = this.Content.Load<Texture2D>("Sword");
            kSwordText = this.Content.Load<Texture2D>("kSword");

            font = this.Content.Load<SpriteFont>("SpriteFont1");
            narratorFont = this.Content.Load<SpriteFont>("SpriteFont2");
            

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

            if (ghostKnightRect.X == 1000)
                ghostIsAtEdge = true;
            if (ghostKnightRect.X == -200)
                ghostIsAtEdge = false;

            if (ghostIsAtEdge == false)
                ghostKnightRect.X++;
            if (ghostIsAtEdge == true)
                ghostKnightRect.X--;

            if (gKnight2Rect.X == 1600)
                ghostIsAtEdge2 = true;
            if (gKnight2Rect.X == -800)
                ghostIsAtEdge2 = false;

            if (ghostIsAtEdge2 == false)
                gKnight2Rect.X++;
            if (ghostIsAtEdge2 == true)
                gKnight2Rect.X--;

            if (seconds < 15)
            {
                sceneOver1 = true;
                if (seconds < 5)
                {
                    kingWords = "I need you to take care of the SkullKnight";
                    kingTextLoc = new Vector2(200, 150);
                }
                else if (seconds < 10)
                {
                    kingWords = "yes my king";
                    kingTextLoc = new Vector2(150, 225);
                }
                else if (seconds < 15)
                {
                    kingWords = "He's threatening our territory.";
                    kingTextLoc = new Vector2(250, 150);
                }
            }
            else if (seconds < 32)
            {
                sceneOver1 = false;
                sceneOver2 = true;
                if (seconds < 22)
                {
                    kingWords = "We will be taking over the red Kingdom";
                    kingTextLoc = new Vector2(200, 150);
                }
                else if (seconds < 27)
                {
                    kingWords = "yes my king, I can't wait";
                    kingTextLoc = new Vector2(425, 275);
                }
                else if (seconds < 32)
                {
                    kingWords = "We will be starting tommorow";
                    kingTextLoc = new Vector2(250, 150);
                }

            }
            else if (seconds < 34)
            {
                sceneOver2 = false;
                clearColor = Color.Black;
                kingTextLoc = new Vector2(150, 150);
                kingWords = "The Next Day";
            }
            else if (seconds < 64)
            {

                if (seconds == 34)
                {
                    kingRect = new Rectangle(100, 100, 100, 100);
                    sKingRect = new Rectangle(600, 100, 100, 100);
                    knightRect = new Rectangle(200, 200, 200, 200);
                    dKnightRect = new Rectangle(400, 200, 200, 200);
                }
                SceneOverBattle = true;
                clearColor = Color.CornflowerBlue;



                if (seconds < 39)
                {
                    kingWords = "That's Him!";
                    kingTextLoc = new Vector2(75, 75);
                }
                else if (seconds < 44)
                {
                    kingWords = "I shall conquer him";
                    kingTextLoc = new Vector2(200, 175);
                }
                else if (seconds < 49)
                {
                    kingWords = "SEIGE!";
                    kingTextLoc = new Vector2(600, 75);
                }
                else if (seconds < 59)
                {
                    kingWords = "Clash!";
                    kingTextLoc = new Vector2(400, 175);
                    if (seconds < 52)
                    {
                        if (kingOutOfScene == false)
                            kingRect.X -= 2;
                        else
                            kingRect.X = 900;
                        if (kingRect.X == -100)
                            kingOutOfScene = true;
                    }
                    else if (seconds < 59)
                    {
                        if (kinginPos == false)
                            kingRect.X--;
                        if (kingRect.X == 700)
                            kinginPos = true;
                        kSwordRect.X = kingRect.X - 50;
                        kSwordRect.Y = kingRect.Y;
                    }
                    if (seconds < 56)
                    {
                        if (knightInPos == false)
                        {
                            knightRect.X++;
                            dKnightRect.X--;
                        }
                        else
                        {
                            knightRect.X--;
                            dKnightRect.X++;
                        }
                        if (knightRect.X == 300)
                            knightInPos = true;
                        if (knightRect.X == 275)
                            knightInPos = false;
                    }
                    else if (seconds < 59)
                    {
                        if (knightRect.X != 200)
                            knightRect.X--;
                        swordRect.X = knightRect.X + 100;
                        swordRect.Y = knightRect.Y;
                    }
                }
                else if (seconds < 64)
                {
                    if (seconds == 59)
                    {
                        kingRect.X = 690;
                        kSwordRect.X = 690 - 50;
                    }

                    kingWords = "GAH! NOO!";
                    knightWords = "NOO!";
                    knightTextLoc = new Vector2(dKnightRect.X, dKnightRect.Y - 25);
                    knightTextLoc = new Vector2(sKingRect.X, sKingRect.Y - 25);
                }
                if (seconds > 40)
                {
                    if (seconds < 42)
                        clearColor = Color.CornflowerBlue;
                    else if (seconds < 44)
                        clearColor = Color.LightBlue;
                    else if (seconds < 46)
                        clearColor = Color.Blue;
                    else if (seconds < 48)
                        clearColor = Color.DarkBlue;
                    else if (seconds < 50)
                        clearColor = Color.DarkGray;
                    else if (seconds < 52)
                        clearColor = Color.Black;
                    else if (seconds < 54)
                        clearColor = Color.Black;
                    else if (seconds < 56)
                        clearColor = Color.DarkGray;
                    else if (seconds < 58)
                        clearColor = Color.DarkBlue;
                    else if (seconds < 60)
                        clearColor = Color.Blue;
                    else if (seconds < 62)
                        clearColor = Color.LightBlue;
                    else if (seconds < 64)
                        clearColor = Color.CornflowerBlue;
                }

            }
            else if (seconds < 70)
            {
                SceneOverBattle = false;
                kingWords = "CONQUERED";
                kingTextLoc = new Vector2(125, 100);
            }
            else if (seconds < 76)
            {
                kingWords = "VICTORY / GROWTH";
                kingTextLoc = new Vector2(25, 75);
            }
            else if (seconds > 75)
            {
                kingWords = "The End";
                kingTextLoc = new Vector2(150, 150);
                clearColor = Color.Black;
            }
                

            base.Update(gameTime);
        }

        /// <summary>
        ///  This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(clearColor);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (sceneOver1 == true)
            {
                spriteBatch.Draw(cityText, cityRect, Color.White);

                if (seconds % 5 == 0)
                    spriteBatch.Draw(ghostKnightText, ghostKnightRect, Color.White);
                if (seconds % 4 == 0)
                    spriteBatch.Draw(gKnight2Text, gKnight2Rect, Color.White);

                spriteBatch.Draw(kingText, kingRect, Color.White);
                spriteBatch.Draw(knightText, knightRect, Color.White);

                spriteBatch.DrawString(font, kingWords, kingTextLoc, Color.White);
            }
            else if (sceneOver2 == true)
            {
                spriteBatch.Draw(dCityText, dCityRect, Color.White);

                if (seconds % 5 == 0)
                    spriteBatch.Draw(ghostKnightText, ghostKnightRect, Color.White);
                if (seconds % 4 == 0)
                    spriteBatch.Draw(gKnight2Text, gKnight2Rect, Color.White);

                spriteBatch.Draw(sKingText, sKingRect, Color.White);
                spriteBatch.Draw(dKnightText, dKnightRect, Color.White);

                spriteBatch.DrawString(font, kingWords, kingTextLoc, Color.White);
            }
            else if (seconds >= 32 && seconds < 34)
                spriteBatch.DrawString(narratorFont, kingWords, kingTextLoc, Color.White);
            else if (SceneOverBattle == true)
            {
                spriteBatch.Draw(plainsText, plainsRect, Color.White);

                spriteBatch.Draw(kingText, kingRect, Color.White);
                spriteBatch.Draw(knightText, knightRect, Color.White);

                if (seconds > 52)
                    spriteBatch.Draw(kSwordText, kSwordRect, Color.White);
                if (seconds > 56)
                    spriteBatch.Draw(swordText, swordRect, Color.White);

                spriteBatch.Draw(sKingText, sKingRect, Color.White);
                spriteBatch.Draw(dKnightText, dKnightRect, Color.White);

                spriteBatch.DrawString(font, kingWords, kingTextLoc, Color.White);
                spriteBatch.DrawString(font, knightWords, knightTextLoc, Color.White);


                if (seconds % 5 == 0)
                    spriteBatch.Draw(ghostKnightText, ghostKnightRect, Color.White);
                if (seconds % 4 == 0)
                    spriteBatch.Draw(gKnight2Text, gKnight2Rect, Color.White);
            }
            else if (seconds < 70 && SceneOverBattle == false)
            {
                spriteBatch.DrawString(narratorFont, kingWords, kingTextLoc, Color.White);
                spriteBatch.Draw(kSwordText, kSwordRect, Color.White);
                spriteBatch.Draw(swordText, swordRect, Color.White);
                spriteBatch.Draw(sKingText, sKingRect, Color.White);
                spriteBatch.Draw(dKnightText, dKnightRect, Color.White);
            }
            else if (seconds < 76)
            {
                spriteBatch.DrawString(narratorFont, kingWords, kingTextLoc, Color.White);
                spriteBatch.Draw(kingText, kingRect, Color.White);
                spriteBatch.Draw(knightText, knightRect, Color.White);
            }
            else if (seconds > 75)
                spriteBatch.DrawString(narratorFont, kingWords, kingTextLoc, Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
