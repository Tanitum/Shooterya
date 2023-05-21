using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
	public int weaponSwitch = 0; // номер используемого оружия
    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
		int currentWeapon = weaponSwitch; // текущее оружие
		
        if(Input.GetAxis("Mouse ScrollWheel") > 0f) // крутим колесо мыши вверх
		{
			if (weaponSwitch >= transform.childCount - 1)
			{
				weaponSwitch = 0;
			}
			else
			{
				weaponSwitch++;	
			}
		}
		
		if(Input.GetAxis("Mouse ScrollWheel") < 0f) // крутим колесо мыши вниз
		{
			if (weaponSwitch <= 0)
			{
				weaponSwitch = transform.childCount - 1;
			}
			else
			{
				weaponSwitch--;	
			}
		}
		
		if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 1) 
			// цифра 1 на клавиатуре, если есть больше 1 оружия
        {
            weaponSwitch = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2) 
			// цифра 2 на клавиатуре, если есть больше 2 оружий
        {
            weaponSwitch = 1;
        }
		
		if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3) 
			// цифра 3 на клавиатуре, если есть больше 3 оружий
        {
            weaponSwitch = 2;
        }
		
		if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4) 
			// цифра 4 на клавиатуре, если есть больше 4 оружий
        {
            weaponSwitch = 3;
        }
		
		if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5) 
			// цифра 5 на клавиатуре, если есть больше 5 оружий
        {
            weaponSwitch = 4;
        }
		
		if (Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6) 
			// цифра 6 на клавиатуре, если есть больше 6 оружий
        {
            weaponSwitch = 5;
        }
		
		if (Input.GetKeyDown(KeyCode.Alpha7) && transform.childCount >= 7) 
			// цифра 7 на клавиатуре, если есть больше 7 оружий
        {
            weaponSwitch = 6;
        }
		
		if (Input.GetKeyDown(KeyCode.Alpha8) && transform.childCount >= 8) 
			// цифра 8 на клавиатуре, если есть больше 8 оружий
        {
            weaponSwitch = 7;
        }
		
		if (Input.GetKeyDown(KeyCode.Alpha9) && transform.childCount >= 9) 
			// цифра 9 на клавиатуре, если есть больше 9 оружий
        {
            weaponSwitch = 8;
        }
		
		if (currentWeapon != weaponSwitch)
		{
			SelectWeapon();
		}
    }
	
	void SelectWeapon()
    {
		int i = 0; // переменная для отсчета оружия
        foreach(Transform weapon in transform)
		{
			if (i == weaponSwitch)
				weapon.gameObject.SetActive(true);
			else
				weapon.gameObject.SetActive(false);
			i++;
		}
    }
	
}
