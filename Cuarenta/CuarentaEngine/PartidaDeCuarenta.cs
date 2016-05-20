using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cuarenta.Naipes;

namespace Cuarenta.CuarentaEngine
{
    class PartidaDeCuarenta
    {
        public GrupoDeNaipes NaipesEnMesa { get; private set; }
        public GrupoDeNaipes[] Manos { get; private set; }
        public GrupoDeNaipes Mazo { get; private set; }
        public GrupoDeNaipes Perros { get; private set; }
        
        public PartidaDeCuarenta()
        {
            ConstructorHelper(2);
        }

        public PartidaDeCuarenta(int numeroDeJugadores)
        {
            if (numeroDeJugadores != 2 && numeroDeJugadores != 4)
                throw new ArgumentOutOfRangeException();

            ConstructorHelper(numeroDeJugadores);
        }

        private void ConstructorHelper(int numeroDeJugadores)
        {
            if (numeroDeJugadores != 2 && numeroDeJugadores != 4)
                throw new ArgumentOutOfRangeException();

            Manos = new GrupoDeNaipes[numeroDeJugadores];
            for (int i = 0; i < numeroDeJugadores; i++)
            {
                Manos[i] = new GrupoDeNaipes();
            }
            NaipesEnMesa = new GrupoDeNaipes();
            Mazo = new GrupoDeNaipes();
            Perros = new GrupoDeNaipes();
        }

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
    }
}
