using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
    public abstract class WorldObject
	{
		protected internal int worldIndex;
		public Vector2 position;

		public bool enabled;
		public bool visible;

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

			worldIndex = World.Add(this);
		}

		public virtual void RemoveFromWorld()
		{
			if (!IsInGame) return;

			worldIndex = -1;
			World.Remove(this, worldIndex);
		}

		internal void UpdateIndex(int newIndex)
		{
			worldIndex = newIndex;
		}

		#endregion

		#endregion

		public abstract void Update();

		public abstract void Draw();
	}
}
