using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearObject : MonoBehaviour
{
    // Start is called before the first frame update
    MeshRenderer mesh_renderer;
    [SerializeField] [ColorUsage(false, true)] Color color;
    [SerializeField] float lerp_speed;
    bool is_clear=false;
    void Start()
    {
        mesh_renderer=GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color current_color=mesh_renderer.material.GetColor("_EmissionColor");
        if(is_clear)
            mesh_renderer.material.SetColor("_EmissionColor",Color.Lerp(current_color,color,Time.deltaTime*lerp_speed));
    }
    public void Shine(Collider col)
    {        
        Debug.Log("auoiu"+col);
        is_clear=true;
        PlayerController player_controller=col.gameObject.GetComponent<PlayerController>();
        if(player_controller!=null)
            player_controller.is_clear=true;
        else 
            Debug.Log("nullだったんだ");
        GameObject ui=GameObject.Find("UI");
        Timer timer=ui.GetComponent<Timer>();
        timer.StopTimer();
        ui.GetComponent<Animator>().CrossFade("StageClear",0);
        //StageData.SaveStageData(SceneManager.GetActiveScene().name,timer.time_left);
        //timer.best_time=StageData.LoadStageData(SceneManager.GetActiveScene().name);
    }

    
}
