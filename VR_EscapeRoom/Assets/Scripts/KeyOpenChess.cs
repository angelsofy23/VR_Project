using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class KeyOpenChest : MonoBehaviour
{
    public GameObject chestToOpen; // O baú a ser aberto
    public XRGrabInteractable objectInsideChest; // Objeto dentro do baú
    private bool keyUsed = false;
    private XRGrabInteractable doorGrabInteractable;
    private HingeJoint doorHinge; // Referência à dobradiça do baú
    private Rigidbody chestRigidbody; // Referência ao Rigidbody do baú

    void Start()
    {
        // Obter o componente XR Grab Interactable da porta do baú
        doorGrabInteractable = chestToOpen.GetComponent<XRGrabInteractable>();
        if (doorGrabInteractable == null)
        {
            Debug.LogWarning("Nenhum componente XRGrabInteractable encontrado na porta do baú!");
        }

        // Obter o componente HingeJoint
        doorHinge = chestToOpen.GetComponent<HingeJoint>();
        if (doorHinge == null)
        {
            Debug.LogWarning("Nenhum componente HingeJoint encontrado no baú!");
        }

        // Obter o componente Rigidbody
        chestRigidbody = chestToOpen.GetComponent<Rigidbody>();
        if (chestRigidbody == null)
        {
            Debug.LogWarning("Nenhum Rigidbody encontrado no baú!");
        }

        if (doorGrabInteractable != null)
        {
            doorGrabInteractable.enabled = false;
        }
        if (doorHinge != null)
        {
            doorHinge.useSpring = false;
        }
        if (chestRigidbody != null)
        {
            chestRigidbody.isKinematic = true;
        }

        if (objectInsideChest != null)
        {
            objectInsideChest.enabled = false;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Verificar se esta chave colide com o baú correspondente
        if (!keyUsed && other.gameObject == chestToOpen)
        {
            // Ativar o XR Grab Interactable na porta do baú
            if (doorGrabInteractable != null)
            {
                doorGrabInteractable.enabled = true;
            }

            // Ativar a mola na dobradiça
            if (doorHinge != null)
            {
                doorHinge.useSpring = true;
            }

            // Tornar o baú não cinemático para que ele possa ser afetado por física
            if (chestRigidbody != null)
            {
                chestRigidbody.isKinematic = false;
            }

            // Ativar interação com o objeto dentro do baú
            if (objectInsideChest != null)
            {
                objectInsideChest.enabled = true;
            }

            // Desativar a chave após o uso
            keyUsed = true;
            gameObject.SetActive(false);
        }
    }
}
