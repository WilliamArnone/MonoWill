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
    public abstract class WorldObject : Instantiator, IDisposable
	{
		protected internal int worldIndex = -1;

		public bool IsInWorld { get; internal set; }
		public bool IsInCanvas => IsInGame && !IsInWorld;
		public bool IsInGame => worldIndex >= 0;

		#region ATTRIBUTES

		public WorldObject Parent{ get; internal set; }

		public Vector2 position;
		public Vector2 scale;

		public bool enabled = true;
		public bool visible = true;

		public Vector2 realPosition
		{
			get
			{
				Vector2 _position = position;
				WorldObject parent = Parent;
				while (parent != null)
				{
					_position += parent.position;
					parent = parent.Parent;
				}
				return _position;
			}
		}

		public Vector2 lossyScale
		{
			get
			{
				Vector2 _scale = scale;
				WorldObject parent = Parent;
				while (parent != null)
				{
					_scale *= parent.scale;
					parent = parent.Parent;
				}
				return _scale;
			}
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

		public void AddChild(WorldObject obj)
		{
			if(obj.Parent != null)
			{
				obj.Parent.RemoveChildren(obj);
			}
			else if(obj.worldIndex != -1)
			{
				Game.World.Remove(obj);
			}
			children.Add(obj);
			obj.Parent = this;
		}

		public void RemoveChildren(WorldObject obj) 
		{
			if(obj.Parent != this) { return; }

			children.Remove(obj);
			obj.Parent = null;
		}

		#endregion

		#region COROUTINES

		List<Coroutine> activeCoroutines = new List<Coroutine>();

		public Coroutine StartCoroutine(IEnumerator routine)
		{
			Coroutine coroutine = new Coroutine(routine);
			activeCoroutines.Add(coroutine);
			return coroutine;
		}

		public void StopCoroutine(Coroutine coroutine)
		{
			coroutine.Dispose();
			activeCoroutines.Remove(coroutine);
		}

		public void StopAllCoroutines()
		{
			foreach (var coroutine in activeCoroutines)
				coroutine.Dispose();

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

		public virtual void Draw(Vector2 parentPosition, Vector2 parentScale)
		{
			parentPosition += position;
			parentScale *= scale;

			foreach (var child in children)
				if(child.visible)
					child.Draw(parentPosition, parentScale);
		}

		public virtual void Dispose()
		{
			foreach (var child in children)
			{
				child.Dispose();
			}
			children.Clear();
		}

		#endregion
	}
}
