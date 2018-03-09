using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBob : MonoBehaviour
{

    [SerializeField]
    private float bobSpeed = 1f;
    [SerializeField]
    private float bobDistance = 1f;
    
    private Transform gun;

    private float horizontal, vertical, timer, waveSlice;
    private Vector3 midPoint;

    void Start()
    {
        gun = gameObject.transform;
        midPoint = gun.localPosition;

    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 localPosition = gun.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveSlice = Mathf.Sin(timer);
            timer = timer + bobSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }
        if (waveSlice != 0)
        {
            float translateChange = waveSlice * bobDistance;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            localPosition.y = midPoint.y + translateChange;
            localPosition.x = midPoint.x + translateChange * 2;
        }
        else
        {
            localPosition.y = midPoint.y;
            localPosition.x = midPoint.x;
        }

        gun.localPosition = localPosition;
    }
}