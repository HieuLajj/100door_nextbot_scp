using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button SpeedButton;
    public bool CheckSpeed;
    public RigidbodyFirstPersonController _rigidbodyFirstPersonController;
    // public PlayerController playerController;
    public Image mSpeed;
    public Image mLight;
    public Button LightButton;
    public bool CheckLight;
    public GameObject LightSpot;
    public PlayerStatus playerStatus;
    public Button findKeysBtn;
    public Arrow arrow;
    public GameObject KeyPlayer;
    void Start()
    {
        SpeedButton.onClick.AddListener(()=>{
            if(!CheckSpeed && playerStatus.checkStamina){
                CheckSpeed = true;
                RaiseS();
            }else if(CheckSpeed){
                CheckSpeed = false;
                ResetS();
            }
        });

        LightButton.onClick.AddListener(()=>{
           CheckLight = !CheckLight;
            if(CheckLight){
                OnLight();
            }else{
                OffLight();
            }
            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.SwitchFlastlight);
        });

        findKeysBtn.onClick.AddListener(()=>{
            StartCoroutine(StartArrow(15));
        });
    }

    public IEnumerator StartArrow(float delay)
    {
        arrow.gameObject.SetActive(true);
        arrow.FindKey();
        findKeysBtn.interactable = false;
        yield return new WaitForSeconds(delay);
        arrow.gameObject.SetActive(false);
        findKeysBtn.interactable = true;
    }
    public void OnLight(){
        LightSpot.SetActive(true);
        mLight.color  = Color.yellow;
    }
    public void OffLight(){
        LightSpot.SetActive(false);
        mLight.color  = Color.white;
    }
    public void ResetS(){
        _rigidbodyFirstPersonController.ResetSpeed();
        mSpeed.color = Color.white;
    }
    public void RaiseS(){
        _rigidbodyFirstPersonController.RaiseSpeed();
        mSpeed.color = Color.yellow;
    }
    
    public void ActiveKey(){
        KeyPlayer.SetActive(true);
        arrow.gameObject.SetActive(false);
        findKeysBtn.interactable = true;
    }
    public void InActiveKey(){
        KeyPlayer.SetActive(false);
        arrow.gameObject.SetActive(false);
    }

    public void ResetFindKey(){
        findKeysBtn.interactable = true;
        arrow.gameObject.SetActive(false);
    }

}