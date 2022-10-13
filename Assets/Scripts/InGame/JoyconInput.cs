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

    public float distanceY;
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
        distanceY = 0;
        charge = false;
    }

    void Update()
    {
        m_pressedButtonL = null;
        m_pressedButtonR = null;
        foreach (var button in m_buttons)
        {
            /*if (m_joyconL.GetButton(button))
            {
                m_pressedButtonL = button;
            }*/
            if (m_joyconR.GetButton(button))
            {
                m_pressedButtonR = button;
            }
        }

        if (countingTime > 0.5f)
        {
            if (Mathf.Abs(distanceY) > 0.5f)
            {
                charge = true;
            }
            else
            {
                charge = false;
            }
            countingTime = 0f;
            distanceY = 0f;
        }
        Vector3 gyro = m_joyconR.GetGyro();
        distanceY += gyro.y * Time.deltaTime;
        countingTime += Time.deltaTime;
        jump = false;
        //Debug.Log((int)m_pressedButtonR);
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
        //Debug.Log(jump);


    }
}
