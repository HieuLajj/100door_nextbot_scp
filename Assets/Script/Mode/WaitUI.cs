using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaitUI : MonoBehaviour
{
    [SerializeField] private TMP_Text tMP_Text;
    [SerializeField] private RigidbodyFirstPersonController rigidbodyFirstPersonController;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private Rigidbody m_RigidBody;
    public void SetupWaitTime(){
        gameObject.SetActive(true);
        
        StartCoroutine(WaitTime(5));
    }
    IEnumerator WaitTime(float currentTime){
        while(currentTime>0){
            yield return null;
            currentTime = currentTime -= Time.deltaTime;
            
            SetTimerText(currentTime);
        }
        gameObject.SetActive(false);
        m_RigidBody.velocity = Vector3.zero;
        rigidbodyFirstPersonController.gameObject.transform.position = GameControll.Instance.GetIndex().transform.position;
        playerStatus.ResetStatus();
        rigidbodyFirstPersonController.ResetPlayer();
        GameControll.Instance.CheckStartPlayer = true;
        SwitchCamera.Instance.cameraPositionChange(0);
        GameControll.Instance.ResetBoomVsSCP();
        ModeSCPUI.Instance.ResetUIWhenDieMod4();
        UIManager.Instance.UICanvasInGame.SetActive(true);
        
        
        
        ModeSCPUI.Instance.boomWait.DestroyBoom();
    }
    private void SetTimerText(float h){
        tMP_Text.text = h.ToString("0");
    }
    
}
