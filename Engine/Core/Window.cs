using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public static class Window
	{
		public static GameWindow GameWindow { get; private set; }
		public static GraphicsDeviceManager DeviceManager { get; private set; }

		public static bool IsFullScreen => DeviceManager.IsFullScreen;

		static int _width;

		internal static void Initialize(GraphicsDeviceManager deviceManager, GameWindow gameWindow)
		{
			GameWindow = gameWindow;
			DeviceManager = deviceManager;
			_width = DeviceManager.PreferredBackBufferWidth;
			_height = DeviceManager.PreferredBackBufferHeight;
			GameWindow.ClientSizeChanged += UpdateWindowSize;
		}

		static void UpdateWindowSize(object sender, EventArgs e)
		{
			DeviceManager.PreferredBackBufferWidth = GameWindow.ClientBounds.Width;
			DeviceManager.PreferredBackBufferHeight = GameWindow.ClientBounds.Height;

			DeviceManager.ApplyChanges();
		}

		/// <summary>
		/// Window width.
		/// </summary>
		public static int Width
		{
			get => IsFullScreen ? Screen.Width : DeviceManager.PreferredBackBufferWidth;
			set
			{
				_width = value;
				if (!IsFullScreen)
				{
					DeviceManager.PreferredBackBufferWidth = _width;
					DeviceManager.ApplyChanges();
				}
			}
		}

		private static int _height;

		/// <summary>
		/// Window height.
		/// </summary>
		public static int Height
		{
			get => IsFullScreen ? Screen.Height : DeviceManager.PreferredBackBufferHeight;
			set
			{
				_height = value;
				if (!IsFullScreen)
				{
					DeviceManager.PreferredBackBufferHeight = _height;
					DeviceManager.ApplyChanges();
				}
			}
		}

		/// <summary>
		/// Allowing borders in window.
		/// </summary>
		public static bool IsBorderless
		{
			get => GameWindow.IsBorderless;
			set => GameWindow.IsBorderless = value;
		}

		public static void SetFullScreen(bool fullscreen)
		{
			if (fullscreen)
			{
				DeviceManager.PreferredBackBufferWidth = Screen.Width;
				DeviceManager.PreferredBackBufferHeight = Screen.Height;
			}
			else
			{
				DeviceManager.PreferredBackBufferWidth = _width;
				DeviceManager.PreferredBackBufferHeight = _height;
			}

			DeviceManager.IsFullScreen = fullscreen;
			DeviceManager.ApplyChanges();
		}


		/// <summary>
		/// Window size.
		/// </summary>
		public static Vector2 Size
		{
			get => new(Width, Height);
			set
			{
				Width = (int)value.X;
				Height = (int)value.Y;
				DeviceManager.ApplyChanges();
			}
		}

		public static void ToggleFullScreen() =>
			SetFullScreen(!IsFullScreen);
	}
}
