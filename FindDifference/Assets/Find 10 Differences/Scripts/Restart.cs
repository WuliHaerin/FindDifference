using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour 
{
    private void OnEnable()
    {
        if(!GameManager.isWin)
		{
			GameObject adRestart= Resources.Load<GameObject>("AdRestart");
			GameObject adRestartObj = Instantiate(adRestart,transform);
			adRestart.transform.position = new Vector3(0, -0.166f, 0);

		}
    }
    void OnMouseDown()
	{
		transform.localScale = new Vector3(0.9f,0.9f,1);
	}
	
	void OnMouseUp()
	{
        transform.localScale = new Vector3(1, 1, 1);
        if (GameManager.isWin)
		{
            Time.timeScale = 1;
            GameManager.isWin = false;
            Application.LoadLevel(Application.loadedLevel);

        }
		else
		{
            transform.localScale = new Vector3(1, 1, 1);
            AdManager.ShowVideoAd("192if3b93qo6991ed0",
                (bol) => {
                    if (bol)
                    {
                        Time.timeScale = 1;
                        GameManager.isWin = false;
                        Application.LoadLevel(Application.loadedLevel);

                        AdManager.clickid = "";
                        AdManager.getClickid();
                        AdManager.apiSend("game_addiction", AdManager.clickid);
                        AdManager.apiSend("lt_roi", AdManager.clickid);


                    }
                    else
                    {
                        StarkSDKSpace.AndroidUIManager.ShowToast("观看完整视频才能获取奖励哦！");
                    }
                },
                (it, str) => {
                    Debug.LogError("Error->" + str);
                    //AndroidUIManager.ShowToast("广告加载异常，请重新看广告！");
                });
        }

		
	}
}
