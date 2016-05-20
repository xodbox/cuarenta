using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Collections.Generic;
using System.IO;
using System;
using Cuarenta.Naipes;
using Cuarenta.CuarentaEngine;

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

        private SpriteFont font;
        private PartidaDeCuarenta partida;
        private int numJug;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            device = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, new PresentationParameters());
            graphics.PreferredBackBufferWidth = device.DisplayMode.Width - 30;
            graphics.PreferredBackBufferHeight = device.DisplayMode.Height - 90;
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
            numJug = 2;
            partida = new PartidaDeCuarenta(numJug);
            partida.IniciarPartida(numJug);

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
            font = Content.Load<SpriteFont>("msgFont");
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
            int i = 0, j = 0;

            spriteBatch.Begin();
            for (j = 0; j < numJug; j++)
            {
                i = 0;
                foreach (Naipe naipe in partida.Manos[j].NaipesEnGrupo)
                {
                    spriteBatch.DrawString(font, naipe.ToString(), new Vector2(j * 200, i * 20), Color.Black);
                    i++;
                }

            }

            i = 0;
            j = 0;
            foreach (Naipe naipe in partida.Mazo.NaipesEnGrupo)
            {
                spriteBatch.DrawString(font, naipe.ToString(), new Vector2(j * 200, 120 + (i % 10) * 20), Color.Black);
                i++;
                if (i % 10 == 0)
                    j++;
            }

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
