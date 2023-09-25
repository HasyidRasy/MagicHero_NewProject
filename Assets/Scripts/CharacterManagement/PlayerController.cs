using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //get model
    private CharacterModel characterModel;
    //cek dash logic
    private bool isDashing = false;

    private void Start() {
        //get component from character model
        characterModel = GetComponent<CharacterModel>();
    }

    private void Update() {
        //Call Function
        CharaMove();
        PlayerStat();
    }

    private void CharaMove() {
        //input movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        //move direction
        Vector3 moveDir = new Vector3(horizontal, 0f, vertical).normalized;

        //move
        transform.position += characterModel.MoveSpeed * Time.deltaTime * moveDir;
        //rotation
        if (moveDir != Vector3.zero) {
            transform.forward = Vector3.Slerp(transform.forward, moveDir, characterModel.RotationSpeed * Time.deltaTime);
        }
        //Call Coroutine Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)) {
            StartCoroutine(Dash());
        }
    }

    //Coroutine Dash Logic
    private IEnumerator Dash() {
        isDashing = true;
        float startTime = Time.time;
        Vector3 pos = transform.position;
        Vector3 dashDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        while (Time.time < startTime + characterModel.DashDuration) {
            transform.position += characterModel.DashSpeed * Time.deltaTime * dashDir;
            yield return null;
        }

        yield return new WaitForSeconds(characterModel.DashCooldown);
        isDashing = false;
    }

    //Player Stat
    private void PlayerStat() {
        float playerHp = characterModel.HealthPoint;
        float playerDef = characterModel.Defence;
    }
}
