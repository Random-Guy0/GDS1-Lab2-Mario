using System.Collections;
using UnityEngine;

public class ToolBar : MonoBehaviour
{
    public Sprite emptyBlock;
    public Animator animator;
    public GameObject item;
    public int maxHits = -1;
    public GameObject manager;

    private bool animating;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !animating && maxHits != 0 && collision.gameObject.transform.position.y < gameObject.transform.position.y)
        {
            Hit();
            if(item != null && item.name == "Coin 1")
            {
                manager.GetComponent<gamesystem>().Add(1, 200);
            }

        }
    }

    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;

        maxHits--;

        if (animator != null)
        {
            animator.enabled = false;
        }

        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
        }

        if(item != null)
        {
            Instantiate(item,transform.position, Quaternion.identity);
        }

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        animating = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);
        animating = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = to;
    }
}
