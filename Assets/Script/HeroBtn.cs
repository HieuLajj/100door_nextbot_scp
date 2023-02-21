using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public HeroItem heroItem;
    public GameObject backgroundSelectHero;
    public Image image;
    public TMP_Text TextInfo;
    public Button button;

    public int AfterType; 
    void Start()
    {
        button.onClick.AddListener(()=>{
            if(AfterType==2){
                if(GameControll.Instance.SavePlayerMain.coin >= heroItem.money){
                    AfterType = 0;
                    TextInfo.gameObject.SetActive(false);
                    GameControll.Instance.SavePlayerMain.coin -= heroItem.money;
                    GameControll.Instance.SavePlayerMain.heros.Add(heroItem.id);
                    PlayerPrefsExtra.SetObject("HieulajjNextdoor",GameControll.Instance.SavePlayerMain);
                }else{
                }
                return;
            }
            GameControll.Instance.SkinnedPlayer.material = heroItem.materialhero;
            if(UISelectHero.Instance.preSelectBtnHero!=null){
                UISelectHero.Instance.preSelectBtnHero.GetComponent<HeroBtn>().backgroundSelectHero.SetActive(false);
            }else{
                NotificationUI.Instance.HideNotification();
            }
            backgroundSelectHero.SetActive(true);
            UISelectHero.Instance.preSelectBtnHero = this.gameObject;
            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.ButtonTrack);
        });
    }

    public void SetUpData(){
        image.sprite = heroItem.spritehero;
        switch(heroItem.type){
            case 0:
                TextInfo.gameObject.SetActive(false);
                AfterType = 0;
                break;
            case 1:
                TextInfo.text = "UNLOCK BY AD";
                AfterType = 1;
                break;
            case 2:
                if(!GameControll.Instance.SavePlayerMain.heros.Contains(heroItem.id)){
                    TextInfo.text = heroItem.money + "SOULS";
                    AfterType = 2;
                }else{
                    TextInfo.gameObject.SetActive(false);
                    AfterType = 0;
                }
                break;
        }
    }
}
