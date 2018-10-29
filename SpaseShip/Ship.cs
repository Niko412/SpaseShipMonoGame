using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaseShip
{
    class Ship
    {
        public Vector2 position;// = new Vector2(100, 100);
        public int speed = 3 * 60;
        public int radius = 36;
        public Ship(int screenWidth, int screenHeight)
        {
            position = new Vector2(screenWidth / 2, screenHeight / 2);
        }
        public void ShipUpdate(GameTime gameTime, bool isGameOver)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (!isGameOver)
            {
                if (kState.IsKeyDown(Keys.Left))
                {
                    position.X -= speed * dt;
                }

                if (kState.IsKeyDown(Keys.Right))
                {
                    position.X += speed * dt;
                }

                if (kState.IsKeyDown(Keys.Up))
                {
                    position.Y -= speed * dt;
                }

                if (kState.IsKeyDown(Keys.Down))
                {
                    position.Y += speed * dt;
                }
            }
        }
    }
}
