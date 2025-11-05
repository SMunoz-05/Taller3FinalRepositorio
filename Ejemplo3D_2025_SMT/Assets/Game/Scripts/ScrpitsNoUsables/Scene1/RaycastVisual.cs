using UnityEngine;

public class RaycastVisual : MonoBehaviour
{
    void Update()
    {

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 30f);

    }
}
