using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] RenderTexture m_renderTexture;
    [SerializeField] Camera m_camera;
    private void Awake()
    {
        m_renderTexture.width = Screen.width;
        m_renderTexture.height = Screen.height;
    }

    private void OnTriggerEnter(Collider other)
    {
        var localPos = transform.InverseTransformPoint(other.transform.position);

        if (localPos.z > 0)
            SwitchCullMask(true);
    }
    private void SwitchCullMask(bool on)
    {
        m_camera.cullingMask |= (on ? 1 : 0) << LayerMask.NameToLayer("Observable");
    }
}
