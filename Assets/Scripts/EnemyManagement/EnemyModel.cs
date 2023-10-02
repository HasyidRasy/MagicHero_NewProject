using UnityEngine;

public class EnemyModel : MonoBehaviour{

    private int healthPoint = 70;
    private int defence = 5;
    private float attackSpeed = 2f;
    private float moveSpeed = 3f;
    private int attack = 20;
    private int elementalBonus = 10;

    public int HealthPoint{

        get { return healthPoint; }
        //set hp to min 0 max 100
        set { healthPoint = Mathf.Clamp(value, 0, 100); ; }
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

}