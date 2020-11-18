using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    // Start is called before the first frame update
    //Rigidbody rb;
    float length = 2;
    float width = 0.05f;
    public LineRenderer lineRender;

    Vector3 rot;

    void Start()
    {
        //rb.GetComponent<Rigidbody>();
        lineRender.enabled = true;
        lineRender.SetWidth(width, width);
    }

    void Update()
    {
        rot = new Vector3 (this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z) ;
        CarRayCaster(this.transform.position, (rot + new Vector3(90,0,0)), length);
    }

    void CarRayCaster(Vector3 pos, Vector3 dir, float len)
    {
        Ray ray = new Ray(pos, dir);
        RaycastHit hit;
        Vector3 endPos = pos + (length * dir);

        Debug.Log(pos);
        Debug.Log(dir);
        Debug.Log(len);

        if(Physics.Raycast(ray, out hit, length))
        {
            endPos = hit.point;
        }

        lineRender.SetPosition( 0, pos );
        lineRender.SetPosition( 1, endPos );
    }
}
