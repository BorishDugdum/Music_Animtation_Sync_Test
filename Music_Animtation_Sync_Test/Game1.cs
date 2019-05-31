using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Music_Animtation_Sync_Test
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle DestinationRectangle, SourceRectangle;
        Vector2 GameScale;
        readonly Point NativeScreen = new Point(320, 180);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            graphics.ApplyChanges();

            //this is used to keep our native screen scaled to the backbuffer
            SourceRectangle = new Rectangle(0, 0, NativeScreen.X, NativeScreen.Y); //native
            var x_center = (SourceRectangle.Width < graphics.PreferredBackBufferWidth) ? (graphics.PreferredBackBufferWidth - SourceRectangle.Width) / 2 : 0;
            var y_center = (SourceRectangle.Height < graphics.PreferredBackBufferHeight) ? (graphics.PreferredBackBufferHeight - SourceRectangle.Height) / 2 : 0;
            DestinationRectangle = new Rectangle(x_center, y_center, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);

            GameScale = new Vector2(
                ((float)DestinationRectangle.Width / (float)SourceRectangle.Width),
                ((float)DestinationRectangle.Height / (float)SourceRectangle.Height));

            DestinationRectangle = new Rectangle(x_center, 0, graphics.PreferredBackBufferWidth, (int)(graphics.PreferredBackBufferHeight * (graphics.PreferredBackBufferHeight / NativeScreen.Y / 2)));
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

            base.Initialize();

            GameScreen.Initialize(Content, graphics.GraphicsDevice, NativeScreen);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            GameScreen.Update(gameTime.ElapsedGameTime.Milliseconds);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            var GameScreenTarget = GameScreen.Draw(gameTime.ElapsedGameTime.Milliseconds);

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);
            spriteBatch.Draw(GameScreenTarget, DestinationRectangle, SourceRectangle, Color.White);
            //spriteBatch.Draw(
            //    GameScreenTarget, null,
            //    DestinationRectangle,
            //    SourceRectangle,
            //    Vector2.Zero,
            //    0f,
            //    GameScale,
            //    Color.White,
            //    SpriteEffects.None, 
            //    0f);

            //spriteBatch.Draw(GameScreenTarget, Vector2.Zero, SourceRectangle, Color.White, 0f, Vector2.Zero, GameScale, SpriteEffects.None, 0f);
            spriteBatch.End();

        }
    }
}
