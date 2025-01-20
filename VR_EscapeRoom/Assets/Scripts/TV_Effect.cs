using UnityEngine;

public class TV_Effect : MonoBehaviour
{
    private Material yourPicMaterial;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null)
        {
            // Acesse o primeiro material (caso haja mais, use renderer.materials)
            Material[] materials = renderer.materials;

            // Encontre o material chamado "YourPic"
            foreach (Material mat in materials)
            {
                if (mat.name.Contains("YourPic"))
                {
                    yourPicMaterial = mat;
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("Renderer não encontrado no objeto alvo.");
        }
    }

    // Exemplo: Altere a cor do material para vermelho
        private Vector2 offset;

    public float scrollSpeedX = 0.1f; // Speed for horizontal offset
    public float scrollSpeedY = 0.1f; // Speed for vertical offset
    public float distanceX;
    void Update()
    {
        // Calculate the new offset based on time and speed
        offset.x = Mathf.Sin(scrollSpeedX * Time.deltaTime) * distanceX;
    
        offset.y += scrollSpeedY * Time.deltaTime;

        // Apply the offset to the material
        if (yourPicMaterial != null)
        {
            yourPicMaterial.SetTextureOffset("_BaseMap", offset); // Use the correct texture property name
        }
    }
}

