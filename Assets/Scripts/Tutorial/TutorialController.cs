using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class TutorialController : MonoBehaviour
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
    public static bool joyconBottun;
    public float rotationLimit;

    private float distanceX_R, distanceY_R, distanceX_L, distanceY_L;

    //anim
    private Animator anim;

    public AudioSource chargeSE;

    void Start()
    {
        //joycon
        m_joycons = JoyconManager.Instance.j;
        if (m_joycons == null || m_joycons.Count <= 0) return;
        m_joyconL = m_joycons.Find(c => c.isLeft);
        m_joyconR = m_joycons.Find(c => !c.isLeft);
        countingTime = 0;
        joyconCharge = false;
        joyconBottun = false;
    }

    void FixedUpdate()
    {
        
            JoyconInput();
     
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
        BottonDown();
        JoyconRotate();
        //joyconBottun();
        countingTime += Time.fixedDeltaTime;
    }

    void JoyconRotate()
    {
        if (countingTime > 0.2f)
        {
            if ((distanceX_L + distanceY_L > rotationLimit) || (distanceX_R + distanceY_R > rotationLimit))
            {
                joyconCharge = true;
            }
            else
            {
                joyconCharge = false;
            }
            JoyconReset();
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
        }
    }

    void JoyconReset()
    {
        countingTime = 0f;
        distanceX_L = 0f;
        distanceY_L = 0f;
        distanceX_R = 0f;
        distanceY_R = 0f;
        joyconBottun = false;
    }

    void BottonDown()
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
                        joyconBottun = true;
                        break;
                    }
                case 1:
                    {
                        joyconBottun = true;
                        break;
                    }
                case 2:
                    {
                        joyconBottun = true;
                        break;
                    }
                case 3:
                    {
                        joyconBottun = true;
                        break;
                    }
                case 11:
                    {
                        joyconBottun = true;
                        break;
                    }
                case 12:
                    {
                        joyconBottun = true;
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
                        joyconBottun = true;
                        break;
                    }
                case 1:
                    {
                        joyconBottun = true;
                        break;
                    }
                case 2:
                    {
                        joyconBottun = true;
                        break;
                    }
                case 3:
                    {
                        joyconBottun = true;
                        break;
                    }
                case 11:
                    {
                        joyconBottun = true;
                        break;
                    }
                case 12:
                    {
                        joyconBottun = true;
                        break;
                    }
            }
        }
    }

}
