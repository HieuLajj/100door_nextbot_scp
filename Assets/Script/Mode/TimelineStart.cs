using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class TimelineStart : MonoBehaviour
{
    private static TimelineStart instance;
    public GameObject camerabrain;
    public GameObject cameramain;
    public GameObject camera3;
    public GameObject CanvasPlayerInGame;
    public SCPMain scpMain;
    public FixedButton fixedButton;
    public static TimelineStart Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<TimelineStart>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    public PlayableDirector pd;
    public void ActiveTimeline(){
        PreActive();
        pd.Play();
    }
    private void PreActive(){
        CanvasPlayerInGame.SetActive(false);
        cameramain.SetActive(false);
        camerabrain.SetActive(true);
        PlayerController.Instance.SpawnEyesInWall.SetActive(true);
        AudioManager.Instance.LongSource.PlayOneShot(AudioManager.Instance.DoorOSTTrack);
        PlayerController.Instance.floatingJoystick.ResetJoystick();
        fixedButton.Pressed = false;
    }

    public void AfterActive(){
        CanvasPlayerInGame.SetActive(true);
        cameramain.SetActive(true);
        camerabrain.SetActive(false);
        scpMain.CheckActive = true;
        scpMain.ActiveRun();
        GameControll.Instance.ActionAnimationLight();
        //PlayerController.Instance.SpawnEyesInWall.SetActive(false);
    }
    public void ResetStop(){
        pd.Stop();
        camerabrain.SetActive(false);
    }
}
