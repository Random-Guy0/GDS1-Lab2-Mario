using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar : MonoBehaviour
{
    public Vector2 destination;
    public Vector2 position;
    public bool touch;
    public bool isMove = false;
    public Sprite emptyBlock;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        destination = new Vector2(transform.position.x, transform.position.y + 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (touch && !isMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * 1.5f);

        }
        if(transform.position.y == destination.y || isMove){
            isMove = true;
            transform.position = Vector2.MoveTowards(transform.position, position, Time.deltaTime * 2);
            if(transform.position.y == position.y)
            {
                GetComponent<SpriteRenderer>().sprite = emptyBlock;
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            touch = true;
        }
    }
}
