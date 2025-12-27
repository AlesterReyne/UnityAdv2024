using UnityEngine;

public class DynamicObstacle : MonoBehaviour
{
    // === Inspector Settings ===
    [SerializeField] private float speed = 1f;

    [SerializeField] private float amplitude = 5f;
    // === Internal State ===
    private float _startZ;  // Original Y position of obstacle
    
    void Start()
    {
        _startZ = transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Vertical sine-wave movement
        MovingBySin(transform, _startZ, speed, amplitude);
    }
    
    public static void MovingBySin(Transform transform, float startPosition, float speed = 1f, float amplitude = 1f)
    {
        Vector3 position = transform.position;
        position.z = startPosition + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = position;
    }
}
