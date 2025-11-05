using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movimiento")]
    public Vector3 moveDirection = Vector3.right; // dirección: X, Y o Z
    public float moveDistance = 5f;               // qué tanto se mueve
    public float moveSpeed = 2f;                  // velocidad
    public bool loop = true;                      // ¿sigue moviéndose de ida y vuelta?

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool goingToTarget = true;

    public void PlayerLanded(Collider player)
    {
        player.transform.SetParent(transform);
    }

    public void PlayerLeft(Collider player)
    {
        player.transform.SetParent(null);
    }


    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveDirection.normalized * moveDistance;
    }

    void Update()
    {
        // Mover la plataforma hacia su destino
        Vector3 target = goingToTarget ? targetPos : startPos;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        // Cuando llega, cambia de dirección
        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            if (loop)
                goingToTarget = !goingToTarget;
        }
    }

    // Esto hace que el jugador se "pegue" a la plataforma cuando está encima
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform.parent); // la plataforma real
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

}
