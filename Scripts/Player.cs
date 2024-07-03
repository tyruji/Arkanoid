using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float _Speed = 300.0f;

	public override void _PhysicsProcess( double delta )
	{
		Vector2 velocity = Velocity;

		float direction = Input.GetAxis( "left", "right" );
		if( direction != 0 )
		{
			velocity.X = direction * _Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward( Velocity.X, 0, _Speed );
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
