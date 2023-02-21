using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTargetCamera : MonoBehaviour
{
    public Transform Target;
    public Transform Obstruction;
    //float zoomSpeed = 2f;
    public List<GameObject> ObjectsHide;
    [SerializeField] private float Speed;
    public Camera camera2;
    public bool CheckActiveFollow = false;

    private static LookAtTargetCamera instance;
    public static LookAtTargetCamera Instance{
        get{
            if(instance == null){
                instance = FindObjectOfType<LookAtTargetCamera>();
            }
            return instance;
        }
        private set{
            instance = value;
        }
    }
    // private void Start() {  
    //     Obstruction = Target;
    // }
    void Update()
    {
        // if(GameControll.Instance.Mod != 4){
        //     if(GameControll.Instance.Player.activeInHierarchy){
        //         camera2.fieldOfView = 60;
        //         // transform.LookAt(Target); 
        //         //ViewObstructed2();
        //     }
        // }
        if(GameControll.Instance.Player.activeInHierarchy && CheckActiveFollow){
            transform.LookAt(GameControll.Instance.Player.transform);
        }
    }
    void ViewObstructed2(){

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, Target.position - transform.position, 10f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.gameObject.tag == "Player") return;
            if (hit.collider.gameObject.tag == "notdisable") return;
            if (hit.collider.gameObject.tag == "Floor") return;
          
                Obstruction = hit.transform;
                if(Obstruction.gameObject?.GetComponent<MeshRenderer>()){
                    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                    if(!ObjectsHide.Contains(Obstruction.gameObject)){
                        ObjectsHide.Add(Obstruction.gameObject);
                    }
                }
                else{
                    Transform tmp = Obstruction.gameObject.transform;
                    int h = tmp.childCount ;
                    if(h >0){
                        for(int j=0; j< h; j++){
                            if(tmp.GetChild(j)?.GetComponent<MeshRenderer>()){
                                tmp.GetChild(j).GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                                //ObjectsHide.Add(tmp.GetChild(i).gameObject);
                                if(!ObjectsHide.Contains(tmp.GetChild(j).gameObject)){
                                    ObjectsHide.Add(tmp.GetChild(j).gameObject);
                                }
                            }
                        }
                    }
                }
            
        }
        
    }
    public void ResetHide(){
        for(int i=0 ; i< ObjectsHide.Count; i++){
            ObjectsHide[i].GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        }
        ObjectsHide.Clear();
    }

    public void TransformCamera(){
        transform.position = Vector3.Slerp(transform.position, new Vector3(Target.position.x, Target.position.y + 5f,Target.position.z), 1000 * Time.deltaTime);
        // StartCoroutine(tranformCamera());
    }
    IEnumerator tranformCamera(){
        int temp = 0;
        while(temp<10){ 
            transform.position = Vector3.Slerp(transform.position, new Vector3(transform.position.x, transform.position.y + temp,transform.position.z), 1000* Time.deltaTime);
            yield return new WaitForSeconds(1000f );
            temp++;
        }
    }
    public void TimelineDestroy(){
        Vector3 indexDir = GameControll.Instance.Player.transform.position;
        transform.position = new Vector3(indexDir.x, indexDir.y+3f, indexDir.z);
        transform.eulerAngles=new Vector3(90,0,0);
        //transform.Rotate(new Vector3(90,0,0));
        // camera2.fieldOfView = Mathf.Lerp( 60, 120, Speed);
        // Debug.Log(Speed);
        StartCoroutine(Zoom(60));
    }
    IEnumerator Zoom(float time)
    {   
        while(time<120){
            yield return null;
            time += Time.deltaTime * 30;
            camera2.fieldOfView = time;
            transform.position =  new Vector3(GameControll.Instance.Player.transform.position.x,transform.position.y,GameControll.Instance.Player.transform.position.z);
        };
    }
    
    
}
