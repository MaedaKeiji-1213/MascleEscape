using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float Speed = 5;     //�J�����̃X�s�[�h
    [SerializeField]GameObject camera_aim;          //���j�e�B�����̃Q�[���I�u�W�F�N�g
    [SerializeField]GameObject camera_target;    //�J�����ʒu�̃Q�[���I�u�W�F�N�g
    Rigidbody rb;

    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //�J�������Ȃ߂炩�ɃJ�����ʒu�܂ňړ�������
        //transform.position = Vector3.Lerp(transform.position, camera_target.transform.position, Time.deltaTime * Speed);
        rb.velocity=(camera_target.transform.position-transform.position)*Speed;
        //���j�e�B�����փJ������������
        transform.LookAt(camera_aim.transform.position);
    }
}