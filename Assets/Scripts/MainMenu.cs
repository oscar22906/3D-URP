using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] AudioSource buttonPressSound;
    public Animator animator;

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
    public void Settings()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayGame()
    {
        Invoke("LoadGame", 2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
