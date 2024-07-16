using Godot;
using System;

public partial class Ball : CharacterBody2D
{
	[Export]
	private float _Speed = 600;
	
	private bool _flying = false;
	
	private Player _player = null;
	
	private Vector2 _velocity = Vector2.Zero;
	
	private Vector2 _dirFromPlayer = Vector2.Zero;
	
	private Godot.Collections.Array<Rid> _rayExceptionArray = null;
	
	public override void _Ready()
	{
		 _rayExceptionArray = new Godot.Collections.Array<Rid> { GetRid() };
		_player = ( Player ) GetTree().GetFirstNodeInGroup( "player" );
		_dirFromPlayer = GlobalPosition - _player.GlobalPosition;
		_player.OnShoot += Shoot;
	}
	
	public override void _PhysicsProcess( double delta )
	{
		if( !_flying )
		{
			GlobalPosition = _player.GlobalPosition + _dirFromPlayer;
			return;
		}
		
		var collision = MoveAndCollide( ( float ) delta * _velocity );
		
		if( collision == null ) return;
		
		var normal = collision.GetNormal();
		var collider = collision.GetCollider();
		
		if( collider is Player )
		{
			_velocity = BounceFromPlayerVelocity();
			return;
		}
		
		_velocity = _velocity.Bounce( normal );
	}
	
	private void OnBodyEntered( Node2D body )
	{
		GD.Print( "collided with: ", body );
		
		if( body is Player )
		{
			_velocity = BounceFromPlayerVelocity();
			return;
		}
		
		var spaceState = GetWorld2D().DirectSpaceState;
		var query = PhysicsRayQueryParameters2D.Create( GlobalPosition, body.GlobalPosition );
		query.Exclude = _rayExceptionArray;
		var result = spaceState.IntersectRay( query );
		
		if( result.Count <= 0 ) return;
		
			// Get the collision normal via raycast
		Vector2 normal = ( Vector2 ) result[ "normal" ];
		GD.Print( "normal: ", normal );
		_velocity = _velocity.Bounce( normal );
	}
	
	private void Shoot()
	{
		if( _flying ) return;
		
		_flying = true;
		
		_velocity = BounceFromPlayerVelocity();
	}
	
	private Vector2 BounceFromPlayerVelocity()
	{
		var length = _player.PaddleLength * .5f;
		float diff = GlobalPosition.X - _player.GlobalPosition.X;
		var angle = Mathf.Pi * .5f * ( 1.0f - diff / length );
		Vector2 dir = new Vector2( Mathf.Cos( angle ), -Mathf.Sin( angle ) );
		
		return dir * _Speed;
	}
}
