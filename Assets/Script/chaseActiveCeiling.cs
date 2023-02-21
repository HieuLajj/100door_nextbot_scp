using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaseActiveCeiling : MonoBehaviour
{
    
    public JumpscareCeiling jumpscareCeiling;
    public void Setup(){
        jumpscareCeiling.Active = false;
        jumpscareCeiling.rigidbodyBoxFire.useGravity = false;
        jumpscareCeiling.rigidbodyBoxFire.gameObject.transform.position = transform.position;
    }
    public void ActiveBoxFire(){
        jumpscareCeiling.rigidbodyBoxFire.useGravity = true;
    }
}
