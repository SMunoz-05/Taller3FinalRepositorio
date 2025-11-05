using UnityEngine;

public class RotarXYZ : MonoBehaviour
{
    public float rotationSpeed = 240f;

    void Update()
    {

    }


    public void RotarX()
    {
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }

    public void RotarY()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void RotarZ()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
