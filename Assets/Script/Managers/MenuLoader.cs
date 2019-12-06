using UnityEngine;
using UnityEngine.UI;

public class MenuLoader : MonoBehaviour
{
    void Awake()
    {
        transform.Find("MainMenuButton").GetComponent<Button>().onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenu);
        });
    }
}
