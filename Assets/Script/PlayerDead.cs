using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{

    [SerializeField]GameObject skinny_model;
    [SerializeField]GameObject lost_effect_pre;
    [SerializeField]GameObject dust_effect_pre;
    [SerializeField]Transform spin;
    Animator UI_animator;
    PlayerController player_controller;
    Timer timer;
    GameObject lost_obj;
    GameObject obj;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator=gameObject.GetComponent<Animator>();
        player_controller=transform.parent.gameObject.GetComponent<PlayerController>();
        timer=GameObject.Find("UI").GetComponent<Timer>();
        UI_animator=GameObject.Find("UI").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(timer.time_left<=0&&player_controller.is_dead==false)
        {
            //Debug.Log("しんでしまうとはなさけない");
            player_controller.is_dead=true;
            player_controller.PlayAnime("Dying",0.2f,WrapMode.ClampForever);
            lost_obj=Instantiate<GameObject>(lost_effect_pre,transform);
            lost_obj.transform.localPosition=Vector3.zero;
            lost_obj.transform.localEulerAngles=Vector3.zero;
            lost_obj.transform.parent=transform.parent;
            Destroy(lost_obj,10);
        }
    }

    public void Dust()
    {
        Debug.Log("2");
        lost_obj.GetComponent<ParticleSystem>().loop=false;
        GameObject dust_obj=Instantiate<GameObject>(dust_effect_pre,spin);
        dust_obj.transform.localPosition=Vector3.zero;
        dust_obj.transform.localEulerAngles=Vector3.zero;
        dust_obj.transform.parent=transform.parent;
        Destroy(dust_obj,10);
        
    }

    public void ChangeToSkinny()
    {
        obj=Instantiate(skinny_model,transform);
        obj.transform.localPosition=Vector3.zero;
        obj.transform.localEulerAngles=Vector3.zero;
        obj.GetComponent<Animator>().Play("Dying",0,animator.GetCurrentAnimatorStateInfo(0).normalizedTime+0.01f);
        obj.transform.parent=transform.parent;
        StartCoroutine(RefreshSize(0.1f));
        UI_animator.Play("Dead",0);
    }
    IEnumerator RefreshSize(float _time)
    {
        yield return new WaitForSeconds(_time);
        obj.transform.localScale=Vector3.one;
        Destroy(gameObject);
    }
}
