using UnityEngine;

public class CursorLock : MonoBehaviour
{
    [SerializeField] bool cursorLock = true; // Начальное состояние курсора

    private void Start() => changeCursorState();

    // Этот метод автоматически сработает при нажатии кнопки Escape
    void OnEscape()
    {
        cursorLock = !cursorLock; // Смена состояния курсора на противоположное
        changeCursorState();
    }

    void changeCursorState()
    {
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
