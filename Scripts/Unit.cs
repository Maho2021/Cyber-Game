using UnityEngine;

public class Unit : MonoBehaviour
{
    // Player Name
    public string Name;
    public string unitChoice;

    // Health Variables
    public int maxHP;
    public int currentHP;

    // Damage Variable
    public int damage;
    
    // Ammo Variables 
    private int minAmmo = 0;
    private int maxAmmo = 5;
    public int currentAmmo = 1;
    public int reloadAmount = 1;

    // Dodge Variable
    public bool dodge = false;


    // Attributes
    public int attack = 0;
    public int defense = 0;
    public int speed = 0;
    public int droneIntelligence = 0;

    // Function that deals damage to player and checks wheather a player is dead or alive returns bool after check
    public bool UnitDeadCheck()
    {
            if (currentHP <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        
    }


    // Function that Reloads ammo for a player but doesnt allow for the player to go above 5 ammo 
    public void ReloadWeapon()
    {
        currentAmmo += reloadAmount;

        if (currentAmmo >= maxAmmo)
        {
            currentAmmo = maxAmmo;
        }

        // Debug.Log("Your Current Ammo is: " + currentAmmo);
    }

    /* public void UpdateDodge()
    {
        if (unitChoice == "D")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    */

    public void ReRollAtt()
    {
        int switchAttribute = Random.Range(1, 5);
        
        if (switchAttribute == 1)
        {
            attack = Random.Range(2, 13);
            Debug.Log("attack Attribute is now: " +  attack + "!");
        }
        else if (switchAttribute == 2)
        {
            defense = Random.Range(2, 13);
            Debug.Log("defense Attribute is now: " + defense + "!");
        }
        else if (switchAttribute == 3)
        {
            speed = Random.Range(2, 13);
            Debug.Log("speed Attribute is now: " + speed + "!");
        }
        else if (switchAttribute == 4)
        {
            droneIntelligence = Random.Range(2, 13);
            Debug.Log("drone intellidence Attribute is now: " + droneIntelligence + "!");
        }
    }

    public void AssignRandomAttributes()
    {
        attack = Random.Range(2, 13);
        defense = Random.Range(2, 13);
        speed = Random.Range(2, 13);
        droneIntelligence = Random.Range(2, 13);
    }
}