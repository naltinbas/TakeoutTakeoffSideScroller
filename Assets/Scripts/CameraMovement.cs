using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private GameObject _player;
    private float _followSpeed = 5f;
    private AirplaneController _airplaneController;
    void Start()
    {
        _player  = GameObject.FindGameObjectWithTag("Player");
        _airplaneController = _player.GetComponent<AirplaneController>();
    }

    void LateUpdate()
    {
        if (_player == null) return;
        Vector3 offset = new Vector3(10f, 5f, 5f);
        if (_airplaneController.IsGoingBack)
        {
            offset.z *= -1;
        }
        Vector3 targetPos = _player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);//Vector3.Lerp(transform.position, targetPos, _followSpeed/4f * Time.deltaTime);
    }
}