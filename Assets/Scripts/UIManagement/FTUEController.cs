using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEController : MonoBehaviour {
    public int id;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FTUEManager.Instance.FTUEActive(id);
        }
    }
}
