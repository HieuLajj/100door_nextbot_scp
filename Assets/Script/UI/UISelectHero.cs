using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UISelectHero : MonoBehaviour
{
    // Start is called before the first frame update
    public Hero HeroDatas;
    public GameObject PreHero;
    public Transform ContainerHeroui;
    public GameObject preSelectBtnHero;

    public NextbotsData NDatas;
    public GameObject PreNextbots;
    public GameObject preSelectBtnNextbot;
    public Transform ContainerNextbotui;
    [SerializeField] private GameObject HeroPanel;
    [SerializeField] private GameObject NextbotPanel;
    [SerializeField] private Button BtnSelectHeroPanel;
    [SerializeField] private Button BtnSelectNextbotPanel;
    public Button NextBtn;
    [SerializeField] private TMP_Text textSelect;
    [SerializeField] private Button customNextbotBtn;


    private static UISelectHero instance;
    public static UISelectHero Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<UISelectHero>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    void Start()
    {
        SpawnHeroUI();
        SpawnNextbotUI();
        BtnSelectHeroPanel.onClick.AddListener(()=>{
            HeroPanel.SetActive(true);
            NextbotPanel.SetActive(false);
            textSelect.text = "CHOOSE SKIN";
        });

        BtnSelectNextbotPanel.onClick.AddListener(()=>{
            HeroPanel.SetActive(false);
            NextbotPanel.SetActive(true);
            textSelect.text = "CHOOSE NEXTBOT";
        });
        NextBtn.onClick.AddListener(()=>{
            // if(UISelectHero.Instance.preSelectBtnHero == null){
            //     NotificationUI.Instance.SendNotofication("Select hero before play");
            //     return;
            // }
            // if( UISelectHero.Instance.preSelectBtnNextbot == null && GameControll.Instance.PathImageSprite == null){
            //     NotificationUI.Instance.SendNotofication("Select nextbot before play");
            //     return;
            // }
            UIManager.Instance.UICanvasInGame.SetActive(true);
            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.StartHorrorTrack);
            GameControll.Instance.PlayGame();
            gameObject.SetActive(false);
            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.ButtonTrack);
        });

        customNextbotBtn.onClick.AddListener(()=>{
            //gameObject.SetActive(false);
            UIManager.Instance.UICanvasCustomNextBot.SetActive(true);
            CustomNextBotUI canvas = UIManager.Instance.UICanvasCustomNextBot.GetComponent<CustomNextBotUI>();
            canvas.ImageNextBotCustom.sprite = null;
        });
    }

    public void SpawnHeroUI(){
        for( int i=0; i < HeroDatas.Datas.Count; i++){
            GameObject g = Instantiate(PreHero,  ContainerHeroui);
            HeroBtn nb = g.GetComponent<HeroBtn>();
            nb.heroItem = HeroDatas.GetItem(i);
            nb.SetUpData();
            if(i==0){
                GameControll.Instance.SkinnedPlayer.material = nb.heroItem.materialhero;
                nb.backgroundSelectHero.SetActive(true);
                UISelectHero.Instance.preSelectBtnHero = g;
            }
        }
    }
    public void SpawnNextbotUI(){
        for( int i=0; i < NDatas.Datas.Count; i++){
            GameObject g = Instantiate(PreNextbots,  ContainerNextbotui);
            NextbotBtn nb = g.GetComponent<NextbotBtn>();
            nb.ItemData = NDatas.GetItem(i);
            nb.SetUpData();
            if(i==0){
                if(nb.ItemData.audioClip != null){
                GameControll.Instance.audioClip = nb.ItemData.audioClip;
                }
                GameControll.Instance.PathImageSprite = nb.ItemData.Sprite;    
                UISelectHero.Instance.preSelectBtnNextbot = g;
                nb.backgroundSelectNextbot.SetActive(true);
            }
        }
    }
}
