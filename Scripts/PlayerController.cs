using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    public GameObject tankGun;
    public GameObject tankBase;
    public float armorDurability;
    public bool deactivateSelf = false;

    float speed = 2f;
    float translation;
    float angle;
    float newAngle;
    Vector3 mPos;
    Vector3 pPos;


	void Awake () 
	{
        armorDurability = 100;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
	}
	
	void Update () 
	{
        if(deactivateSelf)
        {
            this.gameObject.SetActive(false);
        }

        Vector3 mPos = Input.mousePosition;
        Vector3 pPos = Camera.main.WorldToScreenPoint(this.transform.position);
        
        mPos.x = mPos.x - pPos.x;
        mPos.y = mPos.y - pPos.y;

        angle = Mathf.Atan2(mPos.x,mPos.y)*Mathf.Rad2Deg;
        newAngle = Mathf.Clamp(-angle,-36,36);


        translation = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        tankBase.transform.Translate(translation,0,0);
        tankGun.transform.eulerAngles = new Vector3(0,0,newAngle);
	}
}
