using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Player Attribute Text Variables 
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI droneText;

    // Player Buffs Text Variables 
    public TextMeshProUGUI dmgAmountText;
    public TextMeshProUGUI healthBonusText;
    public TextMeshProUGUI dodgeChanceText;
    public TextMeshProUGUI droneDamageText;

    // Player Ammo and health UI variables
    public Image bulletImage;
    public TextMeshProUGUI ammoAmountText;
    public TextMeshProUGUI healthBarText;

    // Enemy Attribute Text Variables
    public TextMeshProUGUI enemyStrengthText;
    public TextMeshProUGUI enemyArmorText;
    public TextMeshProUGUI enemySpeedText;
    public TextMeshProUGUI enemyDroneText;

    // Enemy Buffs Text Variables
    public TextMeshProUGUI enemyDmgAmountText;
    public TextMeshProUGUI enemyHealthBonusText;
    public TextMeshProUGUI enemyDodgeChanceText;
    public TextMeshProUGUI enemyDroneDamageText;

    // Enemy ammo and health UI variables
    public Image enemyBulletImage;
    public TextMeshProUGUI enemyAmmoAmountText;
    public TextMeshProUGUI enemyHealthBarText;
    

    public IEnumerator InitialUiUpdate(Unit player, Unit enemy)
    {
        strengthText.text = player.attack.ToString();
        enemyStrengthText.text = enemy.attack.ToString();
        dmgAmountText.text = player.attackBoost.ToString();
        enemyDmgAmountText.text = enemy.attackBoost.ToString();
        yield return new WaitForSeconds(2f);

        armorText.text = player.armor.ToString();
        enemyArmorText.text = enemy.armor.ToString();
        healthBonusText.text = "+" + player.healthBoost.ToString();
        enemyHealthBonusText.text = "+" + enemy.healthBoost.ToString();
        yield return new WaitForSeconds(2f);

        speedText.text = player.speed.ToString();
        enemySpeedText.text = enemy.speed.ToString();
        dodgeChanceText.text = player.dodgeBoost.ToString() + "%";
        enemyDodgeChanceText.text = enemy.dodgeBoost.ToString() + "%";
        yield return new WaitForSeconds(2f);

        droneText.text = player.droneIntelligence;
        enemyDroneText.text = enemy.droneIntelligence;
        droneDamageText.text = player.droneDmg.ToString();
        enemyDroneDamageText.text = enemy.droneDmg.ToString();
        yield return new WaitForSeconds(2f);

        bulletImage.color = new Color(bulletImage.color.r, bulletImage.color.g, bulletImage.color.b, 1f);
        enemyBulletImage.color = new Color(enemyBulletImage.color.r, enemyBulletImage.color.g, enemyBulletImage.color.b, 1f);
        ammoAmountText.text = "X  " + player.currentAmmo;
        enemyAmmoAmountText.text = "X  " + enemy.currentAmmo;
        yield return new WaitForSeconds(2f);

        healthBarText.text = player.currentHP + "/" + player.maxHP;
        enemyHealthBarText.text = enemy.currentHP + "/" + enemy.maxHP;
        yield return new WaitForSeconds(2f);
    }

    public void UpdateUICard(Unit player, Unit enemy)
    {

            player.CalcBoostsUI();
            strengthText.text = player.attack.ToString();
            dmgAmountText.text = player.attackBoost.ToString();
            armorText.text = player.armor.ToString();
            healthBonusText.text = "+" + player.healthBoost.ToString();
            speedText.text = player.speed.ToString();
            dodgeChanceText.text = player.dodgeBoost.ToString() + "%";
            healthBarText.text = player.currentHP + "/" + player.maxHP;

            enemy.CalcBoostsUI();
            enemyStrengthText.text = enemy.attack.ToString();
            enemyDmgAmountText.text = enemy.attackBoost.ToString();
            enemyArmorText.text = enemy.armor.ToString();
            enemyHealthBonusText.text = "+" + enemy.healthBoost.ToString();
            enemySpeedText.text = enemy.speed.ToString();
            enemyDodgeChanceText.text = enemy.dodgeBoost.ToString() + "%";
            enemyHealthBarText.text = enemy.currentHP + "/" + enemy.maxHP;
    }
}
