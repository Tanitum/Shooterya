using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float offset; // параметр для корректировки поворота пушки
    public GameObject projectile; // внешний вид оружия
    public Transform shotPoint; // место вылета снаряда
	[SerializeField] private AudioSource shotSound;// звук выстрела

    private float timeBtwShots; // время между выстрелами
    public float startTimeBtwShots;

    void Update()
    {
		Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

		if (timeBtwShots <= 0)
		{
			if (Input.GetMouseButton(0))
			{
				shotSound.Play();
				Instantiate(projectile, shotPoint.position, transform.rotation);
				timeBtwShots = startTimeBtwShots;
			}
		}
		else {
			timeBtwShots -= Time.deltaTime;
		}
    }
}
