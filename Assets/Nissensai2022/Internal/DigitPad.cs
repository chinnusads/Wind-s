using System;
using System.Collections;
using System.Collections.Generic;
using Nissensai2022.Internal;
using UnityEngine;
using UnityEngine.UI;
using Logger=Nissensai2022.Runtime.Logger;

public class DigitPad : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    

    private void EnterDigit(int num)
    {
        inputField.text += num;
    }
    
    public void ButtonEnter()
    {
        try
        {
            StartCoroutine(SystemStatusManager.Instance.SendStart(Int32.Parse(inputField.text))); 
        }
        catch (Exception e)
        {
            inputField.ActivateInputField();
            Logger.Warn(e.Message);
            inputField.text = "";
        }
    }

    public void ButtonClear()
    {
        inputField.text = "";
    }
    
    public void Button0()
    {
        EnterDigit(0);
    }
    
    public void Button1()
    {
        EnterDigit(1);
    }
    
    public void Button2()
    {
        EnterDigit(2);
    }
    
    public void Button3()
    {
        EnterDigit(3);
    }
    
    public void Button4()
    {
        EnterDigit(4);
    }
    
    public void Button5()
    {
        EnterDigit(5);
    }
    
    public void Button6()
    {
        EnterDigit(6);
    }

    public void Button7()
    {
        EnterDigit(7);
    }
    
    public void Button8()
    {
        EnterDigit(8);
    }
    
    public void Button9()
    {
        EnterDigit(9);
    }
}
