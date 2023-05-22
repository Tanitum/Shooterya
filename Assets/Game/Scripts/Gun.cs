using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Collections.Generic;

public class Gun : NetworkBehaviour
{
    public float offset; // параметр для корректировки поворота пушки
    public GameObject projectile; // внешний вид оружия
    public Transform shotPoint; // место вылета снаряда
	[SerializeField] private AudioSource _shotSound;// звук выстрела

	private SpriteRenderer sprite;

    private NetworkVariable<float> timeBtwShots = new NetworkVariable<float>(0,  NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner); // оставшееся время между выстрелами
    public float startTimeBtwShots; // время между выстрелами
	
	public NetworkVariable<int> currentAmmo = new NetworkVariable<int>(15, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner); // патроны в обойме
	public int allAmmo = 30; // патроны в запасе
	public int fullAmmo = 15; // размер обоймы
	private float reloadTime;  // оставшееся время перезарядки
	public float startReloadTime; // время перезарядки
	[SerializeField] private AudioSource _reloadSound;// звук выстрела

    private List<GameObject> spawnedProjectiles = new List<GameObject>();

    private void Awake()
    {
		sprite = GetComponent<SpriteRenderer>();

		_shotSound = GetComponents<AudioSource>()[0];
		
		_reloadSound = GetComponents<AudioSource>()[1];
    }

    private void Update()
    {
        if (!IsOwner) return;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

        if (difference.x < 0)
            sprite.flipY = true;
        else
            sprite.flipY = false;

        if (timeBtwShots.Value <= 0 && reloadTime <= 0)
        {
            if (Input.GetMouseButton(0) && currentAmmo.Value > 0)
            {
                ShootServerRpc();
                timeBtwShots.Value = startTimeBtwShots;
                currentAmmo.Value -= 1;
            }
        }
        else
        {
            timeBtwShots.Value -= Time.deltaTime;
            reloadTime -= Time.deltaTime;
        }

        BulletsText.newText = currentAmmo.Value + " / " + allAmmo; // изменение данных о патронах
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
		int reason = fullAmmo - currentAmmo.Value; // недостающее количесвтво патронов в обойме
		if (allAmmo >= reason)
		{
			allAmmo = allAmmo - reason;
			currentAmmo.Value = fullAmmo;
		}
		else
		{
			currentAmmo.Value += allAmmo;
			allAmmo = 0;
		}
		_reloadSound.Play();
	}

	[ServerRpc]
	private void ShootServerRpc()
	{
        _shotSound.Play();

        GameObject spawnedObject = Instantiate(projectile, shotPoint.position, transform.rotation);

        spawnedProjectiles.Add(spawnedObject);
        spawnedObject.GetComponent<Projectile>().parent = this;

        spawnedObject.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void DestroyServerRpc()
    {
        GameObject toDestroy = spawnedProjectiles[0];
        toDestroy.GetComponent<NetworkObject>().Despawn();
        spawnedProjectiles.Remove(toDestroy);
        Destroy(toDestroy);
    }
}
