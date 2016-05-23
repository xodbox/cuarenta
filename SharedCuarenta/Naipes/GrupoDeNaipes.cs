using System;
using System.Collections.Generic;
using System.Linq;
using Cuarenta.Enums;

namespace Cuarenta.Naipes
{
    class GrupoDeNaipes
    {
        private static Random rnd = new Random();

        public IList<Naipe> NaipesEnGrupo { get; }

        public GrupoDeNaipes(IList<Naipe> cards)
        {
            NaipesEnGrupo = cards;
        }

        public GrupoDeNaipes()
        {
            NaipesEnGrupo = new List<Naipe>();
        }

        public void Barajar()
        {
            for (int n = 0; n < NaipesEnGrupo.Count; n++)
            {
                int k = rnd.Next(n, NaipesEnGrupo.Count);
                Naipe temp = NaipesEnGrupo[n];
                NaipesEnGrupo[n] = NaipesEnGrupo[k];
                NaipesEnGrupo[k] = temp;
            }
        }

        public void AnadirArriba(Naipe card)
        {
            NaipesEnGrupo.Add(card);
        }

        public void AnadirAbajo(Naipe card)
        {
            NaipesEnGrupo.Insert(0, card);
        }

        public void AnadirAt(int pos, Naipe card)
        {
            NaipesEnGrupo.Insert(pos, card);
        }

        public Naipe MostrarTop()
        {
            return NaipesEnGrupo.Last();
        }

        public Naipe TomarTop()
        {
            Naipe naipe;
            naipe = NaipesEnGrupo.Last();
            NaipesEnGrupo.RemoveAt(NaipesEnGrupo.Count - 1);
            return naipe;
        }

        public Naipe TomarUno(int pos)
        {
            Naipe naipe;
            naipe = NaipesEnGrupo[pos];
            NaipesEnGrupo.RemoveAt(pos);
            return naipe;
        }

        static IList<Naipe> CrearBarajaCompleta()
        {
            IList<Naipe> baraja = new List<Naipe>();

            foreach (CardPalo palo in Enum.GetValues(typeof(CardPalo)))
            {
                foreach (CardRank rank in Enum.GetValues(typeof(CardRank)))
                {
                    baraja.Add(new Naipe(rank, palo));
                }
            }

            return baraja;
        }
    }
}
