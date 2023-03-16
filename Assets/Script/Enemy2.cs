using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public int health;
    public float walkSpeed;
    public float shellSpeed;
    float curSpeed;
    public float RespawnTime;
    float curRespawnTime;
    public Sprite walkColour;
    public Sprite shellColour;
    public LayerMask lm;
    public GameObject deathEffect;
    public GameObject fireDeathEffect;
    Vector3 effectSpawnOffset = new Vector3(0, -0.25f, 0);
    Vector3 viewPos;
    public RaycastHit2D[] results;
    bool shell = false;
    bool move = false;
    Collider2D[] damageBox;
    Rigidbody2D rb;
    SpriteRenderer sr;
    bool hitdir;
    public Animator animator;
    public bool isShell = false;
   
    // Start is called before the first frame update
    void Start()
    {
        curSpeed = -walkSpeed;
        damageBox = transform.GetChild(0).GetComponentsInChildren<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (0 <= viewPos.x && viewPos.x <= 1 && 0 <= viewPos.y && viewPos.y <= 1)
        {
            if (!shell)
            {
                transform.Translate(new Vector3(curSpeed * Time.deltaTime, 0, 0));
            }
            else if (move)
            {
                transform.Translate(new Vector3(curSpeed * Time.deltaTime, 0, 0));
            }
            else if (!move)
            {
                curRespawnTime += Time.deltaTime;
                if (curRespawnTime >= RespawnTime)
                {
                    ShellMode();
                }
            }
        }
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
                ShellMode();
                return;
            case 2:
                if (hitdir)
                {
                    Instantiate(fireDeathEffect, new Vector3(transform.position.x + effectSpawnOffset.x, transform.position.y + effectSpawnOffset.y, 0), Quaternion.Euler(new Vector3(-50, 90,90))).GetComponent<ParticleSystemRenderer>().material = sr.material;
                }
                else
                {
                    Instantiate(fireDeathEffect, new Vector3(transform.position.x + effectSpawnOffset.x, transform.position.y + effectSpawnOffset.y, 0), Quaternion.Euler(new Vector3(-50, -90, 90))).GetComponent<ParticleSystemRenderer>().material = sr.material;
                }
                break;
            case 3:
                if (hitdir)
                {
                    Instantiate(deathEffect, new Vector3(transform.position.x + effectSpawnOffset.x, transform.position.y + effectSpawnOffset.y, 0), Quaternion.Euler(new Vector3(-50, 90, 90))).GetComponent<ParticleSystemRenderer>().material = sr.material;
                }
                else
                {
                    Instantiate(deathEffect, new Vector3(transform.position.x + effectSpawnOffset.x, transform.position.y + effectSpawnOffset.y, 0), Quaternion.Euler(new Vector3(-50, -90, 90))).GetComponent<ParticleSystemRenderer>().material = sr.material;
                }
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }
    void ShellMode()
    {
        shell = !shell;
        curRespawnTime = 0;
        if (shell)
        {
            animator.SetBool("Shell", true);
            isShell = true;
            curSpeed = 0;
        }
        else
        {
            animator.SetBool("Shell", false);
            isShell = false;
            curSpeed = -walkSpeed;
        }
    }
    void ShellMove(bool direction)
    {
        move = !move;
        if (move)
        {
            tag = "MovingShell";
        }
        else
        {
            tag = "Enemy";
        }
        
        curRespawnTime = 0;
        if (direction)
        {
            curSpeed = shellSpeed;
        }
        else
        {
            curSpeed = -shellSpeed;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!shell)
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
        else
        {
            if (collision.CompareTag("Player"))
            {
                if (collision.attachedRigidbody.velocity.y < 0 || !collision.IsTouching(damageBox[0]))
                {
                    if (collision.transform.position.x < transform.position.x)
                    {
                        ShellMove(true);
                    }
                    else if (collision.transform.position.x > transform.position.x)
                    {
                        ShellMove(false);
                    }
                    else if (collision.transform.position.x == transform.position.x)
                    {
                        ShellMove(true);
                    }
                }
                else if(move)
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
            else if (collision.CompareTag("MovingShell"))
            {
                if (shell)
                {
                    if (collision.transform.position.x < transform.position.x)
                    {
                        curSpeed = Mathf.Abs(curSpeed);
                    }
                    else if (collision.transform.position.x > transform.position.x)
                    {
                        curSpeed = -Mathf.Abs(curSpeed);
                    }
                    else if (collision.transform.position.x == transform.position.x)
                    {
                        curSpeed = Mathf.Abs(curSpeed);
                    }
                }
                else
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
                    Damage(3);
                }
            }
        }
        if (collision.gameObject.name == "edge")
        {
            results = Physics2D.BoxCastAll(transform.position, transform.localScale, 0, new Vector2(curSpeed, 0).normalized, 0.5f, lm);
            if (results.Length > 0)
            {
                curSpeed = -curSpeed;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        results = Physics2D.BoxCastAll(transform.position, transform.localScale, 0, new Vector2(curSpeed, 0).normalized, 0.5f, lm);
        if (results.Length > 0)
        {
            curSpeed = -curSpeed;
        }
        if (collision.transform.CompareTag("Death"))
        {
            Damage(0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + (new Vector3(curSpeed, 0, 0).normalized * 0.5f), transform.localScale);
    }
}
