using UnityEngine;
using System.Collections;

/// <summary>
/// Менеджер уровней.
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Создаем уровень.
    /// </summary>
    public LevelBase createLevel(int numLevel)
    {
        LevelBase retLevel;

        switch (numLevel)
        {
            case 1: retLevel = new Level01(); break;
            case 2: retLevel = new Level02(); break;
            case 3: retLevel = new Level03(); break;
            case 4: retLevel = new Level04(); break;
            case 5: retLevel = new Level02(); break;
            case 6: retLevel = new Level03(); break;
            case 7: retLevel = new Level01(); break;
            case 8: retLevel = new Level04(); break;

            default: retLevel = new Level01(); break;
        }

        retLevel.initialize();
        
        return retLevel;
    }
}
