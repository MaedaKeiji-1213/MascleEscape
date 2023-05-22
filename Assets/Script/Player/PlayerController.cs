using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum playerMode
    {
        playing,
        dead,
        clear
    }

    public readonly string IDLE_ANIMATION="Idle";    
    public readonly string FORWARDWALK_ANIMATION="ForwardWalk";
    public readonly string RIGHTWALK_ANIMATION="RightWalk";
    public readonly string LEFTWALK_ANIMATION="LeftWalk";
    public readonly string BACKWALK_ANIMATION="BackWalk";
    public readonly string RUN_ANIMATION="Run";
    public readonly string PUNCH_ANIMATION="Punch";
    public readonly string CHARGEPUNCH_ANIMATION="ChargePunch";
    public readonly string DEAD_ANIMATION="Dying";
    public readonly string JUMP_ANIMATION="Jump";
    public bool is_dead=false;
    public bool is_clear=false;


    [System.NonSerialized]public playerMode player_mode=playerMode.playing;
    [SerializeField] float walk_speed;
    [SerializeField] float walk_accele_speed;
    [SerializeField] float dash_rate;
    [SerializeField] float jump_force;
    [SerializeField] float camera_sensitivity;
    [SerializeField] Transform aboutCamera;
    [SerializeField] Transform cameraTarget;
    [SerializeField] Transform cameraAim;
    [SerializeField] float animation_trans_time;
    [SerializeField] TouchCheck touch_check;



    public float Speed = 2;             //Player�̃X�s�[�h
    public float Sensitivity = 10;      //�}�E�X�̊��x
    Rigidbody rb;
    public bool is_punch = false;
    private Animator animator;          //�A�j���[�^�[�̃I�u�W�F�N�g
    SwitchAimConstraint switch_aim_constraint;
    [SerializeField] float camera_rotation_limit_top, camera_rotation_limit_bottom;
    [SerializeField] Transform spine;
    bool is_transition = false;
    public bool is_punch_clicked=false;
    bool is_jump = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = transform.Find("Player").GetComponent<Animator>();
        switch_aim_constraint = GetComponent<SwitchAimConstraint>();
    }

    void FixedUpdate()
    {
        bool isGround = touch_check.touch_c();
        bool keyFlg = false;  //�L�[�������ꂽ���̃t���O
        float dash = 1;
        Vector3 move = Vector3.zero;
        Vector3 advance_vector = (cameraAim.position - cameraTarget.position).normalized;
        advance_vector.y = 0;
        
        //playerModeによって動作を変える
        if(player_mode==playerMode.clear)
        {
            
        }
        else if(player_mode==playerMode.dead)
        {
            if(animator!=null&&!animator.GetCurrentAnimatorStateInfo(0).IsName(DEAD_ANIMATION))
                PlayAnime(DEAD_ANIMATION,0.2f,WrapMode.ClampForever);
            is_punch=false; 
            if(spine!=null)
                cameraAim.position=spine.position;
        }
        else{
            if (is_punch || is_jump)
            {
                if (is_punch && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1
                    || !(animator.GetCurrentAnimatorStateInfo(0).IsName(CHARGEPUNCH_ANIMATION) || animator.GetCurrentAnimatorStateInfo(0).IsName(PUNCH_ANIMATION)))
                    is_punch = is_punch_clicked=false;
                if (is_jump && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1
                    || !animator.GetCurrentAnimatorStateInfo(0).IsName(JUMP_ANIMATION))
                    is_jump = false;
            }
            else
            {
                move += advance_vector * Input.GetAxis("Vertical");   //�������̈ړ��i�����j
                move += Vector3.Cross(Vector3.up, advance_vector).normalized * Input.GetAxis("Horizontal");
                if (move.magnitude <= 0) move *= 0.5f;                           //�o�b�N�Ȃ猸��
                if (move.magnitude > 0 && Input.GetKey(KeyCode.LeftControl))
                {
                    dash /= dash_rate;
                }
                else if (move.magnitude > 0 && Input.GetKey(KeyCode.LeftShift))
                {
                    dash *= dash_rate;
                }
                if (move.magnitude <= 0)
                {
                    move.x = -rb.velocity.x;
                    move.z = -rb.velocity.z;
                }
                if (rb.velocity.magnitude <= walk_speed * dash)
                {
                    rb.AddForce(move * Speed); //Player�ړ�
                }
                // else
                //     Debug.Log(rb.velocity.magnitude);
                //1:walk 2:back 3:jump 4:right 5:left6:run

                //�J�[�\���L�[�̏��󂪉����ꂽ�Ƃ�

                if (Input.GetMouseButtonDown(0) && isGround)
                {
                    PlayAnime(PUNCH_ANIMATION, animation_trans_time, WrapMode.ClampForever);
                    StartCoroutine(LateChangeFlag(animation_trans_time));
                    is_punch_clicked=true;
                }
                else if (Input.GetKeyDown(KeyCode.Space) && isGround)
                {
                    PlayAnime(JUMP_ANIMATION, animation_trans_time, WrapMode.ClampForever);
                    rb.AddForce(new Vector3(0, jump_force, 0));
                    is_jump = true;
                    keyFlg = true;
                }
                else if ((Input.GetKey(KeyCode.UpArrow) && isGround) || (Input.GetKey(KeyCode.W) && isGround))
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        PlayAnime(RUN_ANIMATION, animation_trans_time, WrapMode.Loop);
                    }
                    else
                    {
                        PlayAnime(FORWARDWALK_ANIMATION, animation_trans_time, WrapMode.Loop);
                    }
                    keyFlg = true;                                  //�L�[��������
                }
                else if ((Input.GetKey(KeyCode.DownArrow) && isGround) || (Input.GetKey(KeyCode.S) && isGround))
                {
                    PlayAnime(BACKWALK_ANIMATION, animation_trans_time, WrapMode.Loop);
                    keyFlg = true;
                }
                else if ((Input.GetKey(KeyCode.RightArrow) && isGround) || (Input.GetKey(KeyCode.D) && isGround))
                {
                    PlayAnime(RIGHTWALK_ANIMATION, animation_trans_time, WrapMode.Loop);
                    keyFlg = true;
                }
                else if ((Input.GetKey(KeyCode.LeftArrow) && isGround) || (Input.GetKey(KeyCode.A) && isGround))
                {
                    PlayAnime(LEFTWALK_ANIMATION, animation_trans_time, WrapMode.Loop);
                    keyFlg = true;
                }
                else if (keyFlg == false)
                {
                        PlayAnime(IDLE_ANIMATION, animation_trans_time, WrapMode.Loop);
                }
            }
        }
        //Debug.Log(aboutCamera.localEulerAngles-Vector3.up*(aboutCamera.localEulerAngles.y>180?360:0));
        float h = Input.GetAxis("Mouse X") * Sensitivity;
        if (Mathf.Abs(transform.eulerAngles.y - aboutCamera.eulerAngles.y) > 1 && (keyFlg || is_punch))
        {
            Vector3 angle = aboutCamera.localEulerAngles - Vector3.up * (aboutCamera.localEulerAngles.y > 180 ? 360 : 0);
            transform.Rotate(0, Mathf.Sign(angle.y) * 5, 0);
            aboutCamera.Rotate(0, -Mathf.Sign(angle.y) * 5, 0);
        }
        aboutCamera.Rotate(new Vector3(0, h, 0));

        float v = Input.GetAxis("Mouse Y") * Sensitivity * -0.02f;
        cameraTarget.transform.Translate(new Vector3(0, v, 0));     //�J�����ʒu�㉺
        //�J�����̍�������
        if (cameraTarget.transform.localPosition.y < camera_rotation_limit_bottom)
        {
            cameraTarget.transform.localPosition = new Vector3(cameraTarget.transform.localPosition.x, camera_rotation_limit_bottom, cameraTarget.transform.localPosition.z);
        }
        if (cameraTarget.transform.localPosition.y > camera_rotation_limit_top)
        {
            cameraTarget.transform.localPosition = new Vector3(cameraTarget.transform.localPosition.x, camera_rotation_limit_top, cameraTarget.transform.localPosition.z);
        }
        

    }

    public void PlayAnime(string anime_name, float trans_time, WrapMode wrap_mode)
    {
        if (animator.GetCurrentAnimatorClipInfoCount(0)>0&&animator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            Debug.Log(anime_name + ":isChange" + !animator.GetCurrentAnimatorStateInfo(0).IsName(anime_name));
        if (!is_transition && !animator.GetCurrentAnimatorStateInfo(0).IsName(anime_name))
        {
            animator.CrossFadeInFixedTime(anime_name, trans_time);
            animator.GetCurrentAnimatorClipInfo(0)[0].clip.wrapMode = wrap_mode;
            StartCoroutine(WaitTransition(trans_time));
        }
    }
    IEnumerator WaitTransition(float trans_time)
    {
        is_transition = true;
        yield return new WaitForSeconds(trans_time);
        is_transition = false;
    }
    IEnumerator LateChangeFlag(float _time)
    {
        yield return new WaitForSeconds(_time);
        is_punch = true;
    }
}
