using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoWill.Mathematics;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public static class Input
	{
		const float MinDistanceToConsiderDrag = 8f;

		#region KEYBOARD

		static bool[] _keyPressed;
		static List<Keys> _currentKeys;

		public static event Action<Keys> OnKeyDown;
		public static event Action<Keys> OnKeyPress;
		public static event Action<Keys> OnKeyUp;

		public static bool IsKeyDown(Keys key) => _keyPressed[(int)key];

		static void InitKeyboard()
		{
			_currentKeys = new List<Keys>();
			_keyPressed = new bool[Enum.GetValues(typeof(Keys)).Length];
		}

		static void UpdateKeyboard()
		{
			_currentKeys.AddRange(Keyboard.GetState().GetPressedKeys());

			for (int i = 0; i < _keyPressed.Length; i++)
			{
				bool isPressed = false;
				for (int j = _currentKeys.Count-1; j >= 0; j--)
				{
					if ((int)_currentKeys[j] == i)
					{
						_currentKeys.RemoveAt(j);
						isPressed = true;
						break;
					}
				}

				if (_keyPressed[i] && !isPressed)
				{
					OnKeyUp?.Invoke((Keys)i);
				}
				else if (!_keyPressed[i] && isPressed)
				{
					OnKeyDown?.Invoke((Keys)i);
				}
				else if (_keyPressed[i] && isPressed)
				{
					OnKeyPress?.Invoke((Keys)i);
				}

				_keyPressed[i] = isPressed;
			}
			_currentKeys.Clear();
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
		public static event Action OnMouseRightClick;
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

		public static Vector2 GetMousePosition()
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
			pos = Vector2.Transform(pos, Matrix.Invert(Graphic.transformationMatrix));

			mouseNewPos = pos;
			mouseOldPos = pos;
			mouseFirstPos = pos;
		}

		static void UpdateMouseLeft()
		{
			if (_newMouse.LeftButton == ButtonState.Released
				&& _oldMouse.LeftButton == ButtonState.Released)
			{
				return;
			}
			else if (_newMouse.LeftButton == ButtonState.Pressed
				&& _oldMouse.LeftButton == ButtonState.Pressed)
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
			else if (_newMouse.LeftButton == ButtonState.Pressed
				&& _oldMouse.LeftButton == ButtonState.Released)
			{
				mouseLeftStamp = Time.RealTime;
				OnMouseLeftDown?.Invoke();
			}
			else if (_newMouse.LeftButton == ButtonState.Released
				&& _oldMouse.LeftButton == ButtonState.Pressed)
			{
				OnMouseLeftUp?.Invoke();

				if (Time.RealTime - mouseLeftStamp <= clickTimeBetweenInteraction)
				{
					OnMouseLeftClick?.Invoke();
				}

				if (leftDrag)
				{
					leftDrag = false;
					OnMouseLeftDrop?.Invoke();
				}
			}
		}

		static void UpdateMouseRight()
		{
			if (_newMouse.RightButton == ButtonState.Released
				&& _oldMouse.RightButton == ButtonState.Released)
			{
				return;
			}
			else if (_newMouse.RightButton == ButtonState.Pressed
				&& _oldMouse.RightButton == ButtonState.Pressed)
			{
				if (GameMath.PointInRectangle(mouseNewPos, mouseOldPos,
					new Vector2(MinDistanceToConsiderDrag, MinDistanceToConsiderDrag)))
				{
					OnMouseRightHold?.Invoke();
				}
				else if (!rightDrag)
				{
					rightDrag = true;
					OnMouseRightStartDrag?.Invoke();
				}
				else
				{
					OnMouseRightDrag?.Invoke();
				}
			}
			else if (_newMouse.RightButton == ButtonState.Pressed
				&& _oldMouse.RightButton == ButtonState.Released)
			{
				mouseRightStamp = Time.RealTime;
				OnMouseRightDown?.Invoke();
			}
			else if (_newMouse.RightButton == ButtonState.Released
				&& _oldMouse.RightButton == ButtonState.Pressed)
			{
				OnMouseRightUp?.Invoke();

				if (Time.RealTime - mouseRightStamp <= clickTimeBetweenInteraction)
				{
					OnMouseRightClick?.Invoke();
				}

				if (leftDrag)
				{
					rightDrag = false;
					OnMouseRightDrop?.Invoke();
				}
			}
		}

		static void UpdateMouse()
		{
			_oldMouse = _newMouse;
			_newMouse = Mouse.GetState();
			mouseOldPos = mouseNewPos;
			mouseNewPos = new Vector2(_newMouse.Position.X, _newMouse.Position.Y);

			UpdateMouseLeft();
			UpdateMouseRight();
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
