using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [SerializeField] private Animator animator; // Ссылка на Аниматор мишени

        public void HitTarget()
    {
        // Проверяем: если мишень уже лежит (isUp == false), то игнорируем попадание
        if (animator != null && animator.GetBool("isUp") == false) return;

        Debug.Log("МЕТОД HitTarget() СРАБОТАЛ! Пытаемся уронить объект: " + gameObject.name);

        if (animator != null)
        {
            animator.SetBool("isUp", false);
            
            // НОВАЯ СТРОЧКА: Стучимся в GameManager и даем 10 очков за сбитую мишень!
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore(10); 
            }
        }
    }

}
