#if MOUSE
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public class WillMouse
	{
		bool dragging;
		bool rightDrag;

		Vector2 newPos;
		Vector2 oldPos;
		Vector2 firstPos;
		
		Vector2 newAdjustedPos;
		Vector2 systemCursorPos;
		Vector2 screenLoc;

		MouseState newMouse;
		MouseState oldMouse;
		MouseState firstMouse;


		public WillMouse()
		{
			dragging = false;

			newMouse = Mouse.GetState();
			oldMouse = newMouse;
			firstMouse = newMouse;

			Vector2 pos = new Vector2(newMouse.Position.X, newMouse.Position.Y);
			newPos = pos;
			oldPos = pos;
			firstPos = pos;
			newMouse = Mouse.GetState();
			newPos = new Vector2(newMouse.Position.X, newMouse.Position.Y);
		}

		public void Update()
		{
			newMouse = Mouse.GetState();
			newPos = new Vector2(newMouse.Position.X, newMouse.Position.Y);

			if(newMouse.LeftButton != newMouse.LeftButton)
			{
				if(newMouse.LeftButton == ButtonState.Pressed)
				{
					firstMouse = newMouse;
					firstPos = newPos;
				}
			}
		}

		public int GetMouseWheelChange()
		{
			return newMouse.ScrollWheelValue - oldMouse.ScrollWheelValue;
		}


		public Vector2 GetScreenPos(MouseState mouse)
		{
			return new Vector2(mouse.Position.X, mouse.Position.Y);
		}

		public virtual bool LeftClick()
		{
			if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
			if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
			{
				return true;
			}

			return false;
		}

		public virtual bool LeftClickHold()
		{
			bool holding = false;

			if (newMouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
			{
				holding = true;

				if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 || Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
				{
					dragging = true;
				}
			}



			return holding;
		}

		public virtual bool LeftClickRelease()
		{
			if (newMouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed)
			{
				dragging = false;
				return true;
			}

			return false;
		}

		public virtual bool RightClick()
		{
			if (newMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton != ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
			{
				return true;
			}

			return false;
		}

		public virtual bool RightClickHold()
		{
			bool holding = false;

			if (newMouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Pressed && newMouse.Position.X >= 0 && newMouse.Position.X <= Globals.screenWidth && newMouse.Position.Y >= 0 && newMouse.Position.Y <= Globals.screenHeight)
			{
				holding = true;

				if (Math.Abs(newMouse.Position.X - firstMouse.Position.X) > 8 || Math.Abs(newMouse.Position.Y - firstMouse.Position.Y) > 8)
				{
					rightDrag = true;
				}
			}



			return holding;
		}

		public virtual bool RightClickRelease()
		{
			if (newMouse.RightButton == ButtonState.Released && oldMouse.RightButton == ButtonState.Pressed)
			{
				dragging = false;
				return true;
			}

			return false;
		}
	}
}
#endif