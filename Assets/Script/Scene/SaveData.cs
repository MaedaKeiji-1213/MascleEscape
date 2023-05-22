using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class StageData
{
    public static void SaveStageData(string scene_name,float score_time)
    {
        Debug.Log("SaveStageData");
        Dictionary<string,float> save_data=new Dictionary<string, float>();
        object obj=SaveManager.Load("StageSaveData");
        if(obj!=null&&obj is Dictionary<string,float>)
            save_data=(Dictionary<string,float>)obj;
        else 
            save_data=null;
        if(save_data==null)
        {
            save_data=new Dictionary<string,float>();
            save_data.Add("a",114514);
        }
        
        if(save_data.ContainsKey(scene_name))
        {
            save_data[scene_name]=Mathf.Min(save_data[scene_name],score_time);
        }
        else
        {
            save_data.Add(scene_name,score_time);
        }
        foreach (var item in save_data.Keys)
        {
            Debug.Log("item"+item+"Value"+save_data[item]);
        }

        Dictionary<string, int> myDictionary = new Dictionary<string, int>();
        myDictionary.Add("key1", 100);
        myDictionary.Add("key2", 200);
        Debug.Log("save_data"+JsonUtility.ToJson(save_data as object));
        SaveManager.Save("StageSaveData",myDictionary);
    }

    public static float LoadStageData(string scene_name)
    {        
        Debug.Log("LoadStageData");
        float score_time=0.00f;
        Dictionary<string,float> save_data=new Dictionary<string, float>();
        object obj=SaveManager.Load("StageSaveData");
        Debug.Log("obj:"+obj);
        if(obj!=null&&obj is Dictionary<string,float>)
            save_data=(Dictionary<string,float>)obj;
        else 
            save_data=null;
        if(save_data==null)
        {
            save_data=new Dictionary<string,float>();
        }
        
        if(save_data.ContainsKey(scene_name))
        {
            score_time=save_data[scene_name];
        }
        return score_time;
    }
    

}
