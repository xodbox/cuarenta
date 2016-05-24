using Cuarenta.CuarentaEngine;
using Cuarenta.Enums;
using Cuarenta.Naipes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedCuarenta
{
    /// <summary>
    /// Class to manage the whole game and to keep the game state
    /// </summary>
    class GameManager
    {
        #region Fields
        GameState gameState;
        PartidaDeCuarenta partida;
        CardSlots cardSlots;
        static Random rnd = new Random();
        int dataPlayer;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor GameManager
        /// </summary>
        public GameManager(Rectangle windowSize)
        {
            partida = new PartidaDeCuarenta();
            cardSlots = new CardSlots(windowSize);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new Game
        /// </summary>
        /// <param name="numJugadores">Number of players</param>
        /// <param name="thisPlayer">which player is playing this game</param>
        /// <param name="dataPlayer">which player is going to deal</param>
        public void newGame(int numJugadores, int thisPlayer)
        {
            if (numJugadores != 2 && numJugadores != 4)
                throw new ArgumentOutOfRangeException();

            //choose who is goint to deal
            dataPlayer = rnd.Next(0, numJugadores);

            // Begin game (shuffle and deal cardas, choose which cards are face up. SIZE IS NOT ASSIGNED, determine if card is on game)
            partida.IniciarPartida(numJugadores, thisPlayer, dataPlayer);

            //assign slots to dealt cards
            int indexPlayer = thisPlayer;
            for(int i = 0; i < numJugadores; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (numJugadores == 4)
                    {
                        partida.Manos[indexPlayer].NaipesEnGrupo[j].SetCenter(cardSlots.PlayerCardPosition[i, j]);
                        cardSlots.UsedPlayerCardPosition[i, j] = true;
                    }
                    else
                    {
                        partida.Manos[indexPlayer].NaipesEnGrupo[j].SetCenter(cardSlots.PlayerCardPosition[i * 2, j]);
                        cardSlots.UsedPlayerCardPosition[i * 2, j] = true;
                    }
                    partida.Manos[indexPlayer].NaipesEnGrupo[j].CardSize = cardSlots.CardSize;
                }
                indexPlayer++;
                if (indexPlayer >= numJugadores)
                    indexPlayer = 0;
            }

            //asign slots to cards to deal
            foreach (Naipe card in partida.Mazo.NaipesEnGrupo)
            {
                card.SetCenter(cardSlots.ToDealCardPosition[0]);
                card.CardSize = cardSlots.CardSize;
                cardSlots.UsedToDealCardPosition[0] = true;
            }

            //asing size to score cards (perros)
            foreach (Naipe card in partida.Perros.NaipesEnGrupo)
            {
                card.CardSize = cardSlots.CardSize;
            }
            gameState = GameState.Playing;
        }

        public void ProcessClick(Vector2 clickPosition)
        {

        }

        #endregion

        #region Private Methdos
        #endregion

    }
}
