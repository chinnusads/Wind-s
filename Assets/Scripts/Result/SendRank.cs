using System.Collections;
using System.Collections.Generic;
using Nissensai2022.Runtime;
using UnityEngine;

public class SendRank : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        System.Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private Joycon.Button? m_pressedButtonL;
    private Joycon.Button? m_pressedButtonR;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    public static bool jump;

    private ResultRank rank;
    void Start()
    {
        rank = ResultRank.A;

        m_joycons = JoyconManager.Instance.j;
        if (m_joycons == null || m_joycons.Count <= 0)
            return;
        m_joyconL = m_joycons.Find(c => c.isLeft);
        m_joyconR = m_joycons.Find(c => !c.isLeft);
        jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        JoyconInput();
        if (jump)
        {
            Nissensai.SendResult(rank);
        }
    }

    void JoyconInput()
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
    }
}
