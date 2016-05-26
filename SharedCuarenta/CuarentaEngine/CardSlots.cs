using Microsoft.Xna.Framework;
using System;
using SharedCuarenta.Enums;

namespace SharedCuarenta.CuarentaEngine
{
    class CardSlots
    {
        #region Properties
        /// <summary>
        /// Position of the cards of the players and if that slot is used
        /// There are 4 playeres and each one can have 5 cards
        /// </summary>
        public Point[,] PlayerCardPosition { get; } = new Point[4, 5];
        public bool[,] UsedPlayerCardPosition { get; set; } = new bool[4, 5];

        /// <summary>
        /// Position of the cards on the table and if that slot si used
        /// A maximun of 10 cards could be on the table
        /// </summary>
        public Point[] TableCardPosition { get; } = new Point[10];
        public bool[] UsedTableCardPosition { get; set; } = new bool[10];

        /// <summary>
        /// Position of the cards used to keep the score
        /// Each player or team have their own score cards (2 players or teams)
        /// and a max of six cards needed to keep the score
        /// </summary>
        public Point[,] ScoreCardPosition { get; } = new Point[2, 6];
        public bool[,] UsedScoreCardPosition { get; set; } = new bool[2, 6];

        /// <summary>
        /// Position of the cards used to get extra points (carton)
        /// 2 places to put the cards is needed (so the array has two rows)
        /// The column is set to one and will be used for future expansions
        /// </summary>
        public Point[,] CartonCardPosition { get; } = new Point[2, 1];
        public bool[,] UsedCartonCardPosition { get; set; } = new bool[2, 1];

        /// <summary>
        /// Position of the cards that left and will be dealed.
        /// Kept as an array for future expansions
        /// </summary>
        public Point[] ToDealCardPosition { get; } = new Point[1];
        public bool[] UsedToDealCardPosition { get; set; } = new bool[1];

        /// <summary>
        /// size of the cards
        /// </summary>
        public Rectangle CardSize { get; }

        #endregion

