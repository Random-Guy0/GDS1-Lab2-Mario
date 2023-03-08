using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TtemMovement : MonoBehaviour
{
    public float speed;
    public LayerMask lm;
    public RaycastHit2D[] results;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = -speed;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*results = Physics2D.BoxCastAll(transform.position, transform.localScale, 0, new Vector2(speed, 0).normalized, 0.5f, lm);
        if (results.Length > 0)
        {
            speed = -speed;
        }*/
        if (collision.gameObject.CompareTag("Pipe"))
        {
            speed = -speed;
        }
    }
}
