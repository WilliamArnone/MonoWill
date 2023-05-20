using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill.Mathematics
{
	public static class Vector2Extension
	{
		public static float DistanceFrom(this Vector2 vec, Vector2 other)
			=> Vector2.Distance(vec, other);

		public static float DistanceSqrFrom(this Vector2 vec, Vector2 other)
			=> Vector2.DistanceSquared(vec, other);

		//public static Vector2 SizeOfRectangle(Vector2 vec, Vector2 other)
		//	=> GameMath.SizeOfRectangle(vec, other);
		

		public static float AngleToward(this Vector2 v, Vector2 other)
		{
			Vector2 vecDis = other - v;
			return MathF.Atan2(vecDis.Y, other.X);
		}
	}
}
