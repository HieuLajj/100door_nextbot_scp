using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDecor : MonoBehaviour
{
    public List<DecorExplo> DecorList;
    public bool Check = false;
    private void OnTriggerEnter(Collider other) {
        if(!Check){
            ActiveDecorListExplo();
            Check = true;
        }
    }
    public void ActiveDecorListExplo(){
        for(int i=0 ; i < DecorList.Count; i++){
            DecorList[i].ExploDecor();
        }
    }
    public void ResetActive(){
        Check = false;
        for(int i=0 ; i < DecorList.Count; i++){
            DecorList[i].ResetStatusDecor();
        }
    }
}
