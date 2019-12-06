using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JumpAI.Assets.Script.AI;

public class NeuralManager : MonoBehaviour
{
    public static NeuralManager instance;
    public AIPlayerController AIPlayer;
    List<AIPlayerController> players = new List<AIPlayerController>();
    public NeuralGenetic neuralGenetic;
    public Text alivesText;
    public Text generationsText;
    public Text maxScoreText;
    public Text speedText;

    public int populations = 50;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        neuralGenetic = new NeuralGenetic(2, 2, 1, populations,0.2f, 0.1f, 0.5f);
        InitNeuralGenetic();
    }

    void InitNeuralGenetic()
    {
        neuralGenetic.PopulateGenerations();

        for (int i = 0; i < neuralGenetic.generations.Count; i++)
        {
            AIPlayerController player = Instantiate(AIPlayer);
            player.id = i;
            players.Add(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        alivesText.text = "ALIVES: " + Mathf.RoundToInt(GameManager.instance.alives);
        generationsText.text = "GENERATION: " + neuralGenetic.currentGeneration;

        float highScore = neuralGenetic.maxScore;

        if (GameManager.instance.score > neuralGenetic.maxScore)
        {
            highScore = GameManager.instance.score;
        }

        maxScoreText.text = "HIGH SCORE: " + Mathf.RoundToInt(highScore);
        speedText.text = "SPEED: " + Mathf.RoundToInt(GameManager.instance.speed);

        if (GameManager.instance.gameOver)
        {
            InitNeuralGenetic();
            GameManager.instance.gameOver = false;
            GameManager.instance.Reset(true);
        }
    }
}
