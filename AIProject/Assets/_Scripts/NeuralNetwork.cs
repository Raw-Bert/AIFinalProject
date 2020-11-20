// Names: Lillian Fan, Robert Andersen, Safa Nazir, Rowan Luckhurst
// Date: Nov 20, 2020

using System.Collections.Generic;
using System;

public class NeuralNetwork : IComparable<NeuralNetwork>
{
    // contain number of neurons in each layer
    int[] layers;
    // contain final result of each neurons in each layer
    float[][] neurons;
    // contain the weights of each neurons from previous layer wich associate to this neuron in each layer 
    float[][][] weight;
    // score of how good this neural netwoek performed
    float adaptation;

    // constrctor for create a empty NeuralNetwork class
    public NeuralNetwork() { }
    

    // initialize this neuralnetwork with a give layer structure
    public NeuralNetwork(int[] _layers)
    {
        // copy given layers to the layers in this class 
        this.layers = new int[_layers.Length];
        for(int i = 0; i < _layers.Length; i++)
        {
            this.layers[i] = _layers[i];
        }

        // initialize neurons and weights
        InitiateNeurons();
        InitiateWeights();
    }

    // initialize neurons matrix
    void InitiateNeurons()
    {
        // create a list to help initialized neurons
        List<float[]> listNeurons = new List<float[]>();

        // loop through each layer and create neurons' array based based on the layer's value
        // the length of the array will equal to the layer's value
        for(int i = 0; i < layers.Length; i++)
        {
            listNeurons.Add(new float[layers[i]]);
        }

        // conver the list to array and assign to neurons matrix
        neurons = listNeurons.ToArray();
    }

    // initialize weights matrix
    void InitiateWeights()
    {
        // create a list to help initialize weights
        List<float[][]> listWeight = new List<float[][]>();

        // loop through all previous neurons that associate to this neuron's weight
        // each neuron's weight will associate to all previous neurons
        for(int i = 1; i < layers.Length; i++)
        {
            // create a list to help save this layer's weights
            List<float[]> layerWeight = new List<float[]>();

            // get the number of how many neurons contain in previous layer
            int preLayerNeurons = layers[i - 1];

            // loop all neurons in this layer
            for (int j = 0; j < neurons[i].Length; j++)
            {
                // create a array to help save all accociate neurons's weights in this neuron
                float[] thisNeuronWeights = new float[preLayerNeurons];

                // loop throught all previous layer's neurons
                for(int k = 0; k < thisNeuronWeights.Length; k++)
                {
                    // assign the random weight between -1 to 1 for all associate neurons
                    thisNeuronWeights[k] = UnityEngine.Random.Range(-1f, 1f);
                }

                // add neurons weights to layer wight
                layerWeight.Add(thisNeuronWeights);
            }
            // covert layer wight to array and add to the list of weights
            listWeight.Add(layerWeight.ToArray());
        }
        // convert the list weight to array and assign to weight matrix
        weight = listWeight.ToArray();
    }

    // output the result of this neural network with the input value
    public float[]  FeedForwardProcess(float[] _inputs)
    {
        // assign all values to first layer
        for(int i = 0; i< _inputs.Length; i++)
        {
            neurons[0][i] = _inputs[i];
        }

        // loop through rest layers
        for (int i = 1; i < layers.Length; i++)
        {
            // loop through all the neurons of this layer 
            for (int j = 0; j < neurons[i].Length; j++)
            {
                // help save the result of this neuron
                float v = 0;

                // loop though all associate neurons in precious layer
                for (int k = 0; k < neurons[i-1].Length; k++)
                {
                    // Add up this neuron's weight mutiplly the associate neuron's value to result 
                    v += weight[i - 1][j][k] * neurons[i - 1][k];
                }

                // apply the Sigmiod Functions as activition function and assign the result to this neuron
                // the result will between 0 to 1
                neurons[i][j] = SigmoidFunction(v);
                // minus 0.5 will hel conver the result to btween -0.5 to 0.5
                neurons[i][j] -= 0.5f;
            }
        }

        // return the last layer value after calculation
        return neurons[neurons.Length - 1];
    }

    // sigmoid function: y = 1 / (1+ e^x)
    float SigmoidFunction(float _value)
    {
        // since the value after 10 and -10 is very close to 1 and 0
        // return 1 when value lager than 10 and return 0 when calue smaller than -1 to save the runtime
        if (_value > 10)
            return 1.0f;
        else if (_value < -10)
            return 0.0f;
        else
            return 1.0f / (1.0f + (float)Math.Exp(_value));
    }

    // mutate the weight of neural network
    public void Mutation()
    {
        //loop through each weight in weights
        for(int i = 0; i< weight.Length; i++)
        {
            for (int j = 0; j < weight[i].Length; j++)
            {
                for(int k = 0; k <weight[i][j].Length; k++)
                {
                    // get the original weight
                    float thisWeight = weight[i][j][k];

                    // generate a random float between 0 to 100
                    float randFloat = UnityEngine.Random.Range(0f, 100f);

                    // add up the 30% wiehgt with a randome number between 2 to -2 
                    if (randFloat < 30f)
                    {
                        thisWeight += UnityEngine.Random.Range(2f, -2f);
                    }

                    // assign the mutated weight back to the weight
                    weight[i][j][k] = thisWeight;
                }
            } 
        }
    }

    // inherite the weight from other neural network's weight
    public void InheritWeight(float[][][] _oldWeight)
    {
        // loop through all weights and copy the new weight to this weight
        for (int i = 0; i < weight.Length; i++)
        {
            for (int j = 0; j < weight[i].Length; j++)
            {
                for (int k = 0; k < weight[i][j].Length; k++)
                {
                    weight[i][j][k] = _oldWeight[i][j][k];
                }
            }
        }
    }

    // inherit the weight from two other neural network 
    public void SwapWeight(NeuralNetwork _p1, NeuralNetwork _p2)
    {
        // loop through all the weights
        for (int i = 0; i < weight.Length; i++)
        {
            for (int j = 0; j < weight[i].Length; j++)
            {
                for (int k = 0; k < weight[i][j].Length; k++)
                {
                    // generate a randome float between 1 to 100
                    float randFloat = UnityEngine.Random.Range(0f, 100f);

                    // it hve 65% pacent to copy the first parent's weight and 35% with the second parent
                    if (randFloat < 65)
                        weight[i][j][k] = _p1.weight[i][j][k];
                    else
                        weight[i][j][k] = _p2.weight[i][j][k];
                }
            }
        }
    }

    // set the adaptation value
    public void SetAdaptation(float _adapt)
    {
        adaptation = _adapt;
    }

    // get the weight matrix of this neural network
    public float[][][] getWeight()
    {
        return weight;
    }

    // compare two neural netwoeks based on their adaptation
    // overrite the .Sort() function
    public int CompareTo(NeuralNetwork _otherNetwork)
    {
        if (_otherNetwork == null)
            return 1;
        else if (adaptation > _otherNetwork.adaptation)
            return 1;
        else if (adaptation < _otherNetwork.adaptation)
            return -1;
        else
            return 0;
    }
}