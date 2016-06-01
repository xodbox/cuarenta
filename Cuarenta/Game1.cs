using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;
using System;
using SharedCuarenta;
using SharedCuarenta.Utilities;

namespace Cuarenta
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GraphicsDevice device;
        GameManager gameManager;
        Dictionary<String, Texture2D> texturas;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, new PresentationParameters());
            //graphics.PreferredBackBufferWidth = device.DisplayMode.Width - 30;
            //graphics.PreferredBackBufferHeight = device.DisplayMode.Height - 90;

            graphics.PreferredBackBufferWidth = Constants.WinWindowSizeX;
            graphics.PreferredBackBufferHeight = Constants.WinWindowSizeY;

            texturas = new Dictionary<String, Texture2D>();
            gameManager = new GameManager(new Rectangle(0, 0, Constants.WinWindowSizeX, Constants.WinWindowSizeY));
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
            this.IsMouseVisible = true;
            gameManager.newGame(4, 0);

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
            
            //load textures for cards
            foreach (CardPalo palo in Enum.GetValues(typeof(CardPalo)))
            {
                string paloString = "";
                if (palo == CardPalo.Brillo)
                    paloString = "graficos/naipes/cardDiamonds";
                else if (palo == CardPalo.Corazon)
                    paloString = "graficos/naipes/cardHearts";
                else if (palo == CardPalo.CorazonNegro)
                    paloString = "graficos/naipes/cardSpades";
                else if (palo == CardPalo.Trebol)
                    paloString = "graficos/naipes/cardClubs";

                for (int i = 0; i < 13; i++)
                {
                    if (i == 0)
                        texturas.Add(Enum.GetName(typeof(CardRank), i) + palo.ToString(), Content.Load<Texture2D>(paloString + "A"));
                    else if (i > 0 && i <= 9)
                        texturas.Add(Enum.GetName(typeof(CardRank), i) + palo.ToString(), Content.Load<Texture2D>(paloString + (i + 1)));
                    else if (i == 10)
                        texturas.Add(Enum.GetName(typeof(CardRank), i) + palo.ToString(), Content.Load<Texture2D>(paloString + "J"));
                    else if (i == 11)
                        texturas.Add(Enum.GetName(typeof(CardRank), i) + palo.ToString(), Content.Load<Texture2D>(paloString + "Q"));
                    else if (i == 12)
                        texturas.Add(Enum.GetName(typeof(CardRank), i) + palo.ToString(), Content.Load<Texture2D>(paloString + "K"));
                }
            }
            texturas.Add("back", Content.Load<Texture2D>("graficos/naipes/cardBack_blue3"));
            texturas.Add("frame_red", Content.Load<Texture2D>("graficos/otros/frame_red"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            gameManager.Update(Mouse.GetState());
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            gameManager.Draw(spriteBatch, texturas);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