        #region Constructors
        /// <summary>
        /// Initialize all the slots assigning its coorditantes
        /// </summary>
        /// <param name="windowSize"></param>
        public CardSlots(Rectangle windowSize)
        {

            // Calculate the columns (grid) used to position the cards
            int[] columnCenters = new int[11];
            float columnWidth;
            float firstCenter;
            columnWidth = (float)(windowSize.Width - Constants.Margin * 2) / 11;
            firstCenter = columnWidth / 2 + Constants.Margin;
            for(int i = 0; i < 11; i++)
                columnCenters[i] = (int) Math.Round(firstCenter + columnWidth * i);

            //calculate card size according to the window
            Rectangle cardSize = new Rectangle();
            cardSize.Width = (int) columnWidth - Constants.InterCardSpace;
            cardSize.Height = (int) (columnWidth / Constants.CardRatioWH);
            if ((float)cardSize.Height > 2*columnWidth)
            {
                cardSize.Height = (int) (2 * columnWidth) - Constants.InterCardSpace;
                cardSize.Width = (int) (CardSize.Height * Constants.CardRatioWH);
            }
            CardSize = cardSize;

            //calculate slots positions for botton player
            int bottonRowYPositionPlayer = (int)Math.Round(windowSize.Height - (float)cardSize.Height / 2 - Constants.Margin);
            for (int i = 0; i < 5; i++)
            {
                PlayerCardPosition[0, i].X = columnCenters[2 + i];
                PlayerCardPosition[0, i].Y = bottonRowYPositionPlayer;
            }

            //calculate slots positions for top player
            int topRowYPositionPlayer = (int)Math.Round((float)cardSize.Height / 2 + Constants.Margin);
            for (int i = 0; i < 5; i++)
            {
                PlayerCardPosition[2, i].X = columnCenters[2 + i];
                PlayerCardPosition[2, i].Y = topRowYPositionPlayer;
            }

            //calculate slots positions for right player
            int firstPlayerCardYPosition = (int)Math.Round((windowSize.Height - columnWidth * 5) / 2 + columnWidth / 2);
            int rightColumnXPositionPlayer = (int)Math.Round(Constants.Margin + columnWidth * 9 - (float)cardSize.Height / 2); ;
            for (int i = 0; i < 5; i++)
            {
                PlayerCardPosition[1, i].X = rightColumnXPositionPlayer;
                PlayerCardPosition[1, i].Y = (int)Math.Round(firstPlayerCardYPosition + columnWidth * i);
            }

            //calculate slots positons for left player
            int leftColumnXPositionPlayer = (int)Math.Round((float)cardSize.Height / 2 + Constants.Margin);
            for (int i = 0; i < 5; i++)
            {
                PlayerCardPosition[3, i].X = leftColumnXPositionPlayer;
                PlayerCardPosition[3, i].Y = (int)Math.Round(firstPlayerCardYPosition + columnWidth * i);
            }

            //calculate slots for top row table cards
            int topRowTableCardYPosition = (int)Math.Round((float)windowSize.Height / 2 - (float)cardSize.Height / 2 - Constants.InterCardSpace);
            for(int i = 0; i < 5; i++)
            {
                TableCardPosition[i].X = columnCenters[2 + i];
                TableCardPosition[i].Y = topRowTableCardYPosition;
            }

            //calculate slots for botton row table cards
            int bottonRowTableCardYPosition = (int)Math.Round((float)windowSize.Height / 2 + (float)cardSize.Height / 2 + Constants.InterCardSpace);
            for (int i = 0; i < 5; i++)
            {
                TableCardPosition[i + 5].X = columnCenters[2 + i];
                TableCardPosition[i + 5].Y = bottonRowTableCardYPosition;
            }

            //calculate slots for points cards (perros)
            //At the 0 row are the botton cards, and at the 1 row are the top cards
            int perrosSpace = (int)Math.Round(Constants.PerrosSpaceRatioSW * cardSize.Width);
            int scoreCardPlayerTopYInitPosition = (int)Math.Round((float)cardSize.Height / 2 + Constants.Margin);
            int scoreCardPlayerBottonYInitPosition = (int)Math.Round(windowSize.Height - Constants.Margin - (float)cardSize.Height / 2 - perrosSpace *5 );
            for (int i=0; i<6; i++)
            {
                ScoreCardPosition[0, i].X = columnCenters[9];
                ScoreCardPosition[1, i].X = columnCenters[9];
                ScoreCardPosition[0, i].Y = scoreCardPlayerBottonYInitPosition + perrosSpace * i;
                ScoreCardPosition[1, i].Y = scoreCardPlayerTopYInitPosition + perrosSpace * i;
            }

            //calculate slots for extra points cards (carton)
            //At the 0 row are the botton cards, and at the 1 row are the top cards
            CartonCardPosition[0, 0].X = columnCenters[10];
            CartonCardPosition[1, 0].X = columnCenters[10];
            CartonCardPosition[0, 0].Y = (int)Math.Round(windowSize.Height - Constants.Margin - (float)cardSize.Height / 2 - perrosSpace * 5);
            CartonCardPosition[1, 0].Y = (int)Math.Round((float)cardSize.Height / 2 + Constants.Margin);

            //calculate slots for to deal cards
            ToDealCardPosition[0].X = (int)Math.Round(columnCenters[9] + columnWidth / 2);
            ToDealCardPosition[0].Y = (int)Math.Round((float)windowSize.Height / 2);

            //set all Used slots as false (no slot is used)
            for(int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    UsedPlayerCardPosition[i, j] = false;
                }
            }
            for(int i = 0; i < 10; i++)
            {
                UsedTableCardPosition[i] = false;
            }
            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 6; j++)
                {
                    UsedScoreCardPosition[i, j] = false;
                }
            }
            for(int i = 0; i < 2; i++)
            {
                UsedCartonCardPosition[i, 0] = false;
            }
            UsedToDealCardPosition[0] = false;

        }
        #endregion

    }
}
