using UnityEngine;
using System.Collections;

public class LevelBase : MonoBehaviour 
{
	protected int _numLevel;     // номер уровня.
    protected int _numForm;      // номер формы.

	/// <summary>
	/// Инициализация класса.
	/// </summary>
	public virtual void initialize(){}

    public int numForm
    {
        get { return _numForm; }
    }
}
