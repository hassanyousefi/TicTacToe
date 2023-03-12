namespace TicTacToe.Client.Pages
{
    public partial class MainPage
    {
        enum GameStatus
        {
            FirstWin = 0,
            SecondWin = 1,
            Draw = 2,
            NotFinish = 3
        }
        enum ItemStatus
        {
            Clean = 0,
            FirstPlayer = 1,
            SecondPlayer = 2,
        }

        readonly ItemStatus[,] items = {
            { ItemStatus.Clean, ItemStatus.Clean, ItemStatus.Clean },
            { ItemStatus.Clean, ItemStatus.Clean, ItemStatus.Clean },
            { ItemStatus.Clean, ItemStatus.Clean, ItemStatus.Clean }
        };
        bool isEven = true;
        int moveNumber = 0;
        GameStatus? winner = GameStatus.NotFinish;

        public void SelectItem(int row, int col)
        {
            if (items[row, col] != ItemStatus.Clean || winner != GameStatus.NotFinish) return;

            items[row, col] = isEven ? ItemStatus.FirstPlayer : ItemStatus.SecondPlayer;
            isEven = !isEven;
            moveNumber++;
            DetemineResult();
            if (winner != GameStatus.NotFinish) return;
            RandomSelectItem();
        }

        private void RandomSelectItem()
        {
            Random rnd = new Random();
            var row = rnd.Next(0, 3);
            var col = rnd.Next(0, 3);

            if (items[row, col] != 0)
            {
                RandomSelectItem();
                return;
            }
            items[row, col] = isEven ? ItemStatus.FirstPlayer : ItemStatus.SecondPlayer;
            isEven = !isEven;
            moveNumber++;
            DetemineResult();
        }

        public void DetemineResult()
        {
            if ((items[0, 0] == items[1, 1] && items[1, 1] == items[2, 2]) || (items[0, 2] == items[1, 1] && items[1, 1] == items[2, 0]))
            {
                if (items[1, 1] != ItemStatus.Clean)
                    winner = items[1, 1] == ItemStatus.FirstPlayer ? GameStatus.FirstWin : GameStatus.SecondWin;
            }

            for (int index = 0; index < 3 && winner == GameStatus.NotFinish; index++)
            {
                if (items[index, 0] == items[index, 1] && items[index, 1] == items[index, 2])
                {
                    if (items[index, 0] != ItemStatus.Clean)
                        winner = items[index, 0] == ItemStatus.FirstPlayer ? GameStatus.FirstWin : GameStatus.SecondWin;

                }

                if (items[0, index] == items[1, index] && items[1, index] == items[2, index])
                {
                    if (items[0, index] != ItemStatus.Clean)
                        winner = items[0, index] == ItemStatus.FirstPlayer ? GameStatus.FirstWin : GameStatus.SecondWin;
                }
            }

            if (moveNumber >= 9 && winner == GameStatus.NotFinish)
            {
                winner = GameStatus.Draw;
            }
        }
    }
}
