using UnityEngine;

public class SVLever : MonoBehaviour
{
    public float leverOnAngle = -45f;
    public float leverOffAngle = 45f;
    public bool leverIsOn = false;

    private HingeJoint leverHingeJoint;
    private SimpleGrabbable grabbable;


    void Start()
    {
        leverHingeJoint = GetComponent<HingeJoint>();
        grabbable = GetComponent<SimpleGrabbable>();

        // Configurar limites do HingeJoint
        //JointLimits limits = leverHingeJoint.limits;
        //limits.min = leverOnAngle;
        //limits.max = leverOffAngle;
        //leverHingeJoint.limits = limits;
        //leverHingeJoint.useLimits = true;

        // Inicializa o estado da mola
        //UpdateSpring();
    }

    void Update()
    {
        if (grabbable.inHand)
        {
            // Desativar mola enquanto está segurando
            leverHingeJoint.useSpring = false;
        }
        else
        {
            // Atualizar mola para retornar ao estado correto ao soltar
            //UpdateSpring();
        }

        // Atualizar o estado da alavanca
        float currentAngle = transform.localRotation.eulerAngles.z;
        if (currentAngle > 180) currentAngle -= 360; // Corrige ângulos negativos

        leverIsOn = Mathf.Abs(currentAngle - leverOnAngle) < Mathf.Abs(currentAngle - leverOffAngle);
    }

    private void UpdateSpring()
    {
        JointSpring spring = leverHingeJoint.spring;
        spring.spring = 25f; // Força da mola
        spring.damper = 15f; // Amortecimento
        spring.targetPosition = leverIsOn ? leverOnAngle : leverOffAngle;

        leverHingeJoint.spring = spring;
        leverHingeJoint.useSpring = true;
    }
}
