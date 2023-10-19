using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    public static CharacterModel Instance {get; private set;}

    // Private fields to store character properties
    public float healthPoint;
    public float defence;
    public float attackSpeed;
    public float moveSpeed = 5.0f;
    public float attack;
    public float elementalBonus;
    public float move;

    //Dashing
    public float rotationSpeed = 10.0f;
    public float dashSpeed = 10.0f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Getter and setter for healthPoint
    public float HealthPoint {
        get { return healthPoint; }
        set {
            healthPoint = Mathf.Clamp(value, 0, 100); // Health should be between 0 and 100
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
        switch (upgrade.upgradeType)
        {
        case UpgradeType.ElementalAttack:
            elementalBonus += upgrade.upgradeValue;
            Debug.Log("Elemental Bonus: "+elementalBonus);
            break;
        case UpgradeType.BasicAttack:
            attack += upgrade.upgradeValue;
            Debug.Log("Attack: "+Attack);
            break;
        case UpgradeType.HealthPoint:
            healthPoint += upgrade.upgradeValue;
            Debug.Log("Health Point: "+healthPoint);
            break;
        case UpgradeType.AttackSpeed:
            attackSpeed += upgrade.upgradeValue;Debug.Log("Elemental Bonus: "+elementalBonus);
            Debug.Log("Attack Speed :"+AttackSpeed);
            break;
        case UpgradeType.Defense:
            defence += upgrade.upgradeValue;
            Debug.Log("Defense: "+defence);
            break;
        default:
            Debug.LogError("Unknown upgrade type: " + upgrade.upgradeType);
            break;
        }
    }
}
