using System.Collections;
using System.Collections.Generic;
using Nissensai2022.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class SendRank : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        System.Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private Joycon.Button? m_pressedButtonL;
    private Joycon.Button? m_pressedButtonR;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    public static bool buttonInput;
    private bool _resultSent = false;

    [SerializeField]private ResultRank rank;

    private float timer;
    public float maxTime;//ランク文字出現のタイミング
    private Text text;

    public AudioSource rankSE;
    public AudioSource sceneChangeSE;

    void Start()
    {
        text = GetComponent<Text>();
        text.enabled = false;

        m_joycons = JoyconManager.Instance.j;
        if (m_joycons == null || m_joycons.Count <= 0)
            return;
        m_joyconL = m_joycons.Find(c => c.isLeft);
        m_joyconR = m_joycons.Find(c => !c.isLeft);
        buttonInput = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreControl.TotalScore > 400)
        {
            rank = ResultRank.A;
        }
        else if (ScoreControl.TotalScore > 300)
        {
            rank = ResultRank.B;
        }
        else if (ScoreControl.TotalScore > 200)
        {
            rank = ResultRank.C;
        }
        else if (ScoreControl.TotalScore > 100)
        {
            rank = ResultRank.D;
        }
        else
        {
            rank = ResultRank.E;
        }
        text.text = rank.ToString();
        timer += Time.deltaTime;
        if ((timer > maxTime) && (!text.enabled))
        {
            rankSE.Play();
            text.enabled = true;
        }
        JoyconInput();
        if (buttonInput&&!_resultSent)
        {
            sceneChangeSE.Play();
            Nissensai.SendResult(rank);
            _resultSent = true;
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
                        buttonInput = true;
                        break;
                    }
                case 1:
                    {
                        buttonInput = true;
                        break;
                    }
                case 2:
                    {
                        buttonInput = true;
                        break;
                    }
                case 3:
                    {
                        buttonInput = true;
                        break;
                    }
                case 11:
                    {
                        buttonInput = true;
                        break;
                    }
                case 12:
                    {
                        buttonInput = true;
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
                        buttonInput = true;
                        break;
                    }
                case 1:
                    {
                        buttonInput = true;
                        break;
                    }
                case 2:
                    {
                        buttonInput = true;
                        break;
                    }
                case 3:
                    {
                        buttonInput = true;
                        break;
                    }
                case 11:
                    {
                        buttonInput = true;
                        break;
                    }
                case 12:
                    {
                        buttonInput = true;
                        break;
                    }
            }
        }
    }
}
