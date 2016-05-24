//Clase que representa un naipe de una baraja

using System;
using Cuarenta.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Cuarenta.Naipes
{

    public class Naipe
    {
        #region Properties
        public CardRank Rank { get; }
        public CardPalo Palo { get; }
        public bool faceUp { get; set; }
        public bool onGame { get; set; }
        public Rectangle CardSize { get; set; }
        public Texture2D texture { get; set; }
        #endregion

        #region Fields
        Rectangle drawRectangle;
        #endregion

        #region Initializer
        /// <summary>
        /// Naipe Constructor
        /// </summary>
        /// <param name="rank">Card rank</param>
        /// <param name="palo">Card suit</param>
        public Naipe(CardRank rank, CardPalo palo)
        {
            Rank = rank;
            Palo = palo;
            faceUp = false;
            onGame = false;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Set drawing rectangle according the center
        /// </summary>
        /// <param name="center">center of the place where to put the sprite</param>
        public void SetCenter(Point center)
        {
            drawRectangle.X = (int)(center.X - (float)CardSize.Width / 2);
            drawRectangle.Y = (int)(center.Y - (float)CardSize.Height / 2);
            drawRectangle.Width = CardSize.Width;
            drawRectangle.Height = CardSize.Height;
        }

        /// <summary>
        /// Draw a Card
        /// </summary>
        /// <param name="spriteBatch">Game sprite batch</param>
        /// <param name="texture">texture to draw</param>
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, drawRectangle, Color.White);
        }

        /// <summary>
        /// To String method
        /// </summary>
        /// <returns>Returns rank de suit</returns>
        public override string ToString()
        {
            return Rank.ToString() + " de " + Palo.ToString();
        }
        #endregion
    }
}
