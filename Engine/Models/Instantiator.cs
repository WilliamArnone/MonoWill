using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public class Instantiator
	{
		public void AddToWorld(WorldObject worldObject)
		{
			WillGame.World.Add(worldObject);
		}

		public void AddToCanvas(WorldObject worldObject)
		{
			WillGame.World.AddUI(worldObject);
		}

		public void RemoveFromWorld(WorldObject worldObject)
		{
			WillGame.World.Remove(worldObject);
		}
		public void RemoveFromCanvas(WorldObject worldObject)
		{
			WillGame.World.Remove(worldObject);
		}

		public T FindWorldObject<T>() where T : WorldObject
		{
			return WillGame.World.FindWorldObject<T>();
		}

		public List<T> FindWorldObjects<T>() where T : WorldObject
		{
			return WillGame.World.FindWorldObjects<T>();
		}

		public T FindBehaviour<T>() where T : Behaviour
		{
			return WillGame.World.FindBehaviour<T>();
		}

		public List<T> FindAllBehaviours<T>() where T : Behaviour
		{
			return WillGame.World.FindAllBehaviours<T>();
		}
	}
}
