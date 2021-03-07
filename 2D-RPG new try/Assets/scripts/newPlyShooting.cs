using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newPlyShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public magSlider magScript;
    public int normieGunMag;
    public magNr magNumber;
    public shootIntervall intervallScr;
    public newPlyController plyController;
    public float bulletSpeed = 50;
    private bool canShoot = true;
    private bool startTimer= false;
    private float reload = 0;
    private float intervallTime;
    private bool intervallStart = false;

    void Start() 
    {
        intervallScr.SetMaxTime(0.6f);
    }
    void Update()
    {
        if(Input.GetButton("Fire1") && canShoot == true && plyController.ded == false && magNumber.empty == false) {
            StartCoroutine(Shoot());
        }
        else if(startTimer == true) {
            reload += 1 * Time.deltaTime;
            magScript.SetBulletNumber(reload);
        }
        else if(intervallTime <= 0.6 && intervallStart == true) {
            intervallTime += Time.deltaTime;
            intervallScr.SetCurrentTime(intervallTime);
        }
    }

    private IEnumerator Shoot()
    {
        // Time.timeScale = 0.1f;
        canShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);
        normieGunMag--;
        magScript.SetBulletNumber(normieGunMag);
        if(normieGunMag <= 0) {
            magScript.SetMaxBullets(3);
            magScript.SetBulletNumber(0);
            startTimer = true;
            yield return new WaitForSeconds(3.4f);
            magNumber.decreaseNumber();
            startTimer = false;
            reload = 0;
            normieGunMag = 25;
            magScript.SetMaxBullets(normieGunMag);
            canShoot = true;
            yield break;
        } else {
            intervallTime = 0f;
            intervallScr.SetMaxTime(0.6f);
            intervallScr.SetCurrentTime(intervallTime);
            intervallStart = true;
            yield return new WaitForSeconds(0.6f);
            intervallScr.SetMaxTime(1);
            canShoot = true;
        }
        // Time.timeScale = 1;
    }
}
