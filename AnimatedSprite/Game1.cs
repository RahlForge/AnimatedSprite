using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        MouseState prevMouseState;

        // Rings sprite variables
        Texture2D ringsTexture;
        Vector2 ringsPosition;        
        Point ringsFrameSize;
        Point ringsCurrentFrame;
        Point ringsSheetSize;
        int ringsTimeSinceLastFrame;
        int ringsMsPerFrame;
        float ringsXExtent;
        float ringsYExtent;
        const float ringsSpeed = 6;
        const int ringsRectOffset = 10;

        // Skullball sprite variables
        Texture2D skullTexture;
        Vector2 skullPosition;
        Vector2 skullSpeed;
        Point skullFrameSize;
        Point skullCurrentFrame;
        Point skullSheetSize;
        int skullTimeSinceLastFrame;
        int skullMsPerFrame;
        float skullXExtent;
        float skullYExtent;
        const int skullRectOffset = 10;

        // Plus sprite variables
        Texture2D plusTexture;
        Vector2 plusPosition;
        Vector2 plusSpeed;
        Point plusFrameSize;
        Point plusCurrentFrame;
        Point plusSheetSize;
        int plusTimeSinceLastFrame;
        int plusMsPerFrame;
        float plusXExtent;
        float plusYExtent;
        const int plusRectOffset = 10;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // This changes the framerate of the game
            //TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 50); // Call GameUpdate every 50 milliseconds (20fps)
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

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {          
            // TODO: use this.Content to load your game content here
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

            base.Draw(gameTime);
        }
    }
}
