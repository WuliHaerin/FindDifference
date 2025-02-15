using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour 
{
	void Awake()
	{
		if (PlayerPrefs.GetInt("Mute",0) == 1)
		{
			AudioListener.volume = 0;
		}
		else
		{
			AudioListener.volume = 1;
		}
	}

	void Start () 
	{
		Invoke ("MainMnu",2f);
	}
	
	void MainMnu ()
	{
		Application.LoadLevel("1. Main");
	}
}
