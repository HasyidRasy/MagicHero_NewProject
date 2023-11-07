using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsynTrigger : MonoBehaviour {
    public string _nameLevelLoad;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            AsynLoader.Instance.TrigerLoadLevel(_nameLevelLoad);
        }
    }
}
