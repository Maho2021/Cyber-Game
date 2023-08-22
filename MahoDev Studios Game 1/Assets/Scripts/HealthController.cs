using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int maxHealth = 100;
    public int maxAmmo = 5;
    private int minAmmo = 0;
    private int currentHealth;
    private int currentAmmo;

    private void Start()
    {
        currentHealth = maxHealth;
        currentAmmo = 1;
        Debug.Log("You Current Health is: " + currentHealth);
        Debug.Log("You Current Ammo is: " + currentAmmo);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Reload(int amount)
    {
        currentAmmo += amount;
        if (currentAmmo >= maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
    }

    public void Shoot(int amount)
    {
        //Debug.Log("We are in shoot: " + currentAmmo);
        if (currentAmmo == 0)
        {
            currentAmmo = minAmmo;
        }
        else
        {
            currentAmmo -= amount;
            //Debug.Log("Current Ammo: " + currentAmmo);
        }
        //Debug.Log("Current Ammo: " + currentAmmo);

    }

    public void Defend(bool isDefending)
    {
        if (isDefending)
        {
            // Put defend-related logic here.
            // For example, you could reduce incoming damage, increase defense stats, etc.
            Debug.Log("Defending!");
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }


    // You can add more methods here for taking damage, healing, etc.
}
