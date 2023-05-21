using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletsText : MonoBehaviour
{
	Text text;
	public static string newText;
    void Start()
    {
		text = GetComponent<Text>();
		newText = "0 / 0";
    }

    void Update()
    {
	   text.text = newText;
    }
}
