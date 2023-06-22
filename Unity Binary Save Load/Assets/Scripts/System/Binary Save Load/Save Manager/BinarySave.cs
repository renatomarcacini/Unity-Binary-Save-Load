using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Grindar.SaveLoad
{
    public class BinarySave
    {
        public static readonly string DEFAULT_GAME_SAVE = "gamesave";
        public static readonly string DEFAULT_MENU_SAVE = "menusave";

        private static readonly string SAVE_FOLDER = $"{Application.persistentDataPath}/";
        private const string SAVE_EXTENSION = ".dat";

        public static void Save<T>(string dataName, T data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            string path = $"{SAVE_FOLDER}{dataName}{SAVE_EXTENSION}";

            if (File.Exists(path))
            {
                FileStream file = File.Open(path, FileMode.Open);
                bf.Serialize(file, data);
                file.Close();
                Debug.Log("Saved");
            }
            else
            {
                FileStream file = File.Create(path);
                bf.Serialize(file, data);
                file.Close();
                Debug.Log("Saved");
            }
        }

        public static T Load<T>(string dataName) where T : new()
        {
            string path = $"{SAVE_FOLDER}{dataName}{SAVE_EXTENSION}";
            if (File.Exists(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(path, FileMode.Open);
                object dataObject = bf.Deserialize(file);
                file.Close();
                Debug.Log("Loaded");
                return (T)dataObject;
            }
            else
            {
                Debug.LogWarning($"There is no save with name: {dataName}, an empty save was created");
                T instance = new T();
                return instance;
            }
        }

        public static bool FileExist(string dataName)
        {
            string path = $"{SAVE_FOLDER}{dataName}{SAVE_EXTENSION}";
            return File.Exists(path);
        }

        public static void DeleteSave(string dataName)
        {
            string path = $"{SAVE_FOLDER}{dataName}{SAVE_EXTENSION}";
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("Save deleted");
            }
        }

        public static Vector SerializeVector(Vector3 vector)
        {
            Vector v = new Vector();
            v.x = vector.x;
            v.y = vector.y;
            v.z = vector.z;
            return v;
        }

        public static Rotation SerializeRotation(Quaternion quaternion)
        {
            Rotation r = new Rotation();
            r.x = quaternion.x;
            r.y = quaternion.y;
            r.z = quaternion.z;
            r.w = quaternion.w;
            return r;
        }
    }
}

[System.Serializable]
public struct Vector
{
    public float x;
    public float y;
    public float z;

    public override string ToString() => $"({x},{y},{z})";
}

[System.Serializable]
public struct Rotation
{
    public float x;
    public float y;
    public float z;
    public float w;

    public override string ToString() => $"({x},{y},{z},{w})";
}
