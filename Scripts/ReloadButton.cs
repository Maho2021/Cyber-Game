using UnityEngine;
using UnityEngine.UI;

public class ReloadButton : MonoBehaviour
{
    public HealthController healthController; // Reference to your HealthController script
    public int reloadAmount = 1;

    public void ReloadWeapon()
    {
        healthController.Reload(reloadAmount);

        int currentAmmo = healthController.GetCurrentAmmo();
        Debug.Log("Current Ammo: " + currentAmmo);
    }
}