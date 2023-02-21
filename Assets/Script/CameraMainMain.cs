using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMainMain : MonoBehaviour
{
    public float duration = 1f;
    public AnimationCurve curve;
    public void ShakeCameraMain(){
        //StartCoroutine(Shaking());
    }
    // IEnumerator Shaking(){
    //     Vector3 startPosition = transform.position;
    //     float elapsedTime = 0f;
    //     while (elapsedTime < duration){
    //         Debug.Log("HAHAHAHA");
    //         elapsedTime += Time.deltaTime;
    //         transform.position = startPosition + Random.insideUnitSphere;
    //         yield return null;
    //     }
    // }
}
