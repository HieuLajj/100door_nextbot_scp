using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public GameObject ModelBoom;
    [SerializeField] private ParticleSystem BoomParticle;
    public void ActiveBoom(){
        ModelBoom.SetActive(false);
        BoomParticle.Play();
    }
    public void DestroyBoom(){
        BoomParticle.Stop();
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
