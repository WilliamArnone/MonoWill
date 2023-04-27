using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
    public class Object2D : WorldObject
	{
		public Vector2 size;

		Texture2D myModel;

		/// <summary>
		/// Object 2D constructor
		/// </summary>
		/// <param name="addToWorldAfterCreation">If set to false it won't be added to the game objects.</param>
		public Object2D(string path, bool addToWorldAfterCreation = true) : base(addToWorldAfterCreation)
		{
			myModel = Globals.Content.Load<Texture2D>(path);
		}

		public override void Update()
		{
		}

		public override void Draw()
		{
			if (myModel == null) return;

			Globals.SpriteBatch.Draw(myModel, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), null, Color.White, 0, new Vector2(myModel.Bounds.Width/2, myModel.Bounds.Height/2), new SpriteEffects(), 0);
		}
	}
}
