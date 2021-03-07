using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countUpTimer : MonoBehaviour
{
    public Text Timer;
    float currentTime = 0f;
    int storeMinute = 0;
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime < 10) {
                Timer.text = storeMinute.ToString() + ":0" + currentTime.ToString("0");
        }
        else if (currentTime > 9 && currentTime < 60) {
                Timer.text = storeMinute.ToString() + ":" + currentTime.ToString("0");
                Timer.text = storeMinute.ToString() + ":" + currentTime.ToString("0");
        }
        else if (currentTime >= 59) {
            currentTime = 0f;
            storeMinute += 1;
        }
        
    }
}
