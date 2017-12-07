using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprite.Classes.Sprites
{
    class ChasingSprite : Sprite
    {
        SpriteManager spriteManager;        

        public ChasingSprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int collisionOffset,
            SpriteManager spriteManager)
            : base(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                  collisionOffset)
        {
            this.spriteManager = spriteManager;
        }

        // comments
        public ChasingSprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int collisionOffset,
            int millisecondsPerFrame, SpriteManager spriteManager)
            : base(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                  collisionOffset, millisecondsPerFrame)
        {
            this.spriteManager = spriteManager;
        }

        public override Vector2 direction
        {
            get { return speed; }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += speed;

            Vector2 player = spriteManager.GetPlayerPosition();

            if (speed.X == 0)
            {
                if (player.X < position.X)
                    position.X -= Math.Abs(speed.Y);
                else if (player.X > position.X)
                    position.X += Math.Abs(speed.Y);
            }

            if (speed.Y == 0)
            {
                if (player.Y < position.Y)
                    position.Y -= Math.Abs(speed.X);
                else if (player.X > position.X)
                    position.Y += Math.Abs(speed.X);
            }

            base.Update(gameTime, clientBounds);
        }
    }
}
