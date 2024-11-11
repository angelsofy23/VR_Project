using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PuzzleControllerVR : MonoBehaviour
{
    public XRSocketInteractor socketVermelho;
    public XRSocketInteractor socketAzul;
    public XRSocketInteractor socketLaranja;
    public GameObject porta;
    public float tempoDeAbertura = 2.0f;

    void Update()
    {
        // Verifica se todos os bonecos corretos estão nos sockets corretos
        if (VerificaBoneco(socketVermelho, "BonecoVermelho") &&
            VerificaBoneco(socketAzul, "BonecoAzul") &&
            VerificaBoneco(socketLaranja, "BonecoLaranja"))
        {
            AbrirPorta();
        }
    }

    private bool VerificaBoneco(XRSocketInteractor socket, string tagEsperada)
    {
        if (socket.hasSelection)
        {
            var objetoSelecionado = socket.GetOldestInteractableSelected().transform.gameObject;
            return objetoSelecionado.CompareTag(tagEsperada);
        }
        return false;
    }

    private void AbrirPorta()
    {
        porta.transform.position += Vector3.up * tempoDeAbertura;
        Debug.Log("Porta aberta! Puzzle resolvido.");
        this.enabled = false; // Impede múltiplas execuções
    }
}
