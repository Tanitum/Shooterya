
using UnityEngine;
using Unity.Netcode;
using System;

public class Projectile : NetworkBehaviour
{
    public Gun parent;

	public float speed; //скорость полета снаряда
    public float lifeTime; // время до самоуничтожения снаряда
    public float distance; // расстояние до объекта слоя Solid, на котором снаряд уничтожится
    public float damage; // урон от снаряда
    public LayerMask whatIsSolid; // с объектами какого слоя сталкиваемся
	private float flyTimer; // оставшееся время жизни снаряда

    private NetworkObject networkObject;

    private void Start()
    {
        networkObject = GetComponent<NetworkObject>();

        flyTimer = lifeTime;
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null) {
            Debug.Log(hitInfo.collider.tag);
            if(!IsOwner) return;

            if (hitInfo.collider.CompareTag("Enemy"))
			{
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }

            if (hitInfo.collider.CompareTag("Player"))
            {
                Debug.Log("HIT!!!!");
                hitInfo.collider.transform.GetComponentInParent<PlayerHealth>().TakeDamage(damage);
            }

            parent.DestroyServerRpc();
        }
		
		flyTimer -= Time.deltaTime;
		if (flyTimer <= 0)
		{
            if (!IsOwner) return;

            parent.DestroyServerRpc();
        }
		
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
