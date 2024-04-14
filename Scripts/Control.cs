using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{


    float horizontal;
    float vertical;

   public float rotateHorizontal;
    public float rotateVertical;
    public float rotateZAxis;

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the GameObject
        rb = GetComponent<Rigidbody>();
    }

    public float moveSpeed;
    public float rotationSpeed;

    void Update()
    {

      


        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
       // Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Apply movement to the Rigidbody
        //  transform.Translate(gameObject.transform.forward * moveSpeed * Time.deltaTime);

  

        // Apply movement to the Rigidbody velocity
        rb.velocity = gameObject.transform.forward * moveSpeed;

        // Get input for rotation
         //rotateHorizontal = Input.GetAxis("RotateHorizontal");
         //rotateVertical = Input.GetAxis("RotateVertical");
         //rotateZAxis = Input.GetAxis("rotateZAxis");

        // Calculate rotation angles
        float yaw = rotateHorizontal * rotationSpeed;
        float pitch = -rotateVertical * rotationSpeed;
        float roll = -rotateZAxis * rotationSpeed;
        // Create a rotation based on the calculated angles
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(pitch, yaw, roll) * Time.deltaTime);

        // Apply rotation to the Rigidbody rotation
        rb.MoveRotation(rb.rotation * deltaRotation);

    }





}







