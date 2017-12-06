using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace AnimatedSprite.Classes.Sprites
{
    class UserSprite : Sprite
    {
        MouseState prevMouseState;

        public UserSprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int collisionOffset)
            : base(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                  collisionOffset)
        {

        }

        public UserSprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int millisecondsPerFrame, 
            int collisionOffset)
            : base(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                 millisecondsPerFrame, collisionOffset)
        {

        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += direction;

            MouseState currMouseState = Mouse.GetState();
            if (currMouseState.X != prevMouseState.X ||
                currMouseState.Y != prevMouseState.Y)
                position = new Vector2(currMouseState.X, currMouseState.Y);
            prevMouseState = currMouseState;

            float xExtent = clientBounds.Width - frameSize.X;
            if (position.X < 0)
                position.X = 0;
            else if (position.X > xExtent)
                position.X = xExtent;

            float yExtent = clientBounds.Height - frameSize.Y;
            if (position.Y < 0)
                position.Y = 0;
            else if (position.Y > yExtent)
                position.Y = yExtent;

            base.Update(gameTime, clientBounds);
        }

        public override Vector2 direction
        {
            get
            {
                Vector2 inputDirection = Vector2.Zero;

                KeyboardState keyboardState = Keyboard.GetState();
                if (keyboardState.IsKeyDown(Keys.Left))
                    inputDirection.X -= 1;
                if (keyboardState.IsKeyDown(Keys.Right))
                    inputDirection.X += 1;
                if (keyboardState.IsKeyDown(Keys.Up))
                    inputDirection.Y -= 1;
                if (keyboardState.IsKeyDown(Keys.Down))
                    inputDirection.Y += 1;

                return inputDirection * speed;
            }
        }
    }
}
