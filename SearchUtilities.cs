using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OWRichPresence
{
    internal static class SearchUtilities
    {
        public static T FindResourceOfTypeAndName<T>(string name) where T : Object
        {
            T[] firstList = Resources.FindObjectsOfTypeAll<T>();

            for (var i = 0; i < firstList.Length; i++)
            {
                if (firstList[i].name == name)
                {
                    return firstList[i];
                }
            }

            return null;
        }

        public static GameObject FindChild(this GameObject g, string childPath) =>
            g.transform.Find(childPath)?.gameObject;

        /// <summary>
        /// finds active or inactive object by path,
        /// or recursively finds an active or inactive object by name
        /// </summary>
        public static GameObject Find(string path, bool warn = true)
        {
            GameObject go = GameObject.Find(path);

            if (go == null)
            {
                // find inactive use root + transform.find
                var names = path.Split('/');
                var rootName = names[0];
                var root = SceneManager.GetActiveScene().GetRootGameObjects().FirstOrDefault(x => x.name == rootName);
                if (root == null)
                {
                    if (warn) OWRichPresence.WriteLine($"Couldn't find root object in path {path}", OWML.Common.MessageType.Warning);
                    return null;
                }

                var childPath = string.Join("/", names.Skip(1));
                go = root.FindChild(childPath);
                if (go == null)
                {
                    var name = names.Last();
                    if (warn) OWRichPresence.WriteLine($"Couldn't find object in path {path}, will look for potential matches for name {name}", OWML.Common.MessageType.Warning);
                    // find resource to include inactive objects
                    // also includes prefabs but hopefully thats okay
                    go = FindResourceOfTypeAndName<GameObject>(name);
                    if (go == null)
                    {
                        if (warn) OWRichPresence.WriteLine($"Couldn't find object with name {name}", OWML.Common.MessageType.Warning);
                        return null;
                    }
                }
            }

            return go;
        }
    }
}