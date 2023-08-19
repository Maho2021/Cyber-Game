using UnityEngine;
using UnityEngine.UI;

public class DefendButton : MonoBehaviour
{
    public HealthController healthController; // Reference to your HealthController script

    public void Defend()
    {
        healthController.Defend(true);
    }
}
