using UnityEngine;
using System.Collections;

public class AntWalkScript : MonoBehaviour 
{
    float speed = 1f;

    LevelManagerScript lms;

    void Start()
    {
        lms = GameObject.Find("LevelManager").GetComponent<LevelManagerScript>();
    }

    void Update () 
    {
        if(lms.speedIncrease)
        {
            //Debug.Log("Speed before "+speed);
            lms.speedIncrease = false;
            speed += .25f;
            //Debug.Log("Speed after "+speed);
        }   
        

        transform.Translate(Vector3.up * (speed * Time.deltaTime));
    }
}
