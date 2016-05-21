#region description
//----------------------------------
//Implements Intialize and Update common to all the platforms
//
//
//-----------------------------------
#endregion

#region using 
using System;
using System.Collections.Generic;
using System.Text;

using Cuarenta.CuarentaEngine;
using Cuarenta.Naipes;
using Microsoft.Xna.Framework;

#endregion

namespace SharedCuarenta
{
    class GameManager : GameComponent
    {
        #region fields
        enum GameState
        {
            InitGame,
            ToPlaying,
            Playing
        }

        PartidaDeCuarenta partida;
        GameState gameState;

        #endregion

        #region Properties
        

        #endregion

        #region Initialization
        public GameManager(Game game)
            : base(game)
        {
            partida = new PartidaDeCuarenta();
        }

        public override void Initialize()
        {
            gameState = GameState.InitGame;

            base.Initialize();
        }

        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        {
            if(gameState == GameState.InitGame)
            {

            }

            base.Update(gameTime);
        }

        #endregion
    }
}
