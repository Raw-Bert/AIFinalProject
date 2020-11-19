using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour
{
    public float turnSpeed = 100f;
    public float moveSpeed = 5f;
    Rigidbody rb;
    float moveHorizontal;
    float moveVertical;
    //Vector3 movement;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() 
    {
        moveHorizontal = Input.GetAxisRaw ("Horizontal");
        moveVertical = Input.GetAxisRaw ("Vertical");
        
    }

    void FixedUpdate () 
    {

        Vector3 moveDirection = Vector3.zero;
 
        moveDirection += Camera.main.transform.up * Input.GetAxis("Vertical");
        moveDirection += Camera.main.transform.right * Input.GetAxis("Horizontal");
        moveDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(moveDirection);
        transform.Rotate( new Vector3(-90,0,0));
        
         
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);


        transform.Translate (movement * moveSpeed * Time.deltaTime, Space.World);
    }
}