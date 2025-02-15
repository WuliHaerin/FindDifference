using UnityEngine;
using System.Collections;

public class NextLevel : MonoBehaviour 
{
	void OnMouseDown()
	{
		transform.localScale = new Vector3(0.9f,0.9f,1);
	}
	
	void OnMouseUp()
	{
		transform.localScale = new Vector3(1,1,1);
		
		Time.timeScale = 1;
		Application.LoadLevel((int.Parse(Application.loadedLevelName) + 1).ToString());
	}
}
