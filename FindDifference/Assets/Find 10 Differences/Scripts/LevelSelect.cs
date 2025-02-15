using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour 
{
	public SpriteRenderer stars;
	public Sprite[] starsSprite;

	void Start ()
	{
		int temp = PlayerPrefs.GetInt("Level" + gameObject.name, -1);

		if (temp != -1 || gameObject.name == "1")
		{
			if (temp == -1)
			{
				temp = 0;
			}
			
			GetComponent<Collider2D>().enabled = true;
			GetComponent<SpriteRenderer>().enabled = false;
			stars.sprite = starsSprite[temp];
		}
		else if (temp == -1)
		{
			GetComponent<Collider2D>().enabled = false;
			stars.sprite = starsSprite[0];
		}
	}

	void OnMouseDown()
	{
		Application.LoadLevel(gameObject.name);
	}
}
