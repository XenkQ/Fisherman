using UnityEngine;

public class Harpoon : MonoBehaviour
{
    [SerializeField] private float minHarpoonRotationAngle;
    [SerializeField] private float maxHarpoonRotationAngle;
    private ClickToStart clickToStart;
    private Vector3 mousePos;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        clickToStart = GameObject.FindGameObjectWithTag("ClickToStart").GetComponent<ClickToStart>();
    }

    private void Update()
    {
        if(!clickToStart.canDisplay)
        {
            RotateTowardsCameraDirection();
        }
    }

    private void RotateTowardsCameraDirection()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg + 90;

        if(angle <= minHarpoonRotationAngle || angle >= maxHarpoonRotationAngle){return;}
        else
        {
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
    }
}
