using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    // Start is called before the first frame update
    //Rigidbody rb;
    //float length = 2;
    //float width = 0.05f;
    //public LineRenderer lineRender;

    //public GameObject test;
    public LayerMask LayerSense;
    private const float MAX_DIST = 3f;
    private const float MIN_DIST = 0.01f;
    public GameObject markedPoint;
    //Vector3 rot;

    void Start()
    {
        //rb.GetComponent<Rigidbody>();
        //lineRender.enabled = true;
        //lineRender.SetWidth(width, width);
    }

    void Update()
    {
        //Vector3 rot2 = new Vector3 (lineRender.transform.rotation.x, lineRender.transform.rotation.y, lineRender.transform.rotation.z);
        //rot = new Vector3 (this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z) ;
        //CarRayCaster(this.transform.position, rot, length); //(rot + new Vector3(90,-90,0))
        //Debug.Log("Woot" + this.transform.position);
    }

    void FixedUpdate() 
    {
        //Raycast.maxDistance(999);
        Vector2 direction = new Vector2((markedPoint.transform.position.x - this.transform.position.x),(markedPoint.transform.position.z - this.transform.position.z));
        direction.Normalize();
        Debug.Log("Normalized Direction: " + direction);

        RaycastHit hit;
        //Vector3 dir = new Vector3 (direction.x, direction.y, direction.z);
        //RaycastHit2D hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.z), direction, MAX_DIST, LayerSense);
        //RaycastHit2D hit =  Physics2D.Raycast(this.transform.position, direction, MAX_DIST, LayerToSense);

       //Ray frontRay = new Ray(this.transform.position, direction);//);//new Vector3(this.transform.rotation.x,this.transform.rotation.y,this.transform.rotation.z
        
       //RaycastHit hit = Physics.Raycast(this.transform.position, direction, MAX_DIST, LayerSense);

       //markedPoint.transform.position = (Vector3) this.transform.position + direction * hit.distance;
        
        if(Physics.Raycast(this.transform.position, direction, out hit, MAX_DIST, LayerSense))
        {
            
            Debug.Log("Dist: " + hit.distance);
            if (hit.distance < MIN_DIST)
            {
                hit.distance = MIN_DIST;
                Debug.Log("Hit Distance Min: " + hit.distance);
            }
            //
            //direction = new Vector3(direction.x,0,direction.y);
            
            //markedPoint.transform.position = new Vector3 (markedPoint.transform.position.x, 0, markedPoint.transform.position.z);
            
        }
        //if(Physics.Raycast(this.transform.position, direction, out hit, MAX_DIST, LayerSense))
        
        if(hit.collider == null)
        {
            hit.distance = MAX_DIST;
            Debug.Log("Hit Distance null: " + hit.distance);
        }
            
        Vector3 tempDir = new Vector3(direction.x,0,direction.y);
        markedPoint.transform.position = (Vector3)this.transform.position + tempDir * hit.distance;
    }
    //void CarRayCaster(Vector3 pos, Vector3 dir, float len)
    //{
        //Ray ray = new Ray(pos, dir);
        //RaycastHit hit;
        //Vector3 endPos = pos + (length * dir);

        //Debug.Log(pos);
        //Debug.Log(dir);
        //Debug.Log(len);

        //if(Physics.Raycast(ray, out hit, length))
        //{
        //    endPos = hit.point;
        //}

        //lineRender.SetPosition( 0, pos );
        //lineRender.SetPosition( 1, endPos );
    //}
}
