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
		/// <summary>
		/// Width of the screen.
		/// </summary>
		public static int Width => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;

		/// <summary>
		/// Height of the screen.
		/// </summary>
		public static int Height => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

		/// <summary>
		/// Size of the screen.
		/// </summary>
		public static Vector2 ScreenSize => new(Width, Height);
	}
}
