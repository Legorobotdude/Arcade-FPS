using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGrapple : MonoBehaviour {


    LineRenderer laserLine;
    public Transform startPoint;
    public Transform endPoint;
    public Camera fpsCam;
    public float impactForce = 100f;
    public float range = 100f;
    private Player player;
    private Vector3 lastPosition;
    private Rigidbody hitRigidbody;

    // Use this for initialization
    void Start () {
        laserLine = GetComponent<LineRenderer>();
        //laserLine.SetWidth(.2f, .2f);
        player = transform.root.GetComponent<Player>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButton("Fire2"))
            {
            //player.playerController.SetHitAttraction(hit.point);
            if (Input.GetButtonDown("Fire2"))
            {

            
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {


                

                if (hit.transform.GetComponent<Rigidbody>() != null)
                {
                    hit.transform.GetComponent<Rigidbody>().AddForce(hit.normal * impactForce);
                        hitRigidbody = hit.transform.GetComponent<Rigidbody>();

                }
                else
                    {
                        hitRigidbody = null;
                    }
                player.playerController.SetHitAttraction(hit.point);
                laserLine.enabled = true;
                laserLine.SetPosition(0, startPoint.position);
                laserLine.SetPosition(1, hit.point);
                lastPosition = hit.point;
               
            }
                else
                {
                    laserLine.enabled = false;
                }
            }
            else{
                laserLine.SetPosition(0, startPoint.position);
                if (hitRigidbody == null)
                {
                   
                    player.playerController.SetHitAttraction(lastPosition);
                }
                else
                {
                    
                    player.playerController.SetHitAttraction(hitRigidbody.position);
                    laserLine.SetPosition(1, hitRigidbody.position);
                }
                
            }
            
        }
        else
        {
            laserLine.enabled = false;
        }
    }
       
    }

