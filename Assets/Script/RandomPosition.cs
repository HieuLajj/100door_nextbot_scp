using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    public int Index;
    public Enemy enemyPos;
    public void Send(){
        GameControll.Instance.PositionMap.Add(this);
    }
}
