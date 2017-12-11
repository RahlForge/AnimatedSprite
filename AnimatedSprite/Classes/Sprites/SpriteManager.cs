using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace AnimatedSprite.Classes.Sprites
{
    class SpriteManager : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        List<Sprite> spriteList;
        List<AutomatedSprite> playerLives;
        UserSprite player;

        // Spawning
        int enemySpawnMinMilliseconds = 1000;
        int enemySpawnMaxMilliseconds = 2000;
        int enemyMinSpeed = 2;
        int enemyMaxSpeed = 6;
        int nextSpawnTime = 0;
        int chanceAutomated = 60;
        int chanceRandom = 25;
        int chanceChasing = 10;
        //int chanceEvading = 5;

        // Scoring
        int automatedSpriteScore = 10;
        int randomSpiteScore = 15;
        int chasingSpriteScore = 20;
        int evadingSpriteScore = 0;

        // Powerups
        int scaleExpiration = 0;
        int speedExpiration = 0;
        int freezeExpiration = 0;
        float playerHoldSpeed;

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
            playerLives = new List<AutomatedSprite>();
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

            for (int i = 0; i < ((Game1)Game).LivesRemaining; i++)
            {
                int offset = 10 + i * 40;
                playerLives.Add(new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"Images/threerings"),
                    new Vector2(offset, 35), 0.5f, Vector2.Zero, new Point(6, 8),
                    new Point(75, 75), new Point(0, 0), 10, "", 0));
            }

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

            UpdateSprites(gameTime);

            CheckPowerUpExpiration(gameTime);

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
                s.Draw(gameTime, spriteBatch);

            foreach (Sprite s in playerLives)
                s.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void ResetSpawnTime()
        {
            nextSpawnTime = ((Game1)Game).rnd.Next(
                enemySpawnMinMilliseconds,
                enemySpawnMaxMilliseconds);
        }

        /// <summary>
        /// 
        /// </summary>
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

            // Get the sprite type and create the sprite at the random position and speed, starting at a random frame
            int random = ((Game1)Game).rnd.Next(100);
            if (random < chanceAutomated)
                spriteList.Add(new AutomatedSprite(
                    Game.Content.Load<Texture2D>(@"Images/threeblades"), position, speed,
                    new Point(6, 8), frameSize, new Point(
                        ((Game1)Game).rnd.Next(1, 6),
                        ((Game1)Game).rnd.Next(1, 8)),
                    10, "threebladescollision", automatedSpriteScore));
            else if (random < chanceAutomated + chanceRandom)
                spriteList.Add(new RandomSprite(
                    Game.Content.Load<Texture2D>(@"Images/fourblades"), position, speed,
                    new Point(6, 8), frameSize, new Point(
                        ((Game1)Game).rnd.Next(1, 6),
                        ((Game1)Game).rnd.Next(1, 8)),
                    10, "fourbladescollision", randomSpiteScore));
            else if (random < chanceAutomated + chanceRandom + chanceChasing)
            {
                if (((Game1)Game).rnd.Next(2) == 0)
                {
                    spriteList.Add(new ChasingSprite(
                        Game.Content.Load<Texture2D>(@"Images/skullball"), position, speed,
                        new Point(6, 8), frameSize, new Point(
                            ((Game1)Game).rnd.Next(1, 6),
                            ((Game1)Game).rnd.Next(1, 8)),
                        10, "skullcollision", chasingSpriteScore, this));
                }
                else
                {
                    spriteList.Add(new ChasingSprite(
                        Game.Content.Load<Texture2D>(@"Images/plus"), position, speed,
                        new Point(6, 4), frameSize, new Point(
                            ((Game1)Game).rnd.Next(1, 6),
                            ((Game1)Game).rnd.Next(1, 8)),
                        10, "pluscollision", chasingSpriteScore, this));
                }
            }
            else
                spriteList.Add(new EvadingSprite(
                    Game.Content.Load<Texture2D>(@"Images/bolt"), position, speed,
                    new Point(6, 8), frameSize, new Point(
                        ((Game1)Game).rnd.Next(1, 6),
                        ((Game1)Game).rnd.Next(1, 8)),
                    10, "boltcollision", evadingSpriteScore, this, 0.75f, 150));          
        }

        public Vector2 GetPlayerPosition()
        {
            return player.Position;
        }

        /// <summary>
        /// Updates all sprites controlled by the SpriteManager, cycling their
        /// sprite sheet and detecting collisions
        /// </summary>
        /// <param name="gameTime">A snapshot of the currently elapsed game time</param>
        private void UpdateSprites(GameTime gameTime)
        {
            // Update player
            player.Update(gameTime, Game.Window.ClientBounds);

            // Update all remaining sprites
            for (int i = 0; i < spriteList.Count; i++)
            {
                Sprite s = spriteList[i];

                s.Update(gameTime, Game.Window.ClientBounds);

                if (s.IsOutOfBounds(Game.Window.ClientBounds))
                {
                    ((Game1)Game).AddScore(s.ScoreValue);
                    spriteList.RemoveAt(i);
                    --i;
                }

                if (s.GetCollisionRect().Intersects(player.GetCollisionRect()))
                {
                    SoundEffect soundEffect = Game.Content.Load<SoundEffect>(
                        @"Audio/" + s.CollisionEffectName);
                    soundEffect.Play();

                    if (s is AutomatedSprite ||
                        s is RandomSprite)
                    {
                        playerLives.RemoveAt(playerLives.Count - 1);
                        --((Game1)Game).LivesRemaining;
                    }
                    else if (s.CollisionEffectName == "pluscollision")
                    {
                        scaleExpiration = 5000;
                        player.ModifyScale(2f);
                    }
                    else if (s.CollisionEffectName == "skullcollision")
                    {
                        freezeExpiration = 2000;
                        player.ModifySpeed(0f);
                    }
                    else if (s.CollisionEffectName == "boltcollision")
                    {
                        speedExpiration = 5000;
                        player.ModifySpeed(2f);
                    }

                    spriteList.RemoveAt(i);
                    --i;
                }
            }

            foreach (Sprite s in playerLives)
                s.Update(gameTime, Game.Window.ClientBounds);
        }

        protected void UnfreezePlayer()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gameTime"></param>
        protected void CheckPowerUpExpiration(GameTime gameTime)
        {
            // Check scaling power-ups
            if (scaleExpiration > 0)
            {
                scaleExpiration -= gameTime.ElapsedGameTime.Milliseconds;
                if (scaleExpiration <= 0)
                {
                    scaleExpiration = 0;
                    player.ResetScale();
                }
            }

            // Check speed power-ups
            if (speedExpiration > 0)
            {
                speedExpiration -= gameTime.ElapsedGameTime.Milliseconds;
                if (speedExpiration <= 0)
                {
                    speedExpiration = 0;
                    player.ResetSpeed();
                }
            }

            // Check speed power-ups
            if (freezeExpiration > 0)
            {
                freezeExpiration -= gameTime.ElapsedGameTime.Milliseconds;
                if (freezeExpiration <= 0)
                {
                    freezeExpiration = 0;
                    UnfreezePlayer();
                }
            }
        }
    }
}
