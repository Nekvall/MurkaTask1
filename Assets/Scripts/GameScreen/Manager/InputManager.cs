using UnityEngine;
using System.Collections;

/// <summary>
/// Менеджер создания событий взаимодействия игрока с игрой. 
/// Обрабатывает события EventManager.
/// </summary>
public class InputManager : MonoBehaviour 
{
    public static event EventManager.MethodContainer mouseButtonDownEvent;      // Игрок нажал левую кнопку мыши.
    public static event EventManager.MethodContainer mouseButtonUpEvent;        // Игрок отпустил левую кнопку мыши.

	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseButtonDownEvent();
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseButtonUpEvent();
        }
	}
}
