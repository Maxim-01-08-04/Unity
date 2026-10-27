using UnityEngine;

public class PingPongMovement : MonoBehaviour
{
    public enum MovementType { LeftRight, UpDown, TiltLeftRight, DiagonalUpDown }

    [Header("Тип уникального движения мишени")]
    [SerializeField] private MovementType movementType;

    [Header("Настройки скорости и дистанции")]
    [SerializeField] private float speed = 2f;        // Скорость движения/наклона
    [SerializeField] private float distance = 3f;     // Дальность поездки (в метрах) или угол наклона (в градусах)

    [Header("Ссылка на Аниматор мишени")]
    [SerializeField] private Animator targetAnimator; // Ссылка на Null

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Start()
    {
        // Запоминаем стартовую позицию и вращение всего контейнера (объекта script)
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Update()
    {
        // Двигаемся ТОЛЬКО тогда, когда мишень поднята аниматором (isUp == true)
        if (targetAnimator != null && targetAnimator.GetBool("isUp") == true)
        {
            // Математический маятник для плавного хода туда-сюда
            float pingPong = Mathf.PingPong(Time.time * speed, distance) - (distance / 2f);

            switch (movementType)
            {
                case MovementType.LeftRight:
                    // 1. Движение влево-вправо (по оси X)
                    transform.position = startPosition + new Vector3(pingPong, 0, 0);
                    break;

                case MovementType.UpDown:
                    // 2. Движение вверх-вниз (по оси Y)
                    transform.position = startPosition + new Vector3(0, pingPong, 0);
                    break;

                case MovementType.TiltLeftRight:
                    // 3. Наклон (качание) влево-вправо (вращение по оси Z)
                    // Используем синус для более мягкого и реалистичного качания из стороны в сторону
                    float wave = Mathf.Sin(Time.time * speed) * distance;
                    transform.rotation = startRotation * Quaternion.Euler(0, 0, wave);
                    break;

                case MovementType.DiagonalUpDown:
                    // 4. Ездит влево-вправо И одновременно уходит вверх-вниз (диагональ)
                    transform.position = startPosition + new Vector3(pingPong, pingPong, 0);
                    break;
            }
        }
    }
}
