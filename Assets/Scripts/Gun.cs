using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 100f;


    public float fireRate = 15f;
    [SerializeField] protected bool semiAutoFire = true;
    [SerializeField] protected bool burstFire = true;
    [SerializeField]protected bool autoFire = true;
    
    
   
    [SerializeField][Header("0=Semi Auto, 1 = burst, 2 = auto")] protected int fireMode = 0;


    public GameObject impactEffect;


    public ParticleSystem muzzleFlash;

    protected float nextTimeToFire = 0f;
    [SerializeField] protected int burstAmount = 3;
    protected int burstCounter = 0;


    public void ToggleFireMode()
    {
        if (autoFire&&burstFire&&semiAutoFire)
        {
            if (fireMode == 0)
            {
                fireMode=1;
                burstCounter = 0;
            }
            else if(fireMode == 1)
            {
                fireMode = 2;
            }
            else
            {
                fireMode = 0;
            }
        }
        else if(semiAutoFire && autoFire)
        {
            if (fireMode == 0)
            {
                fireMode = 2;
            }
            else
            {
                fireMode = 0;
            }
        }
        else if (semiAutoFire && burstFire)
        {
            if (fireMode == 0)
            {
                fireMode = 1;
                burstCounter = 0;
            }
            else
            {
                fireMode = 0;
            }
        }
        else if (burstFire && autoFire)
        {
            if (fireMode == 1)
            {
                fireMode = 2;
            }
            else
            {
                fireMode = 1;
                burstCounter = 0;
            }
        }


    }

}
