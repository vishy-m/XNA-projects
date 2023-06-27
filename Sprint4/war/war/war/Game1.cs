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

namespace war
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        string[] files;
        Texture2D[] Images = new Texture2D[52];
        Card[] cards = new Card[52];
        int I = 0;
        int place1;
        int place2;
        Boolean Shuffled = false;
        List<Card> cardsL = new List<Card>();
        Random random = new Random();
        Rectangle pos1 = new Rectangle(50, 25, 200, 200);
        Rectangle pos2 = new Rectangle(50, 275, 200, 200);

        SpriteFont font;

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

            files = Directory.GetFiles(@"Content\Assets", "*");

            place1 = random.Next(cards.Length);
            place2 = random.Next(cards.Length);

            for (int r = 0; r <  cards.Length; r++)
            {
                cards[r] = new Card();
            }

            int Index = 0;
            //int I = 0;
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
                char[] chars = NameMinus.ToCharArray();
                String suit = "";
                if (chars[0] == 's')
                    suit = "Spades";
                else if (chars[0] == 'd')
                    suit = "Diamonds";
                else if (chars[0] == 'c')
                    suit = "Clovers";
                else if (chars[0] == 'h')
                    suit = "Hearts";
                cards[I].setSV(suit, Convert.ToInt32(chars[1] + "" + chars[2]));
                I++;
            }

            for (int i = 0; i < cards.Length; i++)
                cardsL.Add(cards[i]);

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
            //if (!Shuffled)
                //Images = Shuffle_Cards(Images[0], texts).ToArray();
            for (I = 0; I < files.Length; I++)
            {
                Images[I] = Content.Load<Texture2D>(@"Assets\\" + files[I]);
                cards[I].setT(Images[I]);
            }
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
            int seconds = gameTime.TotalGameTime.Seconds;
            if (seconds % 5 == 0)
            {
                Shuffled = false;
                seconds++;
            }
            if (!Shuffled)
                cardsL = Shuffle_Cards<Card>(cards[0], cardsL);
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

            String value1 = "" + cardsL[0].getV();
            String value2 = "" + cardsL[1].getV();
            int v = cardsL[0].getV();
            int v2 = cardsL[1].getV();

            if (v == 1)
                value1 = "Ace";
            else if (v == 11)
                value1 = "Jack";
            else if (v == 12)
                value1 = "Queen";
            else if (v == 13)
                value1 = "King";

            if (v2 == 1)
                value2 = "Ace";
            else if (v2 == 11)
                value2 = "Jack";
            else if (v2 == 12)
                value2 = "Queen";
            else if (v2 == 13)
                value2 = "King";

            spriteBatch.Draw(cardsL[0].getT(), pos1, Color.White);
            spriteBatch.DrawString(font, value1 + " of " + cardsL[0].getS(), new Vector2(300, 125), Color.White);
            spriteBatch.Draw(cardsL[1].getT(), pos2, Color.White);
            spriteBatch.DrawString(font, value2 + " of " + cardsL[1].getS(), new Vector2(300, 370), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public List<T> Shuffle_Cards<T>(T Value, List<T> CList)
        {
            // Local Vars
            int I, R;
            bool Flag;
            Random Rand = new Random();
            // Local List of T type
            var CardList = new List<T>();
            // Build Local List as big as passed in list and fill it with default value
            for (I = 0; I < CList.Count; I++)
                CardList.Add(Value);
            // Shuffle the list of cards
            for (I = 0; I < CList.Count; I++)
            {
                Flag = false;
                // Loop until an empty spot is found
                do
                {
                    R = Rand.Next(0, CList.Count);
                    if (CardList[R].Equals(Value))
                    {
                        Flag = true;
                        CardList[R] = CList[I];
                    }
                } while (!Flag);
            }
            // Set global var Shuffled to true
            Shuffled = true;
            // Return the shuffled list
            return CardList;
        }
    }

    public class Card : Microsoft.Xna.Framework.Game
    {
        String suit;
        int value;
        Texture2D texture;

        public Card()
        {
            value = 0;
            suit = "";
            texture = null;
        }

        public void setCard(String s, int val, Texture2D text)
        {
            suit = s;
            value = val;
            texture = text;
        }
        public void setSV(String s, int val)
        {
            suit = s;
            value = val;
        }
        public void setT(Texture2D text)
        {
            texture = text;
        }

        public String getS() { return suit; }
        public int getV() { return value; }
        public Texture2D getT() { return texture; }

    }
}
