using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill.Mathematics
{
	public static class Vector2Math
	{
		public static float Distance(Vector2 vec1, Vector2 vec2)
		{
			return MathF.Sqrt(MathF.Pow(vec1.X, 2) + MathF.Pow(vec2.Y, 2));
		}

		public static float DistanceSqrFrom(this Vector2 vec1, Vector2 vec2)
		{
			return MathF.Pow(vec1.X, 2) + MathF.Pow(vec2.Y, 2);
		}
	}
}
