using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareCeiling : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LayerMask ground;
    public ActiveSCPMain activeSCPMain;
    [SerializeField] Transform groundCheck;
    public ParticleSystem ParticleSystemFire;
    public bool Active = false;
    public Rigidbody rigidbodyBoxFire;
    // Update is called once per frame
    void Update()
    {
        if(CheckGround()){
            if(!Active){
                Boooom();
            }
        }
        if(rigidbodyBoxFire.useGravity && !CheckGround()){
            rigidbodyBoxFire.AddForce(Vector3.down * 2, ForceMode.VelocityChange);
        }
    }

    public bool CheckGround(){
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
    public void Boooom(){
        ParticleSystemFire.Play();
        Active = true;
        activeSCPMain.SCPactive();
    }
}
