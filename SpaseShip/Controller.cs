using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaseShip
{
    class Controller
    {
        public List<Asteroid> asteroids = new List<Asteroid>();
        static double id = 0;
        public double timer = 2D, maxTime = 2D;
        private static float thisScreenWidth, thisScreenHeight, speed = 250F, asteroidSpawn = 1F;

        public Controller(float screenWidth, float screenHeight)
        {
            thisScreenWidth = screenWidth;
            thisScreenHeight = screenHeight;
        }

        public void conUpdate(GameTime gameTime, bool isGameOver)
        {
            if (!isGameOver)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                {
                    if (asteroids.Count > 20)
                    {
                        List<Asteroid> tAsterList = new List<Asteroid>();
                        for (int i = asteroids.Count - 20 < 0 ? 0 : asteroids.Count - 20; i < asteroids.Count; i++)
                        {
                            tAsterList.Add(asteroids[i]);
                        }
                        asteroids = new List<Asteroid>(tAsterList);
                        tAsterList = null;
                    }
                    asteroids.Add(new Asteroid(speed, thisScreenWidth, thisScreenHeight, id++));

                    timer = maxTime;
                    if (maxTime > 0.5)
                    {
                        maxTime -= 0.1;
                    }
                }
            }
        }
    }
}
