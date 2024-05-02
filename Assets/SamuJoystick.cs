using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuJoystick : MonoBehaviour
{
    [SerializeField] private float speed;
    private FixedJoystick fixedJoystick;
    private Rigidbody2D rigidbody;

    private void OnEnable()
    {
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        rigidbody= gameObject.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float xVal = fixedJoystick.Horizontal;
        float yVal = fixedJoystick.Vertical;

        Vector2 movement = new Vector2 (xVal, yVal);
        rigidbody.velocity = movement* speed;

        /*if(xVal !=0 && yVal !=0 )
        {
            transform.eulerAngles=new Vector3 (transform.eulerAngles.x,Mathf.Atan2(xVal,yVal)*Mathf.Rad2Deg, transform.eulerAngles.z);
        }*/
    }
}