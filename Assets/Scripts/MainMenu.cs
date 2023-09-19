using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    void Start()
    {
        Button starter = startButton.GetComponent<Button>();
        starter.onClick.AddListener(StartTheGame);
    }
    void StartTheGame()
    {
        SceneManager.LoadScene(sceneName: "SampleScene");
    }

}
