using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
    public abstract class WorldObject : MonoWillBase
	{
		protected internal int worldIndex = -1;

		public bool IsInGame => worldIndex >= 0;

		#region ATTRIBUTES

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

		public Vector2 position;

		public bool enabled = true;
		public bool visible = true;

		#endregion

		/// <summary>
		/// World Object constructor
		/// </summary>
		/// <param name="addToWorldAfterCreation">If set to false it won't be added to the game objects.</param>
		public WorldObject(bool addToWorldAfterCreation = true)
		{
			if (addToWorldAfterCreation)
				AddSelfToWorld();
		}

		#region WORLD

		public virtual void AddSelfToWorld()
		{
			if (IsInGame) return;

			WillGame.world.Add(this);
		}

		public virtual void RemoveFromWorld()
		{
			if (!IsInGame) return;

			WillGame.world.Remove(this);
		}

		#endregion

		#region BEHAVIOUR
		
		List<Behaviour> behaviours = new List<Behaviour>();

		public void AddBehaviour(Behaviour behaviour)
		{
			if(behaviour.worldObject != null)
			{
				behaviour.worldObject.RemoveBehaviour(behaviour);
			}

			behaviour.worldObject = this;
			behaviours.Add(behaviour);
			behaviour.OnAttach();
		}

		public void RemoveBehaviour(Behaviour behaviour)
		{
			if(behaviour.worldObject != this) 
			{
				throw new InvalidOperationException("Trying to remove a behaviour from another object");
			}

			behaviour.worldObject = null;
			behaviours.Remove(behaviour);
			behaviour.OnDetach();
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

		#endregion

		#region CHILDREN

		List<WorldObject> children = new List<WorldObject>();
		public List<WorldObject> Children => new List<WorldObject>(children);

		public void AddChildren(WorldObject obj)
		{
			if(obj.parent != null)
			{
				obj.parent.RemoveChildren(obj);
			}
			else if(obj.worldIndex != -1)
			{
				WillGame.world.Remove(obj);
			}
			children.Add(obj);
		}

		public void RemoveChildren(WorldObject obj) 
		{
			if(obj.parent != this) { return; }

			children.Remove(obj);
		}

		#endregion

		#region COROUTINES

		List<Coroutine> activeCoroutines = new List<Coroutine>();
		List<Coroutine> sleepCoroutines = new List<Coroutine>();

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

		#region VIRTUALS

		public virtual void OnEnterWorld() { }
		public virtual void OnLeaveWorld() { }

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

		#endregion
	}
}
