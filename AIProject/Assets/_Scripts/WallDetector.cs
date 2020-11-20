// Names: Lillian Fan, Robert Andersen, Safa Nazir, Rowan Luckhurst
// Date: Nov 20, 2020

using UnityEngine;

public class WallDetector : MonoBehaviour
{
    // Check if any car's trigger enter wall's collider, the car will lose when they touch the wall
    private void OnTriggerEnter(Collider other)
    {
        // Set this car's adaptation to the distance between the car's posiiton to the end point position
        other.gameObject.GetComponent<CarDrive>().network.SetAdaptation(Vector3.Distance(other.gameObject.transform.position, other.gameObject.GetComponent<CarDrive>().endPoint.transform.position));
        // Set this car unactive
        other.gameObject.SetActive(false);
    }
}
