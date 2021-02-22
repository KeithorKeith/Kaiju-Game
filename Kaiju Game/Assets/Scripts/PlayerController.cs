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

        if (horizInp != 0.0f || fwdInp != 0.0f)
        {
            transform.position += (fwdInp * transform.forward  + transform.right * horizInp).normalized * playerSpeed * Time.deltaTime;
        }

        /*if (Input.GetButtonDown("Jump"))
        {
            rigidBody.AddForce(1.0f * transform.up, ForceMode.Impulse);
        }*/

        if (Input.GetButtonDown("Interact"))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit interactHit;
            if(Physics.Raycast(ray, out interactHit, 100))
            {
                Terminal termScript = interactHit.transform.GetComponent<Terminal>();
                if (termScript != null)
                {
                    termScript.Activate();
                }
            }
        }
    }
}
