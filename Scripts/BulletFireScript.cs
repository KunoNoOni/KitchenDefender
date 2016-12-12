using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BulletFireScript : MonoBehaviour 
{
    public GameObject tankFire;
    public GameObject[] firePoint;
    public bool hasRapidFire = false;
    public bool hasTripleFire = false;
    public float rfBonusTime = 0f;
    public float tfBonusTime = 0f;
    public AudioClip bulletFire;

    float coolDown;
    float coolDownRate = 0.5f;
    float rfCoolDownRate = 10f;
    float rfCoolDown;
    float tfCoolDownRate = 10f;
    float tfCoolDown;

    Text rapidFire;
    Text tripleFire;
    Text rfTimeRemaining;
    Text tfTimeRemaining;

    string secondsLeft;

    LevelManagerScript lms;

    AudioSource asource;



	void Start () 
	{
        coolDown = coolDownRate;
        rfCoolDown = rfCoolDownRate;
        tfCoolDown = tfCoolDownRate;

        rapidFire = GameObject.Find("RapidFire").GetComponent<Text>();
        tripleFire = GameObject.Find("TripleFire").GetComponent<Text>();
        rfTimeRemaining = GameObject.Find("RFTimeRemaining").GetComponent<Text>();
        tfTimeRemaining = GameObject.Find("TFTimeRemaining").GetComponent<Text>();

        rapidFire.color = Color.red;
        tripleFire.color = Color.red;

        rfTimeRemaining.gameObject.SetActive(false);
        tfTimeRemaining.gameObject.SetActive(false);

        secondsLeft = " second(s) left";

        lms = GameObject.Find("LevelManager").GetComponent<LevelManagerScript>();
        asource = GameObject.Find("TankGun").GetComponent<AudioSource>();

        asource.clip = bulletFire;
	}

    void Update()
    {
        //Testing Cheats
//        if(Input.GetKeyDown(KeyCode.Q))
//        {
//            hasRapidFire = !hasRapidFire;
//        }
//
//        if(Input.GetKeyDown(KeyCode.W))
//        {
//            hasTripleFire = !hasTripleFire;
//        }

        if(!lms.waveActive)
        {
            rapidFire.color = Color.red;
            rfTimeRemaining.gameObject.SetActive(false);
            tripleFire.color = Color.red;
            tfTimeRemaining.gameObject.SetActive(false);

        }

        if(Input.GetMouseButton(0) && lms.waveActive)
        {
            coolDown -= Time.deltaTime;
            if(coolDown < 0)
            {
                coolDown = coolDownRate;
                Fire();
            }
        }

        if(hasRapidFire)
        {
            if(!lms.waveActive)
            {
                rfCoolDown = rfCoolDownRate;
                rfBonusTime = 0f;
                hasRapidFire = false;

            }

            if(!rfTimeRemaining.gameObject.activeInHierarchy)
                rfTimeRemaining.gameObject.SetActive(true);
            rapidFire.color = Color.green;
            rfCoolDown += rfBonusTime;
            rfBonusTime = 0f;
            rfCoolDown -= Time.deltaTime;
            rfTimeRemaining.text = Mathf.RoundToInt(rfCoolDown).ToString() + secondsLeft;

            if(rfCoolDown < 0)
            {
                rfCoolDown = rfCoolDownRate;
                rfBonusTime = 0f;
                hasRapidFire = false;
                rapidFire.color = Color.red;
                rfTimeRemaining.gameObject.SetActive(false);
            }
        }

        if(hasTripleFire)
        {
            if(!lms.waveActive)
            {
                tfCoolDown = tfCoolDownRate;
                tfBonusTime = 0f;
                hasTripleFire = false;
            }

            if(!tfTimeRemaining.gameObject.activeInHierarchy)
                tfTimeRemaining.gameObject.SetActive(true);
            tripleFire.color = Color.green;
            tfCoolDown += tfBonusTime;
            tfBonusTime = 0f;
            tfCoolDown -= Time.deltaTime;
            tfTimeRemaining.text = Mathf.RoundToInt(tfCoolDown).ToString()+secondsLeft;

            if(tfCoolDown < 0)
            {
                tfCoolDown = tfCoolDownRate;
                tfBonusTime = 0f;
                hasTripleFire = false;
                tripleFire.color = Color.red;
                tfTimeRemaining.gameObject.SetActive(false);
            }
        }

    }
	
	void Fire () 
    {
        asource.Play();

        if(hasRapidFire)
        {
            coolDownRate = .2f;
        }
        else
        {
            coolDownRate = .5f;
        }
        
        if(hasTripleFire)
        {
            foreach(GameObject fp in firePoint)
            {
                GameObject obj = ObjectPool.current.GetPooledObject();

                if(obj == null) 
                    return;

                obj.transform.position = fp.transform.position;
                obj.transform.rotation = fp.transform.rotation;
                Instantiate(tankFire, firePoint[0].transform.position, firePoint[0].transform.rotation);
                obj.SetActive(true);   
            }   
        }   
        else
        {
            GameObject obj = ObjectPool.current.GetPooledObject();

            if(obj == null) 
                return;

            obj.transform.position = firePoint[0].transform.position;
            obj.transform.rotation = firePoint[0].transform.rotation;
            Instantiate(tankFire, firePoint[0].transform.position, firePoint[0].transform.rotation);
            obj.SetActive(true);
        }
	}
}

