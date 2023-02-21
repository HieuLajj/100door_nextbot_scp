using UnityEngine;
using UnityEngine.AI;

public class NavMeshBase : MonoBehaviour
{
    public Animator AnimatorEnemy;
    public NavMeshAgent navMeshAgent;
    public void SetupPosition(Vector3 positionVector){
        navMeshAgent.Warp(positionVector);
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
    }
    public void ChaseTarget(Vector3 positionChase){
        navMeshAgent.SetDestination(positionChase);
    }
    public void ActiveRun(){
        AnimatorEnemy.SetTrigger("chase");
    }
    public void ActiveEat(){
        AnimatorEnemy.SetTrigger("eat");
    }
    public void ActiveSitdown(){
        AnimatorEnemy.SetTrigger("sitdown");
    }
}
