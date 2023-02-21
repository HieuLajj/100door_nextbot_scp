using System.Collections;
using UnityEngine;
using TMPro;
public class Bot : NavMeshBase
{
    // Start is called before the first frame update
    private string namebot;
    public int currentWp = 0;
    public bool CheckDestroy = false;
    public Rigidbody _rigidbodybot;
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject body;
    [SerializeField] LayerMask ground;
    [SerializeField] Transform groundCheck;
    [SerializeField] SkinnedMeshRenderer skinMeshRendererBot;
    [SerializeField] TMP_Text nameText;
    public bool checkActive = true;
    public SCPEnemy enemyDestroyBot;
    private Vector3 randomPosition;
    void Update()
    {
        if(!checkActive) return;
        // if(GameControll.Instance.Mod == 4 && GameControll.Instance.CheckActiveBotMode4){
        //     Vector3 randomPosition = GameControll.Instance.Player.transform.position;
        //     randomPosition = new Vector3(randomPosition.x+Random.Range(-5f, 5.0f),randomPosition.y+Random.Range(-5f, 5.0f),randomPosition.z+Random.Range(-5f, 5.0f));
        //     ChaseTarget(randomPosition);
        //     return;
        // }
        if (Vector3.Distance(this.transform.position, GameControll.Instance.PositionMap[currentWp].transform.position) <= 10)
        {
            currentWp += Random.Range(currentWp, currentWp+10);
        }
        if (currentWp >= GameControll.Instance.PositionMap.Count)
        {
            currentWp = Random.Range(0, GameControll.Instance.PositionMap.Count-1);
        }
        Quaternion lookAtWp = Quaternion.LookRotation(GameControll.Instance.PositionMap[currentWp].transform.position - this.transform.position);
        if(navMeshAgent.enabled){
            navMeshAgent.SetDestination(GameControll.Instance.PositionMap[currentWp].transform.position);
        }
        if(CheckDestroy){
            Death(GameControll.Instance.ThrowPlayer);
        }
    }

    private void LateUpdate() {
        if(!checkActive) return;
        if(Random.Range(0,20) == 10 && CheckGround()){
            navMeshAgent.enabled = false;
            Vector3 dir = transform.forward;
            _rigidbodybot.AddForce(new Vector3(dir.x,2f,dir.z) * 150, ForceMode.Impulse);
        }else if(!navMeshAgent.enabled && CheckGround()){
            navMeshAgent.enabled = true;
        }    
    }

    public void SetUpBot(){
        HeroItem heroItemBot =  UISelectHero.Instance.HeroDatas.GetItemRandom();
        skinMeshRendererBot.material = heroItemBot.materialhero;
        name = NameFake.GetRandomName();
        nameText.text = name;
        currentWp = Random.Range(0, GameControll.Instance.PositionMap.Count-1);
    }
    public void Death(Vector3 dir){
        Vector3 h = new Vector3(0, 600, 0);
        _rigidbodybot.velocity = Vector3.zero;
        _rigidbodybot.AddForce(h, ForceMode.Impulse);
        navMeshAgent.enabled = false;
        checkActive = false;
        _rigidbodybot.constraints = RigidbodyConstraints.None;
        transform.rotation = Random.rotation;
        //Vector3 h = new Vector3(dir.x, 0, dir.z);
        // Vector3 h = new Vector3(0, 500, 0);
        // _rigidbodybot.AddForce(h, ForceMode.Impulse);
        TextNotificationIngame.Instance.SetNotification(name);
        enemyDestroyBot.TargetChase = null;
        enemyDestroyBot.checkFindTarget  = false;
        StartCoroutine(DeathEffect1(2.5f));
        //StartCoroutine(DeathEffect2(3f));
        StartCoroutine(DeathEffect3(3f));
    }
    public IEnumerator DeathEffect1(float delay)
    {
        yield return new WaitForSeconds(delay);
        smoke.SetActive(true);
        body.SetActive(false);
    }
    public IEnumerator DeathEffect2(float delay)
    {
        yield return new WaitForSeconds(delay);
        // if(GameControll.Instance.Mod != 4){
        //     gameObject.SetActive(false);
        //     if(GameControll.Instance.Mod!=4){
        //         enemyDestroyBot.ChooseTarget();
        //     }else{
        //         if(GameControll.Instance.Player.activeInHierarchy){
        //             GameControll.Instance.ChangChaseEnemyMod4();
        //         }    
        //     }
        // }else{
            // gameObject.SetActive(false);
            //ameControll.Instance.ChangChaseEnemyMod4();



            // CheckDestroy = false;
            
            // gameObject.SetActive(false);
            //CheckDestroy = false;
            
            
            
            //gameObject.SetActive(false);




            //gameObject.SetActive(true);
            // SetupPosition(GameControll.Instance.RandomPositionBot().position);
            // enemyDestroyBot.TargetChase = null; 
        //}
    }

    public IEnumerator DeathEffect3(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetBot();
        CheckDestroy = false;

    }
    public void ResetBot(){
        //gameObject.SetActive(true);
        _rigidbodybot.constraints = RigidbodyConstraints.FreezeRotation;
        if(!navMeshAgent.enabled){
            navMeshAgent.enabled = true;
        }
        _rigidbodybot.velocity = Vector3.zero; 
        smoke.SetActive(false);
        transform.localRotation = Quaternion.Euler(0,0,0);
        checkActive = true;
        CheckDestroy = false;
        SetupPosition(GameControll.Instance.RandomPositionBot().position);
    }   
    public bool CheckGround(){
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
    
}
