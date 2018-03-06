using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRaycast : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;

    public GameObject impactEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
	}

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {


            HealthManager target = hit.transform.GetComponent<HealthManager>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
