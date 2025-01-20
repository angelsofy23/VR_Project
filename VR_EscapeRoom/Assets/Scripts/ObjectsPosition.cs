using UnityEngine;

public class ObjectsPosition : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToPlace;
    [SerializeField] private GameObject[] _places;
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _door2;
    [SerializeField] private Animator doorAnimator; 
    [SerializeField] private Animator doorAnimator2; 
    [SerializeField] private AudioSource doorAudioSource; 

    private bool isDoorOpening = false;
    private bool hasSoundPlayed = false;

    void Update()
    {
        if (PuzzleSolved() && !isDoorOpening)
        {
            if (!hasSoundPlayed && doorAudioSource != null)
            {
                doorAudioSource.Play();
                hasSoundPlayed = true;
            }
            OpenDoorsWithAnimation();
        }
    }

    void OpenDoorsWithAnimation()
    {
        isDoorOpening = true;
        doorAnimator.SetBool("isOpen", true);
        doorAnimator2.SetBool("isOpen", true);
    }

    bool PuzzleSolved()
    {
        // Add null checks and array length validation
        if (_objectsToPlace == null || _places == null || _objectsToPlace.Length != _places.Length)
        {
            return false;
        }

        for (int i = 0; i < _places.Length; i++)
        {
            if (_objectsToPlace[i] == null || _places[i] == null)
            {
                return false;
            }

            var c1 = _objectsToPlace[i].GetComponent<Collider>();
            var c2 = _places[i].GetComponent<Collider>();

            if (c1 == null || c2 == null || !c1.bounds.Intersects(c2.bounds))
            {
                return false;
            }
        }
        return true;
    }
}
