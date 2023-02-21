using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatus : MonoBehaviour
{
    public float Stamina = 100;
    public float Energy = 100;
    public RigidbodyFirstPersonController RFP;
    public UIPlayerManager UIPlayer;
    public Slider StaminaSlider;
    public Slider EnergySlider;
    public float _preStamina = 2000;
    public float _preEnergy = 100;
    public bool checkStamina = true;

    // Update is called once per frame
    void Update()
    {
        if(UIPlayer.CheckSpeed && RFP.RunAxis != Vector2.zero){
            Stamina -= Time.deltaTime*10;
        }else{
            Stamina += Time.deltaTime*10;
        }
        if(Stamina > 100) {
            Stamina = 100;
            checkStamina = true;
        }else if(Stamina < 0) {
            checkStamina = false;
            Stamina = 0;
            UIPlayer.CheckSpeed = false;
            UIPlayer.ResetS();
        };
        StaminaSlider.value = Stamina;


        if(UIPlayer.CheckLight){
            Energy -= Time.deltaTime * 5;
        }
        if(Energy <0 ){
            Energy = 0;
            UIPlayer.CheckLight = false;
            UIPlayer.OffLight();
        }
        StaminaSlider.value = Stamina;
        EnergySlider.value = Energy;
    }
    public void ResetStatus(){
        UIPlayer.CheckSpeed = false;
        UIPlayer.CheckLight = false;
        UIPlayer.ResetS();
        UIPlayer.OffLight();
        Stamina = _preStamina;
        Energy = _preEnergy;
        PlayerController.Instance.floatingJoystick.ResetJoystick();
        // playerController.handle.anchoredPosition = Vector2.zero;
    }
}
