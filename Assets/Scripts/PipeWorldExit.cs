using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeWorldExit : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player.transform.position = new Vector2(150.5f, 0.25f);
            camera.transform.position = new Vector3(150.5f, 2.45f, -10.0f);
        }
    }
}
