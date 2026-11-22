using UnityEngine;
using UnityEngine.InputSystem.DualShock;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float sens;

    [SerializeField] LayerMask defaultMask;

    FighterPlayerManager player;
    
    float rotateY;
    float rotateX;

    GameObject rotateObj;
    Vector3 cameraBasePos;

    private void Start()
    {
        rotateObj=transform.GetChild(0).gameObject;
        cameraBasePos = rotateObj.transform.localPosition;
        player = FindAnyObjectByType<FighterPlayerManager>();
    }

    private void Update()
    {
        Vector3 lookDir = transform.rotation * cameraBasePos;
        if (Physics.SphereCast(transform.position, 0.5f, lookDir.normalized, out RaycastHit hit, cameraBasePos.magnitude, defaultMask))
        {
            Vector3 changeCameraPos = rotateObj.transform.localPosition;
            changeCameraPos = Vector3.ClampMagnitude(cameraBasePos, hit.distance - 0.4f);
            changeCameraPos = Vector3.Lerp(changeCameraPos, changeCameraPos.normalized * (hit.distance - 0.4f), 5f * Time.deltaTime);
            rotateObj.transform.localPosition = changeCameraPos;
        }
        else
            rotateObj.transform.localPosition = cameraBasePos;

        transform.position = Vector3.Lerp(transform.position, player.transform.position + Vector3.up * 1.5f, 5f * Time.deltaTime);
    }
    public void MoveCamera(Vector2 moved)
    {
        
        rotateY += moved.x * sens;
        rotateX -= moved.y * sens;
        rotateX = Mathf.Clamp(rotateX, -45f,45f);
        transform.rotation = Quaternion.Euler(rotateX,rotateY,0);
    }
}
