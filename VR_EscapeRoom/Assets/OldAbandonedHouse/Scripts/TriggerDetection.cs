using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    private Animator currAnimator;
    private bool isOpen = false;
    private bool canInteract = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(canInteract) 
            {
                Ray ray = new Ray(this.transform.position, this.transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.CompareTag("Door"))
                    {
                        canInteract = false;
                        Debug.Log("Found");
                        currAnimator = hit.collider.gameObject.GetComponent<Animator>();
                        currAnimator.SetBool("isOpen",!isOpen);
                        Debug.Log(hit.collider.gameObject.name);
                        isOpen = !isOpen;
                        canInteract = true;
                    }
                }
            }
            
        }
    }
}
