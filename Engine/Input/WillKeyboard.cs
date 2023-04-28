#if KEYBOARD
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public class WillKeyboard
	{
		KeyboardState newKeyboard;
		KeyboardState oldKeyboard;

		Keys[] pressedKeys;
		Keys[] prevPressedKeys;
		public event Action<Keys> OnKeyDown;
		public event Action<Keys> OnKeyPress;
		public event Action<Keys> OnKeyUp;

		public void Update()
		{
			newKeyboard = Keyboard.GetState();

			GetPressedKeys();
		}

		public void LateUpdate()
		{
			oldKeyboard = newKeyboard;
			prevPressedKeys = pressedKeys;
		}

		public bool IsKeyPressed(Keys key)
		{
			foreach (var pKey in pressedKeys)
			{
				if (pKey == key) return true;
			}
			return false;
		}

		void GetPressedKeys()
		{
			pressedKeys = newKeyboard.GetPressedKeys();
		}
	}
}
#endif