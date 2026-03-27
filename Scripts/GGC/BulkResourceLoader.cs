using Godot;
using System.Collections.Generic;

namespace GGC
{
    public class BulkResourceLoader
    {
        /// <summary>
        /// Loads every file in the specified path as the supplied type using the resource loader and returns a list of those resources
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<T> LoadFilesFromDir<T>(string path) where T : class
        {
            List<T> resources = [];

            string[] files = ResourceLoader.ListDirectory(path);
            resources.Capacity = files.Length;

            for (int i = 0; i < files.Length; i++)
            {
                resources.Add(ResourceLoader.Load<T>($"{path}/{files[i]}"));
            }

            return resources;
        }
    }
}
