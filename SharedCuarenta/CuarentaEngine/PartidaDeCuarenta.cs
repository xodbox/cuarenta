using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuarenta.Naipes;
using Cuarenta.Enums;

namespace Cuarenta.CuarentaEngine
{
    class PartidaDeCuarenta
    {
        #region Fileds
        public GrupoDeNaipes[] Manos { get; }
        public GrupoDeNaipes NaipesEnMesa { get; }
        public GrupoDeNaipes Mazo { get; }
        public GrupoDeNaipes Perros { get; }
        public GrupoDeNaipes[] Carton { get; }
        public GrupoDeNaipes[] Puntos { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of the elements of a Cuarenta Match
        /// </summary>
        public PartidaDeCuarenta()
        {
            Manos = new GrupoDeNaipes[4];
            NaipesEnMesa = new GrupoDeNaipes();
            Mazo = new GrupoDeNaipes();
            Perros = new GrupoDeNaipes();
            Carton = new GrupoDeNaipes[2];
            Puntos = new GrupoDeNaipes[2];
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Begin a Cuarenta Match, clearing everything, shuffleing the deck and dealing the first hand.
        /// </summary>
        /// <param name="numeroDeJugadores">Number of players in the match</param>
        /// <param name="thisPlayer">Which player is this</param>
        /// <param name="dataPlayer">Which player is dealing</param>
        public void IniciarPartida(int numeroDeJugadores, int thisPlayer, int dataPlayer)
        {
            if (numeroDeJugadores != 2 && numeroDeJugadores != 4)
                throw new ArgumentOutOfRangeException();

            Perros.NaipesEnGrupo.Clear();

            foreach (CardPalo palo in Enum.GetValues(typeof(CardPalo)))
            {
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                {
                    if (rank > CardRank.Siete && rank < CardRank.Jota)
                    {
                        Perros.AnadirArriba(new Naipe(rank, palo));
                        Perros.NaipesEnGrupo[Mazo.NaipesEnGrupo.Count - 1].faceUp = false;
                        Perros.NaipesEnGrupo[Mazo.NaipesEnGrupo.Count - 1].onGame = false;
                    }                        
                }
            }
            Perros.Barajar();
            IniciarNuevaData(numeroDeJugadores, thisPlayer, dataPlayer);
        }

        public void IniciarNuevaData(int numeroDeJugadores, int thisPlayer, int dataPlayer)
        {
            if (numeroDeJugadores != 2 && numeroDeJugadores != 4)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < 4; i++)
            {
                Manos[i].NaipesEnGrupo.Clear();
            }
            NaipesEnMesa.NaipesEnGrupo.Clear();
            Mazo.NaipesEnGrupo.Clear();

            foreach (CardPalo palo in Enum.GetValues(typeof(CardPalo)))
            {
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                {
                    if (rank <= CardRank.Siete || rank >= CardRank.Jota)
                    {
                        Mazo.AnadirArriba(new Naipe(rank, palo));
                        Mazo.NaipesEnGrupo[Mazo.NaipesEnGrupo.Count - 1].faceUp = false;
                        Mazo.NaipesEnGrupo[Mazo.NaipesEnGrupo.Count - 1].onGame = true;
                    }
                }
            }
            Mazo.Barajar();
            Repartir(numeroDeJugadores, thisPlayer, dataPlayer);
        }

        /// <summary>
        /// Deals a hand of Cuarenta
        /// </summary>
        /// <param name="numeroDeJugadores">Numbers of players in the Match</param>
        /// <param name="thisPlayer">Which player is this</param>
        /// <param name="dataPlayer">Which player is dealing</param>
        public void Repartir(int numeroDeJugadores, int thisPlayer, int dataPlayer)
        {
            if (numeroDeJugadores != 2 && numeroDeJugadores != 4)
                throw new ArgumentOutOfRangeException();

            int indexPlayer = dataPlayer + 1;
            for (int i = 0; i < numeroDeJugadores; i++)
            {
                if (indexPlayer >= numeroDeJugadores)
                    indexPlayer = 0;
                for (int j = 0; j < 5; j++)
                {
                    Manos[indexPlayer].AnadirArriba(Mazo.TomarTop());
                    if (thisPlayer == indexPlayer)
                        Manos[indexPlayer].NaipesEnGrupo[j].faceUp = true;
                    else
                        Manos[indexPlayer].NaipesEnGrupo[j].faceUp = false;
                }
                indexPlayer++;
            }
        }
        #endregion
    }
}
