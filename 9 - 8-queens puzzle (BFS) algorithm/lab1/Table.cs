using System;
using System.Collections.Generic;
using System.Text;

namespace lab1
{
    class Table
    {
        public string[,] Queens { get; private set; }

        private int _size;

        public int Conflicts
        {
            get
            {
                int conflicts = 0;
                var tempQueens = new string[_size, _size];
                Array.Copy(Queens, tempQueens, Queens.Length);
                bool hasConflicts;
                int queensInRow, queensInColumn, queensInDiagonal;

                for (int row = 0; row < tempQueens.GetLength(0); row++)
                {
                    for (int column = 0; column < tempQueens.GetLength(1); column++)
                    {
                        if (tempQueens[row, column] == "x")
                        {
                            hasConflicts = false;
                            queensInRow = CheckHorizontal(tempQueens, row, column);
                            queensInColumn = CheckVertical(tempQueens, row, column);
                            queensInDiagonal = CheckDiagonal(tempQueens, row, column);

                            if (queensInRow != 0)
                            {
                                hasConflicts = true;
                                conflicts += queensInRow;
                            }
                            if (queensInColumn != 0)
                            {
                                hasConflicts = true;
                                conflicts += queensInColumn;
                            }
                            if (queensInDiagonal != 0)
                            {
                                hasConflicts = true;
                                conflicts += queensInDiagonal;
                            }

                            if (hasConflicts)
                            {
                                tempQueens[row, column] = "o";
                            }
                        }
                    }
                }
                return conflicts;
            }
            set { }
        }

        public bool Solved
        {
            get => Conflicts == 0;
        }

        public Table(int size = 8)
        {
            _size = size;
            Queens = new string[_size, _size];
            for (int row = 0; row < Queens.GetLength(0); row++)
            {
                for (int column = 0; column < Queens.GetLength(1); column++)
                {
                    Queens[row, column] = "o";
                }
            }
        }

        public Table(string[,] queens)
        {
            _size = queens.GetLength(0);
            Queens = queens;
        }

        private int CheckHorizontal(string[,] tempQueens, int rowIndex, int columnIndex)
        {
            int countQueens = 0;
            for (int column = columnIndex + 1; column < tempQueens.GetLength(0); column++)
            {
                if (tempQueens[rowIndex, column] == "x")
                {
                    countQueens++;
                    break;
                }
            }
            return countQueens;
        }

        private int CheckVertical(string[,] tempQueens, int rowIndex, int columnIndex)
        {
            int countQueens = 0;
            for (int row = rowIndex + 1; row < tempQueens.GetLength(0); row++)
            {
                if (tempQueens[row, columnIndex] == "x")
                {
                    countQueens++;
                    break;
                }
            }
            return countQueens;

        }

        private int CheckDiagonal(string[,] tempQueens, int rowIndex, int columnIndex)
        {
            int countQueens = 0;
            for (int row = rowIndex + 1, column = columnIndex + 1; row < tempQueens.GetLength(0) && column < tempQueens.GetLength(1); row++, column++)
            {
                if (tempQueens[row, column] == "x")
                {
                    countQueens++;
                    break;
                }
            }
            for (int row = rowIndex + 1, column = columnIndex - 1; row < tempQueens.GetLength(0) && column > 0; row++, column--)
            {
                if (tempQueens[row, column] == "x")
                {
                    countQueens++;
                    break;
                }
            }
            return countQueens;
        }

        public void Generate()
        {
            //Random generation
            Random r = new Random();
            int row;
            for (int column = 0; column < Queens.GetLength(1); column++)
            {
                row = r.Next(8);
                Queens[row, column] = "x";
            }

            //solved task
            /*Queens[0, 2] = "x";
            Queens[1, 6] = "x";
            Queens[2, 1] = "x";
            Queens[3, 7] = "x";
            Queens[4, 5] = "x";
            Queens[5, 3] = "x";
            Queens[6, 0] = "x";
            Queens[7, 4] = "x";*/

            //1 incorrect
            /*Queens[0, 2] = "x";
            Queens[1, 6] = "x";
            Queens[2, 1] = "x";
            Queens[3, 7] = "x";
            Queens[4, 5] = "x";
            Queens[5, 3] = "x";
            Queens[7, 0] = "x";
            Queens[7, 4] = "x";*/

            //in line
            /*Queens[0, 2] = "x";
            Queens[0, 6] = "x";
            Queens[0, 1] = "x";
            Queens[3, 7] = "x";
            Queens[0, 5] = "x";
            Queens[0, 3] = "x";
            Queens[0, 0] = "x";
            Queens[0, 4] = "x";*/
        }

        public List<Table> GetChildrens(int level)
        {
            while (level>7)
            {
                level -= 8;
            }
            List<Table> possibleMoves = new List<Table>();
            var tempQueens = new string[_size, _size];
            Array.Copy(Queens, tempQueens, Queens.Length);
            for (int row = 0; row < tempQueens.GetLength(0); row++)
            {
                tempQueens[row, level] = "o";
            }
            for (int row = 0; row < tempQueens.GetLength(0); row++)
            {
                var tempArray = new string[_size, _size];
                Array.Copy(tempQueens, tempArray, tempQueens.Length);
                tempArray[row, level] = "x";
                possibleMoves.Add(new Table(tempArray));
            }
            return possibleMoves;
        }
          
        public override string ToString()
        {
            string result = "\n\t";

            for (int row = 0; row < Queens.GetLength(0); row++)
            {
                result += $"{row + 1}\t";
            }
            result += "\n";

            for (int row = 0; row < Queens.GetLength(0); row++)
            {
                result += $"{row + 1}\t";
                for (int column = 0; column < Queens.GetLength(1); column++)
                {
                    result += $"{Queens[row, column]}\t";
                }
                result += "\n";
            }

            result += "\n";

            if (this.Solved)
            {
                result += ">>>This task is solved\n";
            }
            else
            {
                result += $">>>This task is not solved yet\n\n>>>Number of conflicts: {this.Conflicts}\n";
            }

            return result;
        }
    }
}
