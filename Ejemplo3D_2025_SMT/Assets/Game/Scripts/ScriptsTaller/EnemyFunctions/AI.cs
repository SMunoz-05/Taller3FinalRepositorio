using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public NavMeshAgent naveMeshAgent;


    public Transform[] destinations;
    private int i = 0;

    [Header("----------Follow Player?-----------")]
    private GameObject player;
    public bool followPlayer;

    private float distanceToPlayer;
    public float distanceToFollowPlayer = 10;

    public float distanceToFollowPath;

    public int lifes = 3;
    void Start()
    {
        if (destinations == null || destinations.Length == 0)
        {
            transform.gameObject.GetComponent<AI>().enabled= false; 
        }
        else
        {
            naveMeshAgent.destination = destinations[0].transform.position;
            player = FindFirstObjectByType<PlayerMovement>().gameObject;
        }

    }


    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= distanceToFollowPlayer && followPlayer)
        {
            FollowPlayer();
        }
        else
        {
            EnemyPath();
        }
    }

    public void EnemyPath()
    {
        naveMeshAgent.destination = destinations[i].position;

        if (Vector3.Distance(transform.position, destinations[i].position) <= distanceToFollowPath)
        {
            if (destinations[i] != destinations[destinations.Length -1 ]) //evita que el array de las posciones pasen del limite que estan asignadas en unity
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }

    public void FollowPlayer()
    {
        naveMeshAgent.destination = player.transform.position;
    }

    public void GranadeImpact()
    {
        LoseLife(3);
    }

    public void LoseLife(int lifesToLose)
    {
        lifes = lifes - lifesToLose;
        if (lifes<= 0)
        {
            Destroy(gameObject);
        }
    }
}
