using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

// Initialize all 5 states for the game
public enum BattleState { START, DECISION, RESOLVE, WON, LOST}

public class BattleSystem : MonoBehaviour

    //test
{
    // GameObject Prefabs For Spawning
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    // Places for prefabs to spawn in on
    public Transform playerSpawn;
    public Transform enemySpawn;

    // Variables that will be holding all the attributes of the players
    Unit playerUnit;
    Unit enemyUnit;
    private Animator playerAnimator;
    private Animator enemyAnimator;

    //
    public bool isEnemyDead = false;
    public bool isPlayerDead = false;

    // Decison chekcer varibales as well as decision making timer
    bool playerActionSelected = false;
    bool enemyActionSelected = false;
    bool isBattleEnded = false;

    // PlayerAction Dict
    public enum PlayerAction { None, Attack, Reload, Dodge }
    private PlayerAction selectedPlayerAction = PlayerAction.None;
    private int enemyChoice;

    // UI Variables
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI attackattrText;
    public TextMeshProUGUI defenseattrText;
    public TextMeshProUGUI speedattrText;
    public TextMeshProUGUI droneintattrText;
    public Scrollbar playerHealthBar;
    public Scrollbar enemyHealthBar;

    // Initialize BattleState varaible
    public BattleState state;

    // (1)
    void Start()
    {
        // Set state to START
        state = BattleState.START;

        // Move straight to SetupBattle Function
        StartCoroutine(SetupBattle()); 
    }

    // (2)
    IEnumerator SetupBattle()
    {
        // Spawn Player and assign all attributes to the playerUnit Varaible
        GameObject playerGo = Instantiate(playerPrefab, playerSpawn.position, Quaternion.identity);
        playerUnit = playerGo.GetComponent<Unit>();
        playerUnit.Name = "Player";
        playerAnimator = playerUnit.GetComponent<Animator>();
        // playerAnimator = playerUnit.GetComponent<Animator>();

        // Spawn Enemy and assign all attributes to the enemyUnit Varaible
        Quaternion rotation = Quaternion.Euler(0, 180, 0);
        GameObject enemyGo = Instantiate(enemyPrefab, enemySpawn.position, rotation);
        enemyUnit = enemyGo.GetComponent<Unit>();
        enemyUnit.Name = "Enemy";
        enemyAnimator = enemyUnit.GetComponent<Animator>();

        // Wait 2 seconds
        yield return new WaitForSeconds(2f);

        playerUnit.AssignRandomAttributes();
        enemyUnit.AssignRandomAttributes();

        attackattrText.text = playerUnit.attack.ToString();
        yield return new WaitForSeconds(2f);
        defenseattrText.text = playerUnit.defense.ToString();
        yield return new WaitForSeconds(2f);
        speedattrText.text = playerUnit.speed.ToString();
        yield return new WaitForSeconds(2f);
        droneintattrText.text = playerUnit.droneIntelligence;

        playerUnit.BuffHealth();

        // Change GameState to DECISION and jump to SimultaneousDecisionMaking Function
        state = BattleState.DECISION;
        StartCoroutine(SimultaneousDecisionMaking());
    }

    // (3 loop) Function starts a timer for 10 seconds then checks to see if both players make a decsion within certain amount of time
    IEnumerator SimultaneousDecisionMaking()
    {
        if (isBattleEnded)
        {
            yield break;
        }

        yield return new WaitForSeconds(3);
        state = BattleState.DECISION;
        playerAnimator.SetBool("IsShooting", false);
        playerActionSelected = false;
        enemyActionSelected = true;

        yield return new WaitForSeconds(3);

        timerText.enabled = true;
        timerText.text = "Choose Your Next Move!";

        yield return new WaitForSeconds(3);

        float remainingTime = 10f; // Initial time

        while (remainingTime > 0f)
        {
            timerText.text = "Time remaining: " + Mathf.CeilToInt(remainingTime); // Update the UI text with remaining time
            yield return null;
            remainingTime -= Time.deltaTime; // Reduce remaining time by the time passed since the last frame
        }

        timerText.enabled = false;
        

        enemyChoice = Random.Range(1, 6);

        switch (enemyChoice)
        {
            case 1:
                enemyUnit.unitChoice = "A";
                break;
            case 2:
                enemyUnit.unitChoice = "R";
                break;
            case 3:
                enemyUnit.unitChoice = "D";
                break;
            case 4:
                enemyUnit.unitChoice = "RR";
                break;
            case 5:
                enemyUnit.unitChoice = "DA";
                break;
        }

        if (playerActionSelected)
        {
            ResolveActions();
        }
        else
        {
            ResolveActions();
        }
    }

    // (4 loop) Checks Button Clicked. Runs Button Function. Checks GameState. Loops or Moves to Win/Lose. Enemy Attacks Everytime.
    void ResolveActions()
    {
       
        if (playerUnit.speed > enemyUnit.speed)
        {
            PerformUnitAction(playerUnit);
            PerformUnitAction(enemyUnit);
        }
        else
        {
            PerformUnitAction(enemyUnit);
            PerformUnitAction(playerUnit);
        }
        StartCoroutine(SimultaneousDecisionMaking());
    }    

    // (5) Ends Battle with text in terminal. Destroy GO with no HP.
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            Destroy(enemyUnit.gameObject);
            Debug.Log("Congrats You Win!");
        }
        else if (state == BattleState.LOST)
        {
            Destroy(playerUnit.gameObject);
            Debug.Log("Sorry You Lose!");
        }

        isBattleEnded = true;
    }

    // Sets Selected player action to which button was clicked
    public void SetSelectedPlayerAction(string choice)
    {
        playerUnit.unitChoice = choice;
        playerActionSelected = true;
    }

    // Attack Button Listener
    public void OnAttackButtonClicked()
    {
        SetSelectedPlayerAction("A");
    }

    // Reload Button Listener
    public void OnReloadButtonClicked()
    {
        SetSelectedPlayerAction("R");
    }

    // Dodge Button Listener
    public void OnDodgeButtonClicked()
    {
        SetSelectedPlayerAction("D");
    }

    // Reroll Button Listener
    public void OnReRollButtonClicked()
    {
        SetSelectedPlayerAction("RR");
    }

    // Drone Button Listener
    public void OnDroneButtonClicked()
    {
        SetSelectedPlayerAction("DA");
    }

    void PerformUnitAction(Unit unit)
    {
        switch (unit.unitChoice)
        {
            case "A":
                if (unit == playerUnit)
                    PlayerAttack();
                else
                    EnemyAttack();
                break;
            case "R":
                unit.ReloadWeapon();
                Debug.Log(unit.Name + " reloded there current ammo is now: " + unit.currentAmmo);
                break;
            case "D":
                Debug.Log(unit.Name + " dodged!");
                break;
            case "RR":
                unit.ReRollAtt();
                Debug.Log(unit.Name);
                break;
            case "DA":
                if (unit == playerUnit)
                {
                    PlayerDroneAttack();
                }
                else
                    EnemyDroneAttack();
                break;
        }
    }

    void PlayerAttack()
    {
        if (enemyUnit.unitChoice == "D")
        {
            playerUnit.currentAmmo--;
            return;
        }
        if (playerUnit.currentAmmo > 0)
        {
            playerUnit.currentAmmo -= 1;
            playerAnimator.SetBool("IsShooting", true);
            enemyUnit.currentHP -= playerUnit.damage;
            Debug.Log("You hit the ememy, there health is now: " + enemyUnit.currentHP);
            UpdateHealthBar(enemyUnit, enemyHealthBar);
        }
        else
        {
            Debug.Log("You dont have any ammo!");
        }

        isEnemyDead = enemyUnit.UnitDeadCheck();

        if (isEnemyDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
    }

    void PlayerDroneAttack()
    {
        if (playerUnit.currentAmmo >= 3)
        {
            playerUnit.currentAmmo -= 3;
            // playerAnimator.SetTrigger("AttackTrigger");
            enemyUnit.currentHP -= 50;
            Debug.Log("Your drone hit the enemy, there health is now: " + enemyUnit.currentHP);
            UpdateHealthBar(enemyUnit, enemyHealthBar);
        }
        else
        {
            Debug.Log("You dont have enough ammo for a drone attack!");
        }

        isEnemyDead = enemyUnit.UnitDeadCheck();

        if (isEnemyDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
    }

    void EnemyAttack()
    {

        if (playerUnit.unitChoice == "D")
        {
            enemyUnit.currentAmmo--;
            return;
        }

        if (enemyUnit.currentAmmo > 0)
        {
            enemyUnit.currentAmmo -= 1;
            playerUnit.currentHP -= enemyUnit.damage;
            Debug.Log("The enemy has hit you! Your health is now: " + playerUnit.currentHP);
            UpdateHealthBar(playerUnit, playerHealthBar);
        }
        else
        {
            Debug.Log("The enemy tried to attack with no ammo!");
        }

        isPlayerDead = playerUnit.UnitDeadCheck();

        if (isPlayerDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
    }

    void EnemyDroneAttack()
    {
        if (enemyUnit.currentAmmo >= 3)
        {
            enemyUnit.currentAmmo -= 3;
            // playerAnimator.SetTrigger("AttackTrigger");
            playerUnit.currentHP -= 50;
            Debug.Log("The enemy hit you with a drone! Your health is now: " + playerUnit.currentHP);
            UpdateHealthBar(playerUnit, playerHealthBar);
        }
        else
        {
            Debug.Log("The enemy attempted a drone attack but didnt have enough ammo!");
        }

        isPlayerDead = playerUnit.UnitDeadCheck();

        if (isPlayerDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
    }

    void UpdateHealthBar(Unit unit, Scrollbar scrollbar)
    {
        float normalizedHealth = (float)unit.currentHP / unit.maxHP;
        scrollbar.size = normalizedHealth;
    }
}
