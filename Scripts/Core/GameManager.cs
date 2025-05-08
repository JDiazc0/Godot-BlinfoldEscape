using Godot;
using System;

public partial class GameManager : Node
{
	public static GameManager Instance { get; private set; }

	[Export]
	public string[] _levels = {
		"res://Scenes/Level1.tscn"
	};

	public int _currentLevelIndex { get; private set; } = 0;

	public override void _Ready()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			QueueFree();
		}
	}

	public void StartGame()
	{
		_currentLevelIndex = 0;
		LoadCurrentLevel();
	}

	public void LoadCurrentLevel()
	{
		GetTree().ChangeSceneToFile(_levels[_currentLevelIndex]);
	}

	public void GoToNextLevel()
	{
		_currentLevelIndex++;
		if (_currentLevelIndex < _levels.Length)
		{
			LoadCurrentLevel();
		}
		else
		{
			GetTree().ChangeSceneToFile("res://Scenes/MainMenu.tscn");
		}
	}
}
