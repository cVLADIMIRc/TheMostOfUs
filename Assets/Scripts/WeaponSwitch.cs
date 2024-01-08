using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] weapons;
    //public GameObject crowbar;
    //public GameObject pistol;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for(int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].activeInHierarchy == true)
                {
                    weapons[i].SetActive(false);

                    if(i != 0)
                    {
                        weapons[i - 1].SetActive(true);
                    }
                    else
                    {
                        weapons[weapons.Length - 1].SetActive(true);
                    }
                    break;
                }
            }
        }
    }
}
