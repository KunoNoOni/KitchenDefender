using UnityEngine;
using System.Collections;

public class BulletDestroyScript : MonoBehaviour 
{
    public GameObject bulletDestroy;
    public GameObject bulletWallHit;
    public AudioClip bulletDestroySound;
    public AudioClip bulletWallHitSound;

    LevelManagerScript lms;
    PlayerController pc;
    AudioSource asource;
    AudioSource asource2;

    void OnEnable()
    {
        Invoke("Destroy",2.5f);
    }

    void Start()
    {
        lms = GameObject.Find("LevelManager").GetComponent<LevelManagerScript>();
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        asource = GameObject.Find("BulletDestroySound").GetComponent<AudioSource>();
        asource2 = GameObject.Find("Wallhit").GetComponent<AudioSource>();

        asource.clip = bulletDestroySound;
        asource2.clip = bulletWallHitSound;
    }

    void Update()
    {
        if(!lms.waveActive || pc.armorDurability <= 0)
        {
            DestroyWaveDone();
        }
    }


    void Destroy()
    {
        asource.Play();
        Instantiate(bulletDestroy,this.transform.position, this.transform.rotation);
        gameObject.SetActive(false);
    }

    void DestroyWaveDone()
    {
        Instantiate(bulletDestroy,this.transform.position, this.transform.rotation);
        gameObject.SetActive(false);
    }

    void DestroyWallHit()
    {
        Instantiate(bulletWallHit,this.transform.position, this.transform.rotation);
        asource2.Play();
        gameObject.SetActive(false);
    }

    void DestroyAntHit()
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Background"))
        {
            DestroyWallHit();
        }   

        if(other.gameObject.CompareTag("Ant") || other.gameObject.CompareTag("AntRS") || other.gameObject.CompareTag("AntTS"))
        {
            DestroyAntHit();
        }
    }

}
