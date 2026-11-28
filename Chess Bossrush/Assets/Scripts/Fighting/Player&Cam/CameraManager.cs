using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float sens;

    [SerializeField] LayerMask defaultMask;
    [SerializeField] LayerMask entity;

    public bool lockingOn;

    FighterPlayerManager player;

    float rotateY;
    float rotateX;

    GameObject rotateObj;

    Vector3 cameraBasePos;

    Enemy lockOnTarget;

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
            changeCameraPos = Vector3.Lerp(changeCameraPos, changeCameraPos.normalized * (hit.distance - 0.4f), 15f * Time.deltaTime);
            rotateObj.transform.localPosition = changeCameraPos;
        }
        else
            rotateObj.transform.localPosition = cameraBasePos;

        transform.position = Vector3.Lerp(transform.position, player.transform.position + Vector3.up * 1.5f, 15f * Time.deltaTime);
        if (lockingOn && lockOnTarget != null)
            transform.forward = lockOnTarget.transform.position - transform.position + 1.5f * Vector3.up;
    }
    public void MoveCamera(Vector2 moved)
    {
        if (lockingOn)
            return;
        rotateY += moved.x * sens;
        rotateX -= moved.y * sens;
        rotateX = Mathf.Clamp(rotateX, -45f,45f);
        transform.rotation = Quaternion.Euler(rotateX,rotateY,0);
    }

    void CheckLockOn()
    {
        Debug.Log("checking");
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, 30f, entity);
        GameObject lockingOn = null;
        float distance = Mathf.Infinity;
        foreach (Collider collider in hitObjects) 
        {
            Vector3 distanceVec = collider.transform.position - transform.position;
            if (Vector3.Angle(distanceVec.normalized, transform.forward) > 45f)
                continue;
            if (distanceVec.magnitude > distance)
                continue;
            if (collider.GetComponent<Enemy>() == null) 
                continue;
            distance = distanceVec.magnitude;
            lockingOn = collider.gameObject;
        }
        if (lockingOn != null)
            lockOnTarget = lockingOn.GetComponent<Enemy>();
        else
            lockOnTarget=null;
    }

    public void SetUpLockOn()
    {
        CheckLockOn();
        if (lockOnTarget == null || lockingOn)
            lockingOn = false;
        else
            lockingOn = true;
    }
}
