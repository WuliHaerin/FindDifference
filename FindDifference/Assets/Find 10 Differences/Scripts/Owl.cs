using UnityEngine;
using System.Collections;

public class Owl : MonoBehaviour 
{
	void SoundPlay()
	{
		if (Random.Range(0,2) == 0)
		{
			GetComponent<AudioSource>().Play();
		}
	}
}
