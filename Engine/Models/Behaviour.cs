using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public abstract class Behaviour
	{
		public WorldObject worldObject { get; internal set; }

		public bool enabled;

		public virtual void Update()
		{

		}
	}
}
