using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPanting : MonoBehaviour
{
    // Start is called before the first frame update
    Coroutine coroutine;
    [SerializeField] AudioSource audioSource;
    public float speedAudio = 1f;
    [SerializeField] RigidbodyFirstPersonController rfp;
    private bool checkCoroutine = false;
    void Start()
    {
        audioSource.clip = rfp.Painting; 
    }

    // Update is called once per frame
    void Update()
    {
        if(rfp.movementSettings.ForwardSpeed == 8 && rfp.RunAxis.y > 0){
            if(audioSource.pitch != 0.75f && !checkCoroutine){
                coroutine = StartCoroutine(FadedMusic(0.5f,audioSource.pitch,0.75f));
            }else{
                audioSource.pitch = 0.75f;
            }
        }else if(rfp.movementSettings.ForwardSpeed == 12 && rfp.RunAxis.y > 0){
            //audioSource.pitch = 1.2f;
            if(audioSource.pitch != 1.2f && !checkCoroutine){
                coroutine = StartCoroutine(FadedMusic(0.5f,audioSource.pitch,1.2f));
            }else{
                audioSource.pitch = 1.2f;
            }
        }else{
            //audioSource.pitch = 0.55f;
            if(audioSource.pitch != 0.55f && !checkCoroutine){
                coroutine = StartCoroutine(FadedMusic(0.5f,audioSource.pitch,0.55f));
            }else{
                audioSource.pitch = 0.55f;
            }
        }
    }
    public IEnumerator FadedMusic(float waitTime, float begin, float end){
        checkCoroutine = true;
        if(begin >= end){
            for(float i = begin; i >= end; i-=0.01f){
                audioSource.pitch = i;
                yield return new WaitForSeconds(waitTime);    
            }
        }else{
            for(float i = begin; i <= end; i+=0.01f){
                audioSource.pitch = i;
                yield return new WaitForSeconds(waitTime);    
            }
        }
        checkCoroutine = false;
        audioSource.pitch = end;
    }
}
