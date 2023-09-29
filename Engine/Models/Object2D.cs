using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
    public class Object2D : WorldObject
	{
		public Vector2 size;
		public Vector2 pivot;
		public float angle;

		public float sortingOrder; 

		public Color color;

		public Texture2D sprite;

		public Object2D() : base()
		{
			pivot = new Vector2(0.5f, 0.5f);
			scale = new Vector2(1, 1);
			color = Color.White;
			angle = 0f;
			sortingOrder = .9f;
		}

		/// <summary>
		/// Object 2D constructor
		/// </summary>
		/// <param name="addToWorldAfterCreation">If set to false it won't be added to the game objects.</param>
		public Object2D(string path) : base()
		{
			sprite = Content.Manager.Load<Texture2D>(path);
			FitImage();
			pivot = new Vector2(0.5f, 0.5f);
			scale = new Vector2(1, 1);
			color = Color.White;
			angle = 0f;
			sortingOrder = .9f;

		}

		public void FitImage()
		{
			if (sprite == null) return;
			size = new Vector2(sprite.Width, sprite.Height);
		}

		public bool flipX;

		public override void Draw(Vector2 parentPosition, Vector2 parentScale)
		{
			if (sprite != null)
			{
				Vector2 pos = parentPosition + position;
				Vector2 siz = size * parentScale * scale;
				Rectangle rect = new Rectangle((int)pos.X,
												(int) pos.Y,
												(int) siz.X,
												(int) siz.Y);
				Vector2 origin = new Vector2(sprite.Bounds.Width * pivot.X, sprite.Bounds.Height * pivot.Y);
				if(flipX)
					Graphic.SpriteBatch.Draw(sprite, rect, null, color, angle, origin, SpriteEffects.FlipHorizontally, sortingOrder);
				else
					Graphic.SpriteBatch.Draw(sprite, rect, null, color, angle, origin, new SpriteEffects(), sortingOrder);
			}
			base.Draw(parentPosition, parentScale);
		}
	}
}
