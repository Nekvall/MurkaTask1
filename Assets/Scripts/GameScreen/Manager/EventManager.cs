using UnityEngine;
using System.Collections;

/// <summary>
/// Обработчик событий взаимодействия игрока с игрой.
/// </summary>
public class EventManager : MonoBehaviour 
{
    public delegate void MethodContainer();

    private DrawManager _drawManager;

    // Подписываемся на события.
    void Awake()
    {
        _drawManager = GetComponent<DrawManager>();

        InputManager.mouseButtonDownEvent   += _drawManager.startDraw;
        InputManager.mouseButtonUpEvent     += _drawManager.stopDraw;
    }
}
