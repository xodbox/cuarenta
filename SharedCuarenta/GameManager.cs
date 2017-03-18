using SharedCuarenta.CuarentaEngine;
using SharedCuarenta.Utilities;
using SharedCuarenta.Naipes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace SharedCuarenta
{
    /// <summary>
    /// Class to manage the whole game and to keep the game state
    /// </summary>
    class GameManager
    {
        #region Fields
        PartidaDeCuarenta partida;
        CardSlots cardSlots;
        MouseState oldMouseState;
        TouchCollection oldTouchCollection;
        int dataPlayer;
        int numJugadores;
        int thisPlayer;
        int playerPlaying;
        int thisTeam;
        static Random rnd = new Random();
        GameState gameState;
        HandState handState;

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor GameManager
        /// </summary>
        public GameManager(Rectangle windowSize)
        {
            partida = new PartidaDeCuarenta();
            cardSlots = new CardSlots(windowSize);
            oldMouseState = new MouseState();
            oldTouchCollection = new TouchCollection();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new Game
        /// </summary>
        /// <param name="numJugadores">Number of players</param>
        /// <param name="thisPlayer">which player is playing this game</param>
        public void newGame(int numJugadores, int thisPlayer)
        {
            if (numJugadores != 2 && numJugadores != 4)
                throw new ArgumentOutOfRangeException();

            this.numJugadores = numJugadores;
            this.thisPlayer = thisPlayer;
            if (numJugadores == 2)
                thisTeam = thisPlayer;
            else
                thisTeam = thisPlayer % 2;
            
            //choose who is goint to deal
            dataPlayer = rnd.Next(0, numJugadores);
            dataPlayer = 3;
            playerPlaying = dataPlayer + 1;
            if (playerPlaying >= numJugadores)
                playerPlaying = 0;

            // Begin game (shuffle and deal cards, choose which cards are face up. SIZE IS NOT ASSIGNED, determine if card is on game)
            partida.IniciarPartida(numJugadores, thisPlayer, dataPlayer);

            //assign slots to dealt cards
            int indexPlayer = thisPlayer;
            for(int i = 0; i < numJugadores; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Naipe card = partida.Manos[indexPlayer].NaipesEnGrupo[j];
                    card.CardSize = cardSlots.CardSize;
                    if(i == 1 || i == 3)
                        card.Rotated = true;
                    if (numJugadores == 4)
                    {
                        card.SetCenter(cardSlots.PlayerCardPosition[i, j]);
                        card.SlotAssigned = CardSlots.getGroupIndexHand(i, j);
                        cardSlots.UsedPlayerCardPosition[i, j] = true;
                    }
                    else
                    {
                        card.SetCenter(cardSlots.PlayerCardPosition[i * 2, j]);
                        cardSlots.UsedPlayerCardPosition[i * 2, j] = true;
                    }
                }
                indexPlayer++;
                if (indexPlayer >= numJugadores)
                    indexPlayer = 0;
            }

            //asign slots to cards to deal
            foreach (Naipe card in partida.Mazo.NaipesEnGrupo)
            {
                card.CardSize = cardSlots.CardSize;
                card.Rotated = true;
                card.SetCenter(cardSlots.ToDealCardPosition[0]);
                cardSlots.UsedToDealCardPosition[0] = true;
            }

            //asing size to score cards (perros)
            foreach (Naipe card in partida.Perros.NaipesEnGrupo)
            {
                card.CardSize = cardSlots.CardSize;
            }
            
            gameState = GameState.Playing;
            handState = HandState.NormalPlay;
        }

        /// <summary>
        /// Draw the game
        /// </summary>
        /// <param name="spriteBatch">sprite batch for the game</param>
        /// <param name="textures">all the textures used, stored in a dictionary</param>
        public void Draw(SpriteBatch spriteBatch, Dictionary<String, Texture2D> textures)
        {
            for (int i = 0; i < 2; i++)
                foreach (Naipe card in partida.Carton[i].NaipesEnGrupo)
                    card.Draw(spriteBatch, textures);
            for (int i = 0; i < numJugadores; i++)
                foreach (Naipe card in partida.Manos[i].NaipesEnGrupo)
                    card.Draw(spriteBatch, textures);
            foreach (Naipe card in partida.Mazo.NaipesEnGrupo)
                card.Draw(spriteBatch, textures);
            foreach (Naipe card in partida.NaipesEnMesa.NaipesEnGrupo)
                card.Draw(spriteBatch, textures);
            for (int i = 0; i < 2; i++)
                foreach (Naipe card in partida.Puntos[0].NaipesEnGrupo)
                    card.Draw(spriteBatch, textures);


        }

        public void Update (MouseState mouseState)
        {
            if (mouseState.LeftButton == ButtonState.Pressed && mouseState != oldMouseState)
                ProcessInput(mouseState.X, mouseState.Y);
            oldMouseState = mouseState;

        }

        public void Update(TouchCollection touchCollection)
        {
            if (touchCollection.Count > 0 && touchCollection[0] != oldTouchCollection[0])
                ProcessInput((int)touchCollection[0].Position.X, (int)touchCollection[0].Position.Y);
            oldTouchCollection[0] = touchCollection[0];
        }

        #endregion

        #region Private Methdos
        private void ProcessInput(int x, int y)
        {
            Rectangle rec = new Rectangle(0,0, 50, 50);

            if(gameState == GameState.Playing)
            {
                if(handState == HandState.NormalPlay)
                {
                    if(playerPlaying == thisPlayer)
                    {
                        partida.ProcessCardInHandClicked(new Point(x, y), thisPlayer);
                        if (!partida.ProcessCardInMesaClicked(new Point(x, y)) && !partida.oneInMesaTouched)
                        {
                            foreach (Naipe card in partida.Manos[thisPlayer].NaipesEnGrupo)
                            {
                                if (card.Selected)
                                {
                                    partida.ProcessMesaClicked(new Point(x, y), thisPlayer, cardSlots.TableLimits, card, cardSlots);
                                    break;
                                }
                            }
                        }
                        if(rec.Contains(x, y))
                        {
                            partida.MakeMove(thisPlayer, thisTeam, cardSlots);
                        }
                    }
                }
            }
        }
        #endregion

    }
}
