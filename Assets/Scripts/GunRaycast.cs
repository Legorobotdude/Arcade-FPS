using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRaycast : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 100f;
    public float fireRate = 15f;
    public bool autoFire = false;

    public Camera fpsCam;

    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public ParticleSystem muzzleFlash;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Fire1") && Time.time>= nextTimeToFire && autoFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
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
