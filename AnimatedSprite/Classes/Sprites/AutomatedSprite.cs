using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprite.Classes.Sprites
{
    class AutomatedSprite : Sprite
    {
        private bool confineToWindow;

        public AutomatedSprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int collisionOffset,
            int millisecondsPerFrame, string collisionEffectName, int scoreValue, bool confineToWindow = false)
            : base(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                  collisionOffset, millisecondsPerFrame, collisionEffectName, scoreValue)
        {
            this.confineToWindow = confineToWindow;
        }

        public AutomatedSprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int collisionOffset,
            string collisionEffectName, int scoreValue, bool confineToWindow = false)
            : base(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                  collisionOffset, collisionEffectName, scoreValue)
        {
            this.confineToWindow = confineToWindow;
        }

        public override Vector2 direction
        {
            get { return speed; }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += direction;            

            if (confineToWindow)
            {
                if (position.X < 0 ||
                    position.X > clientBounds.Width - frameSize.X)
                    speed.X *= -1;

                if (position.Y < 0 ||
                    position.Y > clientBounds.Height - frameSize.Y)
                    speed.Y *= -1;
            }

            base.Update(gameTime, clientBounds);
        }
    }
}
