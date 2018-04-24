using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Editor_Core
{
    public class Player
    {
        public Vector2 Position;
        private float _speed = 500; // In pixel/second

        public BulletML.Vector2 GetPosition()
        {
            return new BulletML.Vector2(Position.X, Position.Y);
        }

        public void Initialize()
        {
            Position.X = Config.GameAeraSize.X / 2f;
            Position.Y = (Config.GameAeraSize.Y / 2f) + (Editor.Graphics.PreferredBackBufferHeight / 2f) - Editor.Graphics.PreferredBackBufferHeight / 10f;
        }

        public void Update(GameTime gameTime)
        {
            var dt = (float)(gameTime.ElapsedGameTime.TotalSeconds);

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                Position.X -= _speed * dt;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                Position.X += _speed * dt;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                Position.Y -= _speed * dt;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                Position.Y += _speed * dt;

            Position.X = MathHelper.Clamp(Position.X, 0, Config.GameAeraSize.X);
            Position.Y = MathHelper.Clamp(Position.Y, 0, Config.GameAeraSize.Y);
        }
    }
}