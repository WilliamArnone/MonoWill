using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public interface IMouseInteractable
	{
		public bool StopRaycast { get; set; }

		public void OnClick();
		public void OnRightClick();
		public void OnStartDrag();
		public void OnDrag();
		public void OnDrop();
		public void OnRightStartDrag();
		public void OnRightDrag();
		public void OnRightDrop();
		public void OnMouseDown();
		public void OnMouseUp();
		public void OnRightMouseUp();
		public void OnLeftMouseUp();
	}
}
