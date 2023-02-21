using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSCPMain : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform positionSCPMain;
    public Transform positionCamera;
    public chaseActiveCeiling activeCeilingchase;
    public int flag = 0;
    public Map map;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") && flag ==0){
            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.ThunderTrack);
            activeCeilingchase.ActiveBoxFire();
            Invoke("DelayHand",9);
            flag--;
        }
    }
    public void DelayHand(){
        map.ActiveHand();
    }

    public void SCPactive(){
        StartCoroutine(SCPactiveCRT(0.5f));
    }

    public IEnumerator SCPactiveCRT(float delay){
        yield return new WaitForSeconds(delay);
        SCPMain sCPMain = GameControll.Instance.SCPMain.GetComponent<SCPMain>();
        sCPMain.SetupPosition(positionSCPMain.position);
        sCPMain.gameObject.SetActive(true);
        GameControll.Instance.Camera2.transform.position = positionCamera.position;
        GameControll.Instance.Camera2.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        TimelineStart.Instance.camera3.transform.position =  new Vector3(positionCamera.position.x,positionCamera.position.y+0.6f,positionCamera.position.z);
        TimelineStart.Instance.camera3.transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        TimelineStart.Instance.ActiveTimeline();
    }
}
