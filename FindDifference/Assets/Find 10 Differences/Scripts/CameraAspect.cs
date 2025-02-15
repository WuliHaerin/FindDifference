using UnityEngine;
using System.Collections;

public class CameraAspect : MonoBehaviour 
{
	void Start () 
	{
		Camera.main.aspect = 10/16f;
		Screen.sleepTimeout = -1;
	}
}
