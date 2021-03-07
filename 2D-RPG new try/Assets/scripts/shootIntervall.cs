using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shootIntervall : MonoBehaviour
{
    public Slider slider;
    public float intervall;

    void Start() 
    {
        SetMaxTime(intervall);
    }
    public void SetMaxTime(float intervall)
    {
        slider.maxValue = intervall;
        slider.value = intervall;
    }

    public void SetCurrentTime(float interval)
    {
        slider.value = interval;
    }
}
