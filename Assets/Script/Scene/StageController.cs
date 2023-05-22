using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageController : MonoBehaviour
{
    FadeManager fm;
    [SerializeField]Color fade_color;
    [SerializeField] SceneObject scene;
    [SerializeField] float change_time;
 
    // Start is called before the first frame update
    void Start()
    {
        fm = FadeManager.Instance;
    }

    public void ChangeScene()
    {
        Debug.Log(scene);
        fm.fadeColor=fade_color;
        fm.LoadScene(scene, change_time);
    }

    public void RestartScene()
    {
        fm.fadeColor=fade_color;
        fm.LoadScene(SceneManager.GetActiveScene().name, change_time);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            RestartScene();
        if(Input.GetKeyDown(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            fm.LoadScene("TitleScene", change_time);
            
        }

    }
}
