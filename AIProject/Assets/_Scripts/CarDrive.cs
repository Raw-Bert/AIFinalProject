// Names: Lillian Fan, Robert Andersen, Safa Nazir, Rowan Luckhurst
// Date: Nov 20, 2020
// Neural Network help from: https://www.youtube.com/watch?v=Yq0SfuiOVYE&ab_channel=UnderpowerJet & https://arztsamuel.github.io/en/projects/unity/deepCars/deepCars.html

using UnityEngine;

public class CarDrive : MonoBehaviour
{
    public float turnSpeed = 100f; // car turning speed
    public float moveSpeed = 5f; // car move speed
    public GameObject[] rayObjects; // all the ray objects attached to car
    public NeuralNetwork network; // this car's nueral network 
    public GameObject endPoint; // the target postion of the car

    float moveHorizontal; // turing value
    RayCast[] rays; // list of rays
    Quaternion Rotation; // turning rotation

    // initialize this cars neural network and tarhet position
    public void Initiation(NeuralNetwork _network, GameObject _endPoint)
    {
        network = _network;
        endPoint = _endPoint;
    }

    void Start() {
        // init the rays based on the ray objects
        rays = new RayCast[rayObjects.Length];
        for (int i = 0; i < rayObjects.Length; i++)
            rays[i] = rayObjects[i].GetComponent<RayCast>();
    }

    void FixedUpdate () 
    {
        // get the distance of each rays
        float[] _raysInput = new float[rays.Length];
        for (int i = 0; i < rays.Length; i++)
            _raysInput[i] = rays[i].inputDistance;
        // input the rays distance to generate the output value
        float[] rotationOutput = network.FeedForwardProcess(_raysInput);
        // assign the output value to rotate value
        moveHorizontal = rotationOutput[0];

        // make sure the value is in between 1 to -1
        if (moveHorizontal > 1)
            moveHorizontal = 1;
        else if (moveHorizontal < -1)
            moveHorizontal = -1;

        // assign a new roration to the car based on the output value and turning speed
        Rotation = transform.rotation;
        Rotation *= Quaternion.AngleAxis((float)-moveHorizontal * turnSpeed * Time.deltaTime, new Vector3(0, -1, 0));
        transform.rotation = Rotation;

        // update the car's position based on the direction it point and the move speed
        Vector3 direction = new Vector3(1, 0, 0);
        direction = Rotation * direction;
        this.transform.position += direction * moveSpeed * Time.deltaTime;
    }
}