//Clase que representa un naipe de una baraja

using System;

namespace Cuarenta.Naipes
{
    public enum CardRank
    {
        As, Dos, Tres, Cuatro, Cinco, Seis, Siete, Ocho, Nueve, Diez, Jota, Qu, Ka
    }
    public enum CardPalo
    {
        Corazon, Brillo, Trebol, CorazonNegro
    }

    public class Naipe
    {
        public CardRank Rank { get; }
        public CardPalo Palo { get; }
        public Naipe(CardRank rank, CardPalo palo)
        {
            Rank = rank;
            Palo = palo;
        }

        public override string ToString()
        {
            return Rank.ToString() + " de " + Palo.ToString();
        }
    }
}
