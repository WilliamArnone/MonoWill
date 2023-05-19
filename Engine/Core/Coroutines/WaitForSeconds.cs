using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public class WaitForSeconds : YieldInstruction
	{
		float delayRemaining;

		public WaitForSeconds(float seconds) : base()
		{
			delayRemaining = seconds;
		}

		public override void Update()
		{
			delayRemaining -= Time.TimeDelta;
			isPaused = delayRemaining > 0;
		}
	}
}
