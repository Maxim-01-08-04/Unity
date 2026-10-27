using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private Animator targetAnimator; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.GetComponent<CharacterController>() != null)
        {
            if (targetAnimator != null)
            {
                targetAnimator.SetBool("isUp", true);
                Debug.Log("Игрок подошел к зоне! Мишень взведена.");
            }
        }
    }
    // Метод OnTriggerExit удален, теперь мишень сама не упадет!
}