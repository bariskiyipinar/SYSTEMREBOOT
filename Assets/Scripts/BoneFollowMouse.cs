using UnityEngine;

public class BoneFollowMouse : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform gunHolder;

    void Update()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(mainCamera.transform.position.z - transform.position.z);
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = transform.position.z;

        Vector3 direction = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        float rotationOffset = 0f;

        if (playerController.FacingLeft)
        {
            rotationOffset = 180f;
            gunHolder.localScale = new Vector3(1f, -1f, 1f);
        }
        else
        {
            rotationOffset = 0f;
            gunHolder.localScale = new Vector3(1f, 1f, 1f);
        }

        transform.rotation = Quaternion.Euler(0f, 0f, angle + rotationOffset);
    }
}
