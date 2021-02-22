using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float playerSpeed;
    private Rigidbody rigidBody;
    private float horizInp, fwdInp;

    private Camera cam;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        horizInp = Input.GetAxis("Horizontal");
        fwdInp = Input.GetAxis("Vertical");

        if (horizInp != 0.0f)
        {
            rigidBody.AddForce(horizInp * transform.right * playerSpeed);
        }
        if (fwdInp != 0.0f)
        {
            rigidBody.AddForce(fwdInp * transform.forward * playerSpeed);
        }

        /*if (Input.GetButtonDown("Jump"))
        {
            rigidBody.AddForce(1.0f * transform.up, ForceMode.Impulse);
        }*/

        if (Input.GetButtonDown("Interact"))
        {
            Debug.Log("interact");
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit interactHit;
            if(Physics.Raycast(ray, out interactHit, 3))
            {
                Debug.Log(interactHit);
                Destroy(interactHit.transform.gameObject);
            }
        }
    }
}
