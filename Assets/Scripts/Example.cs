using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Example : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    private Joycon.Button? m_pressedButtonL;
    private Joycon.Button? m_pressedButtonR;

    public bool startCounting;
    public float maxGyroX, minGyroX, maxGyroY, minGyroY, maxGyroZ, minGyroZ;
    public float averageGyroX, averageGyroY, averageGyroZ;
    public float maxAccelX, minAccelX, maxAccelY, minAccelY, maxAccelZ, minAccelZ;
    public float averageAccelX, averageAccelY, averageAccelZ;
    public float distanceX,distanceY,distanceZ;
    float totalGyroX, totalGyroY, totalGyroZ;
    float totalAccelX, totalAccelY, totalAccelZ;
    float countingTime;

    private void Start()
    {
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);
        m_joyconR = m_joycons.Find(c => !c.isLeft);

        startCounting = false;
        totalGyroX = 0f;
        totalGyroY = 0f;
        totalGyroZ = 0f;
        totalAccelX = 0f;
        totalAccelY = 0f;
        totalAccelZ = 0f;
    }

    private void Update()
    {
        m_pressedButtonL = null;
        m_pressedButtonR = null;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        foreach (var button in m_buttons)
        {
            if (m_joyconL.GetButton(button))
            {
                m_pressedButtonL = button;
            }
            if (m_joyconR.GetButton(button))
            {
                m_pressedButtonR = button;
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_joyconL.SetRumble(160, 320, 0.6f, 200);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            m_joyconR.SetRumble(160, 320, 0.6f, 200);
        }

        KeyboardControl();
        Reset();
        Counting();
        
    }

    void Counting()
    {
        if (startCounting)
        {
            foreach (var joycon in m_joycons)
            {
                if(!joycon.isLeft)
                {
                    var joyconR = joycon;
                    Vector3 gyro = joyconR.GetGyro();
                    Vector3 accel = joyconR.GetAccel();
                    distanceX += gyro.x * Time.deltaTime;
                    distanceY += gyro.y * Time.deltaTime;
                    distanceZ += gyro.z * Time.deltaTime;
                }
            }
            countingTime += Time.deltaTime;
        }
        else
        {
            if (countingTime != 0f)
            {
                countingTime = 0f;
            }
        }
    }

    void KeyboardControl()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            startCounting = !startCounting;
            if (startCounting)
            {
                Debug.Log("Start");
            }
            else
            {
                Debug.Log("Stop");
            }
        }
    }

    void Reset()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            maxAccelX = 0;
            maxAccelY = 0;
            maxAccelZ = 0;
            maxGyroX = 0;
            maxGyroY = 0;
            maxGyroZ = 0;
            minAccelX = 0;
            minAccelY = 0;
            minAccelZ = 0;
            minGyroX = 0;
            minGyroY = 0;
            minGyroZ = 0;
            totalGyroX = 0f;
            totalGyroY = 0f;
            totalGyroZ = 0f;
            totalAccelX = 0f;
            totalAccelY = 0f;
            totalAccelZ = 0f;
            distanceX = 0f;
            distanceY = 0f;
            distanceZ = 0f;
        }
    }

    private void OnGUI()
    {
        var style = GUI.skin.GetStyle("label");
        style.fontSize = 24;

        if (m_joycons == null || m_joycons.Count <= 0)
        {
            GUILayout.Label("Joy-Con can't find");
            return;
        }

        if (!m_joycons.Any(c => c.isLeft))
        {
            GUILayout.Label("Joy-Con (L) can't find");
            return;
        }

        if (!m_joycons.Any(c => !c.isLeft))
        {
            GUILayout.Label("Joy-Con (R) can't find");
            return;
        }

        GUILayout.BeginHorizontal(GUILayout.Width(960));

        foreach (var joycon in m_joycons)
        {
            var isLeft = joycon.isLeft;
            var name = isLeft ? "Joy-Con (L)" : "Joy-Con (R)";
            var key = isLeft ? "Z KEY" : "X KEY";
            var button = isLeft ? m_pressedButtonL : m_pressedButtonR;
            var stick = joycon.GetStick();
            var gyro = joycon.GetGyro();
            var accel = joycon.GetAccel();
            var orientation = joycon.GetVector();

            GUILayout.BeginVertical(GUILayout.Width(480));
            GUILayout.Label(name);
            GUILayout.Label(key + "£ºshake");
            GUILayout.Label("your are pushing£º" + button);
            GUILayout.Label(string.Format("stick£º({0}, {1})", stick[0], stick[1]));
            GUILayout.Label("gyro£º" + gyro);
            GUILayout.Label("accel£º" + accel);
            GUILayout.Label("orientation£º" + orientation);
            GUILayout.EndVertical();
        }

        GUILayout.EndHorizontal();
    }
}