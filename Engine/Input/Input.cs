using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public static class Input
	{
		const float MinDistanceToConsiderDrag = 8f;

		#region KEYBOARD

		static Keys[] _currentKeys;

		public static event Action<Keys> OnKeyDown;
		public static event Action<Keys> OnKeyPress;
		public static event Action<Keys> OnKeyUp;

		#endregion

		#region MOUSE

		static MouseState _newMouse;
		static MouseState _oldMouse;

		static bool dragging;
		static bool rightDrag;

		static Vector2 mouseNewPos;
		static Vector2 mouseOldPos;
		static Vector2 mouseFirstPos;

		static Vector2 newAdjustedPos;
		static Vector2 systemCursorPos;
		static Vector2 screenLoc;

		static int GetMouseWheelChange()
		{
			return _newMouse.ScrollWheelValue - _oldMouse.ScrollWheelValue;
		}

		public static Vector2 GetMouseScreenPos()
		{
			return mouseNewPos;
		}

		static bool IsLeftDown()
		{
			return _newMouse.LeftButton == ButtonState.Pressed
				&& _oldMouse.LeftButton != ButtonState.Pressed
				&& Screen.IsPointInside(mouseNewPos);
		}

		static bool IsLeftHold()
		{
			bool holding = false;

			if (_newMouse.LeftButton == ButtonState.Pressed 
				&& _oldMouse.LeftButton == ButtonState.Pressed 
				&& Screen.IsPointInside(mouseNewPos))
			{
				holding = true;

				Vector2 size = GameMath.SizeOfRectangle(mouseNewPos, mouseOldPos);
				if (size.X > MinDistanceToConsiderDrag || size.Y > MinDistanceToConsiderDrag)
				{
					dragging = true;
				}
			}



			return holding;
		}

		static bool IsLeftRelease()
		{
			if (_newMouse.LeftButton == ButtonState.Released && _oldMouse.LeftButton == ButtonState.Pressed)
			{
				dragging = false;
				return true;
			}

			return false;
		}

		static bool IsRightDown()
		{
			return _newMouse.RightButton == ButtonState.Pressed 
				&& _oldMouse.RightButton != ButtonState.Pressed 
				&& Screen.IsPointInside(mouseNewPos);
		}

		static bool IsRightClickHold()
		{
			bool holding = false;

			if (_newMouse.RightButton == ButtonState.Pressed 
				&& _oldMouse.RightButton == ButtonState.Pressed 
				&& Screen.IsPointInside(mouseNewPos))
			{
				holding = true;

				Vector2 size = GameMath.SizeOfRectangle(mouseNewPos, mouseOldPos);
				if (size.X > MinDistanceToConsiderDrag || size.Y > MinDistanceToConsiderDrag)
				{
					rightDrag = true;
				}
			}



			return holding;
		}

		static bool IsRightClickRelease()
		{
			if (_newMouse.RightButton == ButtonState.Released && _oldMouse.RightButton == ButtonState.Pressed)
			{
				dragging = false;
				return true;
			}

			return false;
		}

		#endregion

		internal static void Initialize()
		{
			#region MOUSE

			dragging = false;

			MouseState mouse = Mouse.GetState();

			Vector2 pos = new Vector2(mouse.Position.X, mouse.Position.Y);
			mouseNewPos = pos;
			mouseOldPos = pos;
			mouseFirstPos = pos;

			#endregion
		}

		internal static void Update()
		{
			#region KEYBOARD

			List<Keys> prevKeys = new List<Keys>(_currentKeys);
			_currentKeys = Keyboard.GetState().GetPressedKeys();

			foreach(var key in _currentKeys)
			{
				int index = -1;
				for (int i = prevKeys.Count; i >= 0; i--)
				{
					if(key == prevKeys[i])
					{
						index = i;
						prevKeys.RemoveAt(index);
						OnKeyPress(key);
						break;
					}
				}

				if(index == 0)
				{
					OnKeyDown(key);
				}
			}

			foreach(var key in prevKeys)
			{
				OnKeyUp(key);
			}

			#endregion

			#region MOUSE

			_oldMouse = _newMouse;
			_newMouse = _oldMouse = Mouse.GetState();
			mouseNewPos = new Vector2(_newMouse.Position.X, _newMouse.Position.Y);

			if (_newMouse.LeftButton != _newMouse.LeftButton)
			{
				if (_newMouse.LeftButton == ButtonState.Pressed)
				{
					mouseFirstPos = mouseNewPos;
				}
			}

			#endregion
		}
	}
}
