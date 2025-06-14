/* Creator: Lim Xue Zhi Conan
   Date of Creation: 12/6/25
   Script: player Respawn System*/
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    private Vector3 spawnPoint;

    void Start()
    {
        // Save the original spawn point at start
        spawnPoint = transform.position;
    }

    public void Respawn()
    {
        Debug.Log("Player hit a laser! Respawning...");
        transform.position = spawnPoint;
    }
}
