using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimationEnemy : MonoBehaviour
{
    public SCPEnemy scpenemy;
    public void ActiveZoombie(){
        if(scpenemy!=null){
            if(!GameControll.Instance.CheckActiveSCP){
            GameControll.Instance.CheckActiveSCP = true;
            }
            scpenemy.checkFindTarget = true;
        }
    }

    public void ActiveZoombie2(){
        if(scpenemy!=null){
            scpenemy.checkFindTarget = true;
        }
    }
}
