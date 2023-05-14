using UnityEngine;
using Unity.Netcode;

public class Gun : NetworkBehaviour
{
    public float offset; // параметр для корректировки поворота пушки
    public GameObject projectile; // внешний вид оружия
    public Transform shotPoint; // место вылета снаряда
	[SerializeField] private AudioSource _shotSound;// звук выстрела

	private SpriteRenderer sprite;

    private float timeBtwShots; // время между выстрелами
    public float startTimeBtwShots;

    private void Awake()
    {
		sprite = GetComponent<SpriteRenderer>();

		_shotSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

		if (difference.x < 0)
			sprite.flipY = true;
		else
			sprite.flipY = false;

		if (timeBtwShots <= 0)
		{
			if (Input.GetMouseButton(0))
			{
				_shotSound.Play();

				GameObject spawnedObject = Instantiate(projectile, shotPoint.position, transform.rotation);
				spawnedObject.GetComponent<NetworkObject>().Spawn(true);

				timeBtwShots = startTimeBtwShots;
			}
		}
		else {
			timeBtwShots -= Time.deltaTime;
		}
    }
}
