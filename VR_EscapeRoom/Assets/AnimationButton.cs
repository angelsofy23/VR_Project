using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonPress : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnButtonPress()
    {
        animator.SetTrigger("Press");
    }
}


