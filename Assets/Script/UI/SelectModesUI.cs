using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectModesUI : MonoBehaviour
{
    public Modes ModeDatas;
    public GameObject PreMode;
    public Transform ContainerModeui;
    public TMP_Text DescribeText;
    public TMP_Text NameText;
    private static SelectModesUI instance;
    public ButtonModes preSelectbuttonModes;
    public Button NextBtn;
    public static SelectModesUI Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<SelectModesUI>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    private void Start() {
        SpawnModeUI();
        NextBtn.onClick.AddListener(()=>{
            switch (preSelectbuttonModes.modeItem.name)
            {
                case "SCP":
                    GameControll.Instance.Mod = 4;
                    //UIManager.Instance.text_InHero.text = "Start";
                    break;
                case "Door":
                    GameControll.Instance.Mod = 5;
                    //UIManager.Instance.text_InHero.text = "Start";
                    break;
                default :
                    break;
            }
            UIManager.Instance.UICanvasSelectModes.SetActive(false);
            UIManager.Instance.UICanvasSelectHero.SetActive(true);


            Souls.Instance.TextCoin.text = GameControll.Instance.SavePlayerMain.coin+"";



            AudioManager.Instance.SFXSource.PlayOneShot(AudioManager.Instance.ButtonTrack);
        });
    }
    
    public void SpawnModeUI(){
        for( int i=0; i < ModeDatas.Datas.Count; i++){
            GameObject g = Instantiate(PreMode,  ContainerModeui);
            ButtonModes nb = g.GetComponent<ButtonModes>();
            nb.modeItem = ModeDatas.GetItem(i);
            nb.SetUpData();
            if(i==0){
                SelectModesUI.Instance.preSelectbuttonModes = nb;
                nb.CheckSelectImage.SetActive(true);
                SelectModesUI.Instance.DescribeText.text = nb.modeItem.Describe;
                SelectModesUI.Instance.NameText.text = nb.modeItem.name;
            }
        }
    }
}
