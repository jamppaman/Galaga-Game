using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button quitButton;
    void Start()
    {
        Button quitter = quitButton.GetComponent<Button>();
        quitter.onClick.AddListener(QuitTheGame);
    }
    public void QuitTheGame()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");
    }

}
