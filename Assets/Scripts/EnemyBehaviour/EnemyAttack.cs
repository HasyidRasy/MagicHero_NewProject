using UnityEngine;

public class EnemyAttack : MonoBehaviour {
    private float damageAmount;

    private void Start() {
        EnemyController enemyController = GetComponentInParent<EnemyController>();

        if (enemyController != null) {
            damageAmount = enemyController.damageAmount;
        } else {
            Debug.LogError("EnemyController not found on the parent GameObject of EnemyAttack.");
        }
    }


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            NewPlayerController1 playerController = other.gameObject.GetComponentInParent<NewPlayerController1>();
            if (playerController != null) {
                playerController.TakeDamage(damageAmount);
            } else {
                Debug.LogError("PlayerController is null on the player object.");
            }
        }
    }
}
