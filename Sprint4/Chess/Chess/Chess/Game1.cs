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

namespace Chess
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        string[] files;
        Texture2D[] Images = new Texture2D[19];

        int I = 0;
        int Index = 0;
        //int count = 0;
        int screenheight;
        int screenWidth;

        Rectangle[] bPieces = new Rectangle[8];
        Rectangle[] bPawns = new Rectangle[8];
        Rectangle[] wPieces = new Rectangle[8];
        Rectangle[] wPawns = new Rectangle[8];
        Rectangle board;

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
            
            files = Directory.GetFiles(@"Content\Assets", "*");
            foreach (var File in files)
            {
                string[] Temp;
                Console.WriteLine(File);
                Temp = File.Split('.');
                string NameMinus = Temp[0];
                Index = NameMinus.LastIndexOf('\\') + 1;
                NameMinus = NameMinus.Substring(Index);
                Console.WriteLine(NameMinus);
                files[I] = NameMinus;
                I++;
            }
            screenheight = GraphicsDevice.Viewport.Height;
            screenWidth = GraphicsDevice.Viewport.Width;

            board = new Rectangle(0, 0, screenWidth, screenheight);

            for (int i = 0; i < bPieces.Length; i++)
            {
                bPieces[i] = new Rectangle(20 + (100 * i), 10, 50, 50);
                bPawns[i] = new Rectangle(20 + (100 * i), 70, 50, 50);
                wPieces[i] = new Rectangle(20 + (100 * i), screenheight - 60, 50, 50);
                wPawns[i] = new Rectangle(20 + (100 * i), screenheight - 120, 50, 50);
            }
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
            for (I = 0; I < files.Length; I++)
                Images[I] = Content.Load<Texture2D>(@"Assets\\" + files[I]);
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
            spriteBatch.Draw(Images[18], board, Color.White);
            for (int count = 0, piecesCount = 0; count < Images.Length; count++)
            {
                if (count < 8)
                {
                    spriteBatch.Draw(Images[count], bPieces[count], Color.White);
                }
                else if (count == 8)
                {
                    for (int i = 0; i < bPawns.Length; i++)
                        spriteBatch.Draw(Images[8], bPawns[i], Color.White);
                }
                else if (count < 17)
                {
                    spriteBatch.Draw(Images[count], wPieces[piecesCount], Color.White);
                    piecesCount++;
                }
                else if (count == 17)
                {
                    for (int i = 0; i < wPawns.Length; i++)
                        spriteBatch.Draw(Images[17], wPawns[i], Color.White);
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
