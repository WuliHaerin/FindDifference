using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public int Score;
	public int time;
	public Transform timeBar;

	public GameObject missClick;
	public SpriteRenderer numbers;
	public Sprite[] numbersSprite;

	private int _BaseTime;
	private RaycastHit2D hit;

	public SpriteRenderer title;
	public Sprite[] titleSprite;
	public Animator levelStar;
	public GameObject gameOverMenu;
	public GameObject buttonsMenu;
	public GameObject help;
	public AudioClip[] audioClip;

	private bool finish;
	private int _helpTime;
	public static bool isWin;

	void Start () 
	{
		SetButtonMenu(true);
		GameObject adPrefab = Resources.Load<GameObject>("Tishi");
		GameObject adObj=Instantiate(adPrefab,buttonsMenu.transform) as GameObject;
		adObj.transform.position = new Vector3(-0.23F, -3.55f, 0);
		Score = 11;
		SetScore();
		this._BaseTime = (int)Time.time + 480;
		this._helpTime = (int)Time.time + 10;
		SetTime();
		InvokeRepeating("SetTime", 1, 1);

		finish = false;
	}

	private IEnumerator Tishi()
	{
        if (!finish)
        {
            SetButtonMenu(false);
			yield return new WaitForSeconds(5);
			SetButtonMenu(true);
        }

	}
	public void StartTishi()
	{
		StartCoroutine("Tishi");
	}

	public void SetTime()
	{
		if (!finish)
		{
			this.time = this._BaseTime - (int)Time.time;

			if (this.time <= 0)
			{
				this.time = 0;
			}

			timeBar.localScale = new Vector3( (this.time * 57.3f) / 480f ,1.172606f ,1);

			if (this.time == 0)
			{
				finish = true;
				Invoke("GameOver",1);
			}

			//if (buttonsMenu.activeSelf == false && (int)Time.time >= this._helpTime)
			//{
			//	help.SetActive(false);
			//	buttonsMenu.SetActive(true);
			//}
		}
	}

	public void SetScore()
	{
		Score--;
		numbers.sprite = numbersSprite[Score];

		if (Score == 0)
		{
			finish = true;
			Invoke("GameOver",1);
		}
	}

	public void SetButtonMenu(bool a)
	{
		buttonsMenu.SetActive(a);
        help.SetActive(!a);
    }

	void GameOver()
	{
		buttonsMenu.SetActive(false);

		if (this.time > 0)
		{
			isWin = true;
			title.sprite = titleSprite[1];
			gameOverMenu.SetActive(true);
			gameOverMenu.transform.Find("Rate").GetComponent<SpriteRenderer>().sprite=Resources.Load<Sprite>("Owl.0");

			if (PlayerPrefs.GetInt("Level" + (int.Parse(Application.loadedLevelName) + 1), -1) < 0)
				PlayerPrefs.SetInt("Level" + (int.Parse(Application.loadedLevelName) + 1), 0);

			// win
			if (this.time >= 260)
			{
				levelStar.Play("3");
				GetComponent<AudioSource>().clip = audioClip[3];

				PlayerPrefs.SetInt("Level" + Application.loadedLevelName, 3);
			}
			else if (this.time >= 135)
			{
				levelStar.Play("2");
				GetComponent<AudioSource>().clip = audioClip[2];

				if (PlayerPrefs.GetInt("Level" + Application.loadedLevelName, -1) < 2)
					PlayerPrefs.SetInt("Level" + Application.loadedLevelName, 2);
			}
			else
			{
				levelStar.Play("1");
				GetComponent<AudioSource>().clip = audioClip[1];

				if (PlayerPrefs.GetInt("Level" + Application.loadedLevelName, -1) < 1)
					PlayerPrefs.SetInt("Level" + Application.loadedLevelName, 1);
			}

			GetComponent<AudioSource>().Play();
		}
		else
		{
			isWin = false;
			// lose
			title.sprite = titleSprite[0];
			gameOverMenu.SetActive(true);
            gameOverMenu.transform.Find("Rate").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Owl.0");
            GameObject.FindObjectOfType<NextLevel>().gameObject.SetActive(false);

            GetComponent<AudioSource>().clip = audioClip[0];
			GetComponent<AudioSource>().Play();
			if (PlayerPrefs.GetInt("Level" + Application.loadedLevelName, -1) < 0)
				PlayerPrefs.SetInt("Level" + Application.loadedLevelName, 0);
		}
        AdManager.ShowInterstitialAd("1lcaf5895d5l1293dc",
            () => {
                Debug.Log("--插屏广告完成--");

            },
            (it, str) => {
                Debug.LogError("Error->" + str);
            });
        PlayerPrefs.Save();
	}
	
	void Update()
	{
		if (Application.platform == RuntimePlatform.WindowsEditor)
		{
			if (Input.GetMouseButtonDown(0))
			{
				hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				Check(Input.mousePosition);
			}
		}
		else
		{
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint((Input.GetTouch (0).position)), Vector2.zero);
				Check(Input.GetTouch (0).position);
			}
		}
	}

	void Check(Vector3 pos)
	{
		if (!finish)
		{
			if(hit.collider == null)
			{
				pos = Camera.main.ScreenToWorldPoint(pos);
				pos.z = 0;
				if ((pos.x >= -2.3f && pos.x <= 2.3f) && (pos.y >= -3 && pos.y <= 3))
				{
					this._BaseTime -= 50;
					SetTime();
					Instantiate(missClick, pos, Quaternion.identity);
				}
			}
			else if (hit.collider.gameObject.name.Contains("Object"))
			{
				hit.collider.GetComponent<Click>().Check();
			}
		}
	}
}
