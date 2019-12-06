using UnityEngine;

namespace JumpAI.Assets.Script.AI
{
    public class Neuron
    {
        int inputLayer;
        int hiddenLayer;
        int outputLayer;

        public double[,] weightsIH;
        public double[,] weightsHO;

        public Neuron(int input, int hidden, int output)
        {
            inputLayer = input;
            hiddenLayer = hidden;
            outputLayer = output;
            weightsIH = this.generateRandomMatrix(input, hidden);
            weightsHO = this.generateRandomMatrix(hidden, output);
        }

        double[,] generateRandomMatrix(int rows, int cols)
        {
            double[,] matrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i,j] = (Random.Range(0.0f, 1.0f) * 2.0f) - 1.0f;
                }
            }

            return matrix;
        }
    }
}