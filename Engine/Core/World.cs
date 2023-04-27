using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoWill
{
    public abstract class World
    {
        static List<WorldObject> worldObjects;
        static List<WorldObject> canvasObjects;

        internal void Initialize()
        {
            worldObjects = new List<WorldObject>();
            canvasObjects = new List<WorldObject>();
        }

        /// <summary>
        /// Called each frame. Contains the game logic.
        /// </summary>
        public virtual void Update()
        {
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

        internal static int Add(WorldObject obj)
        {
            worldObjects.Add(obj);
            return worldObjects.Count - 1;
        }

        internal static void Remove(WorldObject obj, int indexAssigned)
        {
            indexAssigned = Math.Min(indexAssigned, worldObjects.Count - 1);
            WorldObject temp;

            for (int i = indexAssigned; i >= 0; i--)
            {
                temp = worldObjects[i];
                if (temp == obj)
                {
                    worldObjects.RemoveAt(i);
                    return;
                }
                else
                {
                    temp.UpdateIndex(i);
                }
            }
        }

        internal static int AddUI(WorldObject obj)
        {
            canvasObjects.Add(obj);
            return canvasObjects.Count - 1;
        }

        internal static void RemoveUI(WorldObject obj, int indexAssigned)
        {
            indexAssigned = Math.Min(indexAssigned, canvasObjects.Count - 1);
            WorldObject temp;

            for (int i = indexAssigned; i >= 0; i--)
            {
                temp = canvasObjects[i];
                if (temp == obj)
                {
                    canvasObjects.RemoveAt(i);
                    return;
                }
                else
                {
                    temp.UpdateIndex(i);
                }
            }
        }
    }
}
