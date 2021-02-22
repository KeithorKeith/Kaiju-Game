using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rigidBody;

    private float horizInp, fwdInp;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizInp = Input.GetAxis("Horizontal");
        fwdInp = Input.GetAxis("Vertical");

        if (horizInp != 0.0f)
        {
            rigidBody.AddForce(horizInp * transform.right * 5.0f);
        }
        if (fwdInp != 0.0f)
        {
            rigidBody.AddForce(fwdInp * transform.forward * 5.0f);
        }

        if (Input.GetButtonDown("Jump"))
        {
            rigidBody.AddForce(10.0f * transform.up, ForceMode.Impulse);
        }
    }
}
