using UnityEngine;

public class Unit : MonoBehaviour
{

    //GitHub Test Comment
    //hello

    // Player Name
    public string Name;
    public string unitChoice;

    // Health Variables
    public int maxHP;
    public int currentHP;

    // Damage Variable
    public int damage;
    
    // Ammo Variables 
    // private int minAmmo = 0;
    private int maxAmmo = 5;
    public int currentAmmo = 1;
    public int reloadAmount = 1;

    // Dodge Variable
    public bool dodge = false;


    // Attributes
    public int attack = 0;
    public int defense = 0;
    public int speed = 0;
    public string droneIntelligence;
    private int droneClass = 0;

    //Boost Variables
    private float baseBoostPercentage = 0.05f;

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
        int switchAttribute = Random.Range(1, 4);
        
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

    }

    public void AssignRandomAttributes()
    {
        attack = Random.Range(2, 13);
        defense = Random.Range(2, 13);
        speed = Random.Range(2, 13);

        droneClass = Random.Range(2, 13);

        if (droneClass > 1 && droneClass < 5)
        {
            droneIntelligence = "F";
        }
        else if (droneClass > 4 && droneClass < 8)
        {
            droneIntelligence = "C";
        }
        else if (droneClass > 7 && droneClass < 11)
        {
            droneIntelligence = "B";
        }
        else
        {
            droneIntelligence = "S";
        }
    }

    public void BuffHealth()
    {
        float boostPercentage = baseBoostPercentage + (0.05f * (defense - 2));

        Debug.Log(boostPercentage);
    }
}