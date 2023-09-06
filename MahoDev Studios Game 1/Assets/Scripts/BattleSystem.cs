using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

// Initialize all 5 states for the game
public enum BattleState { START, DECISION, RESOLVE, WON, LOST}

public class BattleSystem : MonoBehaviour

{
    // GameObject Prefabs For Spawning
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public UIManager uiManager;

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
    bool attributeChangeSelected = false;
    bool isBattleEnded = false;

    // PlayerAction Dict
    public enum PlayerAction { None, Attack, Reload, Dodge }
    private PlayerAction selectedPlayerAction = PlayerAction.None;
    public enum PlayerReroll {None, Strength, Armor, Speed }
    private PlayerReroll selectedAttributeChange = PlayerReroll.None;
    private int enemyChoice;

    // UI Variables
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI healthBarText;
    public TextMeshProUGUI ammoAmountText;
    public TextMeshProUGUI rerollText;

    // public Image bulletImage;
    public Button strengthButton;
    public Button armorButton;
    public Button speedButton;


    public TextMeshProUGUI ehealthBarText;
    public TextMeshProUGUI eammoAmountText;

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
        Quaternion rotationp = Quaternion.Euler(0, 20, 0);
        GameObject playerGo = Instantiate(playerPrefab, playerSpawn.position, rotationp);
        playerUnit = playerGo.GetComponent<Unit>();
        playerUnit.Name = "Player";
        playerAnimator = playerUnit.GetComponent<Animator>();
        // playerAnimator = playerUnit.GetComponent<Animator>();

        // Spawn Enemy and assign all attributes to the enemyUnit Varaible
        Quaternion rotation = Quaternion.Euler(0, 230, 0);
        GameObject enemyGo = Instantiate(enemyPrefab, enemySpawn.position, rotation);
        enemyUnit = enemyGo.GetComponent<Unit>();
        enemyUnit.Name = "Enemy";
        enemyAnimator = enemyUnit.GetComponent<Animator>();

        // Wait 2 seconds
        yield return new WaitForSeconds(2f);

        playerUnit.AssignRandomAttributes();
        enemyUnit.AssignRandomAttributes();

        playerUnit.CalcBoosts();
        enemyUnit.CalcBoosts();

        StartCoroutine(uiManager.InitialUiUpdate(playerUnit, enemyUnit));

        // Change GameState to DECISION and jump to SimultaneousDecisionMaking Function
        yield return new WaitForSeconds(9f);
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
        yield return new WaitForSeconds(1);

        state = BattleState.DECISION;
        playerActionSelected = false;
        enemyActionSelected = true;

        yield return new WaitForSeconds(3);

        timerText.enabled = true;
        timerText.text = "Choose Your Next Move!";

        yield return new WaitForSeconds(3);

        float remainingTime = 5f; // Initial time

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
        strengthButton.image.color = new Color(strengthButton.image.color.r, strengthButton.image.color.g, strengthButton.image.color.b, 0f);
        armorButton.image.color = new Color(armorButton.image.color.r, armorButton.image.color.g, armorButton.image.color.b, 0f);
        speedButton.image.color = new Color(speedButton.image.color.r, speedButton.image.color.g, speedButton.image.color.b, 0f);
        rerollText.color = new Color(speedButton.image.color.r, speedButton.image.color.g, speedButton.image.color.b, 0f);

        if (playerUnit.speed > enemyUnit.speed)
        {
            PerformUnitAction(playerUnit, playerAnimator);
            PerformUnitAction(enemyUnit, enemyAnimator);
        }
        else
        {
            PerformUnitAction(enemyUnit, enemyAnimator);
            PerformUnitAction(playerUnit, playerAnimator);
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

    public void SetSelectedAttributeChange(string choice)
    {
        playerUnit.attributeChangeChoice = choice;
        attributeChangeSelected = true;
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
        strengthButton.image.color = new Color(strengthButton.image.color.r, strengthButton.image.color.g, strengthButton.image.color.b, 1f);
        armorButton.image.color = new Color(armorButton.image.color.r, armorButton.image.color.g, armorButton.image.color.b, 1f);
        speedButton.image.color = new Color(speedButton.image.color.r, speedButton.image.color.g, speedButton.image.color.b, 1f);
        rerollText.color = new Color(speedButton.image.color.r, speedButton.image.color.g, speedButton.image.color.b, 1f);
    }

    public void OnStrengthButtonClicked()
    {
        SetSelectedAttributeChange("S");
    }

    public void OnAromorButtonClicked()
    {
        SetSelectedAttributeChange("A");
    }

    public void OnSpeedButtonClicked()
    {
        SetSelectedAttributeChange("Sp");
    }

    // Drone Button Listener
    public void OnDroneButtonClicked()
    {
        SetSelectedPlayerAction("DA");
    }

    void PerformUnitAction(Unit unit, Animator anim)
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
                if (unit == playerUnit && playerUnit.currentAmmo < 6)
                {
                    ammoAmountText.text = "X  " + playerUnit.currentAmmo.ToString();
                }
                else if (unit == enemyUnit && enemyUnit.currentAmmo < 6)
                {
                    eammoAmountText.text = "X  " + enemyUnit.currentAmmo.ToString();
                }
                anim.SetTrigger("Reload");
                Debug.Log(unit.Name + " reloded there current ammo is now: " + unit.currentAmmo);
                break;
            case "D":
                anim.SetTrigger("Dodge");
                Debug.Log(unit + " attempted to dodge!");
                break;
            case "RR":
                if (unit == playerUnit)
                {
                    Debug.Log(unit.Name);
                    unit.ReRollAtt();
                }
                else
                {
                    Debug.Log(unit.Name);
                    unit.EReRollAtt();
                }
                uiManager.UpdateUICard(playerUnit, enemyUnit);
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
            enemyUnit.Dodge();
            if (enemyUnit.didDodge == false)
            {
                float skimed = ((100 - enemyUnit.dodgeBoost) / 100f);
                int result = Mathf.RoundToInt(skimed * playerUnit.attackBoost);
                enemyUnit.currentHP -= result;
                enemyUnit.currentHP = Mathf.Clamp(enemyUnit.currentHP, 0, enemyUnit.maxHP);
                ammoAmountText.text = "X  " + playerUnit.currentAmmo.ToString();
                playerAnimator.SetTrigger("Shoot");
                UpdateHealthBar(enemyUnit, enemyHealthBar);
                ehealthBarText.text = enemyUnit.currentHP + "/" + enemyUnit.maxHP;
            }
            else
            {
                ammoAmountText.text = "X  " + playerUnit.currentAmmo.ToString();
                playerAnimator.SetTrigger("Shoot");
            }
            return;
        }
        if (playerUnit.currentAmmo > 0)
        {
            playerUnit.currentAmmo -= 1;
            ammoAmountText.text = "X  " + playerUnit.currentAmmo.ToString();
            playerAnimator.SetTrigger("Shoot");
            enemyUnit.currentHP -= playerUnit.attackBoost;
            enemyUnit.currentHP = Mathf.Clamp(enemyUnit.currentHP, 0, enemyUnit.maxHP);
            ehealthBarText.text = enemyUnit.currentHP + "/" + enemyUnit.maxHP;
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
            ammoAmountText.text = "X  " + playerUnit.currentAmmo.ToString();
            enemyUnit.currentHP -= playerUnit.droneDmg;
            enemyUnit.currentHP = Mathf.Clamp(enemyUnit.currentHP, 0, enemyUnit.maxHP);
            ehealthBarText.text = enemyUnit.currentHP + "/" + enemyUnit.maxHP;
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
            playerUnit.Dodge();
            if (playerUnit.didDodge == false)
            {
                float skimed = ((100 - playerUnit.dodgeBoost) / 100f);
                int result = Mathf.RoundToInt(skimed * enemyUnit.attackBoost);
                playerUnit.currentHP -= result;
                playerUnit.currentHP = Mathf.Clamp(playerUnit.currentHP, 0, playerUnit.maxHP);
                ammoAmountText.text = "X  " + enemyUnit.currentAmmo.ToString();
                playerAnimator.SetTrigger("Shoot");
                UpdateHealthBar(playerUnit, playerHealthBar);
                healthBarText.text = playerUnit.currentHP + "/" + playerUnit.maxHP;
            }
            else
            {
                ammoAmountText.text = "X  " + enemyUnit.currentAmmo.ToString();
                enemyAnimator.SetTrigger("Shoot");
            }
            
            return;
        }

        if (enemyUnit.currentAmmo > 0)
        {
            enemyUnit.currentAmmo -= 1;
            eammoAmountText.text = "X  " + enemyUnit.currentAmmo.ToString();
            enemyAnimator.SetTrigger("Shoot");
            playerUnit.currentHP -= enemyUnit.attackBoost;
            playerUnit.currentHP = Mathf.Clamp(playerUnit.currentHP, 0, playerUnit.maxHP);
            healthBarText.text = playerUnit.currentHP + "/" + playerUnit.maxHP;
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
            eammoAmountText.text = "X  " + enemyUnit.currentAmmo.ToString();
            playerUnit.currentHP -= enemyUnit.droneDmg;
            playerUnit.currentHP = Mathf.Clamp(playerUnit.currentHP, 0, playerUnit.maxHP);
            healthBarText.text = playerUnit.currentHP + "/" + playerUnit.maxHP;
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
