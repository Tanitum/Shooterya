using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
	
	public float speed; //скорость полета снаряда
    public float lifeTime; // время до самоуничтожения снаряда
    public float distance; // расстояние до объекта слоя Solid, на котором снаряд уничтожится
    public int damage; // урон от снаряда
    public LayerMask whatIsSolid; // с объектами какого слоя сталкиваемся
	private float flyTimer; // оставшееся время жизни снаряда

    private void Start()
    {
        flyTimer = lifeTime;
    }

    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null) {
            if (hitInfo.collider.CompareTag("Enemy"))
			{
                hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
		
		flyTimer -= Time.deltaTime;
		if (flyTimer <= 0)
		{
			Destroy(gameObject);
		}
		
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
	
}
