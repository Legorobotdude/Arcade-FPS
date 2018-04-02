using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    [SerializeField] private Transform player;
    private float yPos;

    void Start()
    {
        yPos = transform.position.y;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }


    // Update is called once per frame
    void LateUpdate ()
    {
        transform.position = new Vector3(player.position.x, yPos, player.position.z);
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y,0);
	}
}
