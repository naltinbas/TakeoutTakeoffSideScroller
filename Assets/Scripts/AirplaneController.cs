using System.Collections;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    // Update is called once per frame
    public float airplaneSpeed = 5f;

    public bool IsGoingBack => (_yaw < 0 ? -_yaw : _yaw) % 360 > 90f 
                               && (_yaw < 0 ? -_yaw : _yaw) % 360 < 270f;

    private float _yaw = 0.0f,
        _pitch = 0.0f,
        _roll = 0.0f;

    private const float YawMultiplier = 120f, 
        PitchMax = 20f,
        RollMax = 20f;

    // Update is called once per frame
    void Update()
    {

        if (RespawnIfNecessary()) return;
        transform.position += transform.forward * airplaneSpeed * Time.deltaTime;
        float horizontal = Input.GetAxis("Horizontal"),
            vertical = Input.GetAxis("Vertical");
        _yaw += horizontal * YawMultiplier * Time.deltaTime;
        _pitch = Mathf.Lerp(0f, PitchMax, Mathf.Abs(vertical)) * Mathf.Sign(-vertical);
        _roll = Mathf.Lerp(0f, RollMax, Mathf.Abs(horizontal)) * Mathf.Sign(-horizontal);
        transform.localRotation = Quaternion.Euler(Vector3.up * _yaw + Vector3.right * _pitch + Vector3.forward * _roll);
        
    }

    private bool RespawnIfNecessary()
    {
        bool shouldReset = !IsInMissionArea(transform.position);
        if (shouldReset)
        {
            ExplosionHandler explosionHandler = GetComponent<ExplosionHandler>();
            if(!explosionHandler) return true;
            explosionHandler.Explode();
        }

        return shouldReset;
    }

    private bool IsInMissionArea(Vector3 point)
    {
        float x = point.x,
            z = point.z;
        return x is < 35f and > -10f && 
               z is > -31f and < 76f;
    }

    // private float CalculatePositiveAngle(float a)
    // {
    //     float Mod(float a, float b)
    //     {
    //         while (a < 0)
    //         {
    //             a += b;
    //         }
    //
    //         while (a > b)
    //         {
    //             a -= b;
    //         }
    //
    //         return a;
    //     }
    //
    //     return Mod(a, 360f);
    // }

    public void Reset()
    {
        transform.position = GameObject.Find("InitialPosition").transform.position;
        _yaw = 0f;
        _pitch = 0f;
        _roll = 0f;
    }
}
