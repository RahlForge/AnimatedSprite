using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using AnimatedSprite.Classes.Sprites;

namespace AnimatedSprite
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteManager spriteManager;
        Texture2D backgroundTexture;
        enum GameState { Start, InGame, GameOver };
        GameState currentGameState = GameState.Start;

        // Audio
        Song track;
        SoundEffect start;

        // Scoring
        SpriteFont scoreFont;
        int currentScore;

        // Randomizer
        public Random rnd { get; private set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // This changes the framerate of the game
            // Call GameUpdate every 50 milliseconds (20fps)
            //TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 50); 
            
            // Window Size - Full            
            //graphics.IsFullScreen = true;
            //graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            // Window Size - Testing
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Randomizer
            rnd = new Random(DateTime.Now.Millisecond);

            // Score
            currentScore = 0;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here            
            backgroundTexture = Content.Load<Texture2D>(@"Images/background");

            scoreFont = Content.Load<SpriteFont>(@"Fonts/Score");

            start = Content.Load<SoundEffect>(@"Audio/start");
            start.Play();

            track = Content.Load<Song>(@"Audio/track");
            MediaPlayer.Play(track);
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            switch (currentGameState)
            {
                case GameState.Start:
                    break;
                case GameState.InGame:
                    break;
                case GameState.GameOver:
                    break;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here         

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here       
            spriteBatch.Begin();

            // Background
            spriteBatch.Draw(backgroundTexture,
                new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height),
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            // Score
            spriteBatch.DrawString(scoreFont, "Score: " + currentScore,
                new Vector2(10, 10), Color.DarkBlue, 0, Vector2.Zero,
                1f, SpriteEffects.None, 1f);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void AddScore(int score)
        {
            currentScore += score;
        }
    }
}
