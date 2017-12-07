using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace AnimatedSprite.Classes.Sprites
{
    class SpriteManager : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        List<Sprite> spriteList;
        UserSprite player;

        int enemySpawnMinMilliseconds = 1000;
        int enemySpawnMaxMilliseconds = 2000;
        int enemyMinSpeed = 2;
        int enemyMaxSpeed = 6;
        int nextSpawnTime = 0;

        public SpriteManager(Game game) 
            : base(game)
        {

        }               

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>       
        public override void Initialize()
        {
            // TODO: Add initialization logic here                        
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            spriteList = new List<Sprite>();
            ResetSpawnTime();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use Game.Content to load your game content here 
            player = new UserSprite(
                Game.Content.Load<Texture2D>(@"Images/threerings"),
                Vector2.Zero, new Vector2(6, 6), new Point(6, 8), 
                new Point(75, 75), new Point(0, 0), 10);

            /*
            spriteList.Add(
                new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"Images/plus"),
                    new Vector2(150, 150), new Vector2(1f, -3f), new Point(6, 4),
                    new Point(75, 75), new Point(0, 0), 10, true));

            spriteList.Add(
                new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"Images/skullball"),
                    new Vector2(150, 300), new Vector2(-2f, -3f), new Point(6, 8),
                    new Point(75, 75), new Point(0, 0), 10, true));

            spriteList.Add(
                new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"Images/plus"),
                    new Vector2(300, 150), new Vector2(3f, -1f), new Point(6, 4),
                    new Point(75, 75), new Point(0, 0), 10, true));

            spriteList.Add(
                new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"Images/skullball"),
                    new Vector2(600, 400), new Vector2(-3f, 1f), new Point(6, 8),
                    new Point(75, 75), new Point(0, 0), 10, true));
            */

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // TODO: Add update logic here

            // Check for spawn
            nextSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
            if (nextSpawnTime < 0)
            {
                SpawnEnemy();

                // Reset the spawn timer
                ResetSpawnTime();
            }

            // Update player
            player.Update(gameTime, Game.Window.ClientBounds);

            // Update all remaining sprites
            for (int i = 0; i < spriteList.Count; i++)
            {
                Sprite s = spriteList[i];

                s.Update(gameTime, Game.Window.ClientBounds);

                if (s.IsOutOfBounds(Game.Window.ClientBounds))
                {
                    spriteList.RemoveAt(i);
                    --i;
                }

                if (s.GetCollisionRect().Intersects(player.GetCollisionRect()))
                {
                    spriteList.RemoveAt(i);
                    --i;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // TODO: Add drawing logic here

            spriteBatch.Begin(SpriteSortMode.BackToFront, 
                BlendState.AlphaBlend);

            // Draw player
            player.Draw(gameTime, spriteBatch);

            // Draw all remaining sprites
            foreach (Sprite s in spriteList)
            {
                s.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ResetSpawnTime()
        {
            nextSpawnTime = ((Game1)Game).rnd.Next(
                enemySpawnMinMilliseconds,
                enemySpawnMaxMilliseconds);
        }

        private void SpawnEnemy()
        {
            Vector2 speed = Vector2.Zero;
            Vector2 position = Vector2.Zero;

            // Default frame size
            Point frameSize = new Point(75, 75);

            // Pick a screen side, position and speed
            switch (((Game1)Game).rnd.Next(4))
            {
                case 0: // Right-to-Left
                    position = new Vector2(
                        -frameSize.X,
                        ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight - frameSize.Y));
                    speed = new Vector2(((Game1)Game).rnd.Next(
                        enemyMinSpeed, enemyMaxSpeed), 0f);
                    break;
                case 1: // Left-to-Right
                    position = new Vector2(
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth,
                        ((Game1)Game).rnd.Next(
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight - frameSize.Y));
                    speed = new Vector2(-((Game1)Game).rnd.Next(
                        enemyMinSpeed, enemyMaxSpeed), 0);
                    break;
                case 2: // Top-to-Bottom
                    position = new Vector2(
                        ((Game1)Game).rnd.Next(0,
                            Game.GraphicsDevice.PresentationParameters.BackBufferWidth - frameSize.X),
                        -frameSize.Y);
                    speed = new Vector2(0, ((Game1)Game).rnd.Next(enemyMinSpeed, enemyMaxSpeed));
                    break;
                default: // Bottom-to-Top
                    position = new Vector2(
                        ((Game1)Game).rnd.Next(0,
                            Game.GraphicsDevice.PresentationParameters.BackBufferWidth - frameSize.X),
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight);
                    speed = new Vector2(0, -((Game1)Game).rnd.Next(enemyMinSpeed, enemyMaxSpeed));
                    break;
            }

            // Create the sprite at the random position and speed, starting at a random frame
            spriteList.Add(new EvadingSprite(
                Game.Content.Load<Texture2D>(@"Images/skullball"), position, speed,
                new Point(6, 8), frameSize, new Point(
                    ((Game1)Game).rnd.Next(1, 6), 
                    ((Game1)Game).rnd.Next(1, 8)), 
                10, this, 0.75f, 150));
        }

        public Vector2 GetPlayerPosition()
        {
            return player.Position;
        }
    }
}
