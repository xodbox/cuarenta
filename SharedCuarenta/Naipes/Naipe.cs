//Clase que representa un naipe de una baraja

using System;
using SharedCuarenta.Utilities;
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
        public bool Selected { get; set; }
        public bool Touched { get; set; }
        public Pair<CardGroup, int> slotAssigned { get; set; }
        #endregion

        #region Fields
        Rectangle drawRectangle;
        Point center;
        #endregion

        #region Initializer
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Naipe()
        {

        }
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
            Selected = false;
            Touched = false;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Set drawing rectangle according the center
        /// </summary>
        /// <param name="center">center of the place where to put the sprite</param>
        public void SetCenter(Point center)
        {
            this.center = center;
            drawRectangle.X = (int) Math.Round(center.X - (float)CardSize.Width / 2);
            drawRectangle.Y = (int) Math.Round(center.Y - (float)CardSize.Height / 2);
            drawRectangle.Width = CardSize.Width;
            drawRectangle.Height = CardSize.Height;
        }

        public void MoveUp()
        {
            drawRectangle.Y -= (int)Math.Round((float)CardSize.Height / 10);
        }

        public void MoveToOriginalPosition()
        {
            drawRectangle.Y = (int)Math.Round(center.Y - (float)CardSize.Height / 2);
        }

        /// <summary>
        /// Return true if a card is clicked
        /// </summary>
        /// <param name="xClickCoordinate">coordinate X of click</param>
        /// <param name="yClickCoordinate">coordinate Y of click</param>
        /// <returns></returns>
        public bool isClicked(int xClickCoordinate, int yClickCoordinate)
        {
            return drawRectangle.Contains(new Point(xClickCoordinate, yClickCoordinate));
        }

        /// <summary>
        /// Draw a Card
        /// </summary>
        /// <param name="spriteBatch">Game sprite batch</param>
        /// <param name="textures">texture to draw</param>
        public void Draw(SpriteBatch spriteBatch, Dictionary<String, Texture2D> textures)
        {
            Texture2D texture = textures[Rank.ToString() + Palo.ToString()];
            Rectangle drawRectangle = this.drawRectangle;

            drawRectangle.X += (int)Math.Round((float)CardSize.Width / 2);
            drawRectangle.Y += (int)Math.Round((float)CardSize.Height / 2);
            float rotation = 0;
            if (Rotated)
                rotation = (float)Math.PI / 2;

            if (OnGame)
            {
                if (FaceUp)
                    spriteBatch.Draw(texture, drawRectangle, null, Color.White, rotation, new Vector2(texture.Width / 2, texture.Height / 2), SpriteEffects.None, 0);
                else
                    spriteBatch.Draw(textures["back"], drawRectangle, null, Color.White, rotation, new Vector2(textures["back"].Width / 2, textures["back"].Height / 2), SpriteEffects.None, 0);

                if (Touched)
                    spriteBatch.Draw(textures["frame_red"], drawRectangle, null, Color.White, rotation, new Vector2(textures["frame_red"].Width / 2, textures["frame_red"].Height / 2), SpriteEffects.None, 0);

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
