using Godot;
using System;

public partial class CheckMovement : ColorRect
{
	private bool mouseClicked = false;
	public bool squareOccupied = true;
	public string pieceOnSquare = "empty";
	public string squareCoord;

	public override void _Ready()
	{
	}

	public override void _Input(InputEvent @event)
	{

		if (@event is InputEventMouseButton mouseButton)
		{
			// check to see if player left clicked
			if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
			{
				// get mouse position of where the player clicked
				Vector2 mousePosition = GetGlobalMousePosition();

				// check to see if player clicked somewhere on the chessboard
				// REMEMBER THIS CODE!
				if (GetGlobalRect().HasPoint(mousePosition))
				{
					GD.Print(squareCoord);
					GD.Print(squareOccupied);
					GD.Print(pieceOnSquare);

					AnimatedSprite2D squareAnimation = GetChild(0).GetChild<AnimatedSprite2D>(1);

					if (squareAnimation.Animation == "w-P")
					{
						squareCoord = GetChild(0).GetChild<Label>(0).Text;
						int rankNum = Convert.ToInt32(squareCoord[1].ToString()) + 1;

						GD.Print($"{squareCoord[0]}{rankNum}");
						squareOccupied = false;
						squareAnimation.Visible = squareOccupied;

					}
				}
			}
		}
	}
}
