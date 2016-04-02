using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimileManager : MonoBehaviour 
{
    private List<GameObject> checkPoint;
    private int checkPointComplete;
    private float distanceToPoint = 0.1f;           // растояние к чек-поинту.
    private List<GameObject> checkPointQueue;       // очередь пройденных чек-поинтов.

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

        //--------------------------------------------------------------------
		// Вытаскиваем из заданой фигуры все чек-поинты.
        for (i = 0; i < taskFigure.transform.childCount; i++)
        {
            child = taskFigure.transform.GetChild(i).gameObject;
            
            if (child.tag == "checkPoint")
            {
                checkPoint.Add(child);
            }
        }
        //--------------------------------------------------------------------

        Vector3 linePointPos = new Vector3();   // позиция точки линии.
        Vector3 checkPointPos = new Vector3();  // позиция чек-поинта.
        float distanceAB;

        //--------------------------------------------------------------------
        // Перебераем всю линию, нарисованою игроком, и пройденые чек-поинты, засовываем в массив.
        for (i = 0; i < paintedFigure.Count; i++)
        {
            linePointPos = paintedFigure[i];

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

        Debug.Log("Очередь не верная ? = " + checkPointQueueFalse);

        //-------------------------------------------------------------------

            
    }


}
