using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] Transform player;      // Ссылка на объект всего игрока
    [SerializeField] Transform visor;       // Ссылка на объект глаз/головы игрока

    [Range(0.01f, 10f)] [SerializeField] float xSens = 0.5f; // Чувствительность по горизонтали
    [Range(0.01f, 10f)] [SerializeField] float ySens = 0.5f; // Чувствительность по вертикали

    Quaternion center;

        private void Start()
    {
        center = visor.localRotation;
        
        // Более безопасный способ включить ввод в Unity 6
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null && playerInput.actions != null)
        {
            playerInput.actions.Enable();
        }
    }


    // Этот метод вызывается компонентом Player Input при движении мыши
    private void OnLook(InputValue lookValue)
    {
if (GameManager.Instance != null && GameManager.Instance.isGameActive == false)
        {
            return; // Персонаж намертво застывает на месте, а мышка освобождается для клика
        }
        Vector2 rotation = lookValue.Get<Vector2>(); // Получение смещения курсора
        
        // Умножаем на 0.1, чтобы сгладить движения
        float mouseX = rotation.x * xSens * 0.1f;
        float mouseY = rotation.y * ySens * 0.1f;
        
        // Расчёт поворота вокруг оси X (вверх/вниз)
        Quaternion yRotation = visor.localRotation * Quaternion.AngleAxis(mouseY, -Vector3.right);
        
        // Ограничение в 90 градусов
        if (Quaternion.Angle(center, yRotation) < 90)
            visor.localRotation = yRotation;

        // Расчёт поворота вокруг оси Y (влево/вправо)
        Quaternion xRotation = player.localRotation * Quaternion.AngleAxis(mouseX, Vector3.up);
        player.localRotation = xRotation;
    }
}
