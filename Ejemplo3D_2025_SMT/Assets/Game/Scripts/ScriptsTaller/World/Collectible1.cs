// Nuevo script para Prefabs recolectables: Collectible.cs
using UnityEngine;

public class Collectible1 : MonoBehaviour
{
    public int points = 10;  // Puntaje que suma este objeto

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(points);
            Destroy(gameObject);
        }
    }
}

