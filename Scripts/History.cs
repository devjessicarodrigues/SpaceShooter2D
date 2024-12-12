using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class History : MonoBehaviour
{
    public float scrollSpeed = 20f;
    public float perspectiveTilt = -10f;
    public float startDelay = 2f;

    public GameObject historyUI;
    public GameObject controlUI;
    public TMP_Text spaceMessage;
    public string nextSceneName = "NextScene";

    private RectTransform textTransform;
    private bool firstTextFinished = false;
    private bool secondTextReady = false;

    void Start()
    {
        textTransform = historyUI.GetComponent<RectTransform>();
        textTransform.rotation = Quaternion.Euler(perspectiveTilt, 0f, 0f);

        spaceMessage.text = "Press SPACE to skip";
        spaceMessage.gameObject.SetActive(false);

        Invoke("StartScrolling", startDelay);
    }

    void StartScrolling()
    {
        enabled = true;
        spaceMessage.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!firstTextFinished)
        {
            textTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            if (textTransform.anchoredPosition.y > 0f) 
            {
                EndFirstText();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                EndFirstText(); 
            }
        }
        else if (!secondTextReady)
        {
            ShowSecondUI();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadNextScene();
            }
        }
    }

    void EndFirstText()
    {
        historyUI.SetActive(false);
        spaceMessage.gameObject.SetActive(false);
        firstTextFinished = true;
        Debug.Log("Primeiro texto encerrado.");
    }

    void ShowSecondUI()
    {
        controlUI.SetActive(true); 
        spaceMessage.text = "Press SPACE to continue";
        spaceMessage.gameObject.SetActive(true);
        secondTextReady = true;
        Debug.Log("Segunda UI exibida.");
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
        Debug.Log("Carregando a próxima cena...");
    }
}
