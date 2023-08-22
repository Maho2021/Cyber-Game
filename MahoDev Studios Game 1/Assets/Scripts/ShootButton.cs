using UnityEngine;
using UnityEngine.UI;

public class ShootButton : MonoBehaviour
{
    public HealthController healthController; // Reference to your HealthController script
    public int shotAmount = 1;
    public int damageAmount = 20;

    public void ShootWeapon()
    {

        int currentAmmo = healthController.GetCurrentAmmo();
        int currentHealth = healthController.GetCurrentHealth();

        //Debug.Log("Current Ammo Top: " + currentAmmo);
        //Debug.Log("Current Health Top: " + currentHealth);

        if (currentAmmo > 0)
        {
            healthController.TakeDamage(damageAmount);
            healthController.Shoot(shotAmount);
        }

        currentAmmo = healthController.GetCurrentAmmo();
        currentHealth = healthController.GetCurrentHealth();


        Debug.Log("Current Ammo: " + currentAmmo);
        Debug.Log("Current Health: " + currentHealth);
    }
}