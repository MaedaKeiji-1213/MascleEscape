using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCheck : MonoBehaviour
{
    bool is_touch_enter=false;//is_touch_enter
    bool is_touch_stay=false;//is_touch_stay
    bool is_touch_exit=false;//is_touch_exit

    bool is_touch=false;
    // Start is called before the first frame update
    void Start()
    {
        
    
    }

    // Update is called once per frame
    void Update(){
    }
    void OnTriggerEnter(Collider col){
        if(col.tag!="Player"&&!col.isTrigger){
            is_touch_enter=true;
            is_touch_stay=false;
            is_touch_exit=false;
        }
    }
    void OnTriggerStay (Collider col){
        if(col.tag!="Player"&&!col.isTrigger){
            is_touch_enter=false;
            is_touch_stay=true;
            is_touch_exit=false;
        }
    }
    void OnTriggerExit (Collider col){
        if(col.tag!="Player"&&!col.isTrigger){
            is_touch_enter=false;
            is_touch_stay=false;
            is_touch_exit=true;
        }
    }

    public bool touch_c()
    {
        if(is_touch_enter==true||is_touch_stay==true){
            is_touch=true;
        }
        else if(is_touch_exit==true){
            is_touch=false;
        }
            
        is_touch_enter=false;
        is_touch_stay=false;
        is_touch_exit=false;

        return is_touch;
    }
}
