using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public abstract class Behaviour : Instantiator
	{
		public WorldObject worldObject { get; internal set; }

		public bool enabled;

		#region VIRTUALS

		public virtual void OnAttach()
		{

		}

		public virtual void Update()
		{

		}

		public virtual void OnDetach()
		{

		}

		#endregion

		#region COROUTINES

		public Coroutine StartCoroutine(IEnumerator routine)
		{
			return worldObject.StartCoroutine(routine);
		}

		public void StopCoroutine(Coroutine coroutine)
		{
			worldObject.StopCoroutine(coroutine);
		}

		public void StopAllCoroutines()
		{
			worldObject.StopAllCoroutines();
		}

		#endregion

		#region MONOWILL_BASE



		#endregion
	}
}
