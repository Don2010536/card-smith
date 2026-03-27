using Godot;
using System.Collections.Generic;
using GGC.Interfaces;

namespace GGC
{
    public class EntityCache<T>(Node parent, PackedScene scene) where T : Node, ICachable, IDeactivate
    {
        private Node parent = parent;
        private PackedScene scene = scene;

        public List<T> Cache { get; } = new List<T>();

        public void InstantiateCache(int count)
        {
            GD.Print($"Caching \"{typeof(T)}\"");

            Vector2 spawn = new Vector2(-100, -100);

            for (int i = 0; i < count; i++)
            {
                Spawn(spawn).Deactivate();
            }

            GD.Print($"Cache \"{typeof(T)}\" ready");
        }

        public virtual T Spawn(Vector2 position, bool useAvailable = true)
        {
            if (useAvailable)
            {
                foreach (T spawned in Cache)
                {
                    if (spawned.Available)
                    {
                        return spawned;
                    }
                }
            }

            T spawn = scene.Instantiate<T>();
            
            Cache.Add(spawn);
            parent.CallDeferred(Node2D.MethodName.AddChild, spawn);
            
            return spawn;
        }
    }
}
