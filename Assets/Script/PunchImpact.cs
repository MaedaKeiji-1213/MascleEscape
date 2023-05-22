using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchImpact : MonoBehaviour
{
    bool is_set=false;

    bool is_display=false;
    Vector3 dir=Vector3.zero;
    float length=0;
    float max_time=0;
    AnimationCurve size_curve;
    float stop_time=0;
    float life_time=0;


    Vector3 origin_position=Vector3.zero;
    float passed_time=0;
    ParticleSystem particle;
    AudioSource audio_source;
    // Start is called before the first frame update
    void Start()
    {
        origin_position=transform.position;
        particle=transform.GetChild(0).GetComponent<ParticleSystem>();
        audio_source=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(is_set)
        {
            Vector3 punch_vec=dir*(passed_time/max_time)*length;
            Collider[] hit_colliders=Physics.OverlapSphere(origin_position+punch_vec,size_curve.Evaluate(punch_vec.magnitude),(1<<3));
            Debug.Log(Vector3.one*size_curve.Evaluate(punch_vec.magnitude));

            transform.position=origin_position+punch_vec;
            transform.localScale=Vector3.one*size_curve.Evaluate(punch_vec.magnitude);
            if(particle!=null&&!is_display)
            {
                particle.Simulate(0f,true);
                particle.Stop();
            }

            foreach (Collider col in hit_colliders)
            {
                Debug.Log("hitPunch"+col);
                if(col.tag=="Break")
                {
                    //当たったものを爆破
                    ExplosionControl Explosion_control=col.transform.parent.GetComponent<ExplosionControl>();//当たり判定から受け取ったものの中で壊れると設定されたものだった時そのオブジェクトについてるスクリプトを通して爆破させる。
                    if(Explosion_control!=null)
                    {
                        Explosion_control.DoExplosion(life_time);
                    }
                }
            }
            if(passed_time>=max_time)//パンチの予定所要時間より経過時間のほうが長かったらパンチを終わる
            {
                Destroy(gameObject,2f);
            }
            else//そうでなければ経過時間を数える
            {
                if(particle!=null&&stop_time<=particle.time)
                {
                    PlaySound.PlaySoundSingle(audio_source,audio_source.clip);
                    particle.Simulate(stop_time-0.2f,true);
                    particle.Play();
                }
                passed_time+=Time.deltaTime;
            }
        }
    }
    public void SetImpact(bool _is_display,Vector3 _dir,float _length,float _max_time,AnimationCurve _size_curve,float _stop_time,float _life_time)
    {
        is_set=true;

        is_display=_is_display;
        dir=_dir;
        length=_length;
        max_time=_max_time;
        size_curve=_size_curve;
        stop_time=_stop_time;
        life_time=_life_time;
    }
}
