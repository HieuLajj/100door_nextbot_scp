using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SCPMain : NavMeshBase
{
    public bool CheckActive = false;
    private void Update() {
        if(CheckActive){
            ChaseTarget(GameControll.Instance.Player.transform.position);
        }
    }
}
