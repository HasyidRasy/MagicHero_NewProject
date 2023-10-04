using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openHeight = 4f;
    private bool isDoorOpen = true;

    public int id;
    private void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit += OnDoorwayClose;
    }
    private void OnDoorwayOpen(int id)
    { 
        if(id == this.id)
        {
            if (!isDoorOpen)
                transform.position = transform.position + new Vector3(0f, openHeight, 0f);
                isDoorOpen = true;
        }
    }
    private void OnDoorwayClose(int id)
    {
        if(id == this.id)
        {
            if (isDoorOpen)
                transform.position = transform.position - new Vector3(0f, openHeight, 0f);
                isDoorOpen = false;
        }
    }
}
