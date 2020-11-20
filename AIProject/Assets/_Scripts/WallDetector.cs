using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<CarDrive>().network.SetAdaptation(Vector3.Distance(other.gameObject.transform.position, other.gameObject.GetComponent<CarDrive>().endPoint.transform.position));
        other.gameObject.SetActive(false);
    }
}
