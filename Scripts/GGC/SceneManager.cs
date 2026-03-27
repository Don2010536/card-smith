using GGC.Interfaces;
using Godot;
using System;

namespace GGC
{
    public static class SceneManager<T> where T : Enum
    {
        private static Node lastScene;

        public static string ScenePath { get; set; }

        public static Node Parent { get; set; }
        public static IMap<T, string> SceneMap { get; set; }

        public static void LoadScene(T scene)
        {
            if (SceneMap.Map.TryGetValue(scene, out var map))
            {
                PackedScene loaded = ResourceLoader.Load<PackedScene>($"{ScenePath}/{map}.tscn");

                if (loaded != null)
                {
                    lastScene?.QueueFree();

                    lastScene = loaded.Instantiate();
                    Parent.AddChild(lastScene);
                }
            }
        }

        public static void ForgetLastScene()
        {
            lastScene = null;
        }
    }
}
