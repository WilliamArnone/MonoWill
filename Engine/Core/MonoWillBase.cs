using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
	public class MonoWillBase
	{
		public void Instantiate(WorldObject worldObject)
		{
			WillGame.world.Add(worldObject);
			worldObject.OnEnterWorld();
		}

		public void Instantiate(WorldObject worldObject, WorldObject parent)
		{
			parent.AddChildren(worldObject);
			worldObject.OnEnterWorld();
		}

		public void Destroy(WorldObject worldObject) 
		{
			WillGame.world.Remove(worldObject);
			worldObject.OnLeaveWorld();
		}

		public T FindWorldObject<T>() where T : WorldObject
		{
			return WillGame.world.FindWorldObject<T>();
		}

		public List<T> FindWorldObjects<T>() where T : WorldObject
		{
			return WillGame.world.FindWorldObjects<T>();
		}

		//public WorldObject FindObjectWithBehaviour<T>() where T : Behaviour
		//{
		//	return WillGame.world.FindObjectWithBehaviour<T>();
		//}

		//public List<WorldObject> FindObjectsWithBehaviour<T>() where T : Behaviour
		//{
		//	return WillGame.world.FindObjectsWithBehaviour<T>();
		//}
	}
}
