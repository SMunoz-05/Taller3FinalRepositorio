using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int recolectados = 0;
    public GameObject portal;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public static GameManager Instance { get; private set; }

    public int gunAmmo = 10;
    public int health = 100;
    public int maxHealth = 100;

    public TextMeshProUGUI timerText;
    private float timer = 0f;
    private bool timerRunning = true;

    public TextMeshProUGUI scoreText;  
    private int score = 0;

    public GameObject nextSceneTrigger;
    private void Awake()
    {
  
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        UpdateScoreText();
    }

    private void Update()
    {
        ammoText.text = gunAmmo.ToString();
        healthText.text = health.ToString();

        if (timerRunning)
        {
            timer += Time.deltaTime;
            UpdateTimerText();
        }
    }
    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }
    private void UpdateTimerText()
    {
        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);
        int centiseconds = (int)((timer * 100) % 100);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, centiseconds);
    }

    public void LoseHealth(int healthToReduce)
    {
        health -= healthToReduce;
        CheckHealth();
    }

    public void CheckHealth()
    {
        if (health <= 0)
        {
            Debug.Log("Has Muerto");

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    public void AddHealth(int health)
    {

        if (this.health + health >= maxHealth)
        {
            this.health = 100;
        }
        else
        {
            this.health += health;  
        }
    }
    public void TryGoToNextScene()
    {
        if (score >= 80)
        {
            if (nextSceneTrigger != null)
            {
                string sceneName = nextSceneTrigger.name;

                // Verificar si la escena está en Build Settings
                int sceneBuildIndex = SceneUtility.GetBuildIndexByScenePath(sceneName + ".unity");

                if (sceneBuildIndex == -1)
                {
                    Debug.LogWarning("La escena \"" + sceneName + "\" no se encuentra en Build Settings o no existe.");
                }
                else
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
            else
            {
                Debug.LogWarning("Next scene trigger no asignado en inspector.");
            }
        }
        else
        {
            Debug.Log("No tienes el suficiente puntaje para cambiar de escena.");
        }
    }

public void RecolectarObjeto(GameObject objeto)
    {
        Destroy(objeto); 
        recolectados++;
        if (recolectados == 3)
            portal.SetActive(true); 
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void ResetTimer()
    {
        timer = 0f;
        UpdateTimerText();
    }

}
