using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openHeight = 4f;
    [SerializeField] private bool isDoorOpen = true;
    [SerializeField] private bool isAreaCleared = true;

    public int id;
    private void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayClose;
        GameEvents.current.onDoorwayTriggerExit += OnDoorwayOpen;
    }
    private void OnDoorwayOpen(int id)
    { 
        if(id == this.id)
        {
            if (!isDoorOpen && !isAreaCleared)
                transform.position = transform.position - new Vector3(0f, openHeight, 0f);
                isDoorOpen = true;
        }
    }
    private void OnDoorwayClose(int id)
    {
        if(id == this.id)
        {
            if (isDoorOpen && isAreaCleared)
                transform.position = transform.position + new Vector3(0f, openHeight, 0f);
                isDoorOpen = false;
                isAreaCleared = false;
        }
    }
}
