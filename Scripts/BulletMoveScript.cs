using UnityEngine;
using System.Collections;

public class BulletMoveScript : MonoBehaviour 
{
    float speed = 3f;

	void Update () 
	{
        transform.Translate(Vector3.up * (speed * Time.deltaTime));
	}
}
