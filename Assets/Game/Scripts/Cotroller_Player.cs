using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cotroller_Player : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        Vector3 movement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        Vector3 velocity = movement * moveSpeed;

        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

        if (movement.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);


        }

    }
}
