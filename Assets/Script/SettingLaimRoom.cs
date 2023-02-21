using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingLaimRoom : MonoBehaviour
{
    public void SetupLightRoom(){
        for(int i=0 ; i < transform.childCount; i++){
            LightItem light = transform.GetChild(i).GetComponent<LightItem>();
            if(light.lightObject==null){
                light.lightObject = GameControll.Instance.InstanceLight();
                light.lightObject.transform.position = transform.GetChild(i).transform.position;
            }
            // else{
            //     light.lightObject.SetActive(true);
            //     light.lightObject.transform.position = transform.GetChild(i).transform.position;
            // }
        }
    }

    public void ResetLightRoom(){
        for(int i=0 ; i < transform.childCount; i++){
            LightItem light = transform.GetChild(i).GetComponent<LightItem>();
            if(light.lightObject!=null){
                LightPoint lightPoint = light.lightObject.GetComponent<LightPoint>();
                lightPoint.animator.SetFloat("Light",0);
                light.lightObject.SetActive(false);
                light.lightObject = null;
            }
        }
    }
}
