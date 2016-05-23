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
        public GrupoDeNaipes NaipesEnMesa { get; private set; }
        public GrupoDeNaipes[] Manos { get; private set; }
        public GrupoDeNaipes Mazo { get; private set; }
        public GrupoDeNaipes Perros { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of the elements of a Cuarenta Match
        /// </summary>
        public PartidaDeCuarenta()
        {
            Manos = new GrupoDeNaipes[4];
            for (int i = 0; i < 4; i++)
            {
                Manos[i] = new GrupoDeNaipes();
            }

            NaipesEnMesa = new GrupoDeNaipes();
            Mazo = new GrupoDeNaipes();
            Perros = new GrupoDeNaipes();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Begin a Cuarenta Match, clearing everything, shuffleing the deck and dealing the first hand.
        /// </summary>
        /// <param name="numeroDeJugadores">Number of players in the match</param>
        public void IniciarPartida(int numeroDeJugadores)
        {
            if (numeroDeJugadores != 2 && numeroDeJugadores != 4)
                throw new ArgumentOutOfRangeException();

            for(int i = 0; i < numeroDeJugadores; i++)
            {
                Manos[i].NaipesEnGrupo.Clear();
            }
            NaipesEnMesa.NaipesEnGrupo.Clear();
            Mazo.NaipesEnGrupo.Clear();
            Perros.NaipesEnGrupo.Clear();

            foreach (CardPalo palo in Enum.GetValues(typeof(CardPalo)))
            {
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                {
                    if (rank <= CardRank.Siete || rank >= CardRank.Jota)
                        Mazo.AnadirArriba(new Naipe(rank, palo));
                    else
                        Perros.AnadirArriba(new Naipe(rank, palo));
                }
            }
            Mazo.Barajar();
            Perros.Barajar();
            Repartir(numeroDeJugadores);
        }

        /// <summary>
        /// Deals a hand of Cuarenta
        /// </summary>
        /// <param name="numeroDeJugadores">Numbers of players in the Match</param>
        public void Repartir(int numeroDeJugadores)
        {
            if (numeroDeJugadores != 2 && numeroDeJugadores != 4)
                throw new ArgumentOutOfRangeException();

            for (int i = 0; i < numeroDeJugadores; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Manos[i].AnadirArriba(Mazo.TomarTop());
                }
            }
        }
        #endregion
    }
}
