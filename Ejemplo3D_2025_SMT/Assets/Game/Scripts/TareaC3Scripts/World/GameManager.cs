using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        ammoText.text = gunAmmo.ToString();
        healthText.text = health.ToString();
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

    public void RecolectarObjeto(GameObject objeto)
    {
        Destroy(objeto); // Elimina la esfera
        recolectados++;
        if (recolectados == 3)
            portal.SetActive(true); // Activa el portal
    }

}
