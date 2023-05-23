using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
    public abstract class World : Instantiator
	{
        List<WorldObject> worldObjects;
        List<WorldObject> canvasObjects;

        public World()
        {
            worldObjects = new List<WorldObject>();
            canvasObjects = new List<WorldObject>();
        }

		public abstract void OnCreation();

        /// <summary>
        /// Called each frame. Contains the game logic.
        /// </summary>
        public virtual void Update()
		{
			foreach (WorldObject obj in worldObjects)
			{
				if (obj.enabled)
				{
					obj.ResumeCoroutines();
				}
			}

			foreach (WorldObject obj in canvasObjects)
			{
				if (obj.enabled)
				{
					obj.ResumeCoroutines();
				}
			}

			foreach (WorldObject obj in worldObjects)
			{
				if (obj.enabled)
				{
					obj.Update();
				}
			}

			foreach (WorldObject obj in canvasObjects)
			{
				if (obj.enabled)
				{
					obj.Update();
				}
			}
		}

        internal void Draw()
        {
            foreach (WorldObject obj in worldObjects)
            {
                if (obj.visible)
                {
                    obj.Draw();
                }
            }

            foreach (WorldObject obj in canvasObjects)
            {
                if (obj.visible)
                {
                    obj.Draw();
                }
            }
        }

		#region WORLDOBJECTS_HANDLING

		internal void Add(WorldObject obj)
        {
			if(obj.IsInGame) { return; }
			if(obj.parent!=null) { obj.parent.RemoveChildren(obj); }

            obj.worldIndex = worldObjects.Count;
			obj.IsInWorld = true;
			worldObjects.Add(obj);
			obj.OnEnterWorld();
		}

        internal void Remove(WorldObject obj)
        {
			if(!obj.IsInWorld) { return; }

            int indexAssigned = Math.Min(obj.worldIndex, worldObjects.Count - 1);
            WorldObject temp;

            for (int i = indexAssigned; i >= 0; i--)
            {
                temp = worldObjects[i];
                if (temp == obj)
                {
                    worldObjects.RemoveAt(i);
                    break;
                }
                else
                {
                    temp.worldIndex = i;
                }
            }

            obj.worldIndex = -1;
			obj.OnLeaveWorld();
		}

        internal void AddUI(WorldObject obj)
		{
			if (obj.IsInGame) { return; }
			if (obj.parent != null) { obj.parent.RemoveChildren(obj); }

			obj.worldIndex = canvasObjects.Count;
			obj.IsInWorld = false;
			canvasObjects.Add(obj);
			obj.OnEnterWorld();
		}

        internal void RemoveUI(WorldObject obj, int indexAssigned)
        {
			if(!obj.IsInCanvas) { return; }

            indexAssigned = Math.Min(indexAssigned, canvasObjects.Count - 1);
            WorldObject temp;

            for (int i = indexAssigned; i >= 0; i--)
            {
                temp = canvasObjects[i];
                if (temp == obj)
                {
                    canvasObjects.RemoveAt(i);
                    break;
                }
                else
                {
                    temp.worldIndex = i;
                }
            }

            obj.worldIndex = -1;
        }

		#endregion

		#region WORLDOBJECT_SEARCH

		internal new T FindWorldObject<T>() where T : WorldObject
		{
			T returnObj;

			foreach (WorldObject obj in worldObjects)
			{
				returnObj = SearchForWorldObject<T>(obj);
				if(returnObj != null)
					return returnObj;
			}
			foreach (WorldObject obj in canvasObjects)
			{
				returnObj = SearchForWorldObject<T>(obj);
				if (returnObj != null)
					return returnObj;
			}

			return null;
		}

		T SearchForWorldObject<T>(WorldObject worldObject) where T : WorldObject
		{
			T returnObj = worldObject as T;
			if (returnObj != null)
				return returnObj;

			foreach (var child in worldObject.Children)
			{
				returnObj = SearchForWorldObject<T>(child);
				if (returnObj != null)
					return returnObj;
			}

			return null;
		}

		internal new List<T> FindWorldObjects<T>() where T : WorldObject
		{
            List<T> objs = new List<T>();

			foreach (WorldObject obj in worldObjects)
				objs.AddRange(SearchForWorldObjects<T>(obj));
			foreach (WorldObject obj in canvasObjects)
				objs.AddRange(SearchForWorldObjects<T>(obj));

			return objs;
		}

		List<T> SearchForWorldObjects<T>(WorldObject worldObject) where T : WorldObject
		{
            List<T> objs = new List<T>();

			T returnObj = worldObject as T;
			if (returnObj != null)
				objs.Add(returnObj);

			foreach (var child in worldObject.Children)
			{
				objs.AddRange(SearchForWorldObjects<T>(child));
			}

			return objs;
		}

		public new T FindBehaviour<T>() where T : Behaviour
		{
			T returnObj;

			foreach (WorldObject obj in worldObjects)
			{
				returnObj = SearchForBehaviour<T>(obj);
				if (returnObj != null)
					return returnObj;
			}
			foreach (WorldObject obj in canvasObjects)
			{
				returnObj = SearchForBehaviour<T>(obj);
				if (returnObj != null)
					return returnObj;
			}

			return null;
		}

		T SearchForBehaviour<T>(WorldObject worldObject) where T : Behaviour
		{
			if (worldObject.TryGetBehaviour(out T behav))
				return behav;

			foreach (var child in worldObject.Children)
			{
				T returnObj = SearchForBehaviour<T>(child);
				if (returnObj != null)
					return returnObj;
			}

			return null;
		}

		public new List<T> FindAllBehaviours<T>() where T : Behaviour
		{
			List<T> objs = new List<T>();

			foreach (WorldObject obj in worldObjects)
				objs.AddRange(SearchForBehavious<T>(obj));
			foreach (WorldObject obj in canvasObjects)
				objs.AddRange(SearchForBehavious<T>(obj));

			return objs;
		}

		List<T> SearchForBehavious<T>(WorldObject worldObject) where T : Behaviour
		{
			List<T> objs = new List<T>();

			if (worldObject.TryGetBehaviour(out T returnObj))
				objs.Add(returnObj);

			foreach (var child in worldObject.Children)
			{
				objs.AddRange(SearchForBehavious<T>(child));
			}

			return objs;
		}

		#endregion
	}
}
