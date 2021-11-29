using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace moveSprite
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _texture;
        private Vector2 _positionMario;

        private Texture2D background;
        private double jump = 0;
        private double endJump = 20;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Window.IsBorderless = true;
            _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            _graphics.ApplyChanges();
            // TODO: Add your initialization logic here

            base.Initialize();
        }



        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("background");
            _texture = Content.Load<Texture2D>("idleMario");
            _positionMario = new Vector2(0,800);
        }

        protected override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                int poseBase = Convert.ToInt32(_positionMario.Y);
                _positionMario.Y -= 5;
                int poseHaut = poseBase - 50;
                bool isUp = false;
                this.jump += gameTime.ElapsedGameTime.Milliseconds;
                while (poseBase != Convert.ToInt32(_positionMario.Y))
                {
                    while (Convert.ToInt32(_positionMario.Y) != poseHaut && isUp == false)
                    {
                        if (jump > endJump)
                        {
                            jump = 0;
                            _positionMario.Y -= 5;
                        }
                    }
                    isUp = true;
                    if (jump > endJump)
                    {
                        jump = 0;
                        _positionMario.Y += 5;
                    }
                }


            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && _positionMario.Y < 870)
            {
                _positionMario.Y += 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && _positionMario.X != 0)
            {
                _positionMario.X -= 5;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && _positionMario.X < _graphics.PreferredBackBufferWidth - (_texture.Width * 4))
            {
                _positionMario.X += 5;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _spriteBatch.Draw(background, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

            _spriteBatch.Draw(_texture, _positionMario, null, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
