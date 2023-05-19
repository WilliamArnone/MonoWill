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

		public static bool PointInRectangle(Vector2 point, Vector2 center, Vector2 size)
		{
			Vector2 halfSize = size / 2; ;
			return point.X >= center.X - halfSize.X
				&& point.X <= center.X + halfSize.X
				&& point.Y >= center.Y - halfSize.Y
				&& point.Y >= center.Y + halfSize.Y;
		}
	}
}
