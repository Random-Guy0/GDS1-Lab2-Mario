using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public int health;
    public float walkSpeed;
    public float shellSpeed;
    public Color walkColour;
    public Color shellColour;
    public GameObject effect;
    public LayerMask lm;
    public RaycastHit2D[] results;
    bool shell;
    Collider2D[] damageBox;
    Rigidbody2D rb;
    Vector3 effectSpawnOffset = new Vector3(0, -0.25f, 0);
    // Start is called before the first frame update
    void Start()
    {
        walkSpeed = -walkSpeed;
        damageBox = transform.GetChild(0).GetComponentsInChildren<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(walkSpeed * Time.deltaTime, 0, 0));
    }
    bool IsPlayerTouching(Collider2D player)
    {
        foreach (Collider2D item in damageBox)
        {
            if (player.IsTouching(item))
            {
                return true;
            }
        }
        return false;
    }
    void Damage(int type)
    {
        health--;
        if (health <= 0)
        {
            Die(type);
        }
    }
    void Die(int type)
    {
        switch (type)
        {
            case 1:
                // shell mode
                return;
            default:
                break;
        }
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.attachedRigidbody.velocity.y < 0 || !collision.IsTouching(damageBox[0]))
            {
                Damage(1);
            }
            else
            {
                Debug.Log(name + ": deal damage");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        results = Physics2D.BoxCastAll(transform.position, transform.localScale, 0, new Vector2(walkSpeed, 0).normalized, 0.5f, lm);
        if (results.Length > 0)
        {
            walkSpeed = -walkSpeed;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (new Vector3(walkSpeed, 0, 0).normalized * 0.5f), transform.localScale);
    }
}
