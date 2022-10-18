using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JoyconInput : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private Joycon.Button? m_pressedButtonL;
    private Joycon.Button? m_pressedButtonR;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    public float distanceY_R, distanceY_L;
    public float countingTime;
    public static bool charge;
    public static bool jump;

    void Start()
    {
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);
        m_joyconR = m_joycons.Find(c => !c.isLeft);
        countingTime = 0;
        distanceY_L = 0f;
        distanceY_R = 0f;
        charge = false;
        jump = false;
    }

    void Update()
    {
        if (m_joyconL!=null)
        {
            Vector3 gyro_L = m_joyconL.GetGyro();
            distanceY_L += gyro_L.y * Time.deltaTime;
        }
        if (m_joyconR!=null)
        {
            Vector3 gyro_R = m_joyconR.GetGyro();
            distanceY_R += gyro_R.y * Time.deltaTime;
        }
        m_pressedButtonL = null;
        m_pressedButtonR = null;
        foreach (var button in m_buttons)
        {
            if (m_joyconL!=null)
            {
                if (m_joyconL.GetButtonDown(button))
                {
                    m_pressedButtonL = button;
                }
            }
            if (m_joyconR!=null)
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
                        jump = true;
                        break;
                    }
                case 1:
                    {
                        jump = true;
                        break;
                    }
                case 2:
                    {
                        jump = true;
                        break;
                    }
                case 3:
                    {
                        jump = true;
                        break;
                    }
                case 11:
                    {
                        jump = true;
                        break;
                    }
                case 12:
                    {
                        jump = true;
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
                        jump = true;
                        break;
                    }
                case 1:
                    {
                        jump = true;
                        break;
                    }
                case 2:
                    {
                        jump = true;
                        break;
                    }
                case 3:
                    {
                        jump = true;
                        break;
                    }
                case 11:
                    {
                        jump = true;
                        break;
                    }
                case 12:
                    {
                        jump = true;
                        break;
                    }
            }
        }
        if (jump)
        {
            charge = false;
        }
        else if ((countingTime > 0.2f) && (PlayerController.jumpState == 0))
        {
            if ((Mathf.Abs(distanceY_R) > 0.2f) || (Mathf.Abs(distanceY_L) > 0.2f))
            {
                charge = true;
            }
            countingTime = 0f;
            distanceY_L = 0f;
            distanceY_R = 0f;
        }
        jump = false;
        countingTime += Time.deltaTime;

    }
}
