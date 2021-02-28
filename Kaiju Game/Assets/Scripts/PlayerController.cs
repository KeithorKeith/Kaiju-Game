using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool canMove = true;
    public float playerSpeed;

    private float horizInp, fwdInp;

    private Camera cam;
    public static readonly float INTERACTDISTANCE = 5.0f;
    private CharacterController charControl;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        charControl = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (canMove)
        {
            horizInp = Input.GetAxisRaw("Horizontal");
            fwdInp = Input.GetAxisRaw("Vertical");

            if (horizInp != 0.0f || fwdInp != 0.0f)
            {
                Vector3 moveVec = (fwdInp * transform.forward + transform.right * horizInp).normalized * Time.deltaTime * playerSpeed;
                charControl.Move(moveVec);
            }

            if (Input.GetButtonDown("Interact"))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit interactHit;
                if (Physics.Raycast(ray, out interactHit, INTERACTDISTANCE))
                {
                    Terminal termScript = interactHit.transform.GetComponent<Terminal>();
                    if (termScript != null)
                    {
                        termScript.Activate(gameObject);
                    }
                }
            }
        }
    }
}
