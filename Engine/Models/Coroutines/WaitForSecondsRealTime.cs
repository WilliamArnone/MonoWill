using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public class WaitForSecondsRealTime : YieldInstruction
	{
		float delayRemaining;

		public WaitForSecondsRealTime(float seconds) : base()
		{
			delayRemaining = seconds;
		}

		public override void Update()
		{
			delayRemaining -= Time.RealDelta;
			isPaused = delayRemaining > 0;
		}
	}
}
