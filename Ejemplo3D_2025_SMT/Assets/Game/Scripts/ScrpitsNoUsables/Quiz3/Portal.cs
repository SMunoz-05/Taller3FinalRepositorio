using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Guarda el total recolectado para mostrarlo en la próxima escena
            PlayerPrefs.SetInt("TotalRecolectados", GameManager.instance.recolectados);

            // Cambia a la Escena 2 (debes llamarla exactamente como la tengas en tu proyecto)
            SceneManager.LoadScene("Escena2");
        }
    }
}
