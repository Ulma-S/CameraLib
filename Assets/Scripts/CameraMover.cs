using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private GameObject _Player;
    private Vector3 _Distance;

    private void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");

        _Distance = transform.position - _Player.transform.position;
    }

    private void Update()
    {
        var destPos = _Player.transform.position + _Distance;

        transform.position = Vector3.Lerp(transform.position, destPos, 1f * Time.deltaTime);
    }
}
