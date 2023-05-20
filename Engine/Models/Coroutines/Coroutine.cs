using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public class Coroutine
	{
		IEnumerator routine;

		YieldInstruction CurrentYield => routine.Current as YieldInstruction;

		internal Coroutine(IEnumerator routine)
		{
			Set(routine);
		}

		internal void Set(IEnumerator routine)
		{
			this.routine = routine;
		}

		internal bool Update()
		{
			if(routine == null) { return true; }

			bool shouldContinue = true;

			if(CurrentYield != null)
			{
				CurrentYield.Update();
				shouldContinue = !CurrentYield.IsPaused;
			}

			if (shouldContinue && !routine.MoveNext())
			{
				Dispose();
				return true;
			}

			return false;
		}

		internal void Dispose()
		{
			routine = null;
		}
	}
}
