using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoWill
{
    public class WillGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private World world;

        public WillGame(World world)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            this.world = world;
            world.Initialize();
        }

        protected override void Initialize()
        {
            Time.Initialize();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.Content = Content;
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Time.Update(gameTime);

            world.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            Globals.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            world.Draw();

            Globals.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}