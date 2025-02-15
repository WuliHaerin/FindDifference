using UnityEngine;
using System.Collections;

public class MissClick : MonoBehaviour 
{
	public GameObject parent;

	void Start () 
	{
		GetComponent<Animator>().Play("MissClick");
	}
	
	void DestroyObj ()
	{
		Destroy(parent);	
	}
}
