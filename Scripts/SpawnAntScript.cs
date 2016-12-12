using UnityEngine;
using System.Collections;

public class SpawnAntScript : MonoBehaviour 
{
    public Transform[] spawnPoints;
    public GameObject ant;
    public GameObject antRS;
    public GameObject antTS;

    float spawnTimer;
    float spawnTimerCoolDown = 1f;

    int rndNum;
    int specialAnt;
    int whichSpecialAnt;

    GameObject obj;

	void Start () 
	{
        spawnTimer = spawnTimerCoolDown;
	}
	
	void Update () 
	{
        spawnTimer -= Time.deltaTime;

        if(spawnTimer < 0)
        {
            spawnTimerCoolDown = 1f;
            spawnTimer = spawnTimerCoolDown;
            specialAnt = Random.Range(0,100);
            if(specialAnt >= 59 && specialAnt <= 69)
            {
                whichSpecialAnt = Random.Range(1,7);
                if(whichSpecialAnt % 2 == 0)
                {
                    obj = antRS.GetComponent<ObjectPool>().GetPooledObject();
                }
                else
                {
                    obj = antTS.GetComponent<ObjectPool>().GetPooledObject();
                }
            }
            else
            {
                rndNum = Random.Range(0,9);
                obj = ant.GetComponent<ObjectPool>().GetPooledObject();    
            }

            if(obj == null) 
                return;

            obj.transform.position = spawnPoints[rndNum].transform.position;
            obj.SetActive(true);
        }

	}
}
