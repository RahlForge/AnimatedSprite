using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace AnimatedSprite.Classes.Sprites
{
    abstract class Sprite
    {
        protected Texture2D textureImage;
        protected Vector2 position;
        protected float scale;
        protected Vector2 speed;
        protected Vector2 initialSpeed;
        protected Point sheetSize;
        protected Point frameSize;
        protected Point currentFrame;
        protected int collisionOffset;        
        protected int millisecondsPerFrame;
        protected int timeSinceLastFrame;
        public string CollisionEffectName { get; protected set; }
        public int ScoreValue { get; protected set; }

        const int defaultMsPerFrame = 16;
        const float initialScale = 1f;


        public Sprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int collisionOffset,
            string collisionEffectName, int scoreValue)
            : this(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                  collisionOffset, defaultMsPerFrame, collisionEffectName, scoreValue)
        {

        }

        public Sprite(Texture2D textureImage, Vector2 position, Vector2 speed,
            Point sheetSize, Point frameSize, Point currentFrame, int collisionOffset,
            int millisecondsPerFrame, string collisionEffectName, int scoreValue)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.speed = speed;
            initialSpeed = speed;
            this.sheetSize = sheetSize;
            this.frameSize = frameSize;
            this.currentFrame = currentFrame;
            this.collisionOffset = collisionOffset;
            CollisionEffectName = collisionEffectName;
            ScoreValue = scoreValue;
            this.millisecondsPerFrame = millisecondsPerFrame;
            ResetScale();
        }

        public Sprite(Texture2D textureImage, Vector2 position, float scale, 
            Vector2 speed, Point sheetSize, Point frameSize, Point currentFrame, 
            int collisionOffset, string collisionEffectName, int scoreValue)
            : this(textureImage, position, speed, sheetSize, frameSize, currentFrame,
                  collisionOffset, defaultMsPerFrame, collisionEffectName, scoreValue)
        {
            this.scale = scale;
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
                scale, SpriteEffects.None, 0f);
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
            return new Rectangle((int)(position.X + (collisionOffset * scale)),
                (int)(position.Y + (collisionOffset * scale)),
                (int)(frameSize.X - (collisionOffset * 2) * scale),
                (int)(frameSize.Y - (collisionOffset * 2) * scale));
        }

        public bool IsOutOfBounds(Rectangle clientBounds)
        {
            // If sprite is out-of-bounds, return true
            if (position.X < -frameSize.X ||
                position.X > clientBounds.Width ||
                position.Y < -frameSize.Y ||
                position.Y > clientBounds.Height)
                return true;
            
            // Otherwise, fall-through to false
            return false;
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public void ModifyScale(float modifier)
        {
            scale *= modifier;
        }

        public void ResetScale()
        {
            scale = initialScale;
        }

        public void ModifySpeed(float modifier)
        {
            speed *= modifier;
        }

        public void ResetSpeed()
        {
            speed = initialSpeed;
        }
    }
}
