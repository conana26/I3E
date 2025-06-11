using UnityEngine;

public class SwordCollector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SwordManager.instance.CollectSword();
            Destroy(gameObject);
        }
    }
}
