using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class NextbotBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public Item ItemData;
    public VideoPlayer videoPlayer;
    public GameObject backgroundSelectNextbot;
    public Image image;
    public TMP_Text YourNameText;
    public Button button;
    private void Start() {
        button.onClick.AddListener(()=>{
            if(UISelectHero.Instance.preSelectBtnNextbot!=null){
                UISelectHero.Instance.preSelectBtnNextbot.GetComponent<NextbotBtn>().backgroundSelectNextbot.SetActive(false);
            }else{
                NotificationUI.Instance.HideNotification();
            }
            backgroundSelectNextbot.SetActive(true);
            UISelectHero.Instance.preSelectBtnNextbot = this.gameObject;


            GameControll.Instance.PathImageSprite = null;
            GameControll.Instance.PathVideo = null;
            GameControll.Instance.linkVideo = null;
            GameControll.Instance.audioClip = null;
            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.ButtonTrack);
            if(ItemData.audioClip != null){
                GameControll.Instance.audioClip = ItemData.audioClip;
            }
            if(ItemData.type == "image"){
                GameControll.Instance.PathImageSprite = ItemData.Sprite;    
                return;           
            }
            if(ItemData.type == "video"){
                GameControll.Instance.linkVideo = ItemData.Clip; 
                return;
            }
            if(ItemData.type == "videourl"){
                PickFileAndroid.Instance.PickVideo();
                return;
            }
            if(ItemData.type == "imageurl"){
                PickFileAndroid.Instance.PickImage();
                return;
            }
        });
    }

    public void SetUpData(){
        if(ItemData.type == "image" || ItemData.type == "imageurl" || ItemData.type == "videourl" ){
            videoPlayer.gameObject.SetActive(false);
            image.sprite = ItemData.Sprite;
            return;
        }
        if(ItemData.type == "video"){
            image.gameObject.SetActive(false);
            videoPlayer.clip = ItemData.Clip;
            return;
        }
    }
}
