using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class Arrow : MonoBehaviour
    {
        [SerializeField]private GameObject Target;
        private void Update() {
            if(Target!=null){
                transform.LookAt(Target.transform);
            }
        }
        public void FindKey(){
            Target = GameControll.Instance.Keys.transform.GetChild(Random.Range(0,GameControll.Instance.Keys.childCount)).gameObject;
            for(int i = 0 ; i < GameControll.Instance.Keys.transform.childCount; i++){
                GameObject g = GameControll.Instance.Keys.transform.GetChild(i).gameObject;
                if(g.activeInHierarchy){
                    Target = g;
                    return;
                }
            }
            gameObject.SetActive(false);
        }
    
    }