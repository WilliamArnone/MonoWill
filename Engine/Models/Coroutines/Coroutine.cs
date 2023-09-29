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
		Stack<IEnumerator> routines;
		public bool isPlaying { get; private set; }

		YieldInstruction CurrentYield => routines.Peek().Current as YieldInstruction;

		internal Coroutine(IEnumerator routine)
		{
			routines = new Stack<IEnumerator>();
			Set(routine);
			isPlaying = true;
		}

		internal void Set(IEnumerator routine)
		{
			routines.Push(routine);
			ExplorePeek();
			isPlaying = true;
		}

		void ExplorePeek()
		{
			IEnumerator routine = routines.Peek();
			while(routine != null && routine.Current is IEnumerator)
			{
				routine = (IEnumerator) routine.Current;
				routines.Push(routine);
			}
		}

		internal bool Update()
		{
			if(routines.Count == 0)
			{
				isPlaying = false;
				return true; 
			}

			bool shouldContinue = true;

			if(CurrentYield != null)
			{
				CurrentYield.Update();
				shouldContinue = !CurrentYield.IsPaused;
			}

			if (shouldContinue)
			{
				while (routines.Count > 0 && !routines.Peek().MoveNext())
				{
					routines.Pop();
				}

				if(routines.Count == 0)
				{
					Dispose();
					return true;
				}

				ExplorePeek();
			}

			isPlaying = true;
			return false;
		}

		internal void Dispose()
		{
			isPlaying = false;
			routines.Clear();
		}
	}
}
