using Cuarenta.CuarentaEngine;
using Cuarenta.Enums;
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

        #endregion

        #region Constructors
        /// <summary>
        /// Constructor GameManager
        /// </summary>
        public GameManager(Rectangle windowSize)
        {
            partida = new PartidaDeCuarenta();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j<5; j++)
                {
                    partida.Manos[i].NaipesEnGrupo[j].CardSize = getCardSize(windowSize);
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Create a new Game
        /// </summary>
        /// <param name="numJugadores">Number of players</param>
        public void newGame(int numJugadores)
        {
            partida.IniciarPartida(numJugadores);
            if(numJugadores == 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    partida.Manos[0].NaipesEnGrupo[i].CardSize = 
                }
            }
            gameState = GameState.Playing;
        }

        public void ProcessClick(Vector2 clickPosition)
        {

        }

        #endregion

        #region Private Methdos
        private Rectangle getCardSize(Rectangle windowSize)
        {
            columnSize = 
        }
        #endregion

    }
}
