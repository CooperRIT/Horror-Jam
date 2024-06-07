using UnityEngine;

public class CursorLock : MonoBehaviour
{
    [SerializeField] private CursorLockMode mode;

    private void Start() => Cursor.lockState = mode;

    private void Update()
    {
        if (Time.timeScale <= 0)
            Cursor.lockState = CursorLockMode.None;
    }
}
