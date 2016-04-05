using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour 
{
    public GameObject timeLineObject;

    public static event EventManager.MethodContainer mouseButtonDownEvent;      // Игрок нажал левую кнопку мыши.

	private LevelBase _level;	//текущий левел
    private int numCurrLevel;

    private LevelManager        _levelManager;
    private DrawManager         _drawManager;       // менеджер отрисовки.
    private FigureManager       _figureManager;     
    private SimileManager       _simileManager;     // менеджер сравнения фигур.
    private TimeLevelManager    _timeLevelManager; // менеджер времени уровня.

    private const int _startHard = 0;           // стартовая сложность уровня.
    private const int _hardOffset = 50;         // увелечение сложности для следующего уровня. 
    private int _currLevelTime;

	void Start () 
	{
        initialize();

		numCurrLevel = 0;

        _currLevelTime = _startHard;
        createNextLevel();
	}

    /// <summary>
    /// Инициализация классов.
    /// </summary>
	private void initialize()
	{
        _levelManager       = GetComponent<LevelManager>();
        _drawManager        = GetComponent<DrawManager>();
        _figureManager      = GetComponent<FigureManager>();
        _simileManager      = GetComponent<SimileManager>();
        _timeLevelManager   = timeLineObject.GetComponent<TimeLevelManager>();
	}

    /// <summary>
    /// Создать следующий уровень.
    /// </summary>
    private void createNextLevel()
    {
        numCurrLevel++;

        // создаем новый уровень.
        _level = _levelManager.createLevel(numCurrLevel);    

        // менеджер отрисует фигуру на сцене.
        _figureManager.show(_level.numForm);

        // усложняем уровень.
        _currLevelTime += _hardOffset;
        if (_currLevelTime < 0)
        {
            _currLevelTime = 0;
        }

        _timeLevelManager.setTime(_currLevelTime);
    }

    /// <summary>
    /// Сравнить нарисованую игроком фигуру, с заданой фигурой. 
    /// </summary>
    public void simileFigure()
    {
        bool simile = _simileManager.compare(_figureManager.currFigure, _drawManager.pointsList);

        if (simile)
        {
            _figureManager.destroyCurrFigure();
            createNextLevel();
        }

        _drawManager.clean();
        _simileManager.clean();
    }

    /// <summary>
    /// Время уровня закончилось.
    /// </summary>
    public void timeLevelEnded()
    {
        _drawManager.isCanDraw = false;
        _drawManager.clean();
    }
}
