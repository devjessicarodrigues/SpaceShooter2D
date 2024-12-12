using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackPlayer : MonoBehaviour
{
    public int playerHealth = 100;

    [Header("Slider")]
    public Slider slider;
    public int maxHealth = 100;
    public bool isDead = false;

    [Header("Score UI")]
    public GameObject gameOverPanel;
    public Text currentScoreText;
    public Text bestScoreText;

    [Header("Audio")]
    public AudioClip collisionSound;
    private AudioSource audioSource;

    void Start()
    {
        UpdateSlider(playerHealth);

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void UpdateSlider(float life)
    {
        if (slider != null)
        {
            slider.value = Mathf.Clamp(life / maxHealth, 0f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int damage = 0;

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            damage = 3;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            damage = 5;
        }
        else if (collision.gameObject.CompareTag("EnemyShooter"))
        {
            damage = 7;
        }
        else if (collision.gameObject.CompareTag("EnemyAlien"))
        {
            damage = 10;
        }
        else if (collision.gameObject.CompareTag("EnemyLoot"))
        {
            damage = 7;
        }
        else if (collision.gameObject.CompareTag("HealthItem")) 
        {
            LootForPlayer(50);
            Destroy(collision.gameObject); 
            return; 
        }

        if (damage > 0)
        {
            GameObject gm = Instantiate(GameManager.Instance.explosion, transform.position, transform.rotation);

            PlayCollisionSound();
            PlayerTakeDamage(damage);
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        playerHealth -= damage;
        UpdateSlider(playerHealth);

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            isDead = true;
            GameOver();
        }
    }

    public void LootForPlayer(int healAmount)
    {
        if (isDead) return;

        playerHealth += healAmount;
        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }

        UpdateSlider(playerHealth);
        Debug.Log($"Player ganhou + {healAmount} de vida! Vida atual: {playerHealth}");
    }

    void GameOver()
    {
        if (gameOverPanel != null)
        {
            Time.timeScale = 0f;

            gameOverPanel.SetActive(true);

            int currentScore = Score.Instance != null ? Score.Instance.currentScore : 0;
            int bestScore = PlayerPrefs.GetInt("best", 0);

            if (currentScore > bestScore)
            {
                bestScore = currentScore;
                PlayerPrefs.SetInt("best", bestScore);
                PlayerPrefs.Save();
            }

            if (currentScoreText != null)
                currentScoreText.text = $"{currentScore}";
            else
                Debug.LogWarning("currentScoreText não atribuído no Inspector.");

            if (bestScoreText != null)
                bestScoreText.text = $"{bestScore}";
            else
                Debug.LogWarning("bestScoreText não atribuído no Inspector.");

            Debug.Log($"Game Over! Score Atual: {currentScore} | Melhor Score: {bestScore}");
        }
        else
        {
            Debug.LogError("gameOverPanel não atribuído no Inspector.");
        }
    }

    private void PlayCollisionSound()
    {
        if (collisionSound == null)
        {
            Debug.LogError("collisionSound não está atribuído!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("audioSource não está atribuído!");
            return;
        }

        Debug.Log("Tocando som de colisão...");
        audioSource.PlayOneShot(collisionSound);
    }
}
