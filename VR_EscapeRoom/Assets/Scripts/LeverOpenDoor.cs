using UnityEngine;

public class LeverOpenDoor : MonoBehaviour
{
    public HingeJoint leverHinge; 
    public Animator doorAnimator;
    public AudioSource doorSound; 
    private bool doorOpened = false; 
    private bool soundPlayed = false; 

    void Update()
    {
        float angle = Mathf.Abs(leverHinge.angle);

        if (angle >= 95 && !doorOpened)
        {
            doorAnimator.SetBool("isOpen", true);

            if (!soundPlayed)
            {
                if (doorSound != null)
                {
                    doorSound.Play();
                }  
                soundPlayed = true; 
            }
        }
    }
}

