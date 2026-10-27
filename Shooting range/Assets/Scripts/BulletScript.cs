using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Игнорируем игрока и оружие
        if (collision.gameObject.CompareTag("Player") || 
            collision.gameObject.GetComponent<CharacterController>() != null || 
            collision.gameObject.GetComponent<WeaponScript>() != null)
        {
            return;
        }

        // ОБЯЗАТЕЛЬНЫЙ ЛОГ: Если пуля вообще существует и бьется, этот текст появится в консоли!
        Debug.Log("ФИЗИЧЕСКАЯ ПУЛЯ УДАРИЛАСЬ О: " + collision.gameObject.name);

        // Ищем скрипт мишени: сначала на самом объекте, затем у его РОДИТЕЛЕЙ (вверх по иерархии)
        TargetScript target = collision.gameObject.GetComponent<TargetScript>();
        if (target == null)
        {
            target = collision.gameObject.GetComponentInParent<TargetScript>();
        }

        // Если нашли скрипт мишени — роняем её
        if (target != null)
        {
            target.HitTarget();
        }

        // Уничтожаем пулю после любого удара
        Destroy(gameObject);
    }
}
