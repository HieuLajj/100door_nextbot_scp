using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Animator AnimatorHand;
    public Animator AnimatorHandPart;
    public void RandomAnimationHand(){
        int h = Random.Range(0,2);
        AnimatorHandPart.SetFloat("randomhand",h);
    }
}
