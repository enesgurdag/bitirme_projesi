using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelText : MonoBehaviour
{
    // Start is called before the first frame update
    public Text m_MyText;
    private string levelName;
    void Start()
    {
        if (m_MyText.name == "PlayerName")
        {
            m_MyText.text = PlayerPrefs.GetString("PlayerName");
        }
        else
        {
            levelName = "ShootsNumber" + m_MyText.name.ToString();
            if (levelName == "ShootsNumber11")
            {
                int totalShoot = PlayerPrefs.GetInt("ShootsNumber1") + PlayerPrefs.GetInt("ShootsNumber2") + PlayerPrefs.GetInt("ShootsNumber3") + PlayerPrefs.GetInt("ShootsNumber4") + PlayerPrefs.GetInt("ShootsNumber5") + PlayerPrefs.GetInt("ShootsNumber6") + PlayerPrefs.GetInt("ShootsNumber7") + PlayerPrefs.GetInt("ShootsNumber8") + PlayerPrefs.GetInt("ShootsNumber9") + PlayerPrefs.GetInt("ShootsNumber10");
                PlayerPrefs.SetInt("ShootsNumber11", totalShoot);
            }
            m_MyText.text = PlayerPrefs.GetInt(levelName).ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (m_MyText.name == "PlayerName")
            {
                m_MyText.text = PlayerPrefs.GetString("PlayerName");
            }
            else
            {
                if (levelName == "ShootsNumber11")
                {
                    int totalShoot = PlayerPrefs.GetInt("ShootsNumber1") + PlayerPrefs.GetInt("ShootsNumber2") + PlayerPrefs.GetInt("ShootsNumber3") + PlayerPrefs.GetInt("ShootsNumber4") + PlayerPrefs.GetInt("ShootsNumber5") + PlayerPrefs.GetInt("ShootsNumber6") + PlayerPrefs.GetInt("ShootsNumber7") + PlayerPrefs.GetInt("ShootsNumber8") + PlayerPrefs.GetInt("ShootsNumber9") + PlayerPrefs.GetInt("ShootsNumber10");
                    PlayerPrefs.SetInt("ShootsNumber11", totalShoot);
                }
                m_MyText.text = PlayerPrefs.GetInt(levelName).ToString();
            }
        }
    }
}
