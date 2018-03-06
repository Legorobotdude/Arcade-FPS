using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float lookSensitivity = 3f;
    [SerializeField] private float jump_force = 500f;


    private PlayerMotor motor;
    private CapsuleCollider playerCollider;

    // Use this for initialization
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        playerCollider = GetComponent<CapsuleCollider>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate movement vel as 3d vector
        float _xMovement = Input.GetAxisRaw("Horizontal");
        float _zMovement = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMovement;
        Vector3 _movVertical = transform.forward * _zMovement;

        Vector3 _velocity;

        if (Input.GetKey(KeyCode.LeftShift) && is_grounded())
        {

            _velocity = (_movHorizontal + _movVertical).normalized * runSpeed;
        }
        else
        {
            _velocity = (_movHorizontal + _movVertical).normalized * speed;
        }

        motor.Move(_velocity);

        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        motor.Rotate(_rotation);

        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotation = _xRot * lookSensitivity;

        motor.RotateCamera(_cameraRotation);

        Vector3 _thrusterForce = Vector3.zero;



        //Jump
        if (Input.GetButtonDown("Jump") && is_grounded())
        {
            motor.handle_jump(jump_force);
        }

    }
    private bool is_grounded()
    {
        float distance_to_ground = playerCollider.bounds.extents.y;
        return Physics.Raycast(transform.position, -Vector3.up, distance_to_ground + 0.25f);
    }
}
