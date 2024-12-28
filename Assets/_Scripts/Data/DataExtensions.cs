using UnityEngine;

namespace _Scripts.Data
{
    public static class DataExtensions
    {
        public static Vector3Data ToVector3Data(this Vector3 vector)
        {
            return new Vector3Data(vector.x, vector.y, vector.z);
        }       
        
        public static Vector3 ToUnityVector3(this Vector3Data vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }
        
        public static T ToDeserialized<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static string ToJson(this object obj)
        {
            return JsonUtility.ToJson(obj);
        }
    }
}