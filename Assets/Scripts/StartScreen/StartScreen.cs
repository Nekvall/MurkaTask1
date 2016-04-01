using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour 
{	
	void Start () 
    {
        initialize();
	}

    private void initialize()
    {
		
    }
	
	void Update () 
    {
	
	}

	/// <summary>
	/// Игрок жмякнул по кнопке Play.
	/// </summary>
	public void clickButtonPlay()
	{
		// переключаемся на игровую сцену.
		Application.LoadLevel(1);
	}

}
