﻿using UnityEngine;
using System.Collections;

public class DestroyEffectScript : MonoBehaviour 
{
    ParticleSystem effect;

	void Start () 
	{
        effect = GetComponent<ParticleSystem>();
	}
	
	void Update () 
	{
        if(effect.isPlaying)
            return;

        Destroy(gameObject);
	
	}
}
