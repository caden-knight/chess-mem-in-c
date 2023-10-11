// using Godot;
// using System;

// public partial class CheckMovement : ColorRect
// {
// 	public bool squareOccupied = true;
// 	public string pieceOnSquare = "empty";
// 	public string squareCoord;

// 	private bool mouseClicked = false;
// 	private bool highlighted = false;
// 	private Color originalColor;

// 	public override void _Ready()
// 	{
// 		originalColor = Color;
// 	}

// 	public override void _Input(InputEvent @event)
// 	{

// 		if (@event is InputEventMouseButton mouseButton)
// 		{
// 			// check to see if player left clicked
// 			if (mouseButton.ButtonIndex == MouseButton.Left && mouseButton.Pressed)
// 			{
// 				// get mouse position of where the player clicked
// 				Vector2 mousePosition = GetGlobalMousePosition();

// 				// check to see if player clicked somewhere on the chessboard
// 				// REMEMBER THIS CODE!
// 				if (GetGlobalRect().HasPoint(mousePosition))
// 				{
// 					GD.Print(squareCoord);
// 					GD.Print(squareOccupied);
// 					GD.Print(pieceOnSquare);

// 					AnimatedSprite2D squareAnimation = GetChild(0).GetChild<AnimatedSprite2D>(1);

// 					if (squareAnimation.Animation == "w-P")
// 					{
// 						squareCoord = GetChild(0).GetChild<Label>(0).Text;

// 						// FIXME: potential problem if planning on using squareOccupied in if statement above
// 						squareOccupied = false;
// 						// squareAnimation.Visible = squareOccupied;

// 						Godot.Collections.Array<Node> squares = GetNode("/root/ChessMemory/ChessBoard").GetChildren();

// 						foreach (ColorRect square in squares)
// 						{
// 							//TODO: come up with better variable names. they're getting confusing.
// 							string squareLabel = square.GetChild(0).GetChild<Label>(0).Text;
// 							int rankNum = Convert.ToInt32(squareCoord[1].ToString()) + 1;
// 							int rankNumOption2 = Convert.ToInt32(squareCoord[1].ToString()) + 2;
// 							string destinationCoord1 = $"{squareCoord[0]}{rankNum}";
// 							string destinationCoord2 = $"{squareCoord[0]}{rankNumOption2}";

// 							if (squareLabel == destinationCoord1 || squareLabel == destinationCoord2)
// 							{
// 								square.Color = Colors.Blue;
// 							}

// 							// if (squareLabel == squareCoord)
// 							// {
// 							// 	GD.Print("destination");
// 							// 	ColorRect squareDest = (ColorRect)square;

// 							// 	AnimatedSprite2D squareDestAnimation = squareDest.GetChild(0).GetChild<AnimatedSprite2D>(1);

// 							// 	squareDestAnimation.Animation = "w-P";
// 							// }

// 						}
// 					}
// 				}
// 			}
// 		}
// 	}
// }
