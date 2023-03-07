using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public GameObject manager;
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Animate());
        manager = GameObject.Find("GameManager");
    }

    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>(); 

        rigidbody2D.isKinematic = true;
        circleCollider2D.enabled = false;
        boxCollider.enabled = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(0.25f);

        spriteRenderer.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + Vector3.up;

        while(elapsed < duration)
        {
            float t = elapsed / duration;

            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        rigidbody2D.isKinematic = false;
        circleCollider2D.enabled = true;
        boxCollider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            switch(gameObject.tag)
            {
                case "RedMushroom":
                    manager.GetComponent<gamesystem>().Add(0,200);
                    break;
                case "GreenMushroom":
                    manager.GetComponent<gamesystem>().Add(0, 1000);
                    break;
                case "Star":
                    manager.GetComponent<gamesystem>().Add(0, 1000);
                    break;
            }
            Destroy(gameObject);
            //PowerUp
        }
    }
}
