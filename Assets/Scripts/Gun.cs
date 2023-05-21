using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class Gun : NetworkBehaviour
{
    public float offset; // параметр для корректировки поворота пушки
    public GameObject projectile; // внешний вид оружия
    public Transform shotPoint; // место вылета снаряда
	[SerializeField] private AudioSource _shotSound;// звук выстрела

	private SpriteRenderer sprite;

    private float timeBtwShots; // оставшееся время между выстрелами
    public float startTimeBtwShots; // время между выстрелами
	
	public int currentAmmo = 15; // патроны в обойме
	public int allAmmo = 30; // патроны в запасе
	public int fullAmmo = 15; // размер обоймы
	private float reloadTime;  // оставшееся время перезарядки
	public float startReloadTime; // время перезарядки
	[SerializeField] private AudioSource _reloadSound;// звук выстрела

    private void Awake()
    {
		sprite = GetComponent<SpriteRenderer>();

		_shotSound = GetComponents<AudioSource>()[0];
		
		_reloadSound = GetComponents<AudioSource>()[1];
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

		if (timeBtwShots <= 0 && reloadTime <= 0)
		{
			if (Input.GetMouseButton(0) && currentAmmo > 0)
			{
				_shotSound.Play();

				GameObject spawnedObject = Instantiate(projectile, shotPoint.position, transform.rotation);
				spawnedObject.GetComponent<NetworkObject>().Spawn(true);

				timeBtwShots = startTimeBtwShots;
				currentAmmo -=1;
			}
		}
		else {
			timeBtwShots -= Time.deltaTime;
			reloadTime -= Time.deltaTime;
		}
		
		BulletsText.newText = currentAmmo + " / " + allAmmo; // изменение данных о патронах
		if (reloadTime <= 0)
		{
			if (Input.GetKeyDown(KeyCode.R) && allAmmo > 0)
			{
				Reload();
				reloadTime = startReloadTime;
			}
		}
    }
	
	private void OnTriggerEnter2D(Collider2D collision) // Подбор патронов в комплектах
	{
		if(collision.GetComponent<AmmoBox>())
		{
			allAmmo+=15;
			Destroy(collision.gameObject);
		}
	}
	
	public void Reload()
	{
		int reason = fullAmmo - currentAmmo; // недостающее количесвтво патронов в обойме
		if (allAmmo >= reason)
		{
			allAmmo = allAmmo - reason;
			currentAmmo = fullAmmo;
		}
		else
		{
			currentAmmo = currentAmmo + allAmmo;
			allAmmo = 0;
		}
		_reloadSound.Play();
	}
}
