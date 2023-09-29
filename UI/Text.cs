using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill.UI
{
	public class Text : WorldObject
	{
		public string text;
		public Vector2 pivot;
		public Vector2 boxSize;
		public Color color;
		public float size;

		public int maxChar = -1;

		public SpriteFont font { get; private set; }

		string _text;

		public Text(string fontpath, string text)
		{
			this.text = text;
			_text = "";
			font = Content.Manager.Load<SpriteFont>(fontpath);
			color = Color.Black;
			pivot = Vector2.Zero;
			size = 1f;
			scale = Vector2.One;
		}

		public override void Update()
		{
			base.Update();

			var textSize = font.MeasureString(text) * size;
			if(textSize.X < boxSize.X) { _text = text; return; }

			int charIndex = 0;
			int rows = 0;
			StringBuilder sb = new StringBuilder(text);

			while(textSize.X > boxSize.X && charIndex<text.Length)
			{
				string row = string.Empty;
				int i;
				int lastSpace = -1;

				for (i = 0; i+charIndex < text.Length 
					&& ((font.MeasureString(row) * size).X < boxSize.X || i==0); i++)
				{
					row += text[charIndex+i];
					if (text[charIndex+i] == ' ') { lastSpace = i; }
				}

				if(lastSpace > -1) { i = lastSpace; }

				charIndex+=i;
				if(charIndex<text.Length)
				{
					sb.Insert(charIndex+rows, "\n");
					if (lastSpace != -1)
					{
						sb.Remove(charIndex + rows + 1, 1);
					}
					else 
					{ 
						rows++;
					}
				}

				_text = sb.ToString();
				textSize = font.MeasureString(_text) * size;
			}
		}

		public bool center = false;

		public override void Draw(Vector2 parentPosition, Vector2 parentScale)
		{
			var pos = parentPosition + position;
			Vector2 orig;
			if(center)
				orig = parentScale * scale * font.MeasureString(_text) * pivot;
			else	
				orig = parentScale * scale * boxSize * pivot;
			if(maxChar == -1)
			{
				Graphic.SpriteBatch.DrawString(font, _text, pos, color, 0, orig, size, SpriteEffects.None, 0.5f);
			}
			else
			{
				Graphic.SpriteBatch.DrawString(font, _text.Substring(0, Math.Min(maxChar, _text.Length)), pos, color, 0, orig, size, SpriteEffects.None, 1f);
			}

			base.Draw(parentPosition, parentScale);
		}
	}
}
