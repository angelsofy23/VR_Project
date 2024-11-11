using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine;

public class PuzzleControllerVR : MonoBehaviour
{
    public HingeJoint hingeJoint;
    public XRSocketInteractor socketVermelho;
    public XRSocketInteractor socketAzul;
    public XRSocketInteractor socketLaranja;

    void Update()
    {
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
        JointMotor motor = hingeJoint.motor;
        motor.targetVelocity = 50; // Ajuste a velocidade conforme necessário
        hingeJoint.motor = motor;
        hingeJoint.useMotor = true;
        Debug.Log("Porta aberta! Puzzle resolvido.");
    }
}
