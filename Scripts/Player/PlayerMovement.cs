using Godot;
using System;

public partial class PlayerMovement : CharacterBody2D
{
	//Movement 
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	
	//Sprite
	private AnimatedSprite2D _animatedSprite2D;
	
	public override void _Ready()
	{
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
	
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			_animatedSprite2D.Play("jump");
			velocity.Y = JumpVelocity;
		}

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "", "");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}
	
		// Flip sprite
		if (velocity.X < -0.1f)
		{
			_animatedSprite2D.FlipH = true;
		}
		else if (velocity.X > 0.1f)
		{
			_animatedSprite2D.FlipH = false;
		}

		if (!IsOnFloor())
		{
			if (velocity.Y > 0.1f)
			{
				_animatedSprite2D.Play("fall");
			}
		}
		else if (Mathf.Abs(velocity.X) > 0.1f)
		{
			_animatedSprite2D.Play("running");
		}
		else
		{
			_animatedSprite2D.Play("default");
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
