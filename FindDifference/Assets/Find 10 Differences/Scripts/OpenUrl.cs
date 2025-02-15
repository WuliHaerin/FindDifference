using UnityEngine;
using System.Collections;

public class OpenUrl : MonoBehaviour 
{
	public string Url;

	void OnMouseDown()
	{
		transform.localScale = new Vector3(0.9f,0.9f,1);
	}
	
	void OnMouseUp()
	{
		transform.localScale = new Vector3(1,1,1);
		Application.OpenURL(Url);
	}
}
