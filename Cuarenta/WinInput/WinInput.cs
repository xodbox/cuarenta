using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuarenta.WinInput
{
    class WinInput
    {
        static MouseState oldState = Mouse.GetState();

        public static Boolean MouseClickCoordinates(out Vector2 coordenadas)
        {
            MouseState mouseState = Mouse.GetState();
            if(mouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton != oldState.LeftButton)
            {
                oldState = mouseState;
                coordenadas.X = mouseState.X;
                coordenadas.Y = mouseState.Y;
                return true;
            }
            oldState = mouseState;
            coordenadas.X = 0;
            coordenadas.Y = 0;
            return false;
        }  
    }
}
