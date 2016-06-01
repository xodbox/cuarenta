//Enums used through the project

namespace SharedCuarenta.Utilities
{

    public enum CardRank
    {
        As, Dos, Tres, Cuatro, Cinco, Seis, Siete, Ocho, Nueve, Diez, Jota, Qu, Ka
    }

    public enum CardPalo
    {
        Corazon, Brillo, Trebol, CorazonNegro
    }

    public enum GameState
    {
        InitGame,
        ToPlaying,
        Playing
    }

    public enum HandState
    {
        NormalPlay
    }

    public enum CardGroup
    {
        Hand0, Hand1, Hand2, Hand3, Table, Points0, Points1, Carton0, Carton1, ToDeal
    }
}