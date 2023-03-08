using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject playerLives;
    // Start is called before the first frame update
    
    public void PlayGame()
    {
        Instantiate(playerLives);
        //Makes it so once the button is pressed, it will play the next scene in order
        //make sure to edit or change the build index around for this to work!!!
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }


}
