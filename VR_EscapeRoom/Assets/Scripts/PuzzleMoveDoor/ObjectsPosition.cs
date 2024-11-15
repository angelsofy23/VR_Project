using UnityEngine;

public class ObjectsPosition : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToPlace;  // Objetos a serem colocados
    [SerializeField] private GameObject[] _places;           // Posições corretas para os objetos
    [SerializeField] private OpenClosedDoor _door;           // Referência ao script que controla a porta

    // Update é chamado uma vez por frame
    void Update()
    {
        // Verifica se o quebra-cabeça foi resolvido
        if (PuzzleSolved())
        {
            Debug.Log("Puzzle is solved");  // Log de depuração
            _door.Open();  // Abre a porta
            Destroy(this);  // Destrói o script após resolver o puzzle (opcional)
        }
    }

    // Função que verifica se todos os objetos estão nas posições corretas
    bool PuzzleSolved()
    {
        for (int i = 0; i < _places.Length; i++)
        {
            var c1 = _objectsToPlace[i].GetComponent<Collider>();  // Collider do objeto
            var c2 = _places[i].GetComponent<Collider>();          // Collider da posição

            // Verifica se o objeto está sobre a posição certa (colisão)
            if (!c1.bounds.Intersects(c2.bounds))
            {
                return false;  // Retorna falso se algum objeto não estiver na posição correta
            }
        }

        // Retorna verdadeiro quando todos os objetos estão na posição correta
        return true;
    }
}
