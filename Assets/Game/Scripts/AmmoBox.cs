using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
	
	public int index = 0;
	
	void OnTriggerEnter2D(Collider2D obj) //«Наезд» на объект
	{
		if (obj.CompareTag("Player"))
		{
			Destroy(gameObject); //Удаление объекта с карты
		}
	}
}
