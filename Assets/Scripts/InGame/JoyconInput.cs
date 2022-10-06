using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JoyconInput : MonoBehaviour
{
    private static readonly Joycon.Button[] m_buttons =
        Enum.GetValues(typeof(Joycon.Button)) as Joycon.Button[];

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    //private Joycon.Button? m_pressedButtonL;
    //private Joycon.Button? m_pressedButtonR;

    public float distanceX, distanceY, distanceZ;
    public float countingTime;
    public static bool charge;

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
            Debug.Log(charge);
            Debug.Log(distanceY);
            countingTime = 0f;
            distanceY = 0f;
        }
        Vector3 gyro = m_joyconR.GetGyro();
        //Vector3 accel = m_joyconR.GetAccel();
        //distanceX += gyro.x * Time.deltaTime;
        distanceY += gyro.y * Time.deltaTime;
        //distanceZ += gyro.z * Time.deltaTime;
        countingTime += Time.deltaTime;
    }
}
