using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    // Ссылка на точку, куда мы переместим игрока (перетащим в инспекторе)
    [SerializeField] private Transform respawnPoint; 

    // Метод срабатывает, когда игрок заходит в зону-триггер
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что в зону зашел именно Игрок (ищем на нем компонент ходьбы)
        CharacterController playerController = other.GetComponent<CharacterController>();

        if (playerController != null && respawnPoint != null)
        {
            Debug.Log("Игрок попал в запретную зону! Телепортируем на респавн.");

            // 1. Временно отключаем контроллер ходьбы, чтобы Unity разрешила сдвинуть объект
            playerController.enabled = false;

            // 2. Мгновенно переносим игрока в координаты нашей точки респавна
            other.transform.position = respawnPoint.position;
            
            // Если нужно, чтобы игрок еще и разворачивался лицом туда, куда смотрит точка:
            other.transform.rotation = respawnPoint.rotation;

            // 3. Включаем ходьбу обратно
            playerController.enabled = true;
        }
    }
}
