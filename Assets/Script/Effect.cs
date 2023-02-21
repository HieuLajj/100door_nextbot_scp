using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering.PostProcessing;
public class Effect : MonoBehaviour
{
    private Vignette vignette;
    //private Grain grain;
    //public CameraFilterPack_TV_Horror CameraFilterPack_TV_Horror;
    //public FrostEffect frostEffect;
    public PostProcessVolume postProcessVolume;
    private static Effect instance;
    //private bool check = false;
    public static Effect Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<Effect>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    private void Start() {
    postProcessVolume.profile.TryGetSettings<Vignette>(out vignette);
    //     //postProcessVolume.profile.TryGetSettings<Grain>(out grain);
    //     // postProcessVolume.profile.TryGetSettings<ChromaticAberration>(out chromaticAberration);
    // }
    // IEnumerator Faded(){
    //     for(float f = 0.0f; f <= 1; f += 0.1f){
        
    //         vignette.intensity.value = f;
    //         yield return new WaitForSeconds(0.2f);
    //     }
    //     GameControll.Instance.RigidbodyFirstPersonPlayer.ResetGamePlayer();
    //     UIManager.Instance.TextNotifi.text = "That bai";
    //     UIManager.Instance.UICanvasReStartGame.SetActive(true);
    //     GameControll.Instance.DisableChildEnemyPlayerKeys();
    //     vignette.intensity.value = 0; 
    // }
    // public void StartFaded(){
    //     StartCoroutine(Faded());
    }
    public void Changevignette( float h){
    //    CameraFilterPack_TV_Horror.Fade = h;
        //frostEffect.FrostAmount = h/3;
        vignette.intensity.value = h/2;
    }
}
