using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SpaseShip
{

    public class Game1 : Game
    {
        const int SCREEN_HEIGHT = 768, SCREEN_WIDTH = 1366;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SoundEffect collisionSound, loseSound;
        SoundEffect backgroundMusic;
        SoundEffectInstance soundEffectInstance;
        Texture2D shipSprite, asteroidSprite, spaceSprite, lifeIcon;
        SpriteFont gameFont, timeFont;


        Ship player = new Ship(SCREEN_WIDTH, SCREEN_HEIGHT);

        bool hasCollision = false, isGameOver = false, isBegin = false;
        private static int numOfLives = 3;
        Controller gameController = new Controller(SCREEN_WIDTH, SCREEN_HEIGHT);
        private bool fasle;
        double collisionAsteroidId = -1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
        }

        protected override void Initialize()
        {


            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            shipSprite = Content.Load<Texture2D>("ship_new");
            asteroidSprite = Content.Load<Texture2D>("asteroid_new");
            spaceSprite = Content.Load<Texture2D>("bakground_space");
            lifeIcon = Content.Load<Texture2D>("heart-icon");

            collisionSound = Content.Load<SoundEffect>("collisionSound");
            loseSound = Content.Load<SoundEffect>("loseSound");
            backgroundMusic = Content.Load<SoundEffect>("background");
            soundEffectInstance = backgroundMusic.CreateInstance();
            soundEffectInstance.IsLooped = true;
            soundEffectInstance.Play();



            gameFont = Content.Load<SpriteFont>("spaceFont");
            timeFont = Content.Load<SpriteFont>("timerFont");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Enter))
            {
                isBegin = true;
            }
            if (isBegin)
            {
                bool collisionOnce = fasle;
                player.ShipUpdate(gameTime, isGameOver);
                gameController.conUpdate(gameTime, isGameOver);
                for (int i = 0; i < gameController.asteroids.Count; i++)
                {
                    gameController.asteroids[i].AsteroidUpdate(gameTime);
                    if (Vector2.Distance(
                        new Vector2(gameController.asteroids[i].position.X + gameController.asteroids[i].radius, gameController.asteroids[i].position.Y + gameController.asteroids[i].radius),
                        new Vector2(player.position.X + player.radius, player.position.Y + player.radius)) + 20 < gameController.asteroids[i].radius + player.radius)
                    {
                        hasCollision = true;
                        if (hasCollision)
                        {
                            if (numOfLives > 0 && !collisionOnce && collisionAsteroidId != gameController.asteroids[i].id)
                            {
                                numOfLives--;
                                collisionOnce = true;
                                collisionAsteroidId = gameController.asteroids[i].id;
                                collisionSound.Play();
                            }
                            hasCollision = fasle;
                        }
                        if (numOfLives == 0)
                        {
                            i = gameController.asteroids.Count;
                            MediaPlayer.Stop();
                            loseSound.Play();
                            isGameOver = true;
                            gameController.asteroids.Clear();
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(spaceSprite, new Vector2(0, 0), Color.White);
            if (!isBegin)
            {
                string msg = "                      WELCOME!\nPRESS ENTER TO BEGIN THE GAME!";
                Vector2 stringSize = gameFont.MeasureString(msg);
                spriteBatch.DrawString(gameFont, msg, new Vector2(SCREEN_WIDTH / 2 - stringSize.X / 2, SCREEN_HEIGHT / 2 - stringSize.Y / 2), Color.Green);
            }
            else
            {
                if (!isGameOver)
                {
                        spriteBatch.Draw(shipSprite, new Vector2(player.position.X - shipSprite.Width, player.position.Y - shipSprite.Height), Color.White);
                    

                    for (int i = 0; i < gameController.asteroids.Count; i++)
                    {
                        Vector2 tempPos = gameController.asteroids[i].position;
                        int tempRadius = gameController.asteroids[i].radius;
                        spriteBatch.Draw(asteroidSprite, new Vector2(tempPos.X - tempRadius, tempPos.Y - tempRadius), Color.White);
                    }
                    int startPos = 5;
                    for (int i = 0; i < numOfLives; i++)
                    {
                        spriteBatch.Draw(lifeIcon, new Vector2(startPos + i * 48, startPos), Color.White);
                    }
                }
                else
                {
                    string msg = "YOU LOSE!";
                    Vector2 stringSize = gameFont.MeasureString(msg);
                    spriteBatch.DrawString(gameFont, msg, new Vector2(SCREEN_WIDTH / 2 - stringSize.X / 2, SCREEN_HEIGHT / 2 - stringSize.Y / 2), Color.IndianRed);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
