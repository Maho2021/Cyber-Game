using UnityEngine;

public class Unit : MonoBehaviour
{

    //GitHub Test Comment
    //hello

    // Player Name
    public string Name;
    public string unitChoice;
    public string attributeChangeChoice;

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
    public int armor = 0;
    public int speed = 0;
    public string droneIntelligence;
    private int droneClass = 0;

    //Boost Variables
    public int attackBoost;
    public int healthBoost;
    public int dodgeBoost;
    public int droneDmg;
    private float baseBoostPercentage = 0.05f;
    private int baseDodgeBoost = 3;

    // Reroll choice variables
    public bool rerollStrength = false;
    public bool rerollArmor = false;
    public bool rerollSpeed = false;

    public bool didDodge = false;

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
    }

    public void EReRollAtt()
    {
        int switchAttribute = Random.Range(1, 4);
        
        if (switchAttribute == 1)
        {
            attack = Random.Range(2, 13);
            Debug.Log("attack Attribute is now: " +  attack + "!");
        }
        else if (switchAttribute == 2)
        {
            armor = Random.Range(2, 13);
            Debug.Log("armor Attribute is now: " + armor + "!");
        }
        else if (switchAttribute == 3)
        {
            speed = Random.Range(2, 13);
            Debug.Log("speed Attribute is now: " + speed + "!");
        }

    }

    public void ReRollAtt()
    {
        if (attributeChangeChoice == "S")
        {
            attack = Random.Range(2, 13);
            Debug.Log("attack Attribute is now: " + attack + "!");
        }
        else if (attributeChangeChoice == "A")
        {
            armor = Random.Range(2, 13);
            Debug.Log("armor Attribute is now: " + armor + "!");
        }
        else if (attributeChangeChoice == "Sp")
        {
            speed = Random.Range(2, 13);
            Debug.Log("speed Attribute is now: " + speed + "!");
        }
        else
        {
            Debug.Log("You didnt choose an option!");
        }
    }

    public void AssignRandomAttributes()
    {
        attack = Random.Range(2, 13);
        armor = Random.Range(2, 13);
        speed = Random.Range(2, 13);

        droneClass = Random.Range(2, 13);

        if (droneClass > 1 && droneClass < 5)
        {
            droneIntelligence = "F";
            droneDmg = 225;
        }
        else if (droneClass > 4 && droneClass < 8)
        {
            droneIntelligence = "C";
            droneDmg = 320;
        }
        else if (droneClass > 7 && droneClass < 11)
        {
            droneIntelligence = "B";
            droneDmg = 400;
        }
        else
        {
            droneIntelligence = "S";
            droneDmg = 525;
        }
    }

    public void CalcBoosts()
    {
        float armorBoostPercentage = (float)(baseBoostPercentage + ((armor - 2) * 0.05));
        healthBoost = (int)(currentHP * armorBoostPercentage);

        maxHP = healthBoost + 500;
        currentHP = maxHP;

        float attackBoostPercentage = (float)(baseBoostPercentage + ((attack - 2) * 0.05));
        attackBoost = (int)(damage * attackBoostPercentage) + damage;

        if (speed == 2)
        {
            dodgeBoost = 30;
        }
        else
        {
            dodgeBoost = ((speed - 2) * 3) +30;
        }     
    }

    public void CalcBoostsUI()
    {
        if (attributeChangeChoice == "A")
        {
            float armorBoostPercentage = (float)(baseBoostPercentage + ((armor - 2) * 0.05));
            int midGameHealthBoost = (int)(500 * (armorBoostPercentage + 1)) - 500;
            int healthChange = midGameHealthBoost - healthBoost; 
            healthBoost = (int)(500 * armorBoostPercentage);
            currentHP += healthChange;
            maxHP = 500 + healthBoost;
            Debug.Log(healthChange);
            // Debug.Log(midGameHealthBoost);


        }
        else if (attributeChangeChoice == "S")
        {
            float attackBoostPercentage = (float)(baseBoostPercentage + ((attack - 2) * 0.05));
            attackBoost = (int)(damage * attackBoostPercentage) + damage;
        }
        else
        {
            if (speed == 2)
            {
                dodgeBoost = 30;
            }
            else
            {
                dodgeBoost = ((speed - 2) * 3) + 30;
            }
        }
    }

    public void Dodge()
    {
        int checker = Random.Range(1, 101);

        if (checker <= dodgeBoost)
        {
            didDodge = true;
            Debug.Log(" Dodge Worked!");
        }
        else
        {
            didDodge = false;
            Debug.Log(" Dodge Failed!");
        }
    }
}