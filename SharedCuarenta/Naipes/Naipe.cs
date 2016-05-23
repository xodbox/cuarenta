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
        public Rectangle CardSize { get; set; }
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
        }
        #endregion

        #region Public Methods

        public void Update(Vector2 center)
        {
            drawRectangle.X = (int)(center.X - (float)CardSize.Width / 2);
            drawRectangle.Y = (int)(center.Y - (float)CardSize.Height / 2);
            drawRectangle.Width = CardSize.Width;
            drawRectangle.Height = CardSize.Height;
        }
        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, drawRectangle, Color.White);
        }
        /// <summary>
        /// To String Method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Rank.ToString() + " de " + Palo.ToString();
        }
        #endregion
    }
}
