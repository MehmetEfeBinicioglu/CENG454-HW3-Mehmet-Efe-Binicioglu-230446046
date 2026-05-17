using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Time.timeScale == 0f) return;

        if (Input.GetMouseButtonDown(0) || (UnityEngine.InputSystem.Mouse.current != null && UnityEngine.InputSystem.Mouse.current.leftButton.wasPressedThisFrame))
        {
            Vector3 mousePos = Input.mousePosition;
            if (UnityEngine.InputSystem.Mouse.current != null)
            {
                Vector2 newMousePos = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
                mousePos = new Vector3(newMousePos.x, newMousePos.y, 0f);
            }

            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                IDamageable damageable = hit.collider.GetComponent<EnemyHealthDecorator>();
                if (damageable == null)
                {
                    damageable = hit.collider.GetComponent<IDamageable>();
                }

                if (damageable != null && hit.collider.gameObject != FindFirstObjectByType<CoreHealth>().gameObject)
                {
                    damageable.TakeDamage(10);
                }
            }
        }
    }
}