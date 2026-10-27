using UnityEngine;

public class FlyAndTiltMovement : MonoBehaviour
{
    [Header("Настройки перемещения вверх-вниз")]
    [SerializeField] private float moveSpeed = 2f;    // Скорость полета по вертикали
    [SerializeField] private float moveDistance = 3f; // Дальность полета в метрах

    [Header("Настройки наклона влево-вправо")]
    [SerializeField] private float tiltSpeed = 3f;    // Скорость качания
    [SerializeField] private float tiltAngle = 15f;   // Угол наклона в градусах

    [Header("Ссылка на Аниматор мишени")]
    [SerializeField] private Animator targetAnimator; // Ссылка на объект Null

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        // Запоминаем стартовую позицию и вращение
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Update()
    {
        // Сложное движение работает ТОЛЬКО тогда, когда мишень поднята
        if (targetAnimator != null && targetAnimator.GetBool("isUp") == true)
        {
            // 1. Считаем перемещение по вертикали (ось Y)
            float pingPongY = Mathf.PingPong(Time.time * moveSpeed, moveDistance) - (moveDistance / 2f);
            transform.position = startPosition + new Vector3(0, pingPongY, 0);

            // 2. Считаем независимый наклон по оси Z (качание влево-вправо)
            float waveZ = Mathf.Sin(Time.time * tiltSpeed) * tiltAngle;
            transform.rotation = startRotation * Quaternion.Euler(0, 0, waveZ);
        }
    }
}
