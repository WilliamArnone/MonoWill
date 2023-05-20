using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;

namespace MonoWill
{
    public class WillGame : Game
    {
        internal static World world { get; private set; }

        static Queue<World> worldQueue = new Queue<World>();
        
        GraphicsDeviceManager _graphics;

        public WillGame(World world)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            WillGame.world = world;
        }

        public static void LoadNewWorld(World world, bool preserveCurrentWorld)
        {
            if (preserveCurrentWorld)
            {
                worldQueue.Enqueue(WillGame.world);
            }
            WillGame.world = world;
            world.OnCreation();
        }

        public static void BackToPreviusWorld()
        {
            if(worldQueue.Count == 0) { return; }
            world = worldQueue.Dequeue();
        }

		#region INITIALIZATION

		protected override void Initialize()
        {
			Globals.Window = Window;
            Screen.Initialize(_graphics);
            Time.Initialize();
            Input.Initialize();

			base.Initialize();
			world.OnCreation();
		}

        protected override void LoadContent()
        {
            Globals.Content = Content;
            Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

		#endregion

		#region GAME_LOOP

		protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Time.Update(gameTime);
            Input.Update();

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

		#endregion
	}
}