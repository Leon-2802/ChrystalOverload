using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class magNr : MonoBehaviour
{
    public Text magNumber;
    public bool empty = false;
    public int normieGunMags;
    float currentNumber;
    float startingNumber;
    void Start()
    {
        startingNumber = normieGunMags;
        currentNumber = startingNumber;
    }

    // Update is called once per frame
    public void decreaseNumber()
    {
        currentNumber -= 1;
        magNumber.text = currentNumber.ToString();
        if (currentNumber <= 0) {
            empty = true;
        }
    }
}
