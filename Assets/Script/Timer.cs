using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    [SerializeField] float time_limit;
    [SerializeField] float denger_time;
    [SerializeField] Animator denger_animator;
    [System.NonSerialized]public float time_left;
    [System.NonSerialized]public float best_time;
    
    [SerializeField]TextMeshProUGUI text_mesh;
    
    [SerializeField]TextMeshProUGUI text_mesh_clear;
    [SerializeField]TextMeshProUGUI text_mesh_best_time;
    [SerializeField]TextMeshProUGUI text_mesh_best_time_clear;
    [SerializeField]Image image;
    bool is_valid=true;

    // Start is called before the first frame update
    void Start()
    {
        time_left=time_limit;
        denger_animator.speed=0;
        
        best_time=StageData.LoadStageData(SceneManager.GetActiveScene().name);

    }

    // Update is called once per frame
    void Update()
    {
        if(is_valid)
            time_left-=Mathf.Min(Time.deltaTime,time_left);

        image.fillAmount=time_left/time_limit*0.8f+0.08f;
        
        text_mesh.text=string.Format("{0:F2}", time_left);
        text_mesh_clear.text=string.Format("{0:F2}", time_left);
        text_mesh_best_time.text="BestTime:残り "+string.Format("{0:F2}",best_time)+" 秒";
        text_mesh_best_time_clear.text="BestTime:残り "+string.Format("{0:F2}",best_time)+" 秒";
        if(time_left<=0)
        {
            denger_animator.speed=0;
            
        }
        else if(time_left<=denger_time)
        {
            denger_animator.speed=1;
        }
    }

    public void StopTimer()
    {
        is_valid=false;


    }
}
