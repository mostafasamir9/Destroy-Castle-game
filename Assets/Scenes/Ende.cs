using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ende : MonoBehaviour
{
    public Text scoree;
    public Text Hscoree;

    // Start is called before the first frame update
    void Awake()
    {
        scoree.text = PlayerPrefs.GetInt("MDScore").ToString();
        Hscoree.text = PlayerPrefs.GetInt("MDHighScore").ToString();

    }

    // Update is called once per frame
    public void OnClickTryAgain()
    {
        Application.LoadLevel("SampleScene");

    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
