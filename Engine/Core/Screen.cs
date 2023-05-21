using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public static class Screen
	{
		// NOTE: To control VSync, use SynchronizeWithVerticalRetrace.

		static GraphicsDeviceManager _graphics;

		public static void Initialize(GraphicsDeviceManager graphics)
		{
			_graphics = graphics;
			_canvasWidth = PreferredBackBufferWidth;
			_canvasHeight = PreferredBackBufferHeight;
		}

		#region SCREEN

		/// <summary>
		/// Width of the screen.
		/// </summary>
		public static int ScreenWidth => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

		/// <summary>
		/// Height of the screen.
		/// </summary>
		public static int ScreenHeight => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

		/// <summary>
		/// Size of the screen.
		/// </summary>
		public static Vector2 ScreenSize => new Vector2(ScreenWidth, ScreenHeight);

		public static bool IsFullScreen => _graphics.IsFullScreen;

		#endregion

		#region WINDOW

		public static int PreferredBackBufferWidth => _graphics.PreferredBackBufferWidth;
		public static int PreferredBackBufferHeight => _graphics.PreferredBackBufferHeight;

		/// <summary>
		/// Window width.
		/// </summary>
		public static int CanvasWidth
		{
			get => _canvasWidth;
			set
			{
				_canvasWidth = value;
				if (!IsFullScreen)
				{
					_graphics.PreferredBackBufferWidth = _canvasWidth;
					_graphics.ApplyChanges();
				}
			}
		}

		private static int _canvasWidth;

		/// <summary>
		/// Window height.
		/// </summary>
		public static int CanvasHeight
		{
			get => _canvasHeight;
			set
			{
				_canvasHeight = value;
				if (!IsFullScreen)
				{
					_graphics.PreferredBackBufferHeight = _canvasHeight;
					_graphics.ApplyChanges();
				}
			}
		}
		private static int _canvasHeight;

		/// <summary>
		/// Window size.
		/// </summary>
		public static Vector2 CanvasSize
		{
			get => new Vector2(CanvasWidth, CanvasHeight);
			set
			{
				CanvasWidth = (int)value.X;
				CanvasHeight = (int)value.Y;
				_graphics.ApplyChanges();
			}
		}

		/// <summary>
		/// Allowing borders in window.
		/// </summary>
		public static bool IsBorderless
		{
			get => Globals.Window.IsBorderless;
			set => Globals.Window.IsBorderless = value;
		}

		#endregion

		public static void SetFullScreen(bool fullscreen)
		{
			if (fullscreen)
			{
				_graphics.PreferredBackBufferWidth = ScreenWidth;
				_graphics.PreferredBackBufferHeight = ScreenHeight;
			}
			else
			{
				_graphics.PreferredBackBufferWidth = _canvasWidth;
				_graphics.PreferredBackBufferHeight = _canvasHeight;
			}

			_graphics.IsFullScreen = fullscreen;
			_graphics.ApplyChanges();
		}


		public static void ToggleFullScreen() =>
			SetFullScreen(!IsFullScreen);
	}
}
