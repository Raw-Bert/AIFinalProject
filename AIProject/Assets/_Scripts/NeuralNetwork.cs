using System.Collections.Generic;
using System;

public class NeuralNetwork : IComparable<NeuralNetwork>
{
    int[] layers;
    float[][] neurons;
    float[][][] weight;
    float adaptation;

    public NeuralNetwork() { }
    
    public NeuralNetwork(int[] _layers)
    {
        this.layers = new int[_layers.Length];
        for(int i = 0; i < _layers.Length; i++)
        {
            this.layers[i] = _layers[i];
        }

        InitiateNeurons();
        InitiateWeights();
    }

    //public NeuralNetwork(NeuralNetwork _inheritNetwork)
    //{
    //    this.layers = new int[_inheritNetwork.layers.Length];
    //    for(int i = 0; i < _inheritNetwork.layers.Length; i++)
    //    {
    //        this.layers[i] = _inheritNetwork.layers[i];
    //    }

    //    InitiateNeurons();
    //    InitiateWeights();
    //    InheritWeight(_inheritNetwork.weight);
    //}

    void InitiateNeurons()
    {
        List<float[]> listNeurons = new List<float[]>();

        for(int i = 0; i < layers.Length; i++)
        {
            listNeurons.Add(new float[layers[i]]);
        }

        neurons = listNeurons.ToArray();
    }

    void InitiateWeights()
    {
        List<float[][]> listWeight = new List<float[][]>();

        for(int i = 1; i < layers.Length; i++)
        {
            List<float[]> layerWeight = new List<float[]>();

            int preLayerNeurons = layers[i - 1];

            for (int j = 0; j < neurons[i].Length; j++)
            {
                float[] thisNeuronWeights = new float[preLayerNeurons];

                for(int k = 0; k < thisNeuronWeights.Length; k++)
                {
                    /////////////////////////////////////////////////////////////// ramdom weight why not 0 to 1?1to-1?
                    //thisNeuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);
                    thisNeuronWeights[k] = UnityEngine.Random.Range(-1f, 1f);
                }

                layerWeight.Add(thisNeuronWeights);
            }
            listWeight.Add(layerWeight.ToArray());
        }
        weight = listWeight.ToArray();
    }

    public float[]  FeedForwardProcess(float[] _inputs)
    {
        // assign value to first layer
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
                float v = 0;

                for (int k = 0; k < neurons[i-1].Length; k++)
                {
                    // plus this neuron's weight mutiplly it's value
                    v += weight[i - 1][j][k] * neurons[i - 1][k];
                }

                ///////////////////////////////////////// Thanh(1, -1) Activation function consider switch to sigmoid(0,1)
                //neurons[i][j] = (float)Math.Tanh(v);
                neurons[i][j] = SigmoidFunction(v);
                neurons[i][j] -= 0.5f;
            }
        }

        // return the last layer value after calculation
        return neurons[neurons.Length - 1];
    }

    float SigmoidFunction(float _value)
    {
        if (_value > 10)
            return 1.0f;
        else if (_value < -10)
            return 0.0f;
        else
            return 1.0f / (1.0f + (float)Math.Exp(_value));
    }

    public void Mutation()
    {
        for(int i = 0; i< weight.Length; i++)
        {
            for (int j = 0; j < weight[i].Length; j++)
            {
                for(int k = 0; k <weight[i][j].Length; k++)
                {
                    float thisWeight = weight[i][j][k];

                    ///////////////////////////////rihgt now is just slightly mutate with random chooice
                    float randFloat = UnityEngine.Random.Range(0f, 100f);

                    if (randFloat < 30f)
                    {
                        thisWeight += UnityEngine.Random.Range(2f, -2f);
                    }

                    //if (randFloat <= 2f)
                    //    thisWeight *= -1f;
                    //else if (randFloat <= 4f)
                    //    thisWeight = UnityEngine.Random.Range(-0.5f, 0.5f);
                    //else if (randFloat <= 6f)
                    //    thisWeight *= UnityEngine.Random.Range(0f, 1f) + 1f;
                    //else if (randFloat <= 8f)
                    //    thisWeight *= UnityEngine.Random.Range(0f, 1f);

                    weight[i][j][k] = thisWeight;
                }
            } 
        }
    }

    public void InheritWeight(float[][][] _oldWeight)
    {
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

    //public NeuralNetwork(NeuralNetwork _parent1, NeuralNetwork _parent2)
    //{
    //    this.layers = new int[_parent1.layers.Length];
    //    for (int i = 0; i < _parent1.layers.Length; i++)
    //    {
    //        this.layers[i] = _parent1.layers[i];
    //    }

    //    InitiateNeurons();
    //    InitiateWeights();
    //    SwapWeight(_parent1, _parent2);
    //}

    public void SwapWeight(NeuralNetwork _p1, NeuralNetwork _p2)
    {
        for (int i = 0; i < weight.Length; i++)
        {
            for (int j = 0; j < weight[i].Length; j++)
            {
                for (int k = 0; k < weight[i][j].Length; k++)
                {
                    float thisWeight = weight[i][j][k];

                    float randFloat = UnityEngine.Random.Range(0f, 100f);

                    if (randFloat < 65)
                        weight[i][j][k] = _p1.weight[i][j][k];
                    else
                        weight[i][j][k] = _p2.weight[i][j][k];
                }
            }
        }
    }

    //public void IncreaseAdaptation(float _adapt)
    //{
    //    adaptation += _adapt;
    //}

    public void SetAdaptation(float _adapt)
    {
        adaptation = _adapt;
    }

    //public float GetAdaptation()
    //{
    //    return adaptation;
    //}

    public float[][][] getWeight()
    {
        return weight;
    }

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
        //return (int)(adaptation - _otherNetwork.adaptation);

    }
}
