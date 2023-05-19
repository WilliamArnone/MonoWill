using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
    public abstract class WorldObject
	{
		protected internal int worldIndex = -1;

		WorldObject _parent;
		public WorldObject parent
		{
			get
			{
				return _parent;
			}
			set
			{
				if (_parent != null) { parent.children.Remove(this); }
				_parent.children.Add(this);
				_parent = value;
			}
		}

		List<WorldObject> children = new List<WorldObject>();
		
		List<Behaviour> behaviours = new List<Behaviour>();

		List<Coroutine> activeCoroutines = new List<Coroutine>();
		List<Coroutine> sleepCoroutines = new List<Coroutine>();

		#region ATTRIBUTES

		public Vector2 position;

		public bool enabled = true;
		public bool visible = true;

		#endregion

		public bool IsInGame => worldIndex >= 0;

		/// <summary>
		/// World Object constructor
		/// </summary>
		/// <param name="addToWorldAfterCreation">If set to false it won't be added to the game objects.</param>
		public WorldObject(bool addToWorldAfterCreation = true)
		{
			if (addToWorldAfterCreation)
				AddSelfToWorld();
		}

		#region INTERNAL_USE

		#region WORLD

		public virtual void AddSelfToWorld()
		{
			if (IsInGame) return;

			World.Add(this);
		}

		public virtual void RemoveFromWorld()
		{
			if (!IsInGame) return;

			World.Remove(this);
		}

		#endregion

		#endregion

		#region TYPES_AND_BEHAVIOURS

		public bool GetType<T>(out T obj) where T : WorldObject
		{
			obj = this as T;
			return obj != null;
		}

		public void AddBehaviour(Behaviour behaviour)
		{
			if(behaviour.worldObject != null)
			{
				throw new InvalidOperationException("Trying to add a behaviour aready attached to an object");
			}

			behaviour.worldObject = this;
			behaviours.Add(behaviour);
		}

		public void RemoveBehaviour(Behaviour behaviour)
		{
			if(behaviour.worldObject != this) 
			{
				throw new InvalidOperationException("Trying to remove a behaviour from another object");
			}

			behaviour.worldObject = null;
			behaviours.Remove(behaviour);
		}

		public T GetBehaviour<T>() where T : Behaviour
		{
			foreach (var _behaviour in behaviours)
			{
				T obj = _behaviour as T;
				if (obj != null)
				{
					return obj;
				}
			}
			return null;
		}

		public List<T> GetBehaviours<T>() where T : Behaviour
		{
			List<T> list = new List<T>();
			foreach (var behaviour in behaviours)
			{
				T obj = behaviour as T;
				if (obj != null)
				{
					list.Add(obj);
				}
			}
			return list;
		}

		public bool TryGetBehaviour<T>(out T behaviour) where T : Behaviour
		{
			behaviour = null;
			foreach (var _behaviour in behaviours)
			{
				behaviour = _behaviour as T;
				if (behaviour != null)
				{
					return true;
				}
			}
			return false;
		}

		public Behaviour[] GetBehaviours()
		{
			return behaviours.ToArray();
		}

		public void AddChildren(WorldObject obj)
		{
			if(obj.parent != null)
			{

			}
			else if(obj.worldIndex != -1)
			{
				World.Remove(obj);
			}
		}

		#endregion

		#region COROUTINES

		public Coroutine StartCoroutine(IEnumerator routine)
		{
			Coroutine coroutine;
			if (sleepCoroutines.Count > 0)
			{
				coroutine = sleepCoroutines[0];
				sleepCoroutines.RemoveAt(0);
				coroutine.Set(routine);
			}
			else
			{
				coroutine = new Coroutine(routine);
			}
			activeCoroutines.Add(coroutine);
			return coroutine;
		}

		public void StopCoroutine(Coroutine coroutine)
		{
			coroutine.Dispose();
			activeCoroutines.Remove(coroutine);
			sleepCoroutines.Add(coroutine);
		}

		public void StopAllCoroutines()
		{
			foreach (var coroutine in activeCoroutines)
				coroutine.Dispose();

			sleepCoroutines.AddRange(activeCoroutines);
			activeCoroutines.Clear();
		}

		internal void ResumeCoroutines()
		{
			Coroutine activeRoutine;
			for (var i = activeCoroutines.Count-1; i>=0; i--)
			{
				activeRoutine = activeCoroutines[i];
				if (activeRoutine.Update())
				{
					activeCoroutines.RemoveAt(i);
					sleepCoroutines.Add(activeRoutine);
				}
			}

			foreach (var child in children)
				if (child.enabled)
					child.ResumeCoroutines();
		}

		#endregion

		public virtual void Update()
		{
			foreach (var behaviour in behaviours)
				if(behaviour.enabled)
					behaviour.Update();

			foreach (var child in children)
				if(child.enabled)
					child.Update();
		}

		public virtual void Draw()
		{
			foreach (var child in children)
				if(child.visible)
					child.Draw();
		}
	}
}
