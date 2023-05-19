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

		static void InitKeyboard()
		{
			//Nedded to avoid NullExcpetion on first UpdateKeyboard
			_currentKeys = new Keys[0];
		}

		static void UpdateKeyboard()
		{
			List<Keys> prevKeys = new List<Keys>(_currentKeys);
			_currentKeys = Keyboard.GetState().GetPressedKeys();

			foreach (var key in _currentKeys)
			{
				int index = -1;
				for (int i = prevKeys.Count; i >= 0; i--)
				{
					if (key == prevKeys[i])
					{
						index = i;
						prevKeys.RemoveAt(index);
						OnKeyPress(key);
						break;
					}
				}

				if (index == 0)
				{
					OnKeyDown(key);
				}
			}

			foreach (var key in prevKeys)
			{
				OnKeyUp(key);
			}
		}

		#endregion

		#region MOUSE

		const float clickTimeBetweenInteraction = .10f;

		static MouseState _newMouse;
		static MouseState _oldMouse;

		static bool leftDrag;
		static bool rightDrag;

		internal static Vector2 mouseNewPos;
		internal static Vector2 mouseOldPos;
		internal static Vector2 mouseFirstPos;

		static float mouseLeftStamp;
		static float mouseRightStamp;

		public static event Action OnMouseLeftClick;
		public static event Action OnMouseRighClick;
		public static event Action OnMouseLeftHold;
		public static event Action OnMouseRightHold;
		public static event Action OnMouseLeftStartDrag;
		public static event Action OnMouseRightStartDrag;
		public static event Action OnMouseLeftDrag;
		public static event Action OnMouseRightDrag;
		public static event Action OnMouseLeftDrop;
		public static event Action OnMouseRightDrop;
		public static event Action OnMouseLeftDown;
		public static event Action OnMouseRightDown;
		public static event Action OnMouseLeftUp;
		public static event Action OnMouseRightUp;

		static int GetMouseWheelChange()
		{
			return _newMouse.ScrollWheelValue - _oldMouse.ScrollWheelValue;
		}

		public static Vector2 GetMousePositino()
		{
			return mouseNewPos;
		}

		internal static void ResetLeftDrag()
		{
			if (leftDrag)
			{
				leftDrag = false;
			}
		}

		static void InitMouse()
		{
			leftDrag = false;

			MouseState mouse = Mouse.GetState();

			Vector2 pos = new Vector2(mouse.Position.X, mouse.Position.Y);
			mouseNewPos = pos;
			mouseOldPos = pos;
			mouseFirstPos = pos;
		}

		static void UpdateMouseLeft()
		{
			if (_newMouse.LeftButton == _oldMouse.LeftButton)
			{
				if (_newMouse.LeftButton == ButtonState.Released)
				{
					return;
				}
				else
				{
					if (GameMath.PointInRectangle(mouseNewPos, mouseOldPos,
						new Vector2(MinDistanceToConsiderDrag, MinDistanceToConsiderDrag)))
					{
						OnMouseLeftHold?.Invoke();
					}
					else if (!leftDrag)
					{
						leftDrag = true;
						OnMouseLeftStartDrag?.Invoke();
					}
					else
					{
						OnMouseLeftDrag?.Invoke();
					}
				}
			}
			else if (_newMouse.LeftButton == ButtonState.Pressed)
			{
				mouseLeftStamp = Time.RealTime;
				OnMouseLeftDown?.Invoke();
			}
			else
			{
				OnMouseLeftUp?.Invoke();

				if (Time.RealTime-mouseLeftStamp <= clickTimeBetweenInteraction)
				{
					OnMouseLeftClick?.Invoke();
				}

				if(leftDrag)
				{
					leftDrag = false;
					OnMouseLeftDrop?.Invoke();
				}

			}
		}

		static void UpdateMouse()
		{
			_oldMouse = _newMouse;
			_newMouse = Mouse.GetState();
			mouseNewPos = new Vector2(_newMouse.Position.X, _newMouse.Position.Y);

			UpdateMouseLeft();
		}

		#endregion

		internal static void Initialize()
		{
			InitKeyboard();

			InitMouse();
		}

		internal static void Update()
		{
			UpdateKeyboard();

			UpdateMouse();
		}

		internal static void Draw()
		{

		}
	}
}
