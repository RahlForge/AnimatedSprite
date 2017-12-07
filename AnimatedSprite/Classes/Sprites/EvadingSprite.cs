using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprite.Classes.Sprites
{
    class EvadingSprite : Sprite
    {
        SpriteManager spriteManager;

        float evasionSpeedModifier;
        int evasionRange;
        bool evade;

        public EvadingSprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int collisionOffset,
            SpriteManager spriteManager, float evasionSpeedModifier, int evasionRange)
            : base(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                  collisionOffset)
        {
            this.spriteManager = spriteManager;
            this.evasionSpeedModifier = evasionSpeedModifier;
            this.evasionRange = evasionRange;
            evade = false;
        }

        // comments
        public EvadingSprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int collisionOffset,
            int millisecondsPerFrame, SpriteManager spriteManager, 
            float evasionSpeedModifier, int evasionRange)
            : base(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                  collisionOffset, millisecondsPerFrame)
        {
            this.spriteManager = spriteManager;
            this.evasionSpeedModifier = evasionSpeedModifier;
            this.evasionRange = evasionRange;
            evade = false;
        }

        public override Vector2 direction
        {
            get { return speed; }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += speed;

            Vector2 player = spriteManager.GetPlayerPosition();

            if (evade)
            {
                if (player.X < position.X)
                    position.X += Math.Abs(speed.Y);
                else if (player.X > position.X)
                    position.X -= Math.Abs(speed.Y);

                if (player.Y < position.Y)
                    position.Y += Math.Abs(speed.X);
                else if (player.X > position.X)
                    position.Y -= Math.Abs(speed.X);
            }
            else
            {
                if (Vector2.Distance(position, player) < evasionRange)
                {
                    speed *= -evasionSpeedModifier;
                    evade = true;
                }
            }

            base.Update(gameTime, clientBounds);
        }
    }
}
