using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public Enemy EnemyChase;
    public SCPEnemy scpenemy;
    public SCPMain scpMain;
    public RigidbodyFirstPersonController PlayerRigidbody;
    private SwitchCamera switchCamera;
    //private LookAtTargetCamera lookAtTargetCamera;
    private void Start() {
        switchCamera = GameControll.Instance.switchCamera;
        PlayerRigidbody = GameControll.Instance.Player.GetComponent<RigidbodyFirstPersonController>();
       // lookAtTargetCamera = GameControll.Instance.Camera2.GetComponent<LookAtTargetCamera>();
    }

    private void OnTriggerEnter(Collider other) {
        if (scpenemy != null && !scpenemy.checkFindTarget){return;}
        if(scpenemy != null && scpenemy.TargetChase != other.gameObject)return;
        if(GameControll.Instance.Mod == 4 && !GameControll.Instance.CheckActiveSCP) return;
        if(other.CompareTag("Player")){
            if (!GameControll.Instance.CheckStartPlayer || PlayerRigidbody.CheckDestroy) return;
            Vector3 throwVector;
            switchCamera.cameraPositionChange(1);
            if(gameObject.CompareTag("SCPmain")){
                LookAtTargetCamera.Instance.CheckActiveFollow = false;
                throwVector = Vector3.zero;
                LookAtTargetCamera.Instance.TimelineDestroy();
            }else{
                LookAtTargetCamera.Instance.CheckActiveFollow = true;
                throwVector = (other.transform.position-transform.position).normalized;
                GameControll.Instance.Camera2.transform.position = transform.position;
            }
            GameControll.Instance.ThrowPlayer = throwVector;
            PlayerRigidbody.CheckDestroy = true;
            if(scpenemy!=null){
                scpenemy.ActiveEat();
            }
            if(scpMain!=null && GameControll.Instance.Mod==5){
                scpMain.ActiveEat();
            }
        }else if(other.CompareTag("Bot")){
            //Vector3 throwVector = (other.transform.position-transform.position).normalized;
            Bot bot = other.GetComponent<Bot>();
            bot.enemyDestroyBot = scpenemy;
            if (bot.CheckDestroy) return;
            //GameControll.Instance.ThrowPlayer = new Vector3(0,0,0);
            bot.CheckDestroy = true;
            if(scpenemy!=null){
                scpenemy.ActiveEat();
            }
        }   
    }
}
