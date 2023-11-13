using UnityEngine;

public class EnemyModel : MonoBehaviour{

    public int healthPoint = 100;
    public int defence = 5;
    public float attackSpeed = 2f;
    public float moveSpeed = 3f;
    public int attack = 20;
    public int elementalBonus = 10;
    public float currentHealth;

    public int HealthPoint{

        get { return healthPoint; }
        //set hp to min 0 max 100
        set { healthPoint = Mathf.Clamp(value, 0, 100); }
    }
    public int Defence{
        get { return defence; }
        set { defence = value; }
    }
    public float AttackSpeed{

        get { return attackSpeed; }
        set { attackSpeed = value; }
    }
    public float MoveSpeed {

        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    public int Attack {

        get { return attack; }
        set { attack = value; }
    }

    public int ElementalBonus{

        get { return elementalBonus; }
        set { elementalBonus = value; }
    }

    public float CurrentHealth
    {

        get { return currentHealth; }
        set { currentHealth = value; }
    }

}
