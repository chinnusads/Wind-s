using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    //joycon
    private static readonly Joycon.Button[] m_buttons =
        System.Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private Joycon.Button? m_pressedButtonL;
    private Joycon.Button? m_pressedButtonR;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    private float countingTime;
    public static bool joyconCharge;
    private bool joyconJump;
    public float rotationLimit;
    public float riseLimit;

    private float distanceX_R, distanceY_R, distanceX_L, distanceY_L;
    public float accelLimit;
    //jump
    public float jumpStartSpeed1, jumpStartSpeed2;
    public float gravity1, gravity2;
    public float jumpTime1, jumpTime2;//uptime only
    public static float jumpSpeed;
    private float nowJumpTime;
    public static bool isJump1, isJump2;
    public static int jumpState;// 0:on the ground; 1:first time jump; 2:second time jump;

    //anim
    private Animator anim;

    //se
    public AudioSource jump1SE, jump2SE, chargeSE;

    void Start()
    {
        //joycon
        m_joycons = JoyconManager.Instance.j;
        if (m_joycons == null || m_joycons.Count <= 0) return;
        m_joyconL = m_joycons.Find(c => c.isLeft);
        m_joyconR = m_joycons.Find(c => !c.isLeft);
        countingTime = 0;
        joyconCharge = false;
        joyconJump = false;
        //jump
        jumpSpeed = 0f;
        isJump2 = false;
        isJump1 = false;
        jumpState = 0;
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if ((CountDown.isGameStart) && (!CountDown.isTimeOut))
        {
            JoyconInput();
            Jump();
            if (jumpState > 0)
            {
                Move();
            }
            GroundReset();
        }
    }

    void GroundReset()
    {
        if (this.gameObject.transform.position.y <= 0f)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 0f, this.gameObject.transform.position.z);
            jumpState = 0;
            nowJumpTime = 0f;
            jumpSpeed = 0f;
            isJump2 = false;
            isJump1 = false;
            anim.SetBool("JumpUp", false);
            anim.SetBool("JumpDown", false);
        }
    }

    void JoyconInput()
    {
        if (m_joyconL != null)
        {
            Vector3 gyro_L = m_joyconL.GetGyro();
            distanceX_L += Mathf.Abs(gyro_L.x * Time.fixedDeltaTime);
            distanceY_L += Mathf.Abs(gyro_L.y * Time.fixedDeltaTime);
        }
        if (m_joyconR != null)
        {
            Vector3 gyro_R = m_joyconR.GetGyro();
            distanceX_R += Mathf.Abs(gyro_R.x * Time.fixedDeltaTime);
            distanceY_R += Mathf.Abs(gyro_R.y * Time.fixedDeltaTime);
        }
        BottonJump();
        JoyconRotate();
        if (joyconCharge)
        {
            if (!chargeSE.isPlaying)
            {
                chargeSE.Play();
            }
        }
        else
        {
            chargeSE.Stop();
        }
        countingTime += Time.fixedDeltaTime;
    }

    void JoyconRotate()
    {
        if (countingTime > 0.2f)
        {
            if (jumpSpeed == 0)
            {
                if ((distanceX_L + distanceY_L> rotationLimit) || (distanceX_R + distanceY_R > rotationLimit))
                {
                    joyconCharge = true;
                }
                else
                {
                    joyconCharge = false;
                }
            }
            JoyconReset();
        }
    }
    
    void JoyconReset()
    {
        countingTime = 0f;
        distanceX_L = 0f;
        distanceY_L = 0f;
        distanceX_R = 0f;
        distanceY_R = 0f;
    }

    void BottonJump()
    {
        m_pressedButtonL = null;
        m_pressedButtonR = null;
        foreach (var button in m_buttons)
        {
            if (m_joyconL != null)
            {
                if (m_joyconL.GetButtonDown(button))
                {
                    m_pressedButtonL = button;
                }
            }
            if (m_joyconR != null)
            {
                if (m_joyconR.GetButtonDown(button))
                {
                    m_pressedButtonR = button;
                }
            }
        }
        if (m_pressedButtonR != null)
        {
            switch ((int)m_pressedButtonR)
            {
                case 0:
                    {
                        joyconJump = true;
                        break;
                    }
                case 1:
                    {
                        joyconJump = true;
                        break;
                    }
                case 2:
                    {
                        joyconJump = true;
                        break;
                    }
                case 3:
                    {
                        joyconJump = true;
                        break;
                    }
                case 11:
                    {
                        joyconJump = true;
                        break;
                    }
                case 12:
                    {
                        joyconJump = true;
                        break;
                    }
            }
        }
        else if (m_pressedButtonL != null)
        {
            switch ((int)m_pressedButtonL)
            {
                case 0:
                    {
                        joyconJump = true;
                        break;
                    }
                case 1:
                    {
                        joyconJump = true;
                        break;
                    }
                case 2:
                    {
                        joyconJump = true;
                        break;
                    }
                case 3:
                    {
                        joyconJump = true;
                        break;
                    }
                case 11:
                    {
                        joyconJump = true;
                        break;
                    }
                case 12:
                    {
                        joyconJump = true;
                        break;
                    }
            }
        }
        if (joyconJump)
        {
            joyconCharge = false;
        }
    }

    void Jump()
    {
        if (jumpState == 0)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || joyconJump) && (GaugeControl.gaugeCharge > 0) && (GaugeControl.gaugeCharge < 3))
            {
                jumpState = 1;
                joyconCharge = false;
                if (GaugeControl.gaugeCharge == 1)
                {
                    isJump1 = true;
                    isJump2 = false;
                    GaugeControl.gaugeCount -= 0.2f;
                    jump1SE.Play();
                }
                if (GaugeControl.gaugeCharge == 2)
                {
                    isJump2 = true;
                    isJump1 = false;
                    GaugeControl.gaugeCount -= 0.6f;
                    jump2SE.Play();
                }
                nowJumpTime = 0f;
            }
        }
        else if (jumpState == 1)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || joyconJump) && (GaugeControl.gaugeCharge == 1))
            {
                joyconCharge = false;
                jumpState = 2;
                isJump1 = true;
                isJump2 = false;
                nowJumpTime = 0f;
                GaugeControl.gaugeCount -= 0.2f;
                jump1SE.Play();
            }
        }
        else if (jumpState == 2)
        {

        }
        joyconJump = false;
    }

    void Move()
    {
        if (isJump1)
        {
            if (nowJumpTime >= jumpTime1)
            {
                JumpDown(gravity1);
            }
            else
            {
                JumpUp(jumpStartSpeed1);
            }
        }
        else if (isJump2)
        {
            if (nowJumpTime < jumpTime2)
            {
                JumpUp(jumpStartSpeed2);
            }
            else
            {
                JumpDown(gravity2);
            }
        }
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y +  jumpSpeed * Time.deltaTime, this.gameObject.transform.position.z);
        nowJumpTime += Time.fixedDeltaTime;
    }

    void JumpUp(float jumpStartSpeed)
    {
        jumpSpeed = jumpStartSpeed;
        anim.SetBool("JumpUp", true);
        anim.SetBool("JumpDown", false);
    }

    void JumpDown(float gravity)
    {
        if (jumpSpeed > 0f)
        {
            jumpSpeed = 0f;
        }
        jumpSpeed -= gravity * Time.fixedDeltaTime;
        anim.SetBool("JumpDown", true);
        anim.SetBool("JumpUp", false);
    }

}
