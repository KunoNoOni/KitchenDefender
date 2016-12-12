using UnityEngine;
using System.Collections;

public class AntDestroyScript : MonoBehaviour 
{
    public GameObject antDestroy;
    public AudioClip tankHurt;
    public AudioClip antDestroySound;

    BulletFireScript bfs;
    PlayerController pc;
    UIManagerScript UIMS;
    LevelManagerScript lms;
    AudioSource asource;
    AudioSource asource2;



    void Start()
    {
        bfs = GameObject.Find("TankGun").GetComponent<BulletFireScript>();
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        UIMS = GameObject.Find("Canvas").GetComponent<UIManagerScript>();
        lms = GameObject.Find("LevelManager").GetComponent<LevelManagerScript>();
        asource = GameObject.Find("AntDestroySound").GetComponent<AudioSource>();
        asource2 = GameObject.Find("TankBase").GetComponent<AudioSource>();

        asource.clip = antDestroySound;
        asource2.clip = tankHurt;
    }

    void Update()
    {
        if(!lms.waveActive || pc.armorDurability <= 0)
        {
            gameObject.SetActive(false);   
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Pantry")
        {
            asource2.Play();
            gameObject.SetActive(false);
            pc.armorDurability -= 20;
            UIMS.UpdateArmorDurability();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bullet"))
        {
            if(other.gameObject.CompareTag("Player"))
            {
                asource2.Play();
                if(this.gameObject.CompareTag("AntRS"))
                {
                    pc.armorDurability -= 15;
                    UIMS.UpdateArmorDurability();
                }
                else if(this.gameObject.CompareTag("AntTS"))
                {
                    pc.armorDurability -= 15;
                    UIMS.UpdateArmorDurability();
                }
                else
                {
                    pc.armorDurability -= 10;
                    UIMS.UpdateArmorDurability();
                }
            }

            if(this.gameObject.CompareTag("AntRS"))
            {
                bfs.hasRapidFire = true;
                //bfs.rfBonusTime += 5f;
            }

            if(this.gameObject.CompareTag("AntTS"))
            {
                bfs.hasTripleFire = true;
                //bfs.tfBonusTime += 5f;
            }   
            
            asource.Play();
            lms.antsRemaining -= 1;
            UIMS.UpdateAntsRemaining(lms.antsRemaining);
            Instantiate(antDestroy,this.transform.position,this.transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
