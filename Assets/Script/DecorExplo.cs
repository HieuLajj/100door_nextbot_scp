using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorExplo : MonoBehaviour
{
    // Start is called before the first frame update
    public bool check1=false;
    public bool check2= false;
    public Rigidbody rigidbodyDecor;
    public float SpeedForrce= 200;
    public float Radius = 4;

    public bool check3 = false;
    public bool check4 = false;
    public Transform parentDecor;
    public Quaternion RotationPre;
    void Start()
    {
        RotationPre = transform.rotation;
    }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(check1){
    //         if(!check2){
    //             Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z+1f);
    //             rigidbodyDecor.AddExplosionForce(SpeedForrce,pos,Radius);
                
    //             check2 = true;
    //         }
    //     }

    //     if(check3){
    //         if(check4) return;
    //         ResetStatusDecor();
    //         check4 = true;
    //     }
    // }

    public void ExploDecor(){
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z+1f);
        rigidbodyDecor.AddExplosionForce(SpeedForrce,pos,Radius);
    }

    public void ResetStatusDecor(){
        transform.position = parentDecor.transform.position;
        transform.rotation = RotationPre;
    }
}
