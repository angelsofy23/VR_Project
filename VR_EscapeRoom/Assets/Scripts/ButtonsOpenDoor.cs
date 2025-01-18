using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class ButtonsOpenDoor : MonoBehaviour
{
    private Vector3 originalPosition; // Posição original do botão
    [SerializeField] private bool isAnimating = false;
    [SerializeField] private float pressDepth = 0.1f; // Distância que o botão se move para baixo
    [SerializeField] private float pressSpeed = 0.5f; // Velocidade de animação
    [SerializeField] private float holdDuration = 0.0f; // Tempo que o botão fica pressionado
    public Animator doorAnimator; // Animator para abrir a porta
    public AudioSource doorSound; // Áudio para abrir a porta
    public int buttonID; // ID único do botão (1, 2, 3)
    public TextMeshPro sequenceText; // Campo de texto para exibir a sequência

    // Sequência correta e sequência atual
    private static readonly List<int> correctSequence = new List<int> { 3, 3, 2, 1, 3, 2 };
    private static List<int> currentSequence = new List<int>();

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void OnButtonPress()
    {
        StartCoroutine(ButtonAnimation());

        if(sequenceText.text.Equals("Access Granted")) return;

        // Impede alterações na sequência do keypad caso a sequência correta já tenha sido concluída
        currentSequence.Add(buttonID);
        sequenceText.text = string.Join(" ", currentSequence);

        // Verifica a sequência apenas quando o jogador escrever toda
        if (currentSequence.Count == correctSequence.Count)
        {
            if (IsSequenceCorrect())
            {
                sequenceText.text = "Access Granted"; // Mensagem de sucesso
                OpenDoor(); // Abre a porta
            }
            else
            {
                sequenceText.text = "Access Denied"; // Mensagem de falha
            }

            ResetSequence(); // Reseta a sequência para uma nova tentativa
        }
    }

    private IEnumerator ButtonAnimation()
    {
        isAnimating = true;
        Vector3 pressedPosition = originalPosition + Vector3.right * pressDepth;

        // Pressiona o botão para baixo
        float elapsedTime = 0f;
        while (elapsedTime < pressSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / pressSpeed;
            transform.localPosition = Vector3.Lerp(originalPosition, pressedPosition, t);
            yield return null;
        }

        // Aguarda
        if (holdDuration > 0)
        {
            yield return new WaitForSeconds(holdDuration);
        }

        // Retorna o botão à posição original
        elapsedTime = 0f;
        while (elapsedTime < pressSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / pressSpeed;
            transform.localPosition = Vector3.Lerp(pressedPosition, originalPosition, t);
            yield return null;
        }

        transform.localPosition = originalPosition;
        isAnimating = false;
    }

    private bool IsSequenceCorrect()
    {
        if (currentSequence.Count != correctSequence.Count) return false;

        // Verifica se a sequência atual é um prefixo da sequência correta
        for (int i = 0; i < currentSequence.Count; i++)
        {
            if (currentSequence[i] != correctSequence[i])
            {
                return false;
            }
        }
        return true;
    }

    private void ResetSequence()
    {
        currentSequence.Clear();
    }

    private void OpenDoor()
    {
        if (doorAnimator != null && !doorAnimator.GetBool("isOpen"))
        {
            doorAnimator.SetBool("isOpen", true);

            if (doorSound != null)
            {
                doorSound.Play();
            }

            // Reseta a sequência após abrir a porta
            ResetSequence();
        }
    }
}
