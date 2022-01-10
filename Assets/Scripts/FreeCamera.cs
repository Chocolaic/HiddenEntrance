using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FreeCamera : MonoBehaviour
{
    [SerializeField] float m_rotSpeed = 2f, m_moveSpeed = 2f;
    [SerializeField] Transform m_camera;
    private Rigidbody m_rigidbody;

    private float _rotX, _rotY;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        ResetAngle();
    }

    private void Update()
    {
        _rotX = Mathf.Clamp(_rotX - Input.GetAxis("Mouse Y") * m_rotSpeed, -90f, 90f);
        _rotY = ClampRotation(_rotY + Input.GetAxis("Mouse X") * m_rotSpeed);
        m_camera.rotation = Quaternion.Euler(_rotX, _rotY, 0);

        var moveDirection = Vector3.zero;
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");

        m_rigidbody.velocity = m_camera.TransformDirection(moveDirection.normalized * m_moveSpeed);
    }
    private void ResetAngle()
    {
        var eulerAngle = m_rigidbody.rotation.eulerAngles;
        _rotX = eulerAngle.x;
        _rotY = eulerAngle.y;
    }
    private float ClampRotation(float angle)
    {
        if (angle < 0) return angle + 360f;
        if (angle > 360f) return angle - 360f;
        return angle;
    }
}
