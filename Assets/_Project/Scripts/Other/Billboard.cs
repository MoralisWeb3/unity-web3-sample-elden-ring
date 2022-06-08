using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera _mainCamera;
    private void Awake()
    {
        _mainCamera = Camera.main;

        if (_mainCamera == null)
        {
            Debug.LogError("We need a MainCamera to work :)");
        }
    }

    private void LateUpdate()
    {
        if (_mainCamera is null) return;
        
        transform.LookAt(_mainCamera.transform);
    }
}
