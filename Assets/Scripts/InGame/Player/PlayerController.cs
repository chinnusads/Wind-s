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

    private float distanceX_R, distanceY_R, distanceZ_R, distanceX_L, distanceY_L, distanceZ_L;
    private float accelX_L,accelY_L,accelZ_L,accelX_R,accelY_R,accelZ_R;
    private float averageAccel_L, averageAccel_R;
    public float accelLimit;
    private float averageGyro_L, averageGyro_R;
    private float afterChargeTime;
    public float pauseChargeTime;

    //jump
    public float jumpStartSpeed1, jumpStartSpeed2;
    public float gravity1, gravity2;
    public float jumpTime1, jumpTime2;//uptime only
    private float jumpSpeed;
    private float nowJumpTime;
    public static bool canJump;
    public static bool isJump1, isJump2;
    public static int jumpState;// 0:on the ground; 1:first time jump; 2:second time jump;


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
        canJump = true;
        jumpSpeed = 0f;
        isJump2 = false;
        isJump1 = false;
        jumpState = 0;
        accelX_L = 10f;
        accelX_R = 10f;
        accelY_L = 0f;
        accelZ_L = 0f;
        accelY_R = 0f;
        accelZ_R = 0f;
        averageGyro_L = 0f;
        averageGyro_R = 0f;
        afterChargeTime = 0f;
    }

    void FixedUpdate()
    {
        JoyconInput();
        Jump();
        if (jumpState > 0)
        {
            Move();
        }
        GroundReset();

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
        }
    }

    void JoyconInput()
    {
        if (m_joyconL != null)
        {
            Vector3 gyro_L = m_joyconL.GetGyro();
            distanceX_L += Mathf.Abs(gyro_L.x * Time.fixedDeltaTime);
            distanceY_L += Mathf.Abs(gyro_L.y * Time.fixedDeltaTime);
            distanceZ_L += Mathf.Abs(gyro_L.z * Time.fixedDeltaTime);
            averageGyro_L += (Mathf.Abs(gyro_L.x) + Mathf.Abs(gyro_L.y) + Mathf.Abs(gyro_L.z)) * Time.fixedDeltaTime;
            Vector3 acce_L = m_joyconL.GetAccel();
            averageAccel_L += (Mathf.Abs(acce_L.x - accelX_L) + Mathf.Abs(acce_L.y - accelY_L) + Mathf.Abs(acce_L.z - accelZ_L)) * Time.fixedDeltaTime;
        }
        if (m_joyconR != null)
        {
            Vector3 gyro_R = m_joyconR.GetGyro();
            distanceX_R += Mathf.Abs(gyro_R.x * Time.fixedDeltaTime);
            distanceY_R += Mathf.Abs(gyro_R.y * Time.fixedDeltaTime);
            distanceZ_R += Mathf.Abs(gyro_R.z * Time.fixedDeltaTime);
            averageGyro_R+= (Mathf.Abs(gyro_R.x) + Mathf.Abs(gyro_R.y) + Mathf.Abs(gyro_R.z)) * Time.fixedDeltaTime;
            Vector3 acce_R = m_joyconR.GetAccel();
            averageAccel_R += (Mathf.Abs(acce_R.x - accelX_R) + Mathf.Abs(acce_R.y - accelY_R) + Mathf.Abs(acce_R.z - accelZ_R)) * Time.fixedDeltaTime;
        }
        BottonJump();
        JoyconRotate();
        JoyconJump();
        countingTime += Time.fixedDeltaTime;
    }

    void JoyconJump()
    {
        if (countingTime > 0.2f) 
        {
            if (afterChargeTime >= pauseChargeTime)
            {
                //Debug.Log(averageGyro_R);
                //Debug.Log(averageAccel_R);
                if ((averageAccel_L < accelLimit) && (averageGyro_L > riseLimit) && (averageGyro_L < riseLimit + 0.8f))
                {
                    joyconJump = true;
                }
                if ((averageAccel_R < accelLimit) && (averageGyro_R > riseLimit) && (averageGyro_R < riseLimit + 0.8f))
                {   
                    joyconJump = true;
                }
            }
            if (joyconJump)
            {
                joyconCharge = false;
            }
            JoyconReset();
        }
    }

    void JoyconRotate()
    {
        if (countingTime > 0.2f)
        {
            if (jumpSpeed == 0)
            {
                if ((distanceX_L + distanceY_L + distanceZ_L > rotationLimit) || (distanceX_R + distanceY_R + distanceZ_R > rotationLimit))
                {
                    joyconCharge = true;
                    afterChargeTime = 0f;
                }
                else
                {
                    joyconCharge = false;
                }
            }
        }
        afterChargeTime += Time.fixedDeltaTime;
    }
    
    void JoyconReset()
    {
        countingTime = 0f;
        distanceX_L = 0f;
        distanceY_L = 0f;
        distanceZ_L = 0f;
        distanceX_R = 0f;
        distanceY_R = 0f;
        distanceZ_R = 0f;
        averageAccel_L = 0f;
        averageAccel_R = 0f;
        averageGyro_L = 0f;
        averageGyro_R = 0f;
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
    }

    void Jump()
    {
        if (jumpState == 0)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || (joyconJump)) && (GaugeControl.gaugeCharge > 0) && (GaugeControl.gaugeCharge < 3))
            {
                jumpState = 1;
                joyconCharge = false;
                if (GaugeControl.gaugeCharge == 1)
                {
                    isJump1 = true;
                    isJump2 = false;
                    GaugeControl.gaugeCount -= 0.2f;
                }
                if (GaugeControl.gaugeCharge == 2)
                {
                    isJump2 = true;
                    isJump1 = false;
                    GaugeControl.gaugeCount -= 0.6f;
                }
                nowJumpTime = 0f;

            }
        }
        else if (jumpState == 1)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || (joyconJump)) && (GaugeControl.gaugeCharge == 1))
            {
                joyconCharge = false;
                jumpState = 2;
                isJump1 = true;
                isJump2 = false;
                nowJumpTime = 0f;
                GaugeControl.gaugeCount -= 0.2f;
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
    }

    void JumpDown(float gravity)
    {
        if (jumpSpeed > 0f)
        {
            jumpSpeed = 0f;
        }
        jumpSpeed -= gravity * Time.fixedDeltaTime;
    }

}
