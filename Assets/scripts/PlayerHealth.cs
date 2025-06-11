using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using StarterAssets;
public class PlayerHealth : MonoBehaviour
{
    private MonoBehaviour[] controlScripts;

    public int health;
    public int maxHealth = 100;
    public TextMeshProUGUI healthText; // Reference to the UI text element to display health
    

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; // Initialize health to maxHealth at the start
        UpdateHealthUI(); // Update the UI to reflect the initial health

    }

    public void TakeDamage(int amount)
    {
        if (isDead) return; // Prevent taking damage if already dead
        
        health -= amount;
        if (health < 0) health = 0; //Clamp health to 0 minimum

        UpdateHealthUI();

        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health.ToString();
        }
    }
    
    void Die()
    {
        isDead = true; // Set the player as dead


    }

}