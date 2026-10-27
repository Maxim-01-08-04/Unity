using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class WeaponScript : MonoBehaviour
{
    [Header("Ссылки на компоненты")]
    [SerializeField] Animator animator;
    [SerializeField] ShellEjectionScript ejection;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firingPoint;
    [SerializeField] float bulletSpeed = 50f;

    [Header("Настройки стрельбы")]
    [SerializeField] Camera playerCamera;
    [SerializeField] float fireRange = 100f;
    [SerializeField] int maxAmmo = 10;

    int currentAmmo;
    bool isFiring = false;

    private void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    private void Update()
    {
        // ЕСЛИ ИГРА ОКОНЧЕНА — полностью блокируем считывание ЛКМ и кнопки R
        if (GameManager.Instance != null && GameManager.Instance.isGameActive == false)
        {
            isFiring = false; // Принудительно гасим зажатый курок
            animator.SetBool("isFiring", false); // Выключаем анимацию стрельбы
            return; // Выходим из метода, код ниже (стрельба и перезарядка) не выполнится!
        }
        if (Mouse.current != null)
        {
            isFiring = Mouse.current.leftButton.isPressed;
        }

        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            animator.SetTrigger("Reloading");
        }

        if (isFiring && currentAmmo > 0)
        {
            animator.SetBool("isFiring", true);
        }
        else
        {
            animator.SetBool("isFiring", false);
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo + " / " + maxAmmo;
        }
    }

    public void FireEvent()
    {
        if (currentAmmo <= 0) return;

        currentAmmo--;
        UpdateAmmoUI();

                
        if (muzzleFlash != null) 
        {
            muzzleFlash.Play(); 
        }


        if (ejection != null)
        {
            ejection.ejectShell();
        }

        // === ЛОГИКА ПОЛЕТА ПУЛИ В ЦЕНТР МИШЕНИ ===
        if (bulletPrefab != null && firingPoint != null && playerCamera != null)
        {
            // Сначала делаем Raycast, чтобы найти точку попадания в мишень
            RaycastHit hit = default;
            bool hasHit = Physics.Raycast(
                playerCamera.transform.position,
                playerCamera.transform.forward,
                out hit,
                fireRange
            );

            // Определяем целевую точку: если попали в объект - используем hit.point, иначе - точка на дальности fireRange
            Vector3 targetPoint = hasHit ? hit.point : (playerCamera.transform.position + playerCamera.transform.forward * fireRange);

            // Рассчитываем направление от дула к целевой точке
            Vector3 shootDirection = (targetPoint - firingPoint.position).normalized;

            // Спавним пулю и направляем её точно в цель
            GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.LookRotation(shootDirection));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.linearVelocity = shootDirection * bulletSpeed;
            }

            Destroy(bullet, 3f);
        }

        // Проверка попадания пули лучом (Raycast)
        ShootRaycast();
    }

    void ShootRaycast()
    {
        if (playerCamera == null) return; // Проверяем камеру

        RaycastHit hit;
        // Пускаем луч вперед только для того, чтобы в консоли регистрировать, куда смотрит игрок
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, fireRange))
        {
            Debug.Log("Луч прицела направлен на объект: " + hit.transform.name); //
            
            // СЮДА НИЧЕГО БОЛЬШЕ НЕ ПИШЕМ! Метод теперь полностью безопасен и не наносит урон.
        }
    }

    

    public void ReloadEvent()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
        Debug.Log("Оружие успешно перезаряжено! Патронов: " + currentAmmo);
    }
}