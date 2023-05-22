using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PunchController : MonoBehaviour
{
    [System.Serializable]
    public class TimeEffectPair : Serialize.KeyAndValue<float,GameObject>{
        public TimeEffectPair (float key,GameObject value) : base (key, value) {}
    }
    [System.Serializable]
    public class TimeEffectTable : Serialize.TableBase<float,GameObject, TimeEffectPair>{}

    [SerializeField]float charge_max_time;
    [SerializeField]AnimationCurve punch_size_curve;
    [SerializeField]AnimationCurve punch_length_curve;
    [SerializeField]float punch_impact_time;
    [SerializeField]float charge_stop_timing; 
    [SerializeField]float effects_cool_time;
    [SerializeField]Transform effect_parent;
    [SerializeField]Transform punch_origin;
    [SerializeField]GameObject impact_effect_pre;
    [SerializeField]float impact_effect_stop_time;
    [SerializeField]ReleasePunch release_punch;
    [SerializeField]float obj_life_time;
    GameObject impact_effect_obj=null;
    ParticleSystem impact_effect;
    PlayerController player_controller;
    Vector3 punch_origin_position=Vector3.zero;
    Vector3 punch_direction=Vector3.zero;
    public TimeEffectTable input_effects;
    Dictionary<float,GameObject> effects;
    GameObject active_effect;
    Animator animator;
    float punch_length;
    
    bool last_is_punch=false;
    
    bool is_end_effect_cooltime=true;

    bool end_mouse_click=true;
    float mouse_click_time=0;

    void Awake () {
        effects=input_effects.GetTable();
        //内容を列挙
        foreach (KeyValuePair<float, GameObject> pair in effects) {
            Debug.Log ("Key : " + pair.Key + "  Value : "  + pair.Value);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator=transform.Find("Player").GetComponent<Animator>();
        player_controller=GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player_controller.is_punch&&!last_is_punch)//今回のフレームからパンチしてたらフラグたちを初期化
        {
            end_mouse_click=false;
            punch_length=0;
            mouse_click_time=0;

        }
        if(end_mouse_click==false&&player_controller.is_punch_clicked)//パンチ中でマウスをクリックし続けてたら
        {
            //Debug.DrawRay(_ray.origin,_ray.direction*(passed_time/punch_impact_time)*punch_length,Color.green,1);
            if(Input.GetMouseButton(0))
            {
                mouse_click_time+=mouse_click_time>=charge_max_time?0:Time.deltaTime;//クリック時間を可算
                release_punch.is_release=false;
                //エフェクトを一定時間ごとに出す
                if(is_end_effect_cooltime&&animator!=null&&animator.GetCurrentAnimatorStateInfo(0).IsName("ChargePunch"))
                {
                    GenerateEffect(mouse_click_time);
                }
            }
            if(Input.GetMouseButtonUp(0))
            {
                
                punch_length=punch_length_curve.Evaluate(mouse_click_time);//クリックし終わったらクリック時間をもとにパンチの飛距飛を変数に入れ、フラグたちを立てる
                end_mouse_click=true;

            }
            if(mouse_click_time>0.1f&&animator!=null&&animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))//アニメーションがパンチかつクリック時間が一定以上ならチャージパンチアニメーションにする
            {
                player_controller.PlayAnime("ChargePunch",0.5f,WrapMode.ClampForever);
            }
        }
        if(animator!=null&&animator.GetCurrentAnimatorStateInfo(0).IsName("ChargePunch")//チャージパンチかつ、チャージを止めるタイミングを迎えていて、まだマウスをクリックしているなら
            &&animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=charge_stop_timing
            &&Input.GetMouseButton(0)
            &&!end_mouse_click&&!player_controller.is_dead)
        {
            animator.speed=0;//アニメーションを止める。
        }
        else if(animator!=null)
        {
            animator.speed=1;//そうでないなら再開
        }

        if(release_punch.is_release)//パンチが放たれたフラグが立っていて、パンチアニメーションからイベントが呼ばれていたら
        {
            punch_origin_position=punch_origin.position;
            punch_direction=Vector3.zero;
            Ray _ray=Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth/2,Camera.main.pixelHeight/2,0));//画面真ん中へのレイを取得
            if(Physics.Raycast(_ray,out RaycastHit hit,100f,~(1<<6)))
            {
                if((hit.point-punch_origin_position).magnitude>2)
                    punch_direction=(hit.point-punch_origin_position).normalized;
                else 
                    punch_direction=_ray.direction;
            }
            else {
                punch_direction.x=Mathf.Cos(this.transform.eulerAngles.y-90*Mathf.Deg2Rad);
                punch_direction.z=Mathf.Sin(this.transform.eulerAngles.y-90*Mathf.Deg2Rad);
            }
            bool is_impact_display;
            if(animator!=null&&animator.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
                is_impact_display=false;
            else
                is_impact_display=true;
            impact_effect_obj=Instantiate<GameObject>(impact_effect_pre,punch_origin_position,Quaternion.identity);
            impact_effect_obj.GetComponent<PunchImpact>().SetImpact(is_impact_display,punch_direction,punch_length,punch_impact_time,punch_size_curve,impact_effect_stop_time,obj_life_time);
            release_punch.is_release=false;
        }
        last_is_punch=player_controller.is_punch;   //今フレーム、パンチしたかどうかを変数に入れる
    }

    public void GenerateEffect(float _time)
    {//引数の_timeにあったエフェクトを選択して変数に入れる
        
        foreach(float boundary_time in effects.Keys)
        {
            if(boundary_time<=_time){
                effects.TryGetValue(boundary_time,out GameObject valid_effect);
                active_effect=valid_effect;
            }
        }
        Destroy(Instantiate(active_effect,effect_parent.position,Quaternion.identity,effect_parent),effects_cool_time);
        StartCoroutine(EffectCoolTime());
        
    }
    IEnumerator EffectCoolTime()
    {
        is_end_effect_cooltime=false;
        yield return new WaitForSeconds(effects_cool_time);
        is_end_effect_cooltime=true;
    }
}
