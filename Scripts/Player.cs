using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	private float _Speed = 300.0f;
	
	[Export]
	private float _MinDistanceToMouse = 15.0f;

	public float PaddleLength => _rectShape.Size.X;

	public event Action OnShoot;

	private RectangleShape2D _rectShape = null;

	public override void _Ready()
	{
		_rectShape = ( RectangleShape2D ) GetNode<CollisionShape2D>( "CollisionShape2D" ).Shape;
	}

	public override void _UnhandledInput( InputEvent @event )
	{
		if( @event.IsActionPressed( "shoot", true, true ) ) OnShoot?.Invoke();
		
	}

	public override void _PhysicsProcess( double delta )
	{
		Vector2 velocity = Velocity;

		Vector2 mouse_pos = GetViewport().GetMousePosition();
		
		var diff = mouse_pos.X - GlobalPosition.X;
		
		float direction = Mathf.Sign( diff );
		
		if( Mathf.Abs( diff ) < _MinDistanceToMouse ) direction = 0;
		
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
