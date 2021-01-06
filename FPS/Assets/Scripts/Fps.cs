﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fps : MonoBehaviour
{
	string label = "";
	float count;

	IEnumerator Start()
	{
		GUI.depth = 2;
		while (true)
		{
			if (Time.timeScale == 1)
			{
				yield return new WaitForSeconds(0.1f);
				count = (1 / Time.deltaTime);
				label = "FPS :" + (Mathf.Round(count));
			}
			else
			{
				label = "Pause";
			}
			yield return new WaitForSeconds(0.5f);
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect(5*3, 40*3, 100*3, 25*3), label);
	}
}
