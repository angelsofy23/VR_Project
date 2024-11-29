using UnityEngine;

[RequireComponent(typeof(SVLever))]
public class SVLeverSoundFX : MonoBehaviour
{
    public AudioClip leverDown; // Som ao mover a alavanca para "ligado"
    public AudioClip leverUp;   // Som ao mover a alavanca para "desligado"
    public float minPitch = 0.8f;
    public float maxPitch = 1.2f;
    public float volume = 1f;

    private SVLever lever;
    private AudioSource audioSource;
    private bool previousLeverState;

    void Start()
    {
        lever = GetComponent<SVLever>();
        audioSource = GetComponent<AudioSource>();
        previousLeverState = lever.leverIsOn; // Estado inicial da alavanca
    }

    void Update()
    {
        if (lever.leverIsOn != previousLeverState) // Detecta mudança no estado da alavanca
        {
            // Reproduz o som apropriado com base no novo estado
            if (lever.leverIsOn && leverOnAudioIsSet())
            {
                PlaySound(leverDown);
            }
            else if (!lever.leverIsOn && leverOffAudioIsSet())
            {
                PlaySound(leverUp);
            }

            // Atualiza o estado anterior
            previousLeverState = lever.leverIsOn;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(clip, volume);
        }
    }

    private bool leverOnAudioIsSet()
    {
        return leverDown != null;
    }

    private bool leverOffAudioIsSet()
    {
        return leverUp != null;
    }
}
