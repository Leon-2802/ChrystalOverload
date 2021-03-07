using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addNewCoin : MonoBehaviour
{
    public Text coinNr;
    public int coins;
    void Start()
    {
        coins = 0;
    }

    public void addUp()
    {
        coins++;
        coinNr.text = coins.ToString();
    }
}
