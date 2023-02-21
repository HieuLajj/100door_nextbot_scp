using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin1 : MonoBehaviour
{
   [SerializeField]
   private Door Door;
   private void OnTriggerEnter(Collider other) {
    if(other.CompareTag("Player")){
        if(GameControll.Instance.KeyInt == 1 && GameControll.Instance.Mod ==3){
            if (!Door.IsOpen)
            {
                Door.Open(other.transform.position);
            }
            GameControll.Instance.WinGame();
        }
    } 
   }
}
