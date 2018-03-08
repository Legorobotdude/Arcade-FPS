using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProjectile : Gun {

   
    public float laserForce = 100f;

    public GameObject muzzle;
    
    public GameObject laserPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && fireMode == 2)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
            else if (Input.GetButtonDown("Fire1") && fireMode == 0)
            {
                Shoot();
            }
            else if (Input.GetButton("Fire1") && burstCounter < burstAmount && Time.time >= nextTimeToFire && fireMode == 1)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
                burstCounter++;
            }
            else if (Input.GetButtonUp("Fire1") && burstCounter >= burstAmount && fireMode == 1)
            {
                burstCounter = 0;
            }

            if (Input.GetButtonDown("ToggleFireMode"))
            {
                ToggleFireMode();
            }
        }


	}

    private void Shoot()
    {
        GameObject laser = Instantiate(laserPrefab,muzzle.transform.position,muzzle.transform.rotation);
        laser.GetComponent<Rigidbody>().AddForce(-laser.transform.up * 100f);
    }
}
