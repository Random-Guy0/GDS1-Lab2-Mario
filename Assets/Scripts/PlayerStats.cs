using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playerCollider;

    private PlayerLives playerLives;

    public PowerupState powerupState;

    public RuntimeAnimatorController small;
    public RuntimeAnimatorController large;
    public RuntimeAnimatorController fire;
    public Animator animator;

    private void Start()
    {
        powerupState = PowerupState.Small;
        playerLives = FindObjectOfType<PlayerLives>();
    }

    public void OneUp()
    {
        playerLives.OneUp();
    }

    private void Update()
    {
        if (powerupState == PowerupState.Small)
        {
            animator.runtimeAnimatorController = small;
        }
        else if (powerupState == PowerupState.Big)
        {
            animator.runtimeAnimatorController = large;
        }
        else if (powerupState == PowerupState.Flower)
        {
            animator.runtimeAnimatorController = fire;
        }
    }

    public int GetLives()
    {
        return playerLives.GetLives();
    }

    public void CollectPowerup()
    {
        switch (powerupState)
        {
            case PowerupState.Small:
                powerupState = PowerupState.Big;
                playerCollider.size = new Vector2(1, 2);
                playerCollider.offset = new Vector2(0, 0f);
                break;
            case PowerupState.Big:
                powerupState = PowerupState.Flower;
                break;
            case PowerupState.Flower:
                break;
        }
    }

    public void TakeDamage()
    {
        if (powerupState == PowerupState.Small)
        {
            animator.SetBool("IsDead", true);
            Die();
        }
        else
        {
            powerupState = PowerupState.Small;
            playerCollider.size = Vector2.one;
            playerCollider.offset = Vector2.zero;
        }
    }

    public void Die()
    {
        playerLives.LoseLife();
        if (playerLives.GetLives() == 0)
        {
            Destroy(playerLives.gameObject);
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            SceneManager.LoadScene("Lives");
        }
    }

    public PowerupState GetPowerupState()
    {
        return powerupState;
    }
}

public enum PowerupState
{
    Small,
    Big,
    Flower
}
