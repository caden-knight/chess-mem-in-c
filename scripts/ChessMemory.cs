using Godot;
using System;
using System.Collections.Generic;

public partial class ChessMemory : Node2D
{
	[Export]
	public Node2D chessBoard;
	public Singleton singleton;

	private int squareCount = 0;
	private bool isWhiteSquare = true;
	private Vector2 viewport;
	private string[] coordLetters = { "A", "B", "C", "D", "E", "F", "G", "H" };


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		singleton = GetNode<Singleton>("/root/Singleton");

		SetupBoard();
	}

	private void SetupBoard()
	{
		// preload the square scene
		PackedScene squareScene = ResourceLoader.Load<PackedScene>("res://scenes/chess_square.tscn");

		// TODO: invert board if player wants to play as black

		viewport = GetViewportRect().Size;

		// create the chessboard
		for (int rank = 1; rank <= 8; rank++)
		{
			for (int file = 1; file <= 8; file++)
			{

				Vector2 currentVectorCoord = new(file, 9 - rank);
				singleton.allCoords.Add(currentVectorCoord);

				Control squareControl = squareScene.Instantiate<Control>();
				ColorRect square = squareControl.GetChild<ColorRect>(0);

				// square styling
				string currentCoord = $"{coordLetters[file - 1]}{9 - rank}";
				Label coordLabel = squareControl.GetChild<Label>(1);
				AnimatedSprite2D squarePiece = squareControl.GetChild<AnimatedSprite2D>(2);
				square.Color = isWhiteSquare ? Colors.White : Colors.DarkGray;
				coordLabel.Text = currentCoord;


				// lays out the squares
				float squareLength = square.GetRect().Size.Y;
				float squareWidth = square.GetRect().Size.X;

				float fileOffset = (file - 1) * squareWidth;
				float rankOffset = (rank - 1) * squareLength;

				squareControl.Position = new Vector2(fileOffset, rankOffset);

				// enures beginning and ending rank squares are colored correctly
				isWhiteSquare = file != 8 ? !isWhiteSquare : isWhiteSquare;

				PlacePieces(currentVectorCoord, squarePiece);

				// determines whether or not a square is occupied or empty
				((CheckMovement)squareControl).squareOccupied = squarePiece.Animation == "empty" ? false : true;

				// determines what piece is occupying the square and where the piece is
				((CheckMovement)squareControl).pieceOnSquare = squarePiece.Animation;
				((CheckMovement)squareControl).vectorCoord = currentVectorCoord;

				chessBoard.AddChild(squareControl);
			}
		}

		// get a reference to one of the squares
		ColorRect squareRef = chessBoard.GetChild<Control>(0).GetChild<ColorRect>(0);

		// calculate the size of the squares. used for centering the board
		float xDifference = viewport.X - (squareRef.GetRect().Size.X * 8);
		float yDifference = viewport.Y - (squareRef.GetRect().Size.Y * 8);

		// centers the chessboard
		chessBoard.Position = new Vector2(xDifference / 2, yDifference / 2);
	}

	private void PlacePieces(Vector2 currentCoordinate, AnimatedSprite2D pieceOnSquare)
	{

		switch (currentCoordinate)
		{
			// Black Rook placement
			case Vector2(1, 8):
				pieceOnSquare.Animation = "b-R";
				break;
			case Vector2(8, 8):
				pieceOnSquare.Animation = "b-R";
				break;

			// Black Knight placement
			case Vector2(2, 8):
				pieceOnSquare.Animation = "b-N";

				break;
			case Vector2(7, 8):
				pieceOnSquare.Animation = "b-N";
				break;

			// Black Bishop placement
			case Vector2(3, 8):
				pieceOnSquare.Animation = "b-B";
				break;
			case Vector2(6, 8):
				pieceOnSquare.Animation = "b-B";
				break;

			// Black King & Queen placement
			case Vector2(4, 8):
				pieceOnSquare.Animation = "b-Q";
				break;
			case Vector2(5, 8):
				pieceOnSquare.Animation = "b-K";
				break;

			// White Rook placement
			case Vector2(1, 1):
				pieceOnSquare.Animation = "w-R";
				break;
			case Vector2(8, 1):
				pieceOnSquare.Animation = "w-R";
				break;

			// White Knight placement
			case Vector2(2, 1):
				pieceOnSquare.Animation = "w-N";
				break;
			case Vector2(7, 1):
				pieceOnSquare.Animation = "w-N";
				break;

			case Vector2(3, 1):
				pieceOnSquare.Animation = "w-B";
				break;
			case Vector2(6, 1):
				pieceOnSquare.Animation = "w-B";
				break;

			// White King and Queen placement
			case Vector2(4, 1):
				pieceOnSquare.Animation = "w-Q";
				break;
			case Vector2(5, 1):
				pieceOnSquare.Animation = "w-K";
				break;
			default:
				pieceOnSquare.Animation = "empty";
				break;
		}

		// placement of Pawns
		pieceOnSquare.Animation = currentCoordinate.Y == 2 ? "w-P" : pieceOnSquare.Animation;
		pieceOnSquare.Animation = currentCoordinate.Y == 7 ? "b-P" : pieceOnSquare.Animation;
	}
}