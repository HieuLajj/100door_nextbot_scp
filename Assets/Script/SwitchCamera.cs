using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchCamera : MonoBehaviour
{
  public GameObject cameraOne;
  public GameObject cameraTwo;

  AudioListener cameraOneAudioLis;
  AudioListener cameraTwoAudioLis;
  public LookAtTargetCamera lacamera;

  private static SwitchCamera instance;
    public static SwitchCamera Instance{
      get{
          if(instance == null){
            instance = FindObjectOfType<SwitchCamera>();
          }
          return instance;
      }
      private set{
        instance = value;
      }
  }

  public void cameraPositionChange(int camPosition){
    if(camPosition == 0){
      cameraOne.SetActive(true);
      cameraTwo.SetActive(false);
      lacamera.ResetHide();
    }

    if(camPosition == 1){
      cameraTwo.SetActive(true);
      cameraOne.SetActive(false);
    }
  } 

}
