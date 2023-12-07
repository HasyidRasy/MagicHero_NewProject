using UnityEngine;

public class EnemyModel : MonoBehaviour {
    public static EnemyModel Instance;

    public int healthPoint = 100;
    public int defence = 5;
    public float attackSpeed = 2f;
    public float moveSpeed = 3f;
    public int attack = 20;
    public int elementalBonus = 10;
    public float currentHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (PlayerPrefs.HasKey("EnemyHealth") && PlayerPrefs.HasKey("EnemyDefence") && PlayerPrefs.HasKey("EnemyAttack")) {
            SaveEnemyStats();
        } else {
            ResetEnemyStats();
        }
    }

    private void Start()
    {
        if (LevelManager.Instance.currentLevel > 1)
        {
            EnemyStatsUp(LevelManager.Instance.currentLevel);
        }
    }

    public int HealthPoint {
        get { return healthPoint; }
        //set hp to min 0 max 100
        set { healthPoint = value; }
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
    public void EnemyStatsUp(int level)
    {
        HealthPoint += (level - 1) * 75 ;
        Attack += (level - 1) * 4;
        Defence += (level - 1) * 3;

        Debug.Log("Enemy stats up");
    }

    public void ResetEnemyStats()
    {
        healthPoint = 120;
        attack = 20;
        defence = 5;
        SaveEnemyStats();
    }

    public void SaveEnemyStats()
    {
        PlayerPrefs.SetInt("EnemyHealth", HealthPoint);
        PlayerPrefs.SetInt("EnemyDefence", Defence);
        PlayerPrefs.SetInt("EnemyAttack", Attack);
        PlayerPrefs.Save();
    }

    public void LoadEnemyStats()
    {
        HealthPoint = PlayerPrefs.GetInt("EnemyHealth");
        Defence = PlayerPrefs.GetInt("EnemyDefence");
        Attack = PlayerPrefs.GetInt("EnemyAttack");
        SaveEnemyStats();
    }
}
