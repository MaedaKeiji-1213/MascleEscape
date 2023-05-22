using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testament : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        Dictionary<string, int> myDictionary = new Dictionary<string, int>();
        myDictionary.Add("key1", 100);
        myDictionary.Add("key2", 200);
        SaveManager.Save("Test",myDictionary);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
