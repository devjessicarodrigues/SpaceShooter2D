using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pauseMenu;

    [Header("Audio")]
    [SerializeField] private AudioClip buttonClickSound;
    private AudioSource audioSource;

    private void Start()
    {
        pauseMenu.SetActive(false);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false; 
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.activeSelf)
            {
                Debug.Log("ESC pressionado - Retomando o jogo");
                PauseGame(false);
            }
            else
            {
                Debug.Log("ESC pressionado - Pausando o jogo");
                PauseGame(true);
            }
        }
    }

    public void PauseGame(bool isPaused)
    {
        PlayButtonSound();
        if (isPaused)
        {
            Debug.Log("PauseGame - Pausando o jogo");
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            Debug.Log("PauseGame - Retomando o jogo");
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Quit()
    {
        PlayButtonSound();
        Debug.Log("Quit chamado - Saindo do jogo");
        Application.Quit();
    }

    private void PlayButtonSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
        else
        {
            Debug.LogWarning("Áudio ou AudioSource não configurados.");
        }
    }
}
