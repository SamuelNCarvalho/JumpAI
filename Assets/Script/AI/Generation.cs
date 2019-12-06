using System;
using UnityEngine;

namespace JumpAI.Assets.Script.AI
{
    public class Generation
    {
        int generation;
        public float score;
        int input;
        int hidden;
        int output;
        public Neuron neuron;

        public Generation(int iLayer, int hLayer, int oLayer, int gen = 0)
        {
            input = iLayer;
            hidden = hLayer;
            output = oLayer;
            generation = gen;
            score = 0;
            neuron = new Neuron(input, hidden, output);
        }

        public void SetScore(float value)
        {
            score = value;
        }

        public double[,] Compute(double[,] input)
        {
            double[,] hidden = Multiply(input, neuron.weightsIH);
            hidden = Sigmoid(hidden);

            double[,] output = Multiply(hidden, neuron.weightsHO);
            output = Sigmoid(output);

            return output;
        }

        double[,] Multiply(double[,] A, double[,] B)
        {
            int rA = A.GetLength(0);
            int cA = A.GetLength(1);
            int rB = B.GetLength(0);
            int cB = B.GetLength(1);
            double temp = 0;
            double[,] kHasil = new double[rA, cB];
            if (cA != rB)
            {
                Console.WriteLine("matrik can't be multiplied !!");
            }
            else
            {
                for (int i = 0; i < rA; i++)
                {
                    for (int j = 0; j < cB; j++)
                    {
                        temp = 0;
                        for (int k = 0; k < cA; k++)
                        {
                            temp += A[i, k] * B[k, j];
                        }
                        kHasil[i, j] = temp;
                    }
                }
            }

            return kHasil;
        }

        double[,] Sigmoid(double[,] matrix)
        {
            double[,] newMatrix = new double[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = (1 / (1 + Mathf.Exp((float)matrix[i, j])));
                }
            }

            return newMatrix;
        }
    }
}