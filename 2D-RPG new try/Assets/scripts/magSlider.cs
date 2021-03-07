using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class magSlider : MonoBehaviour
{
    public Slider slider;
    public float normieGunMag;

    void Start() 
    {
        SetMaxBullets(normieGunMag);
    }
    public void SetMaxBullets(float bullets)
    {
        slider.maxValue = bullets;
        slider.value = bullets;
    }

    public void SetBulletNumber(float bullets)
    {
        slider.value = bullets;
    }

}
