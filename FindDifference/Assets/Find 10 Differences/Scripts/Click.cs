using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour
{
	public GameObject other;
	public Sprite sprite;

	private SpriteRenderer spriteRenderer;
	private GameManager gameM;

	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		gameM = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void Check()
	{
		GetComponent<AudioSource>().Play();
		GetComponent<Collider2D>().enabled = false; 
		spriteRenderer.sprite = sprite;
		gameM.SetScore();

		other.GetComponent<Collider2D>().enabled = false;
		other.GetComponent<SpriteRenderer>().sprite = sprite;
	}
}