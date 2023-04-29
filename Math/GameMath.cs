using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public static class GameMath
	{
		public static float Distance(Vector2 vec1, Vector2 vec2)
		{
			return (vec2 - vec1).Length();
		}

		public static float DistanceSqr(Vector2 vec1, Vector2 vec2)
		{
			return (vec2 - vec1).LengthSquared();
		}

		public static Vector2 SizeOfRectangle(Vector2 vert1, Vector2 vert2)
		{
			return new Vector2(Math.Abs(vert2.X - vert1.X), Math.Abs(vert2.Y - vert1.Y));
		}
	}
}
