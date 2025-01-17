using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOpenDoor : MonoBehaviour
{
    // Reference to the door GameObject that this key can open
    public GameObject doorToOpen;

    // Reference to door's animator component 
    public Animator doorAnimator;

    // Reference to door's audio source
    public AudioSource doorSound;

    // Flag to track if key has been used
    private bool keyUsed = false;

    void Start()
    {
        // Get the animator component from the door
        doorAnimator = doorToOpen.GetComponent<Animator>();
        if (doorAnimator == null)
        {
            Debug.LogWarning("No Animator component found on door!");
        }
    }

    

    void OnTriggerEnter(Collider other)
    {
        // Check if this key collides with its matching door
        if (!keyUsed && other.gameObject == doorToOpen)
        {
            // Trigger the door open animation
            doorAnimator.SetBool("isOpen", true);

            // Play door opening sound
            if (doorSound != null)
            {
                doorSound?.Play();
            }

            // Disable the key after use
            keyUsed = true;
            gameObject.SetActive(false);
        }
    }
}

