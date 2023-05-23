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
		public Vector2 pivot;

		Texture2D myModel;

		/// <summary>
		/// Object 2D constructor
		/// </summary>
		/// <param name="addToWorldAfterCreation">If set to false it won't be added to the game objects.</param>
		public Object2D(string path) : base()
		{
			myModel = Content.Manager.Load<Texture2D>(path);
			size = new Vector2(myModel.Width, myModel.Height);
			pivot = new Vector2(0.5f, 0.5f);
		}

		public override void Update()
		{
		}

		public override void Draw()
		{
			if (myModel == null) return;

			Graphic.SpriteBatch.Draw(myModel, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), null, Color.White, 0, new Vector2(myModel.Bounds.Width*pivot.X, myModel.Bounds.Height*pivot.Y), new SpriteEffects(), 0);
		}
	}
}
