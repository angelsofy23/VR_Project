using UnityEngine;

public class OpenClosedDoor : MonoBehaviour
{
    private float _angle = 90f; // Ângulo final da porta
    private float _speed = 3f;  // Velocidade da rotação
    private bool _isOpen;       // Status da porta (aberta ou fechada)
    private Quaternion _closedAngle;  // Ângulo fechado
    private Quaternion _openedAngle;  // Ângulo aberto
    
    // Start é chamado antes do primeiro frame
    void Start()
    {
        // Porta Fechada (Rotação Inicial da Porta)
        _closedAngle = transform.rotation;
        
        // Porta Aberta
        _openedAngle = Quaternion.Euler(0f, _angle, 0);
    }

    // Update é chamado a cada frame
    void Update()
    {
        // Se a porta está aberta
        if (_isOpen)
        {
            // Faz a rotação suave até o ângulo aberto
            transform.rotation = Quaternion.Slerp(transform.rotation, _openedAngle, Time.deltaTime * _speed);
        }
        else
        {
            // Faz a rotação suave até o ângulo fechado
            transform.rotation = Quaternion.Slerp(transform.rotation, _closedAngle, Time.deltaTime * _speed);
        }
    }

    // Método público para abrir a porta
    public void Open()
    {
        // Define a porta como aberta
        _isOpen = true;
    }
}
