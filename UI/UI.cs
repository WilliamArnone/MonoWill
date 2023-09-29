using MonoWill.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill.UI
{
    public abstract class UI : Object2D
	{
		public bool isSelected { get; protected set; }
		public bool isFocused { get; set; }

		public event Action OnSelected;
		public event Action OnDeselected;
		public event Action OnMouseEnter;
		public event Action OnMouseExit;
		public event Action OnMouseClick;
		public event Action OnMouseDown;
		public event Action OnMouseUp;

		bool isMouseInside;
		bool mouseDown;

		public UI()
		{
			Input.OnMouseLeftClick += HandleClick;
			Input.OnMouseLeftDown += HandleMouseDown;
			Input.OnMouseLeftUp += HandleMouseUp;
		}

		public UI(string path) : base(path)
		{
			Input.OnMouseLeftClick += HandleClick;
			Input.OnMouseLeftDown += HandleMouseDown;
			Input.OnMouseLeftUp += HandleMouseUp;
		}

		public override void Dispose()
		{
			base.Dispose();
			Input.OnMouseLeftClick -= HandleClick;
			Input.OnMouseLeftDown -= HandleMouseDown;
			Input.OnMouseLeftUp -= HandleMouseUp;
			OnSelected = null;
			OnDeselected = null;
			OnMouseEnter = null;
			OnMouseExit = null;
			OnMouseClick = null;
			OnMouseDown = null;
			OnMouseUp = null;
		}

		public override void Update()
		{
			base.Update();
			bool wasMouseInside = isMouseInside;

			isMouseInside = CalculateMouseInside();
			if (isMouseInside && !wasMouseInside)
			{
				MouseEnter();
			}
			else if(!isMouseInside && wasMouseInside)
			{
				MouseExit();
			}
		}

		public bool CalculateMouseInside()
		{
			return GameMath.PointInRectangle(Input.GetMousePosition(), realPosition, size * lossyScale);
		}

		void HandleClick()
		{
			if (isMouseInside && enabled) { MouseClick(); }
		}

		void HandleMouseDown()
		{
			if (isMouseInside && enabled) 
			{
				mouseDown = true;
				MouseDown(); 
			}
		}

		void HandleMouseUp()
		{
			if ((isMouseInside || mouseDown) && enabled) 
			{
				mouseDown = false;
				MouseUp(); 
			}
		}

		public virtual void Select()
		{
			OnSelected?.Invoke();
			isSelected = true;
		}

		public virtual void Deselect()
		{
			OnDeselected?.Invoke();
			if(mouseDown) { HandleMouseUp(); }
			isSelected = false;
			isMouseInside = false;
		}

		protected virtual void MouseClick()
		{
			OnMouseClick?.Invoke();
		}

		protected virtual void MouseEnter()
		{
			OnMouseEnter?.Invoke();
		}

		protected virtual void MouseExit()
		{
			OnMouseExit?.Invoke();
		}

		protected virtual void MouseDown()
		{
			OnMouseDown?.Invoke();
		}

		protected virtual void MouseUp()
		{
			OnMouseUp?.Invoke();
		}
	}
}
