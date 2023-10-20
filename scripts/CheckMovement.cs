using Godot;
using System;
using System.Collections.Generic;


public partial class CheckMovement : Control
{
	public Node2D chessBoard;

	public bool squareOccupied;
	public string pieceOnSquare;
	public string squareCoord;
	public Vector2 clickedSquareCoord;

	private bool mouseClicked = false;
	private bool highlighted = false;
	private Vector2 highlightScale = new Vector2(0.03f, 0.03f);
	private Vector2 normalAnimScale = new Vector2(0.412f, 0.411f);
	private List<string> coordLetters;
	Singleton singleton;


	public override void _Ready()
	{
		chessBoard = GetNode<Node2D>("/root/ChessMemory/ChessBoard");
		singleton = GetNode<Singleton>("/root/Singleton");
		coordLetters = singleton.coordLetters;
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
					GD.Print(clickedSquareCoord);
					ClearHighlightedSquares(chessBoard);

					AnimatedSprite2D squareAnimation = GetChild<AnimatedSprite2D>(2);

					HighlightPossibleMoves(squareAnimation.Animation);
				}
			}
		}
	}
	private void ClearHighlightedSquares(Node2D chessBoard)
	{
		Godot.Collections.Array<Node> squares = chessBoard.GetChildren();

		foreach (Control square in squares)
		{
			AnimatedSprite2D squareAnim = square.GetChild<AnimatedSprite2D>(2);

			if (squareAnim.Animation == "highlight")
			{
				squareAnim.Visible = false;
			}
		}
	}

	private void HighlightPossibleMoves(string piece)
	{
		Godot.Collections.Array<Node> squares = GetNode("/root/ChessMemory/ChessBoard").GetChildren();

		switch (piece)
		{
			case "w-P":
				squareCoord = GetChild<Label>(1).Text;

				// Keep track of the current piece selected to know which code to run
				singleton.pieceSelected = Singleton.Piece.Pawn;

				// FIXME: potential problem if planning on using squareOccupied in if statement above
				// squareOccupied = false;
				// squareAnimation.Visible = squareOccupied;

				foreach (Control square in squares)
				{
					//TODO: come up with better variable names. they're getting confusing.
					string squareLabel = square.GetChild<Label>(1).Text;
					int rankNum = Convert.ToInt32(squareCoord[1].ToString()) + 1;
					int rankNumOption2 = Convert.ToInt32(squareCoord[1].ToString()) + 2;
					string destinationCoord1 = $"{squareCoord[0]}{rankNum}";
					string destinationCoord2 = $"{squareCoord[0]}{rankNumOption2}";

					if (squareLabel == destinationCoord1 || squareLabel == destinationCoord2)
					{
						GD.Print($"CM: {((CheckMovement)square).squareOccupied}");
						if (((CheckMovement)square).squareOccupied == false)
						{
							// Highlight move squares and adjust the highlight's sprite size
							AnimatedSprite2D squareSprite = square.GetChild<AnimatedSprite2D>(2);
							squareSprite.Animation = "highlight";
							squareSprite.Visible = true; // ensure it's visible
							squareSprite.Scale = highlightScale;
						}
					}
				}
				break;

			case "w-N":
				squareCoord = GetChild<Label>(1).Text;
				// FIXME: potential problem if planning on using squareOccupied in if statement above
				// squareOccupied = false;
				// squareAnimation.Visible = squareOccupied;

				foreach (Control square in squares)
				{
					DetermineKnightMoves(square);
				}
				break;

			case "w-R":
				foreach (Control square in squares)
				{
					DetermineRookMoves(square);
				}

				break;
		}
	}

	//  function correctly moves knight in the up 2 over 1 pattern
	// TODO: still needs to calculate up 1 over 2 pattern

	private void DetermineKnightMoves(Control square)
	{
		// FIXME: may be unnecessary ↓
		singleton.pieceSelected = Singleton.Piece.Knight;

		Vector2 passedInSquareCoord = ((CheckMovement)square).clickedSquareCoord;
		List<Godot.Vector2> availableMoves = new();

		Vector2 destCoord,
		destCoord2,
		destCoord3,
		destCoord4,
		destCoord5,
		destCoord6,
		destCoord7,
		destCoord8;

		// FIXME: refactor; make more efficient. lines 155-185
		destCoord = clickedSquareCoord.X + 1 <= 8 && clickedSquareCoord.Y + 2 <= 8
		? new Vector2(clickedSquareCoord.X + 1, clickedSquareCoord.Y + 2)
		: Vector2.Zero;

		destCoord2 = clickedSquareCoord.X - 1 > 0 && clickedSquareCoord.Y + 2 <= 8
		? new Vector2(clickedSquareCoord.X - 1, clickedSquareCoord.Y + 2)
		: Vector2.Zero;

		destCoord3 = clickedSquareCoord.X + 2 <= 8 && clickedSquareCoord.Y + 1 <= 8
		? new Vector2(clickedSquareCoord.X + 2, clickedSquareCoord.Y + 1)
		: Vector2.Zero;

		destCoord4 = clickedSquareCoord.X - 2 > 0 && clickedSquareCoord.Y + 1 <= 8
		? new Vector2(clickedSquareCoord.X - 2, clickedSquareCoord.Y + 1)
		: Vector2.Zero;

		destCoord5 = clickedSquareCoord.X - 1 > 0 && clickedSquareCoord.Y - 2 > 0
		? new Vector2(clickedSquareCoord.X - 1, clickedSquareCoord.Y - 2)
		: Vector2.Zero;

		destCoord6 = clickedSquareCoord.X - 2 > 0 && clickedSquareCoord.Y - 1 > 0
		? new Vector2(clickedSquareCoord.X - 2, clickedSquareCoord.Y - 1)
		: Vector2.Zero;

		destCoord7 = clickedSquareCoord.X + 2 < 9 && clickedSquareCoord.Y - 1 > 0
		? new Vector2(clickedSquareCoord.X + 2, clickedSquareCoord.Y - 1)
		: Vector2.Zero;

		destCoord8 = clickedSquareCoord.X + 1 < 9 && clickedSquareCoord.Y - 2 > 0
		? new Vector2(clickedSquareCoord.X + 1, clickedSquareCoord.Y - 2)
		: Vector2.Zero;

		GD.Print(destCoord7);
		availableMoves.Add(destCoord);
		availableMoves.Add(destCoord2);
		availableMoves.Add(destCoord3);
		availableMoves.Add(destCoord4);
		availableMoves.Add(destCoord5);
		availableMoves.Add(destCoord6);
		availableMoves.Add(destCoord7);
		availableMoves.Add(destCoord8);



		if (availableMoves.Count <= 0) return;

		else if (!((CheckMovement)square).squareOccupied)
		{
			for (int i = 0; i < availableMoves.Count; i++)
			{
				if (availableMoves[i] == passedInSquareCoord)
				{
					// Highlight move squares and adjust the highlight's sprite size
					AnimatedSprite2D squareSprite = square.GetChild<AnimatedSprite2D>(2);
					squareSprite.Animation = "highlight";
					squareSprite.Visible = true; // ensure it's visible
					squareSprite.Scale = highlightScale;
				}
			}
		}
	}

	private void DetermineRookMoves(Control square)
	{
		singleton.pieceSelected = Singleton.Piece.Knight;

		List<Vector2> availableMoves = new();
		float originalY = clickedSquareCoord.Y;

		float yDifference = 8 - originalY;

		for (int i = 0; i < yDifference; i++)
		{

		}
	}

	private Control findSquareByCoord(Vector2 targetCoord, squares)
	{
		Godot.Collections.Array<Node> squares = chessBoard.GetChildren();
		Control targetSquare = chessBoard.GetChild<Control>(0);

		foreach (Control square in squares)
		{

		}

		return targetSquare;
	}
}
