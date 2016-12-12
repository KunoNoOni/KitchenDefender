using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManagerScript : MonoBehaviour 
{
    PlayerController pc;
    Text armorDurabilityText;

    public Text waveNumberText;
    public Text antsRemainingText;
    public Text waveCompleted;
    public Text nextWave;
    public Text nextWaveCountDown;



	void Start () 
	{
        pc = GameObject.Find("Player").GetComponent<PlayerController>();
        armorDurabilityText = GameObject.Find("ArmorDurabilityText").GetComponent<Text>();
        waveNumberText = GameObject.Find("WaveNumberText").GetComponent<Text>();
        antsRemainingText = GameObject.Find("AntsRemainingText").GetComponent<Text>();
        waveCompleted = GameObject.Find("WaveCompleted").GetComponent<Text>();
        nextWave = GameObject.Find("NextWave").GetComponent<Text>();
        nextWaveCountDown = GameObject.Find("NextWaveCountdown").GetComponent<Text>();

        //Need to set these to the variables in the level manager
        waveNumberText.text = "1"; 
        antsRemainingText.text = "20"; 

        UpdateArmorDurability();
        armorDurabilityText.text = pc.armorDurability.ToString() +"%";
	}

    public void UpdateArmorDurability()
    {
        armorDurabilityText.text = pc.armorDurability.ToString() +"%";
    }

    public void UpdateAntsRemaining(int ar)
    {
        antsRemainingText.text = ar.ToString(); 
    }
}
