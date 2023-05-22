using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    enum ViewMode{
        TPS_view,
        FPS_view
    }
    public float Speed = 2;             //Player�̃X�s�[�h
    public float Sensitivity = 10;      //�}�E�X�̊��x
    private Rigidbody rb;
    public int upForce = 200;                 //�W�����v��
    private bool isGround;              //�n�ʔ���
    public bool is_punch=false;
    private Animator animator;          //�A�j���[�^�[�̃I�u�W�F�N�g
    SwitchAimConstraint switch_aim_constraint;
    [SerializeField]GameObject cameraTarget;            //�J�����ʒu�̃Q�[���I�u�W�F�N�g
    [SerializeField]GameObject cameraAim;            //�J�����ʒu�̃Q�[���I�u�W�F�N�g
    [SerializeField]float max_speed;
    [SerializeField]float dash_rate;
    [SerializeField]float animation_trans_time;
    [SerializeField]TouchCheck touch_check;
    bool is_transition=false;
    ViewMode view_mode=ViewMode.FPS_view;

    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        switch_aim_constraint=GetComponent<SwitchAimConstraint>();
    }

    void FixedUpdate()
    {

        if(is_punch)
        {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1)
                is_punch=false;
            return;
        }

        isGround=touch_check.touch_c();
        bool keyFlg = false;  //�L�[�������ꂽ���̃t���O
        Vector3 move=Vector3.zero;
        float angle=(-transform.rotation.eulerAngles.y+360);
        move.x+= Mathf.Cos(Mathf.Deg2Rad*(angle+90))*Input.GetAxis("Vertical");   //�������̈ړ��i�����j
        move.z+= Mathf.Sin(Mathf.Deg2Rad*(angle+90))*Input.GetAxis("Vertical");   //�������̈ړ��i�����j
        move.x+= Mathf.Cos(Mathf.Deg2Rad*angle)*Input.GetAxis("Horizontal");   //�������̈ړ��i�����j
        move.z+= Mathf.Sin(Mathf.Deg2Rad*angle)*Input.GetAxis("Horizontal");   //�������̈ړ��i�����j
        Debug.Log("move_vecter"+angle);
        if (move.magnitude <= 0)move *= 0.5f;                           //�o�b�N�Ȃ猸��
        if (move.magnitude > 0 && Input.GetKey(KeyCode.LeftControl))
        {
            move /= dash_rate;
        }
        else if(move.magnitude > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            move *= dash_rate;
        }
        rb.AddForce(move*Time.deltaTime * Speed *(1-rb.velocity.magnitude/max_speed)); //Player�ړ�

        //1:walk 2:back 3:jump 4:right 5:left6:run

        //�J�[�\���L�[�̏��󂪉����ꂽ�Ƃ�
        if ((Input.GetKey(KeyCode.UpArrow) && isGround) || (Input.GetKey(KeyCode.W) && isGround))
        {
            if(Input.GetKey(KeyCode.LeftControl))
            {
                PlayAnime("CrouchWalk",animation_trans_time,WrapMode.Loop);
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                PlayAnime("Run",animation_trans_time,WrapMode.Loop);
            }
            else
            {
                PlayAnime("ForwardWalk",animation_trans_time,WrapMode.Loop);
            }
            keyFlg = true;                                  //�L�[��������
        }
        //�J�[�\���L�[�̉���󂪉����ꂽ�Ƃ�
        if ((Input.GetKey(KeyCode.DownArrow) && isGround) || (Input.GetKey(KeyCode.S) && isGround))
        {
            if(Input.GetKey(KeyCode.LeftControl))
            {
                PlayAnime("CrouchWalk",animation_trans_time,WrapMode.Loop);
            }
            else
            {
                PlayAnime("BackWalk",animation_trans_time,WrapMode.Loop);
            }
            keyFlg = true;
        }
        //�J�[�\���L�[�̉E��󂪉����ꂽ�Ƃ�
        if ((Input.GetKey(KeyCode.RightArrow) && isGround) || (Input.GetKey(KeyCode.D) && isGround))
        {
            if(Input.GetKey(KeyCode.LeftControl))
            {
                PlayAnime("CrouchWalk",animation_trans_time,WrapMode.Loop);
            }
            else
            {
                PlayAnime("RightWalk",animation_trans_time,WrapMode.Loop);
            }
            keyFlg = true;
        }
        //�J�[�\���L�[�̍���󂪉����ꂽ�Ƃ�
        if ((Input.GetKey(KeyCode.LeftArrow) && isGround) || (Input.GetKey(KeyCode.A) && isGround))
        {
            if(Input.GetKey(KeyCode.LeftControl))
            {
                PlayAnime("CrouchWalk",animation_trans_time,WrapMode.Loop);
            }
            else{
                PlayAnime("LeftWalk",animation_trans_time,WrapMode.Loop);
            }
            keyFlg = true;
        }
        //�X�y�[�X�L�[�������ꂽ�Ƃ�(����������)
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            
            PlayAnime("Jump",animation_trans_time,WrapMode.Loop);
            rb.AddForce(new Vector3(0, upForce, 0));
            keyFlg = true;
        }
        //�L�[��������Ȃ�������
        if (keyFlg == false && isGround)
        {
            if(Input.GetKey(KeyCode.LeftControl))
            {
                PlayAnime("CrouchIdle",animation_trans_time,WrapMode.Loop);
            }
            else
            {
                PlayAnime("Idle",animation_trans_time,WrapMode.Loop);
            }
        }
        if(Input.GetMouseButtonDown(0)&& isGround)
        {
            PlayAnime("Punch",animation_trans_time,WrapMode.ClampForever);
            is_punch=true;
        }


        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(view_mode==ViewMode.FPS_view)
            {
                view_mode=ViewMode.TPS_view;
                switch_aim_constraint.SwitchConstraintActive(false);
                cameraAim.transform.localPosition=new Vector3(0,0,1);
                cameraTarget.transform.localPosition=new Vector3(0,0,0);
            }
            if(view_mode==ViewMode.TPS_view)
            {
                view_mode=ViewMode.FPS_view;
                switch_aim_constraint.SwitchConstraintActive(true);
                cameraAim.transform.localPosition=new Vector3(0,0,0);
                cameraTarget.transform.localPosition=new Vector3(0,0,-3);
            }
        }
        // if(view_mode==ViewMode.FPS_view)
        // {
            //�}�E�X�ɂ�鑀��
            float h = Input.GetAxis("Mouse X") * Sensitivity;
            transform.Rotate(new Vector3(0, h, 0));                     //Player���E��]
            float v = Input.GetAxis("Mouse Y") * Sensitivity * 0.02f;
            cameraAim.transform.Translate(new Vector3(0, v, 0));     //�J�����ʒu�㉺
            //�J�����̍�������
            if (cameraAim.transform.localPosition.y < -2f)
            {
                cameraAim.transform.localPosition = new Vector3(cameraAim.transform.localPosition.x, -2f, cameraAim.transform.localPosition.z);
            }
            if (cameraAim.transform.localPosition.y > 2.5f)
            {
                cameraAim.transform.localPosition = new Vector3(cameraAim.transform.localPosition.x, 2.5f, cameraAim.transform.localPosition.z);
            }
        // }
        // else if(view_mode==ViewMode.TPS_view)
        // {
        //     float h = Input.GetAxis("Mouse X") * Sensitivity;
        //     transform.Rotate(new Vector3(0, h, 0));                     //Player���E��]
        //     float v = Input.GetAxis("Mouse Y") * Sensitivity * -0.02f;
        //     cameraTarget.transform.Translate(new Vector3(0, v, 0));     //�J�����ʒu�㉺
        //     //�J�����̍�������
        //     if (cameraTarget.transform.localPosition.y < -2f)
        //     {
        //         cameraTarget.transform.localPosition = new Vector3(cameraAim.transform.localPosition.x, -2f, cameraAim.transform.localPosition.z);
        //     }
        //     if (cameraTarget.transform.localPosition.y > 2.5f)
        //     {
        //         cameraTarget.transform.localPosition = new Vector3(cameraAim.transform.localPosition.x, 2.5f, cameraAim.transform.localPosition.z);
        //     }
        //}

    }

    void PlayAnime(string anime_name,float trans_time,WrapMode wrap_mode)
    {
        if(!is_transition&&!animator.GetCurrentAnimatorStateInfo(0).IsName(anime_name))
        {
            animator.CrossFadeInFixedTime(anime_name,trans_time);
            animator.GetCurrentAnimatorClipInfo(0)[0].clip.wrapMode=wrap_mode;
            StartCoroutine(WaitTransition(trans_time));
        }
    }
    IEnumerator WaitTransition (float trans_time)
    {
        is_transition=true;
        yield return new WaitForSeconds(trans_time); 
        is_transition=false;
    }
}