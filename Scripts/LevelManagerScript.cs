using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManagerScript : MonoBehaviour 
{
    public GameObject tankBroken;
    public Transform playerLoc;
    public bool speedIncrease = false;
    public bool waveActive = false;
    public int antsRemaining;
    public AudioClip explode;
    public AudioClip[] music;

    PlayerController pc;
    SpawnAntScript sas;
    UIManagerScript UIMS;
    AudioSource asource;
    AudioSource musicSource;

    int waveNumber;
    int antsPerWave = 10;
    float coolDown;
    float coolDownRate = 5f;
    bool gameOver = false;


	void Awake () 
	{
        waveNumber = 0;
        antsRemaining = 0;

        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        sas = GameObject.Find("SpawnZone").GetComponent<SpawnAntScript>();
        sas.gameObject.SetActive(false);

        UIMS = GameObject.Find("Canvas").GetComponent<UIManagerScript>();
        asource = GameObject.Find("LevelManager").GetComponent<AudioSource>();
        musicSource = GameObject.Find("Background").GetComponent<AudioSource>();
        asource.clip = explode;
    }
	
	void Update () 
	{
        if(!waveActive)
        {

            if(!UIMS.waveCompleted.gameObject.activeInHierarchy)
            {
                UIMS.waveCompleted.gameObject.SetActive(true);
            }

            if(!UIMS.nextWave.gameObject.activeInHierarchy && waveNumber < 10)
            {
                UIMS.nextWave.gameObject.SetActive(true);
            }

            if(!UIMS.nextWaveCountDown.gameObject.activeInHierarchy && waveNumber < 10)
            {
                UIMS.nextWaveCountDown.gameObject.SetActive(true);
            }

            coolDown -= Time.deltaTime;

            UIMS.nextWaveCountDown.text = Mathf.RoundToInt(coolDown).ToString()+" Seconds!";

            if(coolDown < 0)
            {
                coolDown = coolDownRate;
                waveActive = true;
                UIMS.waveCompleted.gameObject.SetActive(false);
                UIMS.nextWave.gameObject.SetActive(false);
                UIMS.nextWaveCountDown.gameObject.SetActive(false);
                waveNumber += 1;
                UIMS.waveNumberText.text = waveNumber.ToString();
                if(waveNumber > 1)
                {
                    GetMusic();
                    musicSource.Play();
                    speedIncrease = true;
                    antsPerWave += 5;
                    antsRemaining += antsPerWave;
                }
                else
                {
                    GetMusic();
                    musicSource.Play();
                    antsRemaining = 10;
                }
                UIMS.UpdateAntsRemaining(antsRemaining);
                sas.gameObject.SetActive(true);
            }    
        }

        if(antsRemaining <= 0)
        {
            waveActive = false;
        }

        if(waveNumber == 10 && antsRemaining <= 0)
        {
            SceneManager.LoadScene("WinScreen");
            //Time.timeScale = 0;
        }


        if(pc.armorDurability <= 0)
        {
            if(!gameOver)
            {
                gameOver = true;
                asource.Play();
                Instantiate(tankBroken,playerLoc.transform.position, playerLoc.transform.rotation);
                pc.deactivateSelf = true;
                sas.gameObject.SetActive(false);
                coolDown = 5;
                //Time.timeScale = 0;   
            }
        }

        if(gameOver)
        {
            coolDown -= Time.deltaTime;
            if(coolDown <= 0)
            {
                coolDown = coolDownRate;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("LoseScreen");
            }
        }
	}

    void GetMusic()
    {
        musicSource.clip = music[Random.Range(0,4)];
    }

}
