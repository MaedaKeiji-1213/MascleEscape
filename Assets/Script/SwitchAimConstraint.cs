using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations; 

[RequireComponent(typeof(Animator))]
public class SwitchAimConstraint : MonoBehaviour
{   
    [SerializeField,Tooltip("ConstraintList")] List<AimConstraint>IK_gameobject_list;
    
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void SwitchConstraintActive(bool is_active)
    {
        foreach(AimConstraint set_gameobject in IK_gameobject_list)
        {
            set_gameobject.enabled=is_active;
            
        }
    }
}