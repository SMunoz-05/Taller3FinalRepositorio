using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    public MovingPlatform platform; // arrastra la plataforma desde el inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            platform.PlayerLanded(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            platform.PlayerLeft(other);
    }
}
