using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public static class Input
	{

		#region KEYBOARD
#if KEYBOARD

		public static WillKeyboard keyboard;

#endif
		#endregion

		#region MOUSE
#if MOUSE

		public static WillMouse mouse;

#endif
		#endregion

		#region TOUCH
#if TOUCH

		

#endif
		#endregion

		#region CONTROLLER
#if CONTROLLER

		

#endif
		#endregion

	}
}
