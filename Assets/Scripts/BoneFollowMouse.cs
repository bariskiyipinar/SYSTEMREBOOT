using UnityEngine;

public class BoneFollowMouse : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform gunHolder;

    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        float distance = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        mouseScreenPos.z = distance;

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = transform.position.z;

        Vector3 direction = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Kolun yönünü belirle
        if (playerController.FacingLeft)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);
            gunHolder.localScale = new Vector3(-1f, -1f, 1f); // hem X hem Y ekseninde ters çevir
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            gunHolder.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
