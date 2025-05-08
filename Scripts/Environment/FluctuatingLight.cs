using Godot;
using System;

public partial class FluctuatingLight : Node2D
{
	[Export] public PointLight2D _light;
	[Export] public float _minSize = 0.5f;
	[Export] public float _maxSize = 1.3f;
	[Export] public float _minEnergy = 0.5f;
	[Export] public float _maxEnergy = 1.5f;
	[Export] public float _fluctuationSpeed = 1.5f;
	private Tween _fluctuationTween;

	public override void _Ready()
	{
		_light = GetNode<PointLight2D>("PointLight2D");

		StartFluctuation();
	}

	private void StartFluctuation()
	{
		_fluctuationTween?.Kill();

		_fluctuationTween = CreateTween()
			.SetLoops()
			.SetEase(Tween.EaseType.InOut)
			.SetTrans(Tween.TransitionType.Sine);

		_fluctuationTween.TweenProperty(_light, "texture_scale", _maxSize, _fluctuationSpeed)
			.From(_minSize);

		_fluctuationTween.Parallel()
			.TweenProperty(_light, "energy", _maxEnergy, _fluctuationSpeed)
			.From(_minEnergy);

		_fluctuationTween.Chain()
			.TweenProperty(_light, "texture_scale", _minSize, _fluctuationSpeed)
			.From(_maxSize);

		_fluctuationTween.Parallel()
			.TweenProperty(_light, "energy", _minEnergy, _fluctuationSpeed)
			.From(_maxEnergy);
	}

}
