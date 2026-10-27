using UnityEngine;

public class SpinnerMovement : MonoBehaviour
{
    [Header("Настройки вращения")]
    [SerializeField] private float rotationSpeed = 15f; // Скорость вращения (градусы в секунду)

    [Header("Ссылка на Аниматор мишени")]
    [SerializeField] private Animator targetAnimator;   // Ссылка на объект Null

    private void Update()
    {
        // Вертушка крутится ТОЛЬКО тогда, когда мишень поднята триггером (isUp == true)
        if (targetAnimator != null && targetAnimator.GetBool("isUp") == true)
        {
            // Плавно вращаем объект каждый кадр вокруг оси Z (лицом к игроку)
            // Если мишень начнет крутиться боком, замени (0, 0, rotationSpeed...) на (rotationSpeed, 0, 0) или (0, rotationSpeed, 0)
            transform.Rotate(rotationSpeed, 0, 0 * Time.deltaTime);
        }
    }
}
