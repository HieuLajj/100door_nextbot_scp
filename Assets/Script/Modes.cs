using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableObject", menuName = "ModeData")]
public class Modes : ScriptableObject
{
    public List<ModeItem> Datas = new List<ModeItem>();
    public ModeItem GetItem(int index){
        ModeItem item = Datas[index];
        return item;
    }
    public ModeItem GetItemRandom(){
        ModeItem item = Datas[Random.Range(0, Datas.Count)];
        return item;
    }
}
[System.Serializable]
public class ModeItem{
    public string name;
    public string Describe;
}
