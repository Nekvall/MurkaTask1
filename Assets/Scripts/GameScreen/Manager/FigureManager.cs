﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Менеджер фигур. 
/// </summary>
public class FigureManager : MonoBehaviour 
{
    public GameObject figure1;
    public GameObject figure2;
    public GameObject figure3;

    private GameObject _currFigure;

    /// <summary>
    /// Отрисовать фигуру на экране.
    /// </summary>
    public void show(int numFigure)
    {
        GameObject createFigure;

        switch (numFigure)
        {
            case 1: createFigure = figure1; break;
            case 2: createFigure = figure2; break;
            case 3: createFigure = figure3; break;

            default: createFigure = figure1; break;
        }

        _currFigure = Instantiate(createFigure);
    }

    public GameObject currFigure
    {
        get { return _currFigure; }
    }
}