using System.Collections;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform Target;

    private void Start() {
        Target = GameControll.Instance.Player.transform;
    }
    void Update()
    {
        Vector3 targetPosition = new Vector3(Target.position.x, transform.position.y, Target.position.z);
        transform.LookAt(targetPosition);
    }

}