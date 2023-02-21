using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEye : MonoBehaviour
{
     // These are the resolutions, odd numbers are favourable
    public int xCastNum = 15;
    public int zCastNum = 15;
 
    // Max angle (transform.down is 0° in t$$anonymous$$s case)
    float maxAngle = 50;
 
    // Variables so you can use them later
    int minX;
    int maxX;
 
    int minZ;
    int maxZ;
 
    float angleMultiplierX;
    float angleMultiplierZ;
    void Start()
    {
        RecalcX();
        RecalcZ();

        _timer = Time.time + 0.04f;
    }

    private float _timer;


    // Update is called once per frame
    void Update()
    {
        if(Time.time > _timer){
            _timer = Time.time + 0.04f;

            
        for(int x = minX; x < maxX; x++)
        {
            for (int z = minZ; z < maxZ; z++)
            {
                
                RaycastHit hit;
                Vector3 direction = Quaternion.AngleAxis(x * angleMultiplierX, transform.right) * Quaternion.AngleAxis(z * angleMultiplierZ, transform.forward) * -transform.up;
                //Debug.DrawLine(transform.position, transform.position + direction);
                if (Physics.Raycast(transform.position, direction , out hit, Mathf.Infinity))
                {
                    if(hit.collider.tag.Equals("wall") && !hit.collider.tag.Equals("Eye")){
                        if(Random.Range(0,20)==1){
                            GameControll.Instance.SpawnEye(hit.point);
                        }
                    }
                }
            }
        } 
        }
    }

    void RecalcX()
    {
        minX = -xCastNum / 2;
        maxX = -minX + 1;
        angleMultiplierX = maxAngle / (xCastNum / 2);
    }
 
    void RecalcZ()
    {
        minZ = -zCastNum / 2;
        maxZ = -minZ + 1;
        angleMultiplierZ = maxAngle / (zCastNum / 2);
    }
 
    public void ChangeX(bool add)
    {
        if (add) xCastNum++;
        else xCastNum--;
        RecalcX();
    }
 
    public void ChangeZ(bool add)
    {
        if (add) zCastNum++;
        else zCastNum--;
        RecalcZ();
    }
}
