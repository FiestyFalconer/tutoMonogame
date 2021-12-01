using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;
using System;

namespace moveSprite
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D textureMario;
        private Vector2 positionMario;

        private Texture2D background;

        private Texture2D blockQuestion;
        private Vector2 postionBlock;

        private int jump = 0;
        private int nbPiece = 0;
        private int colisionBlock = 0;
        private int direction = 0;

        private string movement = "idleMarioRight";
        private string block = "questionBlock";

        Dictionary<string, Texture2D> animationBlock = new Dictionary<string, Texture2D>();

        Dictionary<string, Texture2D> animationMario = new Dictionary<string, Texture2D>();

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

            //dictionnaire pour le Mario
            animationMario.Add("idleMarioLeft", Content.Load<Texture2D>("idleMarioLeft"));
            animationMario.Add("idleMarioRight", Content.Load<Texture2D>("idleMarioRight"));
            animationMario.Add("frame0Right", Content.Load<Texture2D>("frame0Right"));
            animationMario.Add("frame0Left", Content.Load<Texture2D>("frame0Left"));
            animationMario.Add("frame1Left", Content.Load<Texture2D>("frame1Left"));
            animationMario.Add("frame1Right", Content.Load<Texture2D>("frame1Right"));
            animationMario.Add("frame2Right", Content.Load<Texture2D>("frame2Right"));
            animationMario.Add("frame2Left", Content.Load<Texture2D>("frame2Left"));
            animationMario.Add("jumpMarioLeft", Content.Load<Texture2D>("jumpMarioLeft"));
            animationMario.Add("jumpMarioRight", Content.Load<Texture2D>("jumpMarioRight"));

            //dictionnaire pour le block
            animationBlock.Add("questionBlock", Content.Load<Texture2D>("questionBlock"));
            animationBlock.Add("blockFrame0", Content.Load<Texture2D>("bolckFrame0"));
            animationBlock.Add("blockFrame1", Content.Load<Texture2D>("bolckFrame1"));
            animationBlock.Add("blockFrame2", Content.Load<Texture2D>("bolckFrame2"));
            animationBlock.Add("blockFrame3", Content.Load<Texture2D>("bolckFrame3"));
            animationBlock.Add("blockFrame4", Content.Load<Texture2D>("bolckFrame4"));

            background = Content.Load<Texture2D>("background");
            textureMario = Content.Load<Texture2D>("idleMarioRight");
            positionMario = new Vector2(0, 870);
            
            blockQuestion = Content.Load<Texture2D>("questionBlock");
            postionBlock = new Vector2(700, 720);
        }

        protected override void Update(GameTime gameTime)
        {

            if (colisionBlock > 0)
            {
                if (colisionBlock < 7 && colisionBlock > 0)
                {
                    block = "blockFrame0";
                    colisionBlock += 1;
                }
                else if (colisionBlock < 14 && colisionBlock > 0)
                {
                    block = "blockFrame1";
                    colisionBlock += 1;
                }
                else if (colisionBlock < 21 && colisionBlock > 0)
                {
                    block = "blockFrame2";
                    colisionBlock += 1;
                }
                else if (colisionBlock < 28 && colisionBlock > 0)
                {
                    block = "blockFrame3";
                    colisionBlock += 1;
                }
                else if (colisionBlock < 35 && colisionBlock > 0)
                {
                    block = "blockFrame4";
                    colisionBlock += 1;
                }
                else if (colisionBlock < 42 && colisionBlock > 0)
                {
                    block = "questionBlock";
                    colisionBlock = 0;
                    nbPiece += 1;
                }

            }

            if (Keyboard.GetState().IsKeyDown(Keys.W) || jump > 0)
            {

                if (jump == 21)
                {
                    if (positionMario.X > 690 && positionMario.X < 710 + blockQuestion.Width)
                    {
                        colisionBlock = 1;
                    }
                }

                if (jump < 21)
                {
                    if (movement == "idleMarioLeft")
                    {
                        movement = "jumpMarioLeft";
                    }
                    else if (movement == "idleMarioRight")
                    {
                        movement = "jumpMarioRight";
                    }
                    positionMario.Y -= 4;
                    jump += 1;
                    if (Keyboard.GetState().IsKeyDown(Keys.A) && positionMario.X != 0)
                    {
                        positionMario.X -= 3;
                        //changer la direction du saut
                        movement = "jumpMarioLeft";
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D) && positionMario.X < _graphics.PreferredBackBufferWidth - (textureMario.Width * 4))
                    {
                        positionMario.X += 3;
                        //changer la direction du saut
                        movement = "jumpMarioRight";
                    }
                }
                else if (jump < 42 && jump > 0)
                {
                    positionMario.Y += 4;
                    jump += 1;
                    if (Keyboard.GetState().IsKeyDown(Keys.A) && positionMario.X != 0)
                    {
                        positionMario.X -= 3;
                        movement = "jumpMarioLeft";


                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D) && positionMario.X < _graphics.PreferredBackBufferWidth - (textureMario.Width * 4))
                    {
                        positionMario.X += 3;
                        movement = "jumpMarioRight";

                    }
                }
                else
                {
                    jump = 0;
                    //aterrisage mario reprend le statu idle
                    if (movement == "jumpMarioLeft")
                    {
                        movement = "idleMarioLeft";
                    }
                    else
                    {
                        movement = "idleMarioRight";
                    }
                }

            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && positionMario.X != 0)
            {
                positionMario.X -= 5;
                if (direction < 10)
                {
                    movement = "frame0Left";
                    direction += 1;
                }
                else if (direction < 20)
                {
                    movement = "frame1Left";
                    direction += 1;
                }
                else if (direction < 32)
                {
                    movement = "frame2Left";
                    direction += 1;
                }
                else if (direction > 20)
                {
                    direction = 0;
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && positionMario.X < _graphics.PreferredBackBufferWidth - (textureMario.Width * 4))
            {
                positionMario.X += 5;
                if (direction < 10)
                {
                    movement = "frame0Right";
                    direction += 1;
                }
                else if (direction < 20)
                {
                    movement = "frame1Right";
                    direction += 1;
                }
                else if (direction < 32)
                {
                    movement = "frame2Right";
                    direction += 1;
                }else if (direction > 20)
                {
                    direction = 0;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _spriteBatch.Draw(background, new Vector2(0, 0), null, Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);

            _spriteBatch.Draw(animationMario[movement], positionMario, null, Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);

            _spriteBatch.Draw(animationBlock[block], postionBlock, null, Color.White, 0f, Vector2.Zero, 3f, SpriteEffects.None, 0f);

            _spriteBatch.DrawString("Nombre de Pieces" + Convert.ToString(nbPiece), Vector2.Zero, Color.Red);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
