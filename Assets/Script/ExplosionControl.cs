using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject static_object;
    [SerializeField] GameObject explosion_object;
    [SerializeField] float all_destroy_time;
    float destroy_cool_time=0;
    bool is_end_cool_time=false;
    void Start()
    {
        destroy_cool_time=all_destroy_time/explosion_object.transform.childCount;
        static_object.SetActive(true);
        explosion_object.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(explosion_object.active==true)
        {
            if(explosion_object.transform.childCount<=0)
            {
                Debug.Log("childCount"+explosion_object.transform.childCount);
                Destroy(gameObject);
            }
            if(is_end_cool_time)
            {
                int index=Random.Range(0,explosion_object.transform.childCount-1);
                Destroy(explosion_object.transform.GetChild(index).gameObject);
                StartCoroutine(CoolTime(destroy_cool_time));
            }
        }
    }
    
    public void DoExplosion(float _time)
    {
        gameObject.layer=0;
        static_object.SetActive(false);
        explosion_object.SetActive(true);
        AudioSource audio_source=GetComponent<AudioSource>();
        PlaySound.PlaySoundSingle(audio_source,audio_source.clip);
        StartCoroutine(CoolTime(destroy_cool_time+_time));

    }
    IEnumerator CoolTime (float _time)
    {
        is_end_cool_time=false;
        yield return new WaitForSeconds(_time);
        is_end_cool_time=true;
    }
}
