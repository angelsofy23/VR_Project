using UnityEngine;

public class SimpleGrabbable : MonoBehaviour
{
    public bool inHand = false;

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se a interação começa
        if (collision.collider.CompareTag("PlayerHand"))
        {
            inHand = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Verifica se a interação termina
        if (collision.collider.CompareTag("PlayerHand"))
        {
            inHand = false;
        }
    }
}
