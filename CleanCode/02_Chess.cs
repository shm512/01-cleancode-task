using System.Collections.Generic;
using System.Linq;

namespace CleanCode
{
	public class Chess
	{
		private readonly Board board;

		public Chess(Board board)
		{
		    this.board = board;
		}

	    public string GetWhiteStatus()
	    {
	        bool check = checkToWhiteKing();
	        bool checkNextMove = true;
	        foreach (Loc oldFigureLoc in board.Figures(Cell.White))
	        {
	            foreach (Loc newFigureLoc in board.Get(oldFigureLoc).Figure.Moves(oldFigureLoc, board))
	            {
                    Cell destCell = board.PerformMove(oldFigureLoc, newFigureLoc);
	                if (!checkToWhiteKing())
	                {
	                    checkNextMove = false;
	                    break;
	                }
	                board.PerformUndoMove(oldFigureLoc, newFigureLoc, destCell);
	            }

	        }
	        if (check)
                return checkNextMove ? "mate" : "check";
	        else if (checkNextMove)
	            return "stalemate";
	        else
	            return "ok";
	}

		private bool checkToWhiteKing()
		{
		    foreach (Loc figureLoc in board.Figures(Cell.Black))
		    {
		        Cell cell = board.Get(figureLoc);
		        IEnumerable<Loc> moves = cell.Figure.Moves(figureLoc, board);
		        if (moves.Any(figureNewLoc => board.Get(figureNewLoc).IsWhiteKing))
                    return true;
		    }
		    return false;
		}
	}
}