using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class messageCtrl : MonoBehaviour
{
    [SerializeField] private GameObject newTrack = null;
    [SerializeField] private GameObject key = null;
    [SerializeField] private GameObject dualBerettas = null;
    private GameObject current = null;
    private bool runTimer = false;
    private float time = 2f;

    private void Update() {
        if(runTimer) {
            time -= Time.deltaTime;
            if(time <= 0) {
                runTimer = false;
                current.SetActive(false);
                current = null;
                time = 2f;
            }
        }
    }
    
    public void showMessage(string type) {
        runTimer = true;
        switch(type) {
            case "track":
                newTrack.SetActive(true);
                current = newTrack;
                break;
            case "key":
                key.SetActive(true);
                current = key;
                break;
            case "dual":
                dualBerettas.SetActive(true);
                current = dualBerettas;
                break;
        }
    }
}
