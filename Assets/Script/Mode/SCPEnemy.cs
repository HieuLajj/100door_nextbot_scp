using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SCPEnemy : NavMeshBase
{
    public GameObject TargetChase;
    public bool checkFindTarget = false;
    void Update()
    {
        if(GameControll.Instance.CheckActiveSCP){
            if(checkFindTarget && TargetChase==null){
                ChooseTarget();
            }
            if(TargetChase!=null){
                ChaseTarget(TargetChase.transform.position);
            }
        }
    }
    public void ChooseTarget()
    {
        TargetChase = null;
        int a = 0;
        int y = 0;
        do
        {
            y++;
            int i = Random.Range(0,GameControll.Instance.TargetforEnemy.Count);
            if(GameControll.Instance.CheckInvisible && GameControll.Instance.TargetforEnemy[i].CompareTag("Player")){a=0;}
            else if(GameControll.Instance.TargetforEnemy[i].activeInHierarchy){
                TargetChase = GameControll.Instance.TargetforEnemy[i];
                a=1;
            }
        }
        while(a==0 && y<50);                 
    }
}
