using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MonoWill
{
	public static class Graphic
	{
		// NOTE: To control VSync, use SynchronizeWithVerticalRetrace.

		public static bool UsePixelart { get; set; }
		public static Color BackgroundColor { get; set; }

		public static GraphicsDevice GraphicsDevice { get; private set; }
		public static SpriteBatch SpriteBatch { get; private set; }

		internal static void Initialize(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
		{
			GraphicsDevice = graphicsDevice;
			SpriteBatch = spriteBatch;
			BackgroundColor = Color.Black;
		}

		public static int Width { get; set; }
		public static int Height { get; set; }

		public static Vector2 Size
		{
			get => new(Width, Height);
			set
			{
				Width = (int)value.X;
				Height = (int)value.Y;
			}
		}

		#region DRAW

		internal static void Begin()
		{
			SetupFullViewport();
			GraphicsDevice.Clear(BackgroundColor);
			SetupVirtualViewport();

			//GraphicsDevice.Clear(Color.CornflowerBlue);
			if (UsePixelart)
			{
				SpriteBatch.Begin(SpriteSortMode.Deferred, 
					BlendState.AlphaBlend, 
					SamplerState.PointClamp,
					null,
					null,
					null,
					Matrix.CreateScale((float)_viewport.Width / Width, (float)_viewport.Height / Height, 1f));
			}
			else
			{
				SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
			}
		}

		internal static void End()
		{
			SpriteBatch.End();
		}

		#endregion

		#region VIEWPORT

		static Viewport _viewport;

		public static void SetupFullViewport()
		{
			var vp = new Viewport();
			vp.X = vp.Y = 0;
			vp.Width = Window.Width;
			vp.Height = Window.Height;

			GraphicsDevice.Viewport = vp;
		}

		public static void SetupVirtualViewport()
		{
			//TODO: use different configurations
			var targetAspectRatio = Width / (float)Height;
			// figure out the largest area that fits in this resolution at the desired aspect ratio

			var width = Window.Width;
			var height = (int)(width / targetAspectRatio + .5f);

			if (height > Window.Height)
			{
				height = Window.Height;
				// PillarBox
				width = (int)(height * targetAspectRatio + .5f);

			}

			_viewport = new Viewport
			{
				X = (Window.Width / 2) - (width / 2),
				Y = (Window.Height / 2) - (height / 2),
				Width = width,
				Height = height
			};
			
			GraphicsDevice.Viewport = _viewport;
		}

		#endregion
	}
}
