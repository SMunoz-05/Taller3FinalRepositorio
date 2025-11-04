using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float distanciaRecoleccion = 2f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Recolección al pasar por encima usando trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Recolectar();
        }
    }

    // Recolección al hacer clic si está cerca del jugador
    void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, player.position) < distanciaRecoleccion)
        {
            Recolectar();
        }
    }

    void Recolectar()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.RecolectarObjeto(gameObject);
        }
    }
}
