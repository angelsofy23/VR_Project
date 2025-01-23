using UnityEngine;

public class EndGame : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif

            Application.Quit();
        }
    }
}