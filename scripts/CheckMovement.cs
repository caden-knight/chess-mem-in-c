using Godot;
using System;
using System.Collections.Generic;

public partial class CheckMovement : Control
{
	public Node2D chessBoard;

	public bool squareOccupied;
	public string pieceOnSquare;
	public string squareCoord;

	private bool mouseClicked = false;
	private bool highlighted = false;
	private Vector2 highlightScale = new Vector2(0.03f, 0.03f);
	private Vector2 normalAnimScale = new Vector2(0.412f, 0.411f);
	private string[] coordLetters = { "A", "B", "C", "D", "E", "F", "G", "H" };


	public override void _Ready()
	{
		chessBoard = GetNode<Node2D>("/root/ChessMemory/ChessBoard");
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
					GD.Print(squareOccupied);
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

				GD.Print(squareOccupied);
				GD.Print(pieceOnSquare);

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

				GD.Print(squareOccupied);
				GD.Print(pieceOnSquare);

				// FIXME: potential problem if planning on using squareOccupied in if statement above
				squareOccupied = false;
				// squareAnimation.Visible = squareOccupied;


				foreach (Control square in squares)
				{
					DetermineKnightMoves(square);
				}
				break;

		}
	}

	// TODO: function correctly moves knight in the up 2 over 1 pattern
	// still needs to calculate up 1 over 2 pattern
	private void DetermineKnightMoves(Control square)
	{
		// variables for calculating and identifying the coordinate
		string squareLabel = square.GetChild<Label>(1).Text;
		int rankNum;
		int rankNumOption2;
		string destinationCoord1 = "";
		string destinationCoord2 = "";
		List<string> availableMoves = new List<string>();

		// ensures the square isn't occupied
		// so the game won't highlight a spot that is occupied by another piece
		if (!((CheckMovement)square).squareOccupied)
		{
			for (int i = 0; i < coordLetters.Length; i++)
			{
				if (coordLetters[i] == squareCoord[0].ToString())
				{
					string letterCoord1 = i <= 6 ? coordLetters[i + 1] : null;
					string letterCoord2 = i >= 1 ? coordLetters[i - 1] : null;

					GD.Print(letterCoord1);
					GD.Print(letterCoord2);

					//FIXME: disgusting code, refactor this later
					if (letterCoord1 != null)
					{
						rankNum = Convert.ToInt32(squareCoord[1].ToString()) + 2;
						rankNumOption2 = Convert.ToInt32(squareCoord[1].ToString()) - 2;

						destinationCoord1 = rankNum <= 8 ? $"{letterCoord1}{rankNum}" : null;

						if (destinationCoord1 != null)
						{
							availableMoves.Add(destinationCoord1);
						}
						if (letterCoord2 != null)
						{
							destinationCoord2 = rankNum <= 8 ? $"{letterCoord2}{rankNum}" : null;

							if (destinationCoord2 != null)
							{
								availableMoves.Add(destinationCoord2);
							}
						}



					}
				}
			}

			GD.Print(availableMoves.Count);

			for (int i = 0; i < availableMoves.Count; i++)
			{
				if (squareLabel == availableMoves[i])
				{
					// Highlight move squares and adjust the highlight's sprite size
					AnimatedSprite2D squareSprite = square.GetChild<AnimatedSprite2D>(2);
					squareSprite.Animation = "highlight";
					squareSprite.Visible = true; // make it visible since it may have been invisible
					squareSprite.Scale = highlightScale;
				}
			}


		}
	}
}
