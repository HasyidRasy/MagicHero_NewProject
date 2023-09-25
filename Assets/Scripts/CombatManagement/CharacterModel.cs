using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour
{
    // Private fields to store character properties
    public float healthPoint;
    private float defence;
    private float attackSpeed;
    public float moveSpeed = 5.0f;
    private float attack;
    private float elementalBonus;

    //Dashing
    public float rotationSpeed = 10.0f;
    public float dashSpeed = 10.0f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2.0f;

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
}
