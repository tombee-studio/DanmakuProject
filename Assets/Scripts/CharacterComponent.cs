using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class CharacterComponent : MonoBehaviour
{
    const float speed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: これはとりあえず入れただけ. 
        if(Input.GetKey(KeyCode.W)){
            transform.position += new Vector3(0, speed, 0);
        }

        if(Input.GetKey(KeyCode.A)){
            transform.position += new Vector3(-speed, 0,0);
        }

        if(Input.GetKey(KeyCode.S)){
            transform.position += new Vector3(0,-speed, 0);
        }

        if(Input.GetKey(KeyCode.D)){
            transform.position += new Vector3(speed, 0,0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
    }
}
