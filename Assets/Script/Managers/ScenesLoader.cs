using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScenesLoader : MonoBehaviour
{
    void Awake()
    {
        transform.Find("PlayerButton").GetComponent<Button>().onClick.AddListener(() => {
            Loader.Load(Loader.Scene.Player);
        });

        transform.Find("AIButton").GetComponent<Button>().onClick.AddListener(() => {
            Loader.Load(Loader.Scene.NeuralGenetic);
        });
    }
}
