using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int time = 5;
    int currentSceneIndex;
    private void Start()
    {
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
