using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public class Screen
	{
		public static int height;
		public static int width;

		public static bool IsPointInside(Vector2 point)
		{
			return point.X >= 0 && point.X <= width && point.Y >= 0 && point.Y <= height;
		}
	}
}
