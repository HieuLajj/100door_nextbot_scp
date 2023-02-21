using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public Map map;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            switch(map.GamePlay){
                case 5:
                    GameControll.Instance.SCPMain.SetActive(false);
                    PlayerController.Instance.SpawnEyesInWall.SetActive(false);
                    AudioManager.Instance.LongSource.Stop();
                    break;
                case 2:
                    GameControll.Instance.DisplayEnemy();
                    break;

            }
        }   
    }
}
