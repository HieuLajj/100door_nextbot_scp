using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
[CreateAssetMenu(fileName = "ScriptableObject", menuName = "NextbotData")]
public class NextbotsData : ScriptableObject
{
    public List<Item> Datas = new List<Item>();
    public Item GetItem(int index){
        Item item = Datas[index];
        return item;
    }
}
[System.Serializable]
public class Item
{
    public string type;
    public string Name;
    public Sprite Sprite;
    public VideoClip Clip;
    public AudioClip audioClip;

    public Item(string _type, string _Name, Sprite _Sprite, AudioClip _audioClip){
        type = _type;
        Name = _Name;
        Sprite = _Sprite;
        audioClip = _audioClip;
    }
}
