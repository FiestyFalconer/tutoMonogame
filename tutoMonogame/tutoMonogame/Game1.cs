using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace tutoMonogame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D target_Sprite;
        Texture2D crosshairs_Sprite;
        Texture2D background_Sprite;

        SpriteFont gameFont;

        Vector2 targetPosition = new Vector2(300,300);
        const int TARGET_RADIUS = 45;

        MouseState mState;
        bool mReleased = true;
        float mouseTargetDist;

        int score = 0;
        float timer = 10f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            target_Sprite = Content.Load<Texture2D>("target");
            crosshairs_Sprite = Content.Load<Texture2D>("crosshairs");
            background_Sprite = Content.Load<Texture2D>("sky");

            gameFont = Content.Load<SpriteFont>("galleryFont");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (timer > 0)
            {
                timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            

            mState = Mouse.GetState();

            mouseTargetDist = Vector2.Distance(targetPosition, new Vector2(mState.X, mState.Y));

            if (mState.LeftButton == ButtonState.Pressed && mReleased)
            {
                if (mouseTargetDist < TARGET_RADIUS && timer > 0)
                {
                    score++;

                    Random rnd = new Random();

                    targetPosition.X = rnd.Next(TARGET_RADIUS, _graphics.PreferredBackBufferWidth - TARGET_RADIUS + 1);
                    targetPosition.Y = rnd.Next(TARGET_RADIUS, _graphics.PreferredBackBufferHeight - TARGET_RADIUS + 1);
                }
                mReleased = false;
            }

            if (mState.LeftButton == ButtonState.Released)
            {
                mReleased = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DeepSkyBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(background_Sprite, new Vector2(0,0), Color.White);
            
            if (timer > 0)
            {
                _spriteBatch.Draw(target_Sprite, targetPosition, null, Color.White, 0f, new Vector2(target_Sprite.Width / 2, target_Sprite.Height / 2), 1f, SpriteEffects.None, 0f);
                _spriteBatch.DrawString(gameFont, "Time: " + Math.Ceiling(timer).ToString(), new Vector2(3, 40), Color.White);
            }
            else
            {
                _spriteBatch.DrawString(gameFont, "Time: 0", new Vector2(3, 40), Color.White);
            }
            _spriteBatch.DrawString(gameFont, "Score: " + score.ToString(), new Vector2(3, 3), Color.White);

            _spriteBatch.Draw(crosshairs_Sprite, new Vector2(mState.X, mState.Y), null, Color.White, 0f, new Vector2(crosshairs_Sprite.Width / 2, crosshairs_Sprite.Height / 2), 1f, SpriteEffects.None, 0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
