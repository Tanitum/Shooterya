using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Newtonsoft.Json.Linq;

public class WeaponSwitch : NetworkBehaviour
{
    private NetworkVariable<int> weaponSwitch = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner); // номер используемого оружия

    private void Awake()
    {
        weaponSwitch.OnValueChanged += (int prevValue, int newValue) =>
        {
            weaponSwitch.Value = newValue;
            int i = 0;

            foreach (Transform weapon in transform)
            {
                if (i == weaponSwitch.Value)
                    weapon.gameObject.SetActive(true);

                else
                    weapon.gameObject.SetActive(false);

                i++;
            }
        };
    }

    private void Update()
    {
        if (!IsOwner) return;

        int currentWeapon = weaponSwitch.Value; // текущее оружие

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // крутим колесо мыши вверх
        {
            if (weaponSwitch.Value >= transform.childCount - 1)
            {
                weaponSwitch.Value = 0;
            }
            else
            {
                weaponSwitch.Value++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // крутим колесо мыши вниз
        {
            if (weaponSwitch.Value <= 0)
            {
                weaponSwitch.Value = transform.childCount - 1;
            }
            else
            {
                weaponSwitch.Value--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 1)
        // цифра 1 на клавиатуре, если есть больше 1 оружия
        {
            weaponSwitch.Value = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        // цифра 2 на клавиатуре, если есть больше 2 оружий
        {
            weaponSwitch.Value = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        // цифра 3 на клавиатуре, если есть больше 3 оружий
        {
            weaponSwitch.Value = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        // цифра 4 на клавиатуре, если есть больше 4 оружий
        {
            weaponSwitch.Value = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
        // цифра 5 на клавиатуре, если есть больше 5 оружий
        {
            weaponSwitch.Value = 4;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) && transform.childCount >= 6)
        // цифра 6 на клавиатуре, если есть больше 6 оружий
        {
            weaponSwitch.Value = 5;
        }

        if (Input.GetKeyDown(KeyCode.Alpha7) && transform.childCount >= 7)
        // цифра 7 на клавиатуре, если есть больше 7 оружий
        {
            weaponSwitch.Value = 6;
        }

        if (Input.GetKeyDown(KeyCode.Alpha8) && transform.childCount >= 8)
        // цифра 8 на клавиатуре, если есть больше 8 оружий
        {
            weaponSwitch.Value = 7;
        }

        if (Input.GetKeyDown(KeyCode.Alpha9) && transform.childCount >= 9)
        // цифра 9 на клавиатуре, если есть больше 9 оружий
        {
            weaponSwitch.Value = 8;
        }
    }
}
