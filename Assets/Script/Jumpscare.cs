using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumpscare : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            StartCoroutine(Scare());
        }
    }

    IEnumerator Scare(){
        AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.JumpcareTrack);
        UIManager.Instance.Jumpscare.SetActive(true);
        yield return new WaitForSeconds(2f);
        UIManager.Instance.Jumpscare.SetActive(false);
        gameObject.SetActive(false);
    }
}
