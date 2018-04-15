using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {

    public float damage = 10f;
    public float range = 100f;
    
    public float fireRate = 15f;

    public int maxAmmo = 10;
    protected int currentAmmo;
    public float reloadTime = 1f;
    protected bool isReloading = false;

    [SerializeField] Animator anim;

    [SerializeField] protected bool semiAutoFire = true;
    [SerializeField] protected bool burstFire = true;
    [SerializeField] protected bool autoFire = true;

    public CameraShake cameraShake;
   
    [SerializeField][Header("0=Semi Auto, 1 = burst, 2 = auto")] protected int fireMode = 0;


    public GameObject impactEffect;


    public ParticleSystem muzzleFlash;

    protected float nextTimeToFire = 0f;
    [SerializeField] protected int burstAmount = 3;
    protected int burstCounter = 0;

    [SerializeField]float firingForce = 100f;

    protected AudioSource audioSource;
    [SerializeField]AudioClip fireSound;

    protected GameObject player;

    void Start()
    {
        currentAmmo = maxAmmo;
        //StartCoroutine(cameraShake.Shake(1f,0.15f));
        audioSource = GetComponent<AudioSource>();
        player = transform.root.gameObject;
    }

    void OnEnable()
    {
        isReloading = false;
        //anim.SetBool("Reloading",false);
    }



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

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if (currentAmmo <= 0 || ((Input.GetButton("Reload")&&currentAmmo!=maxAmmo)))
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && fireMode == 2)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            currentAmmo--;
            Shoot();
            ShootAbstract();
        }
        else if (Input.GetButtonDown("Fire1") && fireMode == 0)
        {
            currentAmmo--;
            Shoot();
            ShootAbstract();
        }
        else if (Input.GetButton("Fire1") && burstCounter < burstAmount && Time.time >= nextTimeToFire && fireMode == 1)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            currentAmmo--;
            Shoot();
           ShootAbstract();
            burstCounter++;
        }
        else if (Input.GetButtonUp("Fire1") && burstCounter >= burstAmount && fireMode == 1)
        {
            burstCounter = 0;
        }

        if (Input.GetButtonDown("ToggleFireMode") && !Input.GetButton("Fire1"))
        {
            ToggleFireMode();

        }

    }

    IEnumerator Reload()
    {
        isReloading = true;
        //Debug.Log("Reloading");
        if (anim != null)
            anim.SetTrigger("Reload");
        //anim.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        //anim.SetBool("Reloading", false);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    abstract protected void Shoot();

    void ShootAbstract()
    {
        audioSource.PlayOneShot(fireSound);
        player.GetComponent<Rigidbody>().AddForce(transform.forward*firingForce);
    }

}
