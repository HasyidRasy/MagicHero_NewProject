using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public int id;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) //untuk membuka pintu selama testing
        {
            GameEvents.current.DoorwayTriggerEnter(id); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {          
            GameEvents.current.DoorwayTriggerExit(id);
            NewAudioManager.Instance.bgmSource.Stop();
            NewAudioManager.Instance.PlaySFX("DoorClose"); // Play sfx door close
            NewAudioManager.Instance.PlayBGM("Battle"); //Play bgm battle
        }
    }
}
