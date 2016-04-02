using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimileManager : MonoBehaviour 
{
    private List<GameObject> checkPoint;
    private int checkPointComplete;
    private float distanceToPoint = 0.05f;                  // расстояние к чекпоинту, на котором линия должна пройти, что бы защитаться.
    private float offsetBetweenLengthFigure = 0.5f;         // допустимая разница длинны, между нарисованой и заданой формой.
    private List<GameObject> checkPointQueue;               // очередь пройденных чек-поинтов.

    void Awake()
    {
        checkPoint = new List<GameObject>();
        checkPointQueue = new List<GameObject>();
    }

    /// <summary>
    /// Сравнить фигуры.
    /// </summary>
    public void compare(GameObject taskFigure, List<Vector3> paintedFigure)
    {
        GameObject child;
        int i;
        int j;
        float distanceBetweenCheckPoint = 0;                     // растояние между чек-поинтами. 

        //--------------------------------------------------------------------
		// Вытаскиваем из заданой фигуры все чек-поинты.
        for (i = 0; i < taskFigure.transform.childCount; i++)
        {
            child = taskFigure.transform.GetChild(i).gameObject;
            
            if (child.tag == "checkPoint")
            {
                if (checkPoint.Count > 0)
                {
                    distanceBetweenCheckPoint += Vector2.Distance(checkPoint[checkPoint.Count-1].transform.position, child.transform.position);
                }

                checkPoint.Add(child);
            }

            // Соеденяем последний и первый чек-поинт.
            if (i == taskFigure.transform.childCount - 1)
            {
                distanceBetweenCheckPoint += Vector2.Distance(checkPoint[0].transform.position, checkPoint[checkPoint.Count-1].transform.position);
            }
        }
        //--------------------------------------------------------------------

        Vector3 linePointPos = new Vector3();       // позиция точки линии.
        Vector3 checkPointPos = new Vector3();      // позиция чек-поинта.
        float distanceLine = 0;                     // длинная линии нарисованой игроком.
        float distanceAB;

        //--------------------------------------------------------------------
        // Перебераем всю линию, нарисованою игроком, и пройденые чек-поинты, засовываем в массив.
        for (i = 0; i < paintedFigure.Count; i++)
        {
            linePointPos = paintedFigure[i];

            if (i > 0)
            {
                distanceLine += Vector3.Distance(paintedFigure[i - 1], paintedFigure[i]);
            }

            for (j = 0; j < checkPoint.Count; j++)
            {
                checkPointPos = checkPoint[j].transform.position;

                distanceAB = Vector3.Distance(linePointPos, checkPointPos);

                if (distanceAB < distanceToPoint)
                {
                    checkPointQueue.Add(checkPoint[j]);
                    checkPoint.RemoveAt(j);
                    j--;
                }
            }
        }
        //--------------------------------------------------------------------


        int currPoint = 0;
        int prewPoint = 0;
        bool checkPointQueueFalse = false;   // очередь чек-поинтов была неверной?

        //-------------------------------------------------------------------
        // Проверяем, пройденые чек-поинты имеют линейное прохождение, 
        // тоесть checkPoint_0, checkPoint_1, checkPoint_2, checkPoint_3.
        for (i = 0; i < checkPointQueue.Count; i++)
        {
            currPoint = checkPointQueue[i].GetComponent<CheckPoint>().numPoint;

            if (i == 0)
            {
                prewPoint = currPoint;
                continue;
            }

			if ((prewPoint + 1) == currPoint || (prewPoint - 1) == currPoint)
            {
                // Линейная проверка, как 0, 1, 2, 3...
                prewPoint = currPoint;
            }
            else
            {
                // Проверка чек-поинтов на начало очереди. 
                // Например, если чек-поинты были пройденые, как 3, 0, 1, 2.                
				if (currPoint == 0 || currPoint == checkPointQueue.Count-1)
                {
                    prewPoint = currPoint;
                }
                else
                {
                    checkPointQueueFalse = true;
                }
            }
        }
        //-------------------------------------------------------------------

        if (checkPointQueueFalse)
        {
            // Очередь не верная.
            Debug.Log("Очередь не верная. СТОП!!!");
            return;
        }

        //------------------------------------------------------------------
        // Проверяем длинну нарисованой игроком линии и длинну заданой фигуры.
        Debug.Log("L = " + distanceLine + "    " + distanceBetweenCheckPoint);
        distanceAB = Mathf.Abs(distanceBetweenCheckPoint - distanceLine);
        if (distanceAB > offsetBetweenLengthFigure)
        {
            Debug.Log("Длинна нарисованой линии слишком большая. СТОП!!!");
            return;
        }
        //------------------------------------------------------------------


        Debug.Log("Фигура совпала =)");

        dispose();
    }

    private void dispose()
    {
        checkPoint.Clear();
        checkPointQueue.Clear();
    }
}
