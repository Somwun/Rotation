using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Rotation
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D _windowTexture;
        Rectangle _windowRect;
        Vector2 _windowPos;
        KeyboardState keyboardState;
        MouseState mouseState;
        int _speed;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferHeight = 750;
            _graphics.PreferredBackBufferWidth = 750;
            _graphics.ApplyChanges();
            _windowRect = new Rectangle(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2, 200, 200);
            _windowPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            _speed = 4;
            base.Initialize();

        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _windowTexture = Content.Load<Texture2D>("4PanelWindow (1)");
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                _windowPos.X += _speed;
            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                _windowPos.X -= _speed;
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                _windowPos.Y -= _speed;
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                _windowPos.Y += _speed;
            if (_windowRect.Right >= _graphics.PreferredBackBufferWidth)
                _windowPos.X -= _speed;
            else if (_windowRect.Left <= 0)
                _windowPos.X += _speed;
            if (_windowRect.Top <= 0)
                _windowPos.Y += _speed;
            else if (_windowRect.Bottom >= _graphics.PreferredBackBufferHeight)
                _windowPos.Y -= _speed;
            _windowRect.X = (int)_windowPos.X;
            _windowRect.Y = (int)_windowPos.Y;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_windowTexture, _windowRect, null, Color.White, GetAngle(_windowPos, new Vector2(mouseState.X, mouseState.Y)), new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), SpriteEffects.None, 0f);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        public float GetAngle(Vector2 originPoint, Vector2 secondPoint)
        {
            float rise = secondPoint.Y - originPoint.Y;
            float run = secondPoint.X - originPoint.X;

            // First or Fourth Quadrant
            if (originPoint.X <= secondPoint.X && originPoint.Y <= secondPoint.Y || originPoint.X <= secondPoint.X && originPoint.Y >= secondPoint.Y)
                return (float)Math.Atan(rise / run);
            //Second or Third Quadrant
            else
                return (float)(Math.PI + Math.Atan(rise / run));
        }
    }
}