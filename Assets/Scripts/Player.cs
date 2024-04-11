using UnityEngine;

public class Player : MonoBehaviour


{

public int maxHealth = 100;
public int currentHealth;
public HealthBar healthBar;

public bool isShielded = false;
    
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

   public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
