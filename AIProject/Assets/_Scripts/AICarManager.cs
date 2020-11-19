using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarManager : MonoBehaviour
{
    public GameObject car;
    public GameObject endPoint;
    public GameObject startPoint;
    public int generations = 0;

    int population = 20;
    int[] layersLevel = new int[] { 3, 5, 3, 1 };
    List<NeuralNetwork> networks;
    List<GameObject> cars;
    bool isAllDied = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAllDied)
        {
            if(generations == 0)
            {
                InitisteCarsNeuralNetworks();
            }
            else
            {
                networks.Sort();
                NeuralNetwork firstPlace = new NeuralNetwork();
                NeuralNetwork secondPlace = new NeuralNetwork();
                for(int i = 0; i < population; i++)
                {
                    if(i == 0)
                    {
                        firstPlace = networks[i];
                        networks[i].SetAdaptation(0f);
                    }
                    else if(i == 2)
                    {
                        secondPlace = networks[i];
                        networks[i].SetAdaptation(0f);
                    }
                    else if(i > 2 && i < 5)
                    {
                        networks[i].Mutation();
                        networks[i].SetAdaptation(0f);
                    }
                    else if(i > 4 && i < 10)
                    {
                        networks[i] = new NeuralNetwork(networks[i - 5]);
                        networks[i].Mutation();
                        networks[i].SetAdaptation(0f);
                    }
                    else
                    {
                        networks[i] = new NeuralNetwork(firstPlace, secondPlace);
                        networks[i].Mutation();
                        networks[i].SetAdaptation(0f);
                    }
                }
            }

            generations++;
            isAllDied = false;
            LoadCarsBodys();
        }

        if (!AllCarsActive())
            isAllDied = true;
    }

    bool AllCarsActive()
    {
        bool _allCarActive = false;
        for (int i = 0; i < cars.Count; i++)
        {
            if (cars[i].activeSelf)
                _allCarActive = true;
        }
        return _allCarActive;
    }

    void LoadCarsBodys()
    {
        if(cars != null)
        {
            for (int i = 0; i < cars.Count; i++)
            {
                cars[i].transform.position = startPoint.transform.position;
                cars[i].transform.rotation = startPoint.transform.rotation;
            }
        }
        else
        {
            cars = new List<GameObject>();
            for(int i = 0; i < population; i++)
            {
                GameObject tmpCar = Instantiate(car, startPoint.transform.position, startPoint.transform.rotation);
                cars.Add(tmpCar);
            }
        }
    }

    void InitisteCarsNeuralNetworks()
    {
        if (population != 20)
        {
            population = 20;
        }

        networks = new List<NeuralNetwork>();

        for(int i = 0; i< population; i++)
        {
            NeuralNetwork tmpNetwork = new NeuralNetwork(layersLevel);
            tmpNetwork.Mutation();
            networks.Add(tmpNetwork);
        }
    }
}
