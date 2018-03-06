using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 0f;

    public float laserForce = 100f;

    public GameObject muzzle;
    public ParticleSystem muzzleFlash;
    public GameObject laserPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }


	}

    private void Fire()
    {
        GameObject laser = Instantiate(laserPrefab,muzzle.transform.position,muzzle.transform.rotation);
        laser.GetComponent<Rigidbody>().AddForce(-laser.transform.up * 100f);
    }
}
