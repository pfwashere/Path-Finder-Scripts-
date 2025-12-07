using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetComponent : MonoBehaviour
{
    //public float speed = 0f ;
    void Start()
    {
        GameObject gr = GameObject.Find("getrigid");
        Rigidbody rb = gr.GetComponent<Rigidbody>();
        Debug.Log(rb);
        Debug.Log(rb.mass);
        Debug.Log(rb.useGravity);
        Debug.Log(rb.velocity);
        //rb.velocity = transform.forward * speed;
    }
}
