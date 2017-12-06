using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AnimatedSprite.Classes.Sprites
{
    abstract class Sprite
    {
        protected Texture2D textureImage;
        protected Vector2 position;
        protected Vector2 speed;
        protected Point sheetSize;
        protected Point frameSize;
        protected Point currentFrame;
        protected int collisionOffset;        
        protected int millisecondsPerFrame;
        protected int timeSinceLastFrame;
        protected bool edgeBounce;

        const int defaultMsPerFrame = 16;

        public Sprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int collisionOffset)
            : this(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                  defaultMsPerFrame, collisionOffset)
        {

        }

        public Sprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, 
            int millisecondsPerFrame, int collisionOffset)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.speed = speed;
            this.sheetSize = sheetSize;
            this.frameSize = frameSize;
            this.currentFrame = currentFrame;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.collisionOffset = collisionOffset;            
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                CycleSpriteSheet();
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage,
                position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                Color.White, 0f, Vector2.Zero, 
                1f, SpriteEffects.None, 0f);
        }

        protected void CycleSpriteSheet()
        {            
            ++currentFrame.X;
            if (currentFrame.X >= sheetSize.X)
            {
                currentFrame.X = 0;
                ++currentFrame.Y;
                if (currentFrame.Y >= sheetSize.Y)
                    currentFrame.Y = 0;
            }
        }

        public abstract Vector2 direction
        {
            get;
        }

        public Rectangle GetCollisionRect()
        {
            return new Rectangle((int)position.X + collisionOffset,
                (int)position.Y + collisionOffset,
                frameSize.X - (collisionOffset * 2),
                frameSize.Y - (collisionOffset * 2));
        }
    }
}
