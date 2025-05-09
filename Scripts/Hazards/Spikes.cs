using Godot;
using System;

public partial class Spikes : Area2D
{
	[Export] public float _activeTime = 2f;
	[Export] public float _inactiveTime = 1.5f;
	[Export] public float _warningTime = 3f;
	[Export] public string _playerGroup = "Player";

	private AnimatedSprite2D _sprite;
	private CollisionShape2D _col;
	private Timer _stateTimer;
	private PointLight2D _light;

	private enum SpikeState { Default, Warning, Attack }
	private SpikeState _currentState;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_col = GetNode<CollisionShape2D>("CollisionShape2D");
		_light = GetNode<PointLight2D>("PointLight2D");

		_stateTimer = new Timer();
		_stateTimer.OneShot = true;
		AddChild(_stateTimer);
		_stateTimer.Timeout += OnTimerTimeout;
		BodyEntered += OnBodyEntered;

		SetState(SpikeState.Default);
	}

	private void SetState(SpikeState newState)
	{
		_currentState = newState;

		switch (_currentState)
		{
			case SpikeState.Default:
				_sprite.Play("default");
				_col.Disabled = true;
				_light.Energy = 0f;
				_stateTimer.Start(_inactiveTime);
				break;

			case SpikeState.Warning:
				_sprite.Play("warning");
				_col.Disabled = true;
				_light.Energy = .7f;
				_stateTimer.Start(_warningTime);
				break;

			case SpikeState.Attack:
				_sprite.Play("attack");
				_col.Disabled = false;
				_light.Energy = 1.5f;
				_stateTimer.Start(_activeTime);
				break;
		}
	}

	private void OnTimerTimeout()
	{
		switch (_currentState)
		{
			case SpikeState.Default:
				SetState(SpikeState.Warning);
				break;

			case SpikeState.Warning:
				SetState(SpikeState.Attack);
				break;

			case SpikeState.Attack:
				SetState(SpikeState.Default);
				break;
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body.IsInGroup(_playerGroup))
		{
			GameManager.Instance.LoadCurrentLevel();
		}
	}
}
