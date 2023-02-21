using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonModes : MonoBehaviour
{
    public string Mode;
    private Button _button;
    public ModeItem modeItem;
    [SerializeField] private TMP_Text tMP_Text;
    [SerializeField] private Image imageBtn;
    public GameObject CheckSelectImage;
    void Start()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(()=>{
            SelectModesUI.Instance.DescribeText.text = modeItem.Describe;
            SelectModesUI.Instance.NameText.text = modeItem.name;
            if(SelectModesUI.Instance.preSelectbuttonModes!=null){
                SelectModesUI.Instance.preSelectbuttonModes.CheckSelectImage.SetActive(false);
            }
            SelectModesUI.Instance.preSelectbuttonModes = this;
            CheckSelectImage.SetActive(true);
        });

    }
    public void SetUpData(){
        tMP_Text.text = modeItem.name;
    }

}
