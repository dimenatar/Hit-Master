using UnityEngine;
using System.IO;


public static class JsonSaver
{
    public static void Save<T>(T item, string fileName)
    {
         var json = JsonUtility.ToJson(item,true);
       // var json = JsonFormatter.SerializeObject(item);
      // var json = Json.SerializeToString<T>(item);

        Debug.Log($"@@@ Saving to {GetSavePath(fileName)}:\n{json}");
        File.WriteAllText(GetSavePath(fileName), json);
    }
    
    public static T Load<T>(string fileName)
    {
        var path = GetSavePath(fileName);
        if (File.Exists(path))
        {

            var json = File.ReadAllText(GetSavePath(fileName));
            Debug.Log($"!!! Loaded from {path}:\n{json}");
            return JsonUtility.FromJson<T>(json);
            //return (UserData)JsonFormatter.Deserialize(json);
            //return new JsonFormatter().Deserialize<T>(json);
            //return Json.Deserialize<T>(json);
        }
        else
        {
            return default;
        }
    }

    public static string GetSavePath(string fileName)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        return Path.Combine(Application.persistentDataPath, fileName + ".json");
#else
        return Path.Combine(Application.dataPath + "/Resources/Saves/", fileName + ".json");
#endif
    }

    public static bool IsExistsSave(string fileName)
    {
        return File.Exists(GetSavePath(fileName));
    }
}
