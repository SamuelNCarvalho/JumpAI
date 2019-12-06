using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JumpAI.Assets.Script.AI
{
    public class NeuralGenetic
    {
        int inputLayer;
        int hiddenLayer;
        int outputLayer;
        int population;
        float elitism;
        float mutationRate;
        float mutationRange;
        public List<Generation> generations = new List<Generation>();
        Generation betterOfGeneration;
        public int currentGeneration = 0;
        public float totalScore = 0;
        public float maxScore = 0;

        public NeuralGenetic(int input, int hidden, int output, int populationSize, float elitismSize, float mutationRateSize,float mutationRangeSize)
        {
            inputLayer = input;
            hiddenLayer = hidden;
            outputLayer = output;
            population = populationSize;
            elitism = elitismSize;
            mutationRate = mutationRateSize;
            mutationRange = mutationRangeSize;
        }

        public void PopulateGenerations()
        {
            if (generations.Count == 0)
            {
                FirstGeneration();
            }
            else
            {
                NextGeneration();
            }

            currentGeneration++;
        }

        void FirstGeneration()
        {
            for (int i = 0; i < population; i++)
            {
                generations.Add(new Generation(inputLayer, hiddenLayer, outputLayer, currentGeneration));
            }

            betterOfGeneration = generations[0];
        }

        void OrderGeneration()
        {
            generations = generations.OrderByDescending(g => g.score).ToList();
            betterOfGeneration = generations[0];
        }

        void SetTotalScore()
        {
            totalScore = 0;

            for (int i = 0; i < generations.Count; i++)
            {
                if (generations[i].score > maxScore)
                {
                    maxScore = generations[i].score;
                }

                totalScore += generations[i].score;
            }
        }

        void NextGeneration()
        {
            OrderGeneration();
            SetTotalScore();

            List<Generation> nextGeneration = new List<Generation>();

            for (int i = 0; i < Mathf.RoundToInt(elitism * population); i++)
            {
                if (nextGeneration.Count < population)
                {
                    nextGeneration.Add(generations[i]);
                }
            }

            int diff = (generations.Count - nextGeneration.Count);

            for (int i = 0; i < diff; i+= 2) {
                int parent1 = getParentIndex();
                int parent2 = getParentIndex();

                List<Generation> children = CrossOver(generations[parent1], generations[parent2]);

                nextGeneration.Add(children[0]);
                nextGeneration.Add(children[1]);
            }

            generations = nextGeneration;
            betterOfGeneration = generations[0];
        }

        List<Generation> CrossOver(Generation parent1, Generation parent2)
        {
            List<Generation> children =  new List<Generation>();

            children.Add(new Generation(inputLayer, hiddenLayer, outputLayer, currentGeneration));
            children.Add(new Generation(inputLayer, hiddenLayer, outputLayer, currentGeneration));

            for (int i = 0; i < inputLayer; i++)
            {
                for (int j = 0; j < hiddenLayer; j++)
                {
                    if (Random.Range(0.0f, 1.0f) <= 0.5)
                    {
                        double weights = children[0].neuron.weightsIH[i,j];
                        children[0].neuron.weightsIH[i,j] = children[1].neuron.weightsIH[i,j];
                        children[1].neuron.weightsIH[i,j] = weights;
                    }
                }
            }

            for (int i = 0; i < hiddenLayer; i++)
            {
                for (int j = 0; j < outputLayer; j++)
                {
                    if (Random.Range(0.0f, 1.0f) <= 0.5)
                    {
                        double weights = children[0].neuron.weightsHO[i,j];
                        children[0].neuron.weightsHO[i,j] = children[1].neuron.weightsHO[i,j];
                        children[1].neuron.weightsHO[i,j] = weights;
                    }
                }
            }

            for (int i = 0; i < inputLayer; i++)
            {
                for (int j = 0; j < hiddenLayer; j++)
                {
                    if (Random.Range(0.0f, 1.0f) <= mutationRate) {
                        children[0].neuron.weightsIH[i,j] = Random.Range(0.0f, 1.0f) * mutationRange * 2 - mutationRange;
                    }

                    if (Random.Range(0.0f, 1.0f) <= mutationRate) {
                        children[1].neuron.weightsIH[i,j] = Random.Range(0.0f, 1.0f) * mutationRange * 2 - mutationRange;
                    }
                }
            }

            for (int i = 0; i < hiddenLayer; i++)
            {
                for (int j = 0; j < outputLayer; j++)
                {
                    if (Random.Range(0.0f, 1.0f) <= mutationRate) {
                        children[0].neuron.weightsHO[i,j] = Random.Range(0.0f, 1.0f) * mutationRange * 2 - mutationRange;
                    }

                    if (Random.Range(0.0f, 1.0f) <= mutationRate) {
                        children[1].neuron.weightsHO[i,j] = Random.Range(0.0f, 1.0f) * mutationRange * 2 - mutationRange;
                    }
                }
            }

            return children;
        }

        int getParentIndex()
        {
            int index = -1;

            float randomScoreSorted = Random.Range(0.0f, 1.0f) * totalScore;
            float scoreSum = 0;

            int i = 0;
            while (i < generations.Count && scoreSum < randomScoreSorted)
            {
                scoreSum += generations[i].score;
                index++;
                i++;
            }

            return index;
        }
    }
}