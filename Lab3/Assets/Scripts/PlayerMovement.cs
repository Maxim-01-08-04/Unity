using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;            // Скорость перемещения персонажа
    [SerializeField] float jumpForce = 5f;            // Сила прыжка персонажа
    [SerializeField] CharacterController controller;  // Ссылка на контроллер персонажа

    [SerializeField] float gravity = -9.81f;          // Сила притяжения
    Vector3 direction = Vector3.zero;                 // Направление перемещения персонажа
    Vector3 velocity = Vector3.zero;                  // Скорость перемещения игрока
    bool grounded = true;                             // Флаг, определяющий, стоит ли персонаж на поверхности

    // Метод, вызываемый системой ввода при нажатии клавиш перемещения (WASD)
    private void OnMove(InputValue movementValue)
    {
if (GameManager.Instance != null && GameManager.Instance.isGameActive == false)
        {
            return; // Персонаж намертво застывает на месте, а мышка освобождается для клика
        }
        Vector2 movementVector = movementValue.Get<Vector2>();
        // Расчёт направления перемещения
        direction = new Vector3(movementVector.x, 0, movementVector.y).normalized;
    }

    // Метод, вызываемый системой ввода при нажатии клавиши прыжка (Пробел)
    private void OnJump()
    {
if (GameManager.Instance != null && GameManager.Instance.isGameActive == false)
        {
            return; // Персонаж намертво застывает на месте, а мышка освобождается для клика
        }
        if (grounded) // Если персонаж имеет контакт с поверхностью снизу
        {
            velocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravity);
        }
    }

    private void LateUpdate()
    {
        grounded = controller.isGrounded; // Получение информации о контакте игрока с поверхностью
        
        // Перемещение в плоскости XZ
        controller.Move(transform.TransformDirection(direction) * (moveSpeed * Time.deltaTime));

        if (grounded && velocity.y < 0)
        {
            velocity.y = 0; // Предотвращение прохождения персонажа сквозь поверхность снизу
        }

        // Применение силы тяжести
        velocity.y += gravity * Time.deltaTime;

        // Перемещение персонажа по оси Y (гравитация и прыжок)
        controller.Move(velocity * Time.deltaTime);
    }
}
