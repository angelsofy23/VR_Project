using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    // Sequência correta e sequência atual
    private static readonly List<int> correctSequence = new List<int> { 1, 1, 1, 2, 2, 3 };
    private static List<int> currentSequence = new List<int>();

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void OnButtonPress()
    {
        if (isAnimating) return;

        // Adiciona o ID do botão pressionado à sequência atual
        currentSequence.Add(buttonID);
        Debug.Log($"Botão {buttonID} pressionado. Sequência atual: {string.Join(", ", currentSequence)}");

        // Verifica se a sequência está correta até agora
        if (!IsSequenceCorrect())
        {
            Debug.Log("Sequência incorreta! Resetando...");
            ResetSequence();
        }
        else if (currentSequence.Count == correctSequence.Count)
        {
            Debug.Log("Sequência correta! Abrindo a porta...");
            OpenDoor();
        }

        StartCoroutine(ButtonAnimation());
    }

    private IEnumerator ButtonAnimation()
    {
        isAnimating = true;
        Vector3 pressedPosition = originalPosition + Vector3.down * pressDepth;

        // Pressiona o botão para baixo
        float elapsedTime = 0f;
        while (elapsedTime < pressSpeed)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / pressSpeed;
            transform.localPosition = Vector3.Lerp(originalPosition, pressedPosition, t);
            yield return null;
        }

        // Aguarda (se necessário)
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
        // Ativa a animação e som da porta (se configurados)
        if (doorAnimator != null)
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


