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
			Game.World.Add(worldObject);
		}

		public void AddToCanvas(WorldObject worldObject)
		{
			Game.World.AddUI(worldObject);
		}

		public void RemoveFromWorld(WorldObject worldObject)
		{
			Game.World.Remove(worldObject);
		}
		public void RemoveFromCanvas(WorldObject worldObject)
		{
			Game.World.Remove(worldObject);
		}

		public T FindWorldObject<T>() where T : WorldObject
		{
			return Game.World.FindWorldObject<T>();
		}

		public List<T> FindWorldObjects<T>() where T : WorldObject
		{
			return Game.World.FindWorldObjects<T>();
		}

		public T FindBehaviour<T>() where T : Behaviour
		{
			return Game.World.FindBehaviour<T>();
		}

		public List<T> FindAllBehaviours<T>() where T : Behaviour
		{
			return Game.World.FindAllBehaviours<T>();
		}
	}
}
