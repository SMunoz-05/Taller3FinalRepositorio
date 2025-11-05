using UnityEngine;

public class EscalaAuto : MonoBehaviour
{
    public float minScale = 0.5f;
    public float maxScale = 2.0f;
    public float speed = 1.0f;
    private bool creciendo = true;

    void Update()
    {
        float escala = transform.localScale.x + (creciendo ? 1 : -1) * speed * Time.deltaTime;

        if (escala > maxScale)
        {
            escala = maxScale;
            creciendo = false;
        }
        else if (escala < minScale)
        {
            escala = minScale;
            creciendo = true;
        }

        transform.localScale = new Vector3(escala, escala, escala);
    }
}
