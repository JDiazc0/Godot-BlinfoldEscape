using Godot;
using System;

public partial class MainMenu : Control
{
	private Button _startGame;

	public override void _Ready()
	{
		_startGame = GetNode<Button>("StartGame");
		_startGame.Pressed += OnStartGamePressed;
	}

	public void OnStartGamePressed()
	{

		GameManager.Instance.StartGame();
	}
}
