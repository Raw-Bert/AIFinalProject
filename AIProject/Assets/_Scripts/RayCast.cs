// This script is attached to the sensor objects
//This script is responsible for raycasting from the sensors on the cars towards the red cylinders in order to find out how far away walls are.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public float inputDistance;

    public LayerMask LayerSense;

    public GameObject markedPoint;

    private const float MAX_DIST = 5f;
    private const float MIN_DIST = 0.01f;


    //Fixed update called 30 times a second regardless of framerate
    void FixedUpdate() 
    {
        //Get the direction from the sensor to the marker. This is then normalized and converted back to a Vector3
        Vector2 direction = new Vector2((markedPoint.transform.position.x - this.transform.position.x),(markedPoint.transform.position.z - this.transform.position.z));
        direction.Normalize();
        Vector3 dirrr = new Vector3(direction.x,0, direction.y);
       
        //Creates Raycast which detects collisions
        RaycastHit hit;

        //Inputs the position and direction the ray is shot from as well as distance and layer of the obstacles
        if(Physics.Raycast(this.transform.position, dirrr, out hit, MAX_DIST, LayerSense))
        {
            //changes the distance of the raycast hit to minimum distance if its less than the minimum
            if (hit.distance < MIN_DIST)
            {
                hit.distance = MIN_DIST;
            }            
        }

        //If no hit detected, changes hit distance to the maximum distance
        if(hit.collider == null)
        {
            hit.distance = MAX_DIST;
        }

        
        inputDistance = hit.distance;

        //Sets new position for the marked point object
        Vector3 tempDir = new Vector3(direction.x,0,direction.y);
        markedPoint.transform.position = (Vector3)this.transform.position + tempDir * hit.distance;
    }
}
