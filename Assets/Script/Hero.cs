using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ScriptableObject", menuName = "HeroData")]
public class Hero : ScriptableObject
{
    public List<HeroItem> Datas = new List<HeroItem>();
    public HeroItem GetItem(int index){
        HeroItem item = Datas[index];
        return item;
    }   
    public HeroItem GetItemRandom(){
        HeroItem item = Datas[Random.Range(0, Datas.Count)];
        return item;
    }
}
[System.Serializable]
public class HeroItem {
    public int id;
    public string name;
    public Material materialhero;
    public Sprite spritehero;
    public float type;
    public int money;
}
