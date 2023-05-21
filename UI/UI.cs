using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill.UI
{
    public abstract class UI : Object2D
	{
		protected UI(string path, bool addToWorldAfterCreation = true) : base(path)
		{
		}
	}
}
