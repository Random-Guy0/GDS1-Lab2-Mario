using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int time = 5;
    int currentSceneIndex;

    [SerializeField] private TMP_Text livesText;
    private PlayerLives lives;
    
    private void Start()
    {
        lives = FindObjectOfType<PlayerLives>();
        livesText.SetText("x " + lives.GetLives());
        
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 1)
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
