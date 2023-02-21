using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTriggerEnemy : MonoBehaviour
{
    public GameObject Point;
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            gameObject.SetActive(false);
            GameControll.Instance.SpawnEnemyInDoor(Point.transform.position);
        }
    }

    public void ResetTriggerEnemy(){
        gameObject.SetActive(true);
    }
}
