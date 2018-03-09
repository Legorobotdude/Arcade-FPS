using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRaycast : Gun {

  public Camera fpsCam;

    public float impactForce = 100f;

    // Use this for initialization
    void Start () {
		
	}



    protected override void Shoot()
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
