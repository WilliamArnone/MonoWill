using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public abstract class YieldInstruction
	{
		protected bool isPaused;

		internal bool IsPaused => isPaused;

		public YieldInstruction()
		{
			isPaused = true;
		}

		public abstract void Update();
	}
}
