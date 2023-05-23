using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public static class Content
	{
		public static ContentManager Manager { get; private set; }

		internal static void Initialize(ContentManager contentManager)
		{
			Manager = contentManager;
		}
	}
}
