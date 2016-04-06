using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour 
{
    private UIEnd _uiEnd;

    void Start()
    {
        initialize();

        _uiEnd.setLevelTxt(PlayerPrefs.GetInt("Player Level"), 15);
    }

    private void initialize()
    {
        _uiEnd = GetComponent<UIEnd>();
    }

    public void restartGame()
    {
        Application.LoadLevel(1);
    }
}
