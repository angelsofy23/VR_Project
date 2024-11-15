using UnityEngine;

public class BotaoDeslizante : MonoBehaviour
{
    public float limiteInferior = -0.1f; // Limite de quanto o bot�o pode ser pressionado
    public float limiteSuperior = 0.0f; // Posi��o original do bot�o
    private Vector3 posicaoInicial;

    void Start()
    {
        posicaoInicial = transform.localPosition;
    }

    void Update()
    {
        Vector3 posicaoAtual = transform.localPosition;
        posicaoAtual.y = Mathf.Clamp(posicaoAtual.y, posicaoInicial.y + limiteInferior, posicaoInicial.y + limiteSuperior);
        transform.localPosition = posicaoAtual;
    }
}
