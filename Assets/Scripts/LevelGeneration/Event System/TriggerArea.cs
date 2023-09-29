using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public int id;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameEvents.current.DoorwayTriggerEnter(id);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameEvents.current.DoorwayTriggerExit(id);
    }

    
}
