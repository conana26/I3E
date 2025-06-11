using UnityEngine;

public class Laser : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawner respawner = other.GetComponent<PlayerRespawner>();
            if (respawner != null)
            {
                respawner.Respawn();
            }
        }
    }
}
