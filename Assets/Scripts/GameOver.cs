using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] int time = 5;
    int currentSceneIndex;
    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 3)
        {
            StartCoroutine(WaitLoadingScreen());
        }
    }

    private IEnumerator WaitLoadingScreen()
    {
        yield return new WaitForSeconds(time);
        LoadNextScene();

    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
