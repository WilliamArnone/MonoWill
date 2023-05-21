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
			WillGame.world.Add(worldObject);
		}

		public void AddToCanvas(WorldObject worldObject)
		{
			WillGame.world.AddUI(worldObject);
		}

		public void RemoveFromWorld(WorldObject worldObject)
		{
			WillGame.world.Remove(worldObject);
		}
		public void RemoveFromCanvas(WorldObject worldObject)
		{
			WillGame.world.Remove(worldObject);
		}

		public T FindWorldObject<T>() where T : WorldObject
		{
			return WillGame.world.FindWorldObject<T>();
		}

		public List<T> FindWorldObjects<T>() where T : WorldObject
		{
			return WillGame.world.FindWorldObjects<T>();
		}

		public T FindBehaviour<T>() where T : Behaviour
		{
			return WillGame.world.FindBehaviour<T>();
		}

		public List<T> FindAllBehaviours<T>() where T : Behaviour
		{
			return WillGame.world.FindAllBehaviours<T>();
		}
	}
}
