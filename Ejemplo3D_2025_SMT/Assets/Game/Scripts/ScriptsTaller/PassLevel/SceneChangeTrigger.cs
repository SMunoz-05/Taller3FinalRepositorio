using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Llama al método del GameManager para intentar cambiar de escena
            GameManager.Instance.TryGoToNextScene();
        }
    }
}
