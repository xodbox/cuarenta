//Clase que representa un naipe de una baraja

using System;
using SharedCuarenta.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SharedCuarenta.Naipes
{

    public class Naipe
    {
        #region Properties
        public CardRank Rank { get; }
        public CardPalo Palo { get; }
        public bool FaceUp { get; set; }
        public bool OnGame { get; set; }
        public Rectangle CardSize { get; set; }
        public bool Rotated { get; set; }
        #endregion

        #region Fields
        Rectangle drawRectangle;
        Vector2 position;
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
            FaceUp = false;
            OnGame = false;
            Rotated = false;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Set drawing rectangle according the center
        /// </summary>
        /// <param name="center">center of the place where to put the sprite</param>
        public void SetCenter(Point center)
        {
            drawRectangle.X = center.X;
            drawRectangle.Y = center.Y;
            drawRectangle.Width = CardSize.Width;
            drawRectangle.Height = CardSize.Height;
        }

        /// <summary>
        /// Draw a Card
        /// </summary>
        /// <param name="spriteBatch">Game sprite batch</param>
        /// <param name="textures">texture to draw</param>
        public void Draw(SpriteBatch spriteBatch, Dictionary<String, Texture2D> textures)
        {
            Texture2D texture = textures[Rank.ToString() + Palo.ToString()];
            float rotation = 0;
            if (Rotated)
                rotation = (float)Math.PI / 2;
                
            if (OnGame)
            {
                if (FaceUp)
                    spriteBatch.Draw(texture, drawRectangle, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height/2), SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(textures["back"], drawRectangle, null, Color.White, rotation, new Vector2(textures["back"].Width / 2, textures["back"].Height / 2), SpriteEffects.None, 0);
            }
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
