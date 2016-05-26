//Enums used through the project

namespace SharedCuarenta.Enums
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

    public class Constants
    {
        public const int Margin = 3;
        public const int InterCardSpace = 5;
        public const float CardRatioWH = 0.7368421f;
        public const float PerrosSpaceRatioSW = 0.1875f;
        public const int WinWindowSizeX = 1000;
        public const int WinWindowSizeY = 600;
    }

}