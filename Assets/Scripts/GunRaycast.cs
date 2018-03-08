using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRaycast : Gun {

  

    public Camera fpsCam;

    

    

    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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

    private void Shoot()
    {
        if(muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {


            HealthManager target = hit.transform.GetComponent<HealthManager>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                hit.transform.GetComponent<Rigidbody>().AddForce(-hit.normal * impactForce);

            }

            //ToDo: Play impact sounds
            
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}
