using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;

namespace MonoWill
{
    public class Game : Microsoft.Xna.Framework.Game
	{
        internal static World World { get; private set; }
        public static Game Instance { get; private set; }

        static readonly Queue<World> worldQueue = new();
		readonly GraphicsDeviceManager _graphics;

        public Game(World world)
        {
            Instance = this;
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            World = world;
        }

        World resetRequest = null;
        bool preserveRequest = false;

        public static void LoadNewWorld(World world, bool preserveCurrentWorld)
        {
            Instance.resetRequest = world;
            Instance.preserveRequest = preserveCurrentWorld;
        }


		static void _LoadNewWorld(World world, bool preserveCurrentWorld)
        {
            if (preserveCurrentWorld)
            {
                worldQueue.Enqueue(World);
            }
            else
            {
                World.Dispose();
            }
			World = world;
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
            if (resetRequest != null)
            {
                _LoadNewWorld(resetRequest, preserveRequest);
                resetRequest = null;
            }

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