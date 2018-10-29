using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaseShip
{
    class Asteroid
    {
        public Vector2 position;
        public double id;
        private float speed;
        public int radius = 128/2; //this is the radius of the asteroid
        static Random rnd = new Random();
        public Asteroid(float newSpeed, float screenWidth,float screenHeight, double id)
        {
            speed = newSpeed;
            this.id = id;
            position = new Vector2(screenWidth, rnd.Next(0, (int)screenHeight + 1));
        }
        public void AsteroidUpdate(GameTime gametime)
        {
            float dt = (float)gametime.ElapsedGameTime.TotalSeconds;
            position.X -= speed * dt;
        }
    }
}
