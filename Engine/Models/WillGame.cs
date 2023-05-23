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
        internal static World World { get; private set; }

        static readonly Queue<World> worldQueue = new();
		readonly GraphicsDeviceManager _graphics;

        public WillGame(World world)
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            World = world;
        }

        public static void LoadNewWorld(World world, bool preserveCurrentWorld)
        {
            if (preserveCurrentWorld)
            {
                worldQueue.Enqueue(WillGame.World);
            }
            WillGame.World = world;
            world.OnCreation();
        }

        public static void BackToPreviusWorld()
        {
            if(worldQueue.Count == 0) { return; }
            World = worldQueue.Dequeue();
        }

		#region INITIALIZATION

		protected override void Initialize()
        {
            MonoWill.Window.Initialize(_graphics, Window);
            Time.Initialize();
            Input.Initialize();

			base.Initialize();
			World.OnCreation();
		}

        protected override void LoadContent()
        {
            MonoWill.Content.Initialize(Content);
			Graphic.Initialize(GraphicsDevice, new SpriteBatch(GraphicsDevice));
        }

		#endregion

		#region GAME_LOOP

		protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Time.Update(gameTime);
            Input.Update();

            World.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Graphic.Begin();

            World.Draw();

            Graphic.End();
            base.Draw(gameTime);
        }

		#endregion
	}
}