using UnityEngine;

public class ShellEjectionScript : MonoBehaviour
{
    [SerializeField] Transform ejectionPoint;   // ссылка на точку выброса гильзы
    [SerializeField] GameObject shellPrefab;    // ссылка на шаблон гильзы
    [Range(1, 100)] [SerializeField] float ejectionForce = 10; // сила выброса
    [SerializeField] [Range(1, 10)] float duration = 5;       // длительность существования гильзы

    public void ejectShell()
    {
        GameObject shell = Instantiate(shellPrefab, ejectionPoint.position, ejectionPoint.rotation);
        shell.GetComponent<Rigidbody>().AddForce(ejectionPoint.right * ejectionForce, ForceMode.Impulse);

        Destroy(shell, duration);
    }
}
