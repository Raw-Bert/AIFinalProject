using System.Collections.Generic;
using System;

public class NeuralNetwork
{
    int[] layers;
    float[][] neurons;
    float[][][] weight;
    float adapt;

    public NeuralNetwork(int[] _layers)
    {
        this.layers = new int[_layers.Length];
        for(int i = 0; i < _layers.Length; i++)
        {
            this.layers[i] = _layers[i];
        }

        InitiateNerous();
        InitiateWeights();
    }

    public NeuralNetwork(NeuralNetwork _oldNetwork)
    {
        this.layers = new int[_oldNetwork.layers.Length];
        for(int i = 0; i < _oldNetwork.layers.Length; i++)
        {
            this.layers[i] = _oldNetwork.layers[i];
        }

        InitiateNerous();
        InitiateWeights();
        TransferWeight(_oldNetwork.weight);
    }

    void TransferWeight(float[][][] _oldWeight)
    {
        for (int i = 0; i < weight.Length; i++)
        {
            for(int j = 0; j < weight[i].Length; j++)
            {
                for(int k = 0; k< weight[i][j].Length; k++)
                {
                    weight[i][j][k] = _oldWeight[i][j][k];
                }
            }
        }
    }

    void InitiateNerous()
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

            int preLayerNerons = layers[i - 1];

            for (int j = 0; j < neurons[i].Length; j++)
            {
                float[] thisNeuronWeights = new float[preLayerNerons];

                for(int k = 0; k < thisNeuronWeights.Length; k++)
                {
                    thisNeuronWeights[k] = UnityEngine.Random.Range(-0.5f, 0.5f);
                }

                layerWeight.Add(thisNeuronWeights);
            }
            listWeight.Add(layerWeight.ToArray());
        }
        weight = listWeight.ToArray();
    }

    public float[]  neuronOutput(float[] inputs)
    {
        // assign value to first layer
        for(int i = 0; i< inputs.Length; i++)
        {
            neurons[0][i] = inputs[i];
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
                    v += weight[i - 1][j][k] * neurons[i - 1][k];
                }

                neurons[i][j] = (float)Math.Tanh(v);
            }
        }

        // return the last layer value after calculation
        return neurons[neurons.Length - 1];
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

                    float randFloat = UnityEngine.Random.Range(0f, 100f);

                    if (randFloat <= 2f)
                        thisWeight *= -1f;
                    else if (randFloat <= 4f)
                        thisWeight = UnityEngine.Random.Range(-0.5f, 0.5f);
                    else if (randFloat <= 6f)
                        thisWeight *= UnityEngine.Random.Range(0f, 1f) + 1f;
                    else if (randFloat <= 8f)
                        thisWeight*= UnityEngine.Random.Range(0f, 1f);

                    weight[i][j][k] = thisWeight;
                }
            } 
        }
    }

    public void AddAdapt(float _adapt)
    {
        adapt += _adapt;
    }

    public void SetAdapt(float _adapt)
    {
        adapt = _adapt;
    }

    public float GetAdapt()
    {
        return adapt;
    }

    public int AdaptValueComparesion(NeuralNetwork _otherNetwork)
    {
        if (_otherNetwork == null)
            return 1;
        else if (adapt > _otherNetwork.adapt)
            return 1;
        else if (adapt < _otherNetwork.adapt)
            return -1;
        else
            return 0;

    }
}
