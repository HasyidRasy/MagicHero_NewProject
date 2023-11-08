using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrasastiController : MonoBehaviour {
    [SerializeField] private string _prasastiName;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FTUEManager.Instance.PrasastiEnable(_prasastiName);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            FTUEManager.Instance.PrasastiDisable(_prasastiName);
        }
    }
}
