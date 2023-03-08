using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public GameObject jumpDeathEffect;
    public GameObject fireDeathEffect;
    public LayerMask lm;
    public RaycastHit2D[] results;
    SpriteRenderer sr;
    Collider2D[] damageBox;
    Rigidbody2D rb;
    Vector3 effectSpawnOffset = new Vector3(0,-0.25f,0);
    bool hitdir;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        speed = -speed;
        damageBox = transform.GetChild(0).GetComponentsInChildren<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime,0,0));
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
                Instantiate(jumpDeathEffect,new Vector3(transform.position.x + effectSpawnOffset.x, transform.position.y + effectSpawnOffset.y,0), Quaternion.identity).GetComponent<ParticleSystemRenderer>().material = sr.material;
                break;
            case 2:
                if (hitdir)
                {
                    Instantiate(fireDeathEffect, new Vector3(transform.position.x + effectSpawnOffset.x, transform.position.y + effectSpawnOffset.y, 0), Quaternion.Euler(new Vector3(-50, 90, 90))).GetComponent<ParticleSystemRenderer>().material = sr.material;
                }
                else
                {
                    Instantiate(fireDeathEffect, new Vector3(transform.position.x + effectSpawnOffset.x, transform.position.y + effectSpawnOffset.y, 0), Quaternion.Euler(new Vector3(-50, -90, 90))).GetComponent<ParticleSystemRenderer>().material = sr.material;
                }
                break;
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
        else if (collision.CompareTag("Fireball"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                hitdir = true;
            }
            else if (collision.transform.position.x > transform.position.x)
            {
                hitdir = false;
            }
            else if (collision.transform.position.x == transform.position.x)
            {
                hitdir = true;
            }
            Damage(2);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        results = Physics2D.BoxCastAll(transform.position, transform.localScale, 0, new Vector2(speed, 0).normalized, 0.5f, lm);
        if (results.Length>0)
        {
            speed = -speed;
        }
        if (collision.transform.CompareTag("Death"))
        {
            Damage(0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (new Vector3(speed, 0,0).normalized * 0.5f), transform.localScale);
    }
}
