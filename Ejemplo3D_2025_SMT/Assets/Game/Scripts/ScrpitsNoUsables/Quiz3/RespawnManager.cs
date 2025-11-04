using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public GameObject esferaPrefab;
    public Transform[] puntosSpawn;

    void Start()
    {
        List<int> indicesUsados = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            int idx;
            do
            {
                idx = Random.Range(0, puntosSpawn.Length);
            } while (indicesUsados.Contains(idx));
            indicesUsados.Add(idx);
            Instantiate(esferaPrefab, puntosSpawn[idx].position, Quaternion.identity);
        }
    }
}
