using Unity.Mathematics.Geometry;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    
    public float airplaneSpeed = 5f;

    private float _yaw = 0.0f;

    private const float YawMultiplier = 120f, 
        PitchMax = 20f,
        RollMax = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * airplaneSpeed * Time.deltaTime;
        float horizontal = Input.GetAxis("Horizontal"),
            vertical = Input.GetAxis("Vertical");
        _yaw += horizontal * YawMultiplier * Time.deltaTime;
        float pitch = Mathf.Lerp(0f, PitchMax, Mathf.Abs(vertical)) * Mathf.Sign(vertical),
            roll = Mathf.Lerp(0f, RollMax, Mathf.Abs(horizontal)) * Mathf.Sign(-horizontal);
        transform.localRotation = Quaternion.Euler(Vector3.up * _yaw + Vector3.right * pitch + Vector3.forward * roll);
        
    }
}
