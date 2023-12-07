using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    public static CharacterModel Instance {get; private set;}
    private PlayerController playerController;

    // Private fields to store character properties
    public float healthPoint;
    public float maxHealthPoint = 100;
    public float defence;
    public float attackSpeed;
    public float moveSpeed = 5.0f;
    public float attack;
    public float elementalBonus;
    public float move;
    private float increaseStat;

    //Dashing
    public float rotationSpeed = 10.0f;
    public float dashSpeed = 10.0f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2.0f;

    public List<UpgradeData> chosenUpgrades = new List<UpgradeData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        playerController = FindObjectOfType<PlayerController>();
    }

    // Getter and setter for healthPoint
    public float HealthPoint {
        get { return healthPoint; }
        set {
            healthPoint = value; // Health should be between 0 and 100
        }
    }

    public float MaxHealthPoint{
        get { return maxHealthPoint; }
        set { 
            maxHealthPoint = value; 
        }
    }

    // Getter and setter for defence
    public float Defence {
        get { return defence; }
        set { defence = value; }
    }

    // Getter and setter for attackSpeed
    public float AttackSpeed {
        get { return attackSpeed; }
        set { attackSpeed = value; }
    }

    // Getter and setter for moveSpeed
    public float MoveSpeed {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    // Getter and setter for attack
    public float Attack {
        get { return attack; }
        set { attack = value; }
    }

    // Getter and setter for elementalBonus
    public float ElementalBonus {
        get { return elementalBonus; }
        set { elementalBonus = value; }
    }

    // Getter and setter for rotationSpeed
    public float RotationSpeed {
        get { return rotationSpeed; }
        set { rotationSpeed = value; }
    }

    // Getter and setter for dashSpeed
    public float DashSpeed {
        get { return dashSpeed; }
        set { dashSpeed = value; }
    }

    // Getter and setter for dashDuration
    public float DashDuration {
        get { return dashDuration; }
        set { dashDuration = value; }
    }

    // Getter and setter for dashCooldown
    public float DashCooldown {
        get { return dashCooldown; }
        set { dashCooldown = value; }
    }

    //Apply Upgrades (Avin)
    public void ApplyUpgrade(UpgradeData upgrade)
    {

        foreach(var stat in upgrade.stats)
        {
            switch (stat.upgradeType)
            {
            case UpgradeType.ElementalAttack:
                ElementalBonus += stat.upgradeValueStatic;
                break;
            case UpgradeType.BasicAttack:
                Attack += stat.upgradeValueStatic;
                break;
            case UpgradeType.MaxHealthPoint:
                if(maxHealthPoint == 0){
                    maxHealthPoint = healthPoint;
                }
                MaxHealthPoint += stat.upgradeValueStatic;
                HealthPoint += stat.upgradeValueStatic;
                Debug.Log("Max Health Point: "+maxHealthPoint);
                break;
            case UpgradeType.AttackSpeed:
                AttackSpeed += stat.upgradeValueStatic;
                Debug.Log("Attack Speed :"+AttackSpeed);
                break;
            case UpgradeType.Defense:
                Defence += stat.upgradeValueStatic;
                Debug.Log("Defense: "+defence);
                break;
            case UpgradeType.HealthPoint:
                HealthPoint += stat.upgradeValueStatic;
                if(HealthPoint > MaxHealthPoint)
                {
                    HealthPoint = MaxHealthPoint;
                }
                break;
            default:
                Debug.LogError("Unknown upgrade type: " + stat.upgradeType);
                break;
            }
        }
        
    }

    public void ResetStats()
    {
        healthPoint = 100f;
        maxHealthPoint = 100f;
        defence = 5f;
        attackSpeed = 1f;
        moveSpeed = 20.0f;
        attack = 0f;
        elementalBonus = 0f;
        move = 0f;

        // Dashi
        rotationSpeed = 500f;
        dashSpeed = 25000f;
        dashDuration = 0.35f;
        dashCooldown = 2f;
        SavePlayerStats();
    }
    // Save player stats to PlayerPrefs
    public void SavePlayerStats()
    {
        PlayerPrefs.SetFloat("PlayerHealth", HealthPoint);
        PlayerPrefs.SetFloat("PlayerMaxHealth", MaxHealthPoint);
        PlayerPrefs.SetFloat("PlayerDefence", Defence);
        PlayerPrefs.SetFloat("PlayerAttackSpeed", AttackSpeed);
        PlayerPrefs.SetFloat("PlayerMoveSpeed", MoveSpeed);
        PlayerPrefs.SetFloat("PlayerAttack", Attack);
        PlayerPrefs.SetFloat("PlayerElementalBonus", ElementalBonus);
        for (int i = 0;i < chosenUpgrades.Count; i++)
        {
            PlayerPrefs.SetInt("Upgrade "+i, chosenUpgrades[i].upgradeID);
        }
        // Save PlayerPrefs
        PlayerPrefs.Save();
    }

    // Load player stats from PlayerPrefs
    public void LoadPlayerStats()
    {
        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            HealthPoint = PlayerPrefs.GetFloat("PlayerHealth");
        }

        if (PlayerPrefs.HasKey("PlayerMaxHealth"))
        {
            MaxHealthPoint = PlayerPrefs.GetFloat("PlayerMaxHealth");
        }

        if (PlayerPrefs.HasKey("PlayerDefence"))
        {
            Defence = PlayerPrefs.GetFloat("PlayerDefence");
        }

        if (PlayerPrefs.HasKey("PlayerAttackSpeed"))
        {
            AttackSpeed = PlayerPrefs.GetFloat("PlayerAttackSpeed");
        }

        if (PlayerPrefs.HasKey("PlayerMoveSpeed"))
        {
            MoveSpeed = PlayerPrefs.GetFloat("PlayerMoveSpeed");
        }

        if (PlayerPrefs.HasKey("PlayerAttack"))
        {
            Attack = PlayerPrefs.GetFloat("PlayerAttack");
        }

        if (PlayerPrefs.HasKey("PlayerElementalBonus"))
        {
            ElementalBonus = PlayerPrefs.GetFloat("PlayerElementalBonus");
        }

        for (int i = 0;i < chosenUpgrades.Count; i++)
        {
            if(PlayerPrefs.HasKey("Upgrade "+i))
            {
                foreach(UpgradeData upgrade in chosenUpgrades)
                {
                    if(PlayerPrefs.GetInt("Upgrade "+i) == upgrade.upgradeID)
                    {
                        chosenUpgrades.Add(upgrade);
                    }
                }
            }
        }
    }
}
