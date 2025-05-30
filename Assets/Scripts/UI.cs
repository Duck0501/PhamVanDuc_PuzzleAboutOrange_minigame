using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button buttonPlay;
    public Button buttonHowToPlay;
    public Button buttonToHome;
    public Button buttonReset;
    public Button buttonNext;
    public Button buttonLevel1;
    public Button buttonLevel2;
    public Button buttonLevel3;
    public Button buttonLevel4;
    public Button buttonLevel5;
    public Button buttonLevel6;

    void Start()
    {
        if (buttonPlay != null)
            buttonPlay.onClick.AddListener(() => SceneManager.LoadScene("Level"));

        if (buttonHowToPlay != null)
            buttonHowToPlay.onClick.AddListener(() => SceneManager.LoadScene("How to play"));

        if (buttonToHome != null)
            buttonToHome.onClick.AddListener(() => SceneManager.LoadScene("Home"));

        if (buttonLevel1 != null)
            buttonLevel1.onClick.AddListener(() => SceneManager.LoadScene("Level 1"));

        if(buttonLevel2 != null)
            buttonLevel2.onClick.AddListener(() => SceneManager.LoadScene("Level 2"));

        if (buttonLevel3 != null)
            buttonLevel3.onClick.AddListener(() => SceneManager.LoadScene("Level 3"));

        if (buttonLevel4 != null)
            buttonLevel4.onClick.AddListener(() => SceneManager.LoadScene("Level 4"));

        if (buttonLevel5 != null)
            buttonLevel5.onClick.AddListener(() => SceneManager.LoadScene("Level 5"));

        if (buttonLevel6 != null)
            buttonLevel6.onClick.AddListener(() => SceneManager.LoadScene("Level 6"));

        if (buttonReset != null)
            buttonReset.onClick.AddListener(() =>
            {
                if (SceneManager.GetActiveScene().name == "Win" || SceneManager.GetActiveScene().name == "Lose")
                {
                    string lastLevel = PlayerPrefs.GetString("LastLevel", "Level 1");
                    SceneManager.LoadScene(lastLevel);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            });

        if (buttonNext != null)
            buttonNext.onClick.AddListener(() =>
            {
                if (SceneManager.GetActiveScene().name == "Win")
                {
                    string lastLevel = PlayerPrefs.GetString("LastLevel", "Level 1");
                    string nextLevel;

                    switch (lastLevel)
                    {
                        case "Level 1":
                            nextLevel = "Level 2";
                            break;
                        case "Level 2":
                            nextLevel = "Level 3";
                            break;
                        case "Level 3":
                            nextLevel = "Level 4";
                            break;
                        case "Level 4":
                            nextLevel = "Level 5";
                            break;
                        case "Level 5":
                            nextLevel = "Level 6";
                            break;
                        case "Level 6":
                            nextLevel = "Home";
                            break;
                        default:
                            nextLevel = "Level 1";
                            break;
                    }

                    SceneManager.LoadScene(nextLevel);
                }
            });
    }
}
