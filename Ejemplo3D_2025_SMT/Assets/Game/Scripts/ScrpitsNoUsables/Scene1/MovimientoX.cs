using UnityEngine;

public class MovimientoX : MonoBehaviour
{
    public float speed = 2f;
    public bool autoMove = false;

    void Update()
    {
        if (autoMove)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }


    public void ToggleAutoMove()
    {
        autoMove = !autoMove;
    }


    public void StopMove()
    {
        autoMove = false;
    }
}
