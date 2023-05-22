using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchObject : MonoBehaviour
{
    FixedJoint fixed_joint;
    [SerializeField]float break_force;
    [SerializeField]float break_torque;
    public bool is_grasp=false;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if(fixed_joint==null)
            is_grasp=false;
        if(Input.GetMouseButtonDown(0))
            is_grasp=!is_grasp;
        if(is_grasp==false&&fixed_joint!=null)
            Destroy(fixed_joint);
    }
    void OnTriggerStay(Collider col)
    {
        if(is_grasp&&fixed_joint==null&&col.attachedRigidbody!=null&&col.isTrigger!=true){
            fixed_joint=this.gameObject.AddComponent<FixedJoint>();
            fixed_joint.breakForce=break_force;
            fixed_joint.breakTorque=break_torque;
            fixed_joint.connectedBody=col.attachedRigidbody;
        }
    }
}
