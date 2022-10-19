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
    public float distanceGyroX, distanceGyroY, distanceGyroZ;
    public float averageDistanceGyro;
    public float maxAccelX, minAccelX, maxAccelY, minAccelY, maxAccelZ, minAccelZ;
    public float averageAccelX, averageAccelY, averageAccelZ;
    public float averageDistanceAccel;
    //public float distanceAccelX, distanceAccelY, distanceAccelZ;
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
                if(joycon.isLeft)
                {
                    var joyconL = joycon;
                    Vector3 gyro = joyconL.GetGyro();
                    Vector3 accel = joyconL.GetAccel();
                    distanceGyroX += gyro.x * Time.deltaTime * 10;
                    distanceGyroY += gyro.y * Time.deltaTime * 10;
                    distanceGyroZ += gyro.z * Time.deltaTime * 10;
                    averageDistanceAccel += Mathf.Sqrt(accel.x * accel.x * 100 + accel.y * accel.y * 100 + accel.z * accel.z * 100);
                    averageDistanceGyro += Mathf.Sqrt(gyro.x * gyro.x * 100 + gyro.y * gyro.y * 100 + gyro.z * gyro.z * 100);
                    if (gyro.x > maxGyroX)
                    {
                        maxGyroX = gyro.x * 10;
                    }
                    if (gyro.y > maxGyroY)
                    {
                        maxGyroY = gyro.y * 10;
                    }
                    if (gyro.z > maxGyroZ)
                    {
                        maxGyroZ = gyro.z * 10;
                    }
                    if (accel.x > maxAccelX)
                    {
                        maxAccelX = accel.x * 10;
                    }
                    if (accel.y > maxAccelY)
                    {
                        maxAccelY = accel.y * 10;
                    }
                    if (accel.z > maxAccelZ)
                    {
                        maxAccelZ = accel.z * 10;
                    }
                    if (gyro.x < minGyroX)
                    {
                        minGyroX = gyro.x * 10;
                    }
                    if (gyro.y < minGyroY)
                    {
                        minGyroY = gyro.y * 10;
                    }
                    if (gyro.z < minGyroZ)
                    {
                        minGyroZ = gyro.z * 10;
                    }
                    if (accel.x < minAccelX)
                    {
                        minAccelX = accel.x * 10;
                    }
                    if (accel.y < minAccelY)
                    {
                        minAccelY = accel.y * 10;
                    }
                    if (accel.z < minAccelZ)
                    {
                        minAccelZ = accel.z * 10;
                    }
                    totalGyroX += gyro.x * 10;
                    totalGyroY += gyro.y * 10;
                    totalGyroZ += gyro.z * 10;
                    totalAccelX += accel.x * 10;
                    totalAccelY += accel.y * 10;
                    totalAccelZ += accel.z * 10;
                }
            }
            countingTime += 1f;
        }
        else
        {
            if (countingTime != 0f)
            {
                averageGyroX = totalGyroX / countingTime;
                averageGyroY = totalGyroY / countingTime;
                averageGyroZ = totalGyroZ / countingTime;
                averageAccelX = totalAccelX / countingTime;
                averageAccelY = totalAccelY / countingTime;
                averageAccelZ = totalAccelZ / countingTime;
                averageDistanceAccel = averageDistanceAccel / countingTime;
                averageDistanceGyro = averageDistanceGyro / countingTime;
                Debug.Log("averageGyro(" + "x:" + averageGyroX.ToString("f2") + "y:" + averageGyroY.ToString("f2") + "z:" + averageGyroZ.ToString("f2") + ")");
                Debug.Log("averageAccel(" + "x:" + averageAccelX.ToString("f2") + "y:" + averageAccelY.ToString("f2") + "z:" + averageAccelZ.ToString("f2") + ")");
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
            distanceGyroX = 0f;
            distanceGyroY = 0f;
            distanceGyroZ = 0f;
            averageAccelX = 0;
            averageAccelY = 0;
            averageAccelZ = 0;
            averageGyroX = 0;
            averageGyroY = 0;
            averageGyroZ = 0;
            averageDistanceAccel = 0;
            averageDistanceGyro = 0;
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
            var orientation = joycon.GetVector().eulerAngles;

            GUILayout.BeginVertical(GUILayout.Width(480));
            GUILayout.Label(name);
            GUILayout.Label(key + "shake");
            GUILayout.Label("your are pushing:" + button);
            GUILayout.Label(string.Format("stick:({0}, {1})", stick[0], stick[1]));
            GUILayout.Label("gyro:" + gyro * 10);
            GUILayout.Label("accel:" + accel * 10);
            GUILayout.Label("orientation:" + orientation);
            GUILayout.EndVertical();
        }

        GUILayout.EndHorizontal();
    }
}