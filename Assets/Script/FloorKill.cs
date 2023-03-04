using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorKill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(!GameControll.Instance.CheckStartPlayer) return;
        GameControll.Instance.switchCamera.cameraPositionChange(1);
        LookAtTargetCamera.Instance.CheckActiveFollow = false;
        GameControll.Instance.ThrowPlayer = Vector3.zero;
        LookAtTargetCamera.Instance.TimelineDestroy();
        GameControll.Instance.Player.GetComponent<RigidbodyFirstPersonController>().CheckDestroy = true;
    }
}
