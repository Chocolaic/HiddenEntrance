using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Gate : MonoBehaviour
{
    [SerializeField] Camera m_mainCamera, m_observCamera;

    private RenderTexture _renderTexture;
    private int _cullingLayer;
    private void Awake()
    {
        _renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        m_observCamera.targetTexture = _renderTexture;
        GetComponent<MeshRenderer>().material.mainTexture = _renderTexture;

        _cullingLayer = m_mainCamera.cullingMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        var localPos = transform.InverseTransformPoint(other.transform.position);

        if (localPos.z > 0)
            m_mainCamera.cullingMask = _cullingLayer | 1 << LayerMask.NameToLayer("Observable");
    }
    private void OnTriggerExit(Collider other)
    {
        var localPos = transform.InverseTransformPoint(other.transform.position);

        if (localPos.z > 0)
            m_mainCamera.cullingMask = _cullingLayer;
    }
}
