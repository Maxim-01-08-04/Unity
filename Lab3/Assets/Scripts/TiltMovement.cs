using UnityEngine;

public class TiltMovement : MonoBehaviour
{
    [Header("Настройки наклона")]
    [SerializeField] private float tiltSpeed = 2f;    // Скорость качания
    [SerializeField] private float tiltAngle = 20f;   // Максимальный угол наклона в градусах

    [Header("Ссылка на Аниматор мишени")]
    [SerializeField] private Animator targetAnimator; // Ссылка на объект Null

    private Quaternion startRotation;

    private void Start()
    {
        // Запоминаем изначальное ровное вращение
        startRotation = transform.rotation;
    }

    private void Update()
    {
        // Качаемся ТОЛЬКО тогда, когда мишень поднята (isUp == true)
        if (targetAnimator != null && targetAnimator.GetBool("isUp") == true)
        {
            // Используем плавную волну синуса для качания влево-вправо по оси Z
            float wave = Mathf.Sin(Time.time * tiltSpeed) * tiltAngle;
            transform.rotation = startRotation * Quaternion.Euler(0, 0, wave);
        }
    }
}
