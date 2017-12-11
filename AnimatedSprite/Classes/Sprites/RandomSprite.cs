using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprite.Classes.Sprites
{
    class RandomSprite : Sprite
    {
        Random rnd;
        int switchMilliseconds;
        int millisecondsSinceSwitch;
        const int minSwitchMilliseconds = 1000;
        const int maxSwitchMilliseconds = 2000;
        const int minSpeed = -6;
        const int maxSpeed = 6;

        public RandomSprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int collisionOffset,
            int millisecondsPerFrame, string collisionEffectName, int scoreValue)
            : base(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                  collisionOffset, millisecondsPerFrame, collisionEffectName, scoreValue)
        {
            rnd = new Random(DateTime.Now.Millisecond);
            millisecondsSinceSwitch = 0;
            switchMilliseconds = rnd.Next(minSwitchMilliseconds, maxSwitchMilliseconds);
        }

        public RandomSprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int collisionOffset,
            string collisionEffectName, int scoreValue)
            : base(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                  collisionOffset, collisionEffectName, scoreValue)
        {
            rnd = new Random(DateTime.Now.Millisecond);
            millisecondsSinceSwitch = 0;
            switchMilliseconds = rnd.Next(minSwitchMilliseconds, maxSwitchMilliseconds);
        }

        public override Vector2 direction
        {
            get { return speed; }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += speed;

            millisecondsSinceSwitch += gameTime.ElapsedGameTime.Milliseconds;
            if (millisecondsSinceSwitch > switchMilliseconds)
            {
                RandomizeSprite();
            }

            base.Update(gameTime, clientBounds);
        }

        private void RandomizeSprite()
        {
            speed.X = rnd.Next(minSpeed, maxSpeed);
            speed.Y = rnd.Next(minSpeed, maxSpeed);
            millisecondsSinceSwitch = 0;
            switchMilliseconds = rnd.Next(minSwitchMilliseconds, maxSwitchMilliseconds);
        }
    }
}
