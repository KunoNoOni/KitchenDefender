using UnityEngine;
using System.Collections;

public class FireworksScript : MonoBehaviour 
{
    public GameObject fireWorks;
    public GameObject tankFire;
    public Transform fireFrom;
    public Transform explodeHere;
    public AudioClip explode;
    public AudioClip tankFireSound;

    float fireworksCoolDown;
    float fireworksCooldDownRate = 2f;
    float tankFireCoolDown;
    float tankFireCoolDownRate = 2f;
	
    AudioSource asource;
    AudioSource asource2;

    void Start()
    {
        fireworksCoolDown = fireworksCooldDownRate;
        tankFireCoolDown = tankFireCoolDownRate;

        asource = GameObject.Find("Player").GetComponent<AudioSource>();
        asource2 = GameObject.Find("TankBase").GetComponent<AudioSource>();
    }

	void Update () 
	{
        fireworksCoolDown -= Time.deltaTime;
        tankFireCoolDown -= Time.deltaTime;

        if(tankFireCoolDown <= 0)
        {
            asource2.clip = tankFireSound;
            asource2.Play();
            Instantiate(tankFire, fireFrom.transform.position, fireFrom.transform.rotation);
            tankFireCoolDown = tankFireCoolDownRate;
        }

        if(fireworksCoolDown <= 0)
        {
            asource.clip = explode;
            asource.Play();
            Instantiate(fireWorks, explodeHere.transform.position, explodeHere.transform.rotation);
            fireworksCoolDown = fireworksCooldDownRate;
        }
	}
}
