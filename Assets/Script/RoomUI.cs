using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class RoomUI : MonoBehaviour
{
    TextMeshProUGUI tm_pro;
    // Start is called before the first frame update
    void Start()
    {
        tm_pro=GetComponent<TextMeshProUGUI>();
        tm_pro.text=SceneManager.GetActiveScene().name.Replace("_"," ");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
