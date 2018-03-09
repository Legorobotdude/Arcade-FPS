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
	

    protected override void Shoot()
    {
        GameObject laser = Instantiate(laserPrefab,muzzle.transform.position,muzzle.transform.rotation);
        laser.GetComponent<Rigidbody>().AddForce(-laser.transform.up * 100f);
    }
}
