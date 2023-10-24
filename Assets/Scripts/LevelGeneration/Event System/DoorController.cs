using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openHeight = -4f;
    private bool isDoorOpen = true;
    private bool isAreaCleared = true;
    private Vector3 targetPosition; // The target position for smooth movement.
    [SerializeField] private float doorSpeed = 5f;
    public int id;

    private void Start()
    {
        GameEvents.current.onDoorwayTriggerEnter += OnDoorwayOpen;
        GameEvents.current.onDoorwayTriggerExit += OnDoorwayClose;

        // Initialize the target position to the current position.
        targetPosition = transform.position;
    }

    private void Update()
    {
        // Use Lerp to smoothly transition the door's position.
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * doorSpeed); // Adjust the speed (5f) as needed.
    }

    private void OnDoorwayOpen(int id)
    {
        if (id == this.id)
        {
            if (!isDoorOpen && !isAreaCleared)
            {
                targetPosition = transform.position - new Vector3(0f, openHeight, 0f);
                isDoorOpen = true;
                NewAudioManager.Instance.PlaySFX("DoorOpen");
            }
        }
    }

    private void OnDoorwayClose(int id)
    {
        if (id == this.id)
        {
            if (isDoorOpen && isAreaCleared)
            {
                targetPosition = transform.position + new Vector3(0f, openHeight, 0f);
                isDoorOpen = false;
                isAreaCleared = false;
                NewAudioManager.Instance.bgmSource.Stop();
                NewAudioManager.Instance.PlaySFX("DoorClose"); // Play sfx door close
                NewAudioManager.Instance.PlayBGM("Battle"); //Play bgm battle
            }
        }
    }
}
