using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public static Score Instance;
    public int currentScore = 0;

    [Header("UI Elements")]
    public Text sliderScoreText;
    public Text scoreText;
    public Text recordText;

    [Header("Audio Settings")]
    public AudioClip buttonClickSound;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }


    private void Start()
    {
        if (scoreText == null)
        {
            GameObject scoreObject = GameObject.FindWithTag("ScoreText");
            if (scoreObject != null) scoreText = scoreObject.GetComponent<Text>();
            else Debug.LogWarning("ScoreText não encontrado pela tag!");
        }

        if (sliderScoreText == null)
        {
            GameObject sliderScoreObject = GameObject.FindWithTag("SliderScoreText");
            if (sliderScoreObject != null) sliderScoreText = sliderScoreObject.GetComponent<Text>();
            else Debug.LogWarning("SliderScoreText não encontrado pela tag!");
        }

        if (recordText == null)
        {
            GameObject recordScoreObject = GameObject.FindWithTag("RecordText");
            if (recordScoreObject != null) recordText = recordScoreObject.GetComponent<Text>();
            else Debug.LogWarning("RecordText não encontrado pela tag!");
        }

        if (scoreText == null || sliderScoreText == null || recordText == null)
        {
            Debug.LogError("Faltando referência(s) de UI! Verifique as tags no Inspector.");
        }

        audioSource = gameObject.AddComponent<AudioSource>();

        UpdateScoreText();
        UpdateRecordText();
    }


    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreText();
        Debug.Log("Pontuação atual: " + currentScore);
    }

    public void SaveScore()
    {
        int bestScore = PlayerPrefs.GetInt("best", 0);
        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("best", currentScore);
            PlayerPrefs.Save();
            Debug.Log("Nova melhor pontuação salva: " + currentScore);
            UpdateRecordText();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }

        if (sliderScoreText != null)
        {
            sliderScoreText.text = ("Score: " + currentScore).ToString();
        }
    }

    private void UpdateRecordText()
    {
        if (recordText != null)
        {
            int bestScore = PlayerPrefs.GetInt("best", 0);
            recordText.text = bestScore.ToString();
        }
    }

    public void RestartGame()
    {
        PlayButtonSound();
        SaveScore();
        currentScore = 0;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        PlayButtonSound();
        SaveScore();
        Debug.Log("Jogo encerrado. Melhor pontuação salva: " + PlayerPrefs.GetInt("best", 0));
        Application.Quit();
    }

    private void PlayButtonSound()
    {
        if (buttonClickSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        scoreText = GameObject.FindWithTag("ScoreText")?.GetComponent<Text>();
        sliderScoreText = GameObject.FindWithTag("SliderScoreText")?.GetComponent<Text>();
        recordText = GameObject.FindWithTag("RecordText")?.GetComponent<Text>();

        if (scoreText == null || sliderScoreText == null || recordText == null)
        {
            Debug.LogError("Referências de UI não encontradas na nova cena. Verifique as tags.");
        }

        UpdateScoreText();
        UpdateRecordText();
    }
}
