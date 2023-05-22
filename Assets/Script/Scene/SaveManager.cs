using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
public static class SaveManager
{
    public static void Save(string file_name,object save_object)
    {
        string json = JsonUtility.ToJson(save_object);
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory();
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
        path += ("/" + file_name+".json");
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(json);
        writer.Flush();
        writer.Close();
    }

    public static object Load(string file_name)
    {
        try
        {
#if UNITY_EDITOR
            string path = Directory.GetCurrentDirectory();
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
#endif
            FileInfo info = new FileInfo(path + "/" + file_name + ".json");
            StreamReader reader = new StreamReader(info.OpenRead());
            string json = reader.ReadToEnd();
            reader.Close();
            return JsonUtility.FromJson<object>(json);
        }
        catch (Exception e)
        {
            return null;
        }
    }
}