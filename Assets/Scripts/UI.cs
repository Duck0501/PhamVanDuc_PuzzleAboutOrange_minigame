using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button buttonPlay;
    public Button buttonToHome;
    public Button buttonReset;
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
            buttonReset.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
}
