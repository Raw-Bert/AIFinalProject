// Names: Lillian Fan, Robert Andersen, Safa Nazir, Rowan Luckhurst
// Date: Nov 20, 2020

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AICarManager : MonoBehaviour
{
    public int population = 1; // how many car will be instantiate
    public GameObject car; // car prefab
    public GameObject endPoint; // the goal where car aim to drive
    public GameObject startPoint; // the place to intantiate cars
    public Text genText; // text for showing generations

    int generations = 0; // int to save current generation
    int[] layersLevel = new int[] { 3, 4, 3, 1 }; // neural network layers structure
    List<NeuralNetwork> networks; // list of all neural network
    List<GameObject> cars; // list of all car initializeobjects
    bool isAllDied = true; // bool for check if all car died

    void Update()
    {
        // if all car died, restart the game
        if (isAllDied)
        {
            // if this is the first time start the game, initialize cars' neural network
            if(generations == 0)
            {
                InitCarsNeuralNetworks();
            }
            // if this is not the first time, make evolution for all neural network to help they learn 
            else
            {
                // sort all neural networks, from good performance to bad performance
                networks.Sort();

                // create variables to save the best and second best neural network
                NeuralNetwork firstPlace = new NeuralNetwork();
                NeuralNetwork secondPlace = new NeuralNetwork();
                
                // loop through all car's neural networks
                for(int i = 0; i < population; i++)
                {
                    // check is this the first one
                    if(i == 0)
                    {
                        // keep the weight same, assign it to the best nueral network, reset it's adaptation 
                        firstPlace = networks[i];
                        networks[i].SetAdaptation(0f);
                    }
                    // check is this the second one
                    else if (i == 1)
                    {
                        // keep the weight same, assign it to the second best nueral network, reset it's adaptation
                        secondPlace = networks[i];
                        networks[i].SetAdaptation(0f);
                    }
                    // check is this the 3 to 5 in the list
                    else if (i > 1 && i < 5)
                    {
                        // mutate the weights and reset the adaptation
                        networks[i].Mutation();
                        networks[i].SetAdaptation(0f);
                    }
                    // check is this the 5 to 10 in the list
                    else if (i > 4 && i < 10)
                    {
                        // copy the best neural network's weight, mutate the coped weight and rest the adaption
                        networks[i].InheritWeight(firstPlace.getWeight());
                        networks[i].Mutation();
                        networks[i].SetAdaptation(0f);
                    }
                    // check is this the 11 to 20 in the list
                    else
                    {
                        // inherit the weight by mix fiat and second, mutate the inherited weight and reset the adaptation
                        networks[i].SwapWeight(firstPlace,secondPlace);
                        networks[i].Mutation();
                        networks[i].SetAdaptation(0f);
                    }
                }
            }

            // increase the genration by one
            generations++;
            // set the generation text to new generation text
            genText.text = "Generation " + generations;
            // set the game all cars died to false
            isAllDied = false;
            // load or reactive all cars
            LoadCarsBodys();
        }

        // check if all cards died, set the bool to true
        if (!AllCarsActive())
            isAllDied = true;
    }

    // function to help check if all cars are active
    bool AllCarsActive()
    {
        // loop through all cars
        bool _allCarActive = false;
        for (int i = 0; i < cars.Count; i++)
        {
            //if any of them are active will return true
            if (cars[i].activeSelf)
                _allCarActive = true;
        }
        return _allCarActive;
    }

    // load or active all cars game object
    void LoadCarsBodys()
    {
        // if already are cars there
        if(cars != null)
        {
            // loop through all the cars
            for (int i = 0; i < cars.Count; i++)
            {
                // reset the position to start position
                cars[i].transform.position = startPoint.transform.position;
                // reset rotation to start rotation
                cars[i].transform.rotation = startPoint.transform.rotation;
                // set the car to active
                cars[i].SetActive(true);
            }
        }
        // if is the start of game
        else
        {
            // inite the cars
            cars = new List<GameObject>();
            // generate the cars amount same as population
            for(int i = 0; i < population; i++)
            {
                // instantiate a car model at start position with start rotation
                GameObject tmpCar = Instantiate(car, startPoint.transform.position, startPoint.transform.rotation);
                // assign the nueral network and end point to the car
                tmpCar.GetComponent<CarDrive>().Initiation(networks[i], endPoint);
                // add this car to cars list
                cars.Add(tmpCar);
            }
        }
    }

    // initialize cars neural network
    void InitCarsNeuralNetworks()
    {
        // inite the networks
        networks = new List<NeuralNetwork>();

        // generate the amount of neral networks same as the population
        for(int i = 0; i< population; i++)
        {
            // inite the neural network by using the layer sructure
            NeuralNetwork tmpNetwork = new NeuralNetwork(layersLevel);
            // muate this neural network
            tmpNetwork.Mutation();
            // add this nueral network to the neural networks list
            networks.Add(tmpNetwork);
        }
    }
}
