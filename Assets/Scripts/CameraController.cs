using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 pos;

    private void Awake()
    {
        if (!_player)
            _player = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        pos = new Vector3(_player.position.x, _player.position.y, -10f);

        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }
}
