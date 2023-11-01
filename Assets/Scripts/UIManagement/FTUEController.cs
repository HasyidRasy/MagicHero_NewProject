using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FTUEController : MonoBehaviour {
    public string _nameFtue;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FTUEManager.Instance.FTUEActive(_nameFtue);
        }
    }
}
