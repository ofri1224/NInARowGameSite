using System;
using System.Web.UI.WebControls;

namespace FourInARowGameSite
{
    public partial class MainPage : System.Web.UI.Page
    {
        static char[,] boardState;
        //static Button[,] buttons;
        private static int TurnCount;
        private static bool click, reload, Win;
        private static int wLength;


        protected void Page_Load(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("_______________________");
            //System.Diagnostics.Debug.WriteLine(Request.Params["__EVENTTARGET"]);
            if (click && !reload)
            {
                //System.Diagnostics.Debug.WriteLine("1");
                CreateTable(boardState);
                //CreateTable(buttons);
            }
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("load complete");

            if (click && reload)
            {
                //System.Diagnostics.Debug.WriteLine("2");
                //CreateTable(boardState);
                CreateTable(boardState);

                reload = false;
            }
            if (Win)
            {
                VictoryScreen.CssClass = "Victory";
                ReButton.CssClass = "ReButton";
            }
            else
            {
                VictoryScreen.CssClass = "";
                ReButton.CssClass = "Invisible";
            }
        }
        /*
        protected void Page_Load(object sender, EventArgs e)
        {
            if (click && !reload)
            {
                //System.Diagnostics.Debug.WriteLine("1");
                CreateTable(buttons);
                //CreateTable(buttons);
            }
        }
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (click && reload)
            {
                //System.Diagnostics.Debug.WriteLine("2");
                //CreateTable(boardState);
                CreateTable(buttons);

                reload = false;
            }
            if (Win)
            {
                VictoryScreen.CssClass = "Victory";
                ReButton.CssClass = "ReButton";
            }
            else
            {
                VictoryScreen.CssClass = "";
                ReButton.CssClass = "Invisible";
            }
        }
        */


        protected void CreateTable(char[,] board)
        {
            GameBoard.Controls.Clear();
            Table t = new Table() { CssClass = "board" };
            for (int i = 0; i < board.GetLength(0); i++)
            {
                TableRow r = new TableRow();
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Button b = new Button() { ID = "BTN" + i.ToString() + j.ToString(), Height = 50, Width = 50 };
                    switch (board[i, j])
                    {
                        case 'R':
                            b.Enabled = false;
                            b.CssClass = "RPiece";
                            break;
                        case 'Y':
                            b.Enabled = false;
                            b.CssClass = "YPiece";
                            break;
                        default:
                            b.Enabled = true;
                            b.CssClass = "NoPiece";
                            break;
                    }
                    b.Click += Cell_Click;
                    b.Attributes.Add("xpos", j.ToString());
                    TableCell c = new TableCell();
                    c.Controls.Add(b);
                    r.Cells.Add(c);
                }
                t.Rows.Add(r);
            }
            GameBoard.Controls.Add(t);
        }
        /*
        protected void CreateTable(Button[,] buttons)
        {
            GameBoard.Controls.Clear();
            Table t = new Table() { CssClass = "board" };
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                TableRow r = new TableRow();
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    buttons[i, j] = new Button() { ID = "BTN" + i.ToString() + j.ToString(), Height = 50, Width = 50, CssClass="NoPiece" };
                    buttons[i, j].Click += Cell_Click;
                    buttons[i, j].Attributes.Add("xpos", j.ToString());
                    TableCell c = new TableCell();
                    c.Controls.Add(buttons[i, j]);
                    r.Cells.Add(c);
                }
                t.Rows.Add(r);
            }
            GameBoard.Controls.Add(t);
        }
        */

        private void Cell_Click(object sender, EventArgs e)
        {
            if (!Win)
            {
                //System.Diagnostics.Debug.WriteLine("Cell_Click()");
                int column = int.Parse(((Button)sender).Attributes["xpos"]);
                int height = 0;
                while (height < boardState.GetLength(0) - 1 && boardState[height + 1, column] == '\0')
                {
                    height++;
                }
                if (TurnCount % 2 == 0)
                {
                    boardState[height, column] = 'R';
                    if (Win_Check('R', height, column))
                    {
                        Win = true;
                        VictoryText.Text = "Red" + " Wins";
                    }
                    //System.Diagnostics.Debug.WriteLine(boardState.GetLength(1) - column);
                }
                else
                {
                    boardState[height, column] = 'Y';
                    if (Win_Check('Y', height, column))
                    {
                        Win = true;
                        VictoryText.Text = "Yellow" + " Wins";
                    }
                }
                TurnCount++;



                reload = true;
            }

        }


        //private void Cell_Click(object sender, EventArgs e)
        //{
        //    if (!Win)
        //    {
        //        //System.Diagnostics.Debug.WriteLine("Cell_Click()");
        //        int column = int.Parse(((Button)sender).Attributes["xpos"]);
        //        int height = 0;
        //        while (height < buttons.GetLength(0) - 1 && buttons[height + 1, column].CssClass == "NoPiece")
        //        {
        //            height++;
        //        }
        //        if (TurnCount % 2 == 0)
        //        {
        //            buttons[height, column].CssClass = "RPiece";
        //            if (Win_Check("RPiece", height, column))
        //            {
        //                Win = true;
        //                VictoryText.Text = "Red" + " Wins";
        //            }
        //            //System.Diagnostics.Debug.WriteLine(boardState.GetLength(1) - column);
        //        }
        //        else
        //        {
        //            buttons[height, column].CssClass = "YPiece";
        //            if (Win_Check("YPiece", height, column))
        //            {
        //                Win = true;
        //                VictoryText.Text = "Yellow" + " Wins";
        //            }
        //        }
        //        TurnCount++;



        //        reload = true;
        //    }

        //}

        protected void ReButton_Click(object sender, EventArgs e)
        {
            if (Win)
            {
                Win = false;
                VictoryText.Text = "";
                boardState = new char[boardState.GetLength(0), boardState.GetLength(1)];
                click = reload = true;
            }
        }

        //protected void ReButton_Click(object sender, EventArgs e)
        //{
        //    if (Win)
        //    {
        //        Win = false;
        //        VictoryText.Text = "";
        //        buttons = new Button[buttons.GetLength(0), buttons.GetLength(1)];
        //        click = reload = true;
        //    }
        //}

        private bool Win_Check(char ch, int YPlacement, int XPlacement)
        {

            #region
            /*
            bool right, left, up, down;
            int XCurrent = YPlacement;
            int YCurrent = YPlacement;
            int rcount = 1;
            if (boardState.GetLength(0) - YPlacement > WINLENGTH)
            {
                //downwards long enough to win
                down = true;
            }
            else
            {
                if (YPlacement >= WINLENGTH)
                {
                    up = true;
                }
            }
            rcount = 1;
            if (boardState.GetLength(1) - XPlacement > WINLENGTH)
            {
                // right long enough to win
                while (XCurrent - XPlacement < WINLENGTH && boardState[YCurrent, XCurrent] == ch)
                {
                    rcount++;
                    if (rcount >= WINLENGTH)
                    {
                        return true;
                    }
                    XCurrent++;
                }
                if (XPlacement >= WINLENGTH - rcount)
                {
                    XCurrent = XPlacement;
                    while (XPlacement - XCurrent < WINLENGTH && boardState[YCurrent, XCurrent] == ch)
                    {
                        rcount++;
                        if (rcount >= WINLENGTH)
                        {
                            return true;
                        }
                        XCurrent--;
                    }
                }

            }
            else
            {
                if (XPlacement >= WINLENGTH)
                {
                    // left long enough to win
                    while (XPlacement - XCurrent < WINLENGTH && boardState[YCurrent, XCurrent] == ch)
                    {
                        rcount++;
                        if (rcount >= WINLENGTH)
                        {
                            return true;
                        }
                        XCurrent--;
                    }

                    if (boardState.GetLength(1) - XPlacement > WINLENGTH - rcount)
                    {
                        XCurrent = XPlacement;
                        while (XCurrent - XPlacement < WINLENGTH && boardState[YCurrent, XCurrent] == ch)
                        {
                            rcount++;
                            if (rcount >= WINLENGTH)
                            {
                                return true;
                            }
                            XCurrent++;
                        }
                    }
                }
            }


            return true;
            */
            #endregion
            int count = 0;

            // Horizontal check
            for (int i = 0; i < boardState.GetLength(1); i++)
            {
                if (boardState[YPlacement, i] == ch)
                    count++;
                else
                    count = 0;

                if (count >= wLength)
                    return true;
            }
            //Vertical check
            for (int i = 0; i < boardState.GetLength(0); i++)
            {
                if (boardState[i, XPlacement] == ch)
                    count++;
                else
                    count = 0;

                if (count >= wLength)
                    return true;
            }
            count = 0;
            // 4 in a row diagonally
            for (int i = XPlacement + 1, j = YPlacement + 1; i < boardState.GetLength(0) && j < boardState.GetLength(1); i++, j++)
            {
                if (boardState[j, i] != ch)
                {
                    count = 1;
                    break;
                }
                count++;
            }
            // 4 in a row diagonally
            for (int i = XPlacement - 1, j = YPlacement - 1; i >= 0 && j >= 0; i--, j--)
            {
                if (boardState[j, i] != ch)
                {
                    count = 1;
                    break;
                }
                count++;
            }
            // 4 in a row diagonally
            for (int i = XPlacement, j = YPlacement; i + 1 < boardState.GetLength(1) && j >= 0; i++, j--)
            {
                if (boardState[j, i] != ch)
                {
                    count = 1;
                    break;
                }
                count++;
            }

            for (int i = XPlacement - 1, j = YPlacement + 1; i >= 0 && j < boardState.GetLength(0); i--, j++)
            { // 4 in a row diagonally
                if (boardState[j, i] != ch)
                {
                    count = 1;
                    break;
                }
                count++;
            }

            if (count >= wLength)
                return true;

            return false;
        }
        /*
                private bool Win_Check(string text, int YPlacement, int XPlacement)
                {
                    #region
                    
                    bool right, left, up, down;
                    int XCurrent = YPlacement;
                    int YCurrent = YPlacement;
                    int rcount = 1;
                    if (boardState.GetLength(0) - YPlacement > WINLENGTH)
                    {
                        //downwards long enough to win
                        down = true;
                    }
                    else
                    {
                        if (YPlacement >= WINLENGTH)
                        {
                            up = true;
                        }
                    }
                    rcount = 1;
                    if (boardState.GetLength(1)-XPlacement > WINLENGTH)
                    {
                        // right long enough to win
                        while (XCurrent - XPlacement < WINLENGTH && boardState[YCurrent, XCurrent] == ch)
                        {
                            rcount++;
                            if (rcount>=WINLENGTH)
                            {
                                return true;
                            }
                            XCurrent++;
                        }
                        if (XPlacement >= WINLENGTH-rcount)
                        {
                            XCurrent = XPlacement;
                            while (XPlacement - XCurrent < WINLENGTH && boardState[YCurrent, XCurrent] == ch)
                            {
                                rcount++;
                                if (rcount >= WINLENGTH)
                                {
                                    return true;
                                }
                                XCurrent--;
                            }
                        }

                    }
                    else
                    {
                        if (XPlacement >= WINLENGTH)
                        {
                            // left long enough to win
                            while (XPlacement - XCurrent < WINLENGTH && boardState[YCurrent, XCurrent] == ch)
                            {
                                rcount++;
                                if (rcount >= WINLENGTH)
                                {
                                    return true;
                                }
                                XCurrent--;
                            }

                            if (boardState.GetLength(1) - XPlacement > WINLENGTH - rcount)
                            {
                                XCurrent = XPlacement;
                                while (XCurrent - XPlacement < WINLENGTH && boardState[YCurrent, XCurrent] == ch)
                                {
                                    rcount++;
                                    if (rcount >= WINLENGTH)
                                    {
                                        return true;
                                    }
                                    XCurrent++;
                                }
                            }
                        }
                    }


                    return true;
                    
        #endregion

    //            int count = 0;

    //                // Horizontal check
    //                for (int i = 0; i<buttons.GetLength(1); i++)
    //                {
    //                    if (buttons[YPlacement, i].CssClass == text)
    //                        count++;
    //                    else
    //                        count = 0;

    //                    if (count >= wLength)
    //                        return true;
    //                }
    //                //Vertical check
    //                for (int i = 0; i<buttons.GetLength(0); i++)
    //                {
    //                    if (buttons[i, XPlacement].CssClass == text)
    //                        count++;
    //                    else
    //                        count = 0;

    //                    if (count >= wLength)
    //                        return true;
    //                }
    //    count = 0;
    //    // 4 in a row diagonally
    //    for (int i = XPlacement + 1, j = YPlacement + 1; i < buttons.GetLength(0) && j < buttons.GetLength(1); i++, j++)
    //    {
    //        if (buttons[j, i].CssClass != text)
    //        {
    //            count = 1;
    //            break;
    //        }
    //        count++;
    //    }
    //    // 4 in a row diagonally
    //    for (int i = XPlacement - 1, j = YPlacement - 1; i >= 0 && j >= 0; i--, j--)
    //    {
    //        if (buttons[j, i].CssClass != text)
    //        {
    //            count = 0;
    //            break;
    //        }
    //        count++;
    //    }
    //    // 4 in a row diagonally
    //    for (int i = XPlacement, j = YPlacement; i + 1 < buttons.GetLength(1) && j >= 0; i++, j--)
    //    {
    //        if (buttons[j, i].CssClass != text)
    //        {
    //            count = 0;
    //            break;
    //        }
    //        count++;
    //    }

    //    for (int i = XPlacement - 1, j = YPlacement + 1; i >= 0 && j < buttons.GetLength(0); i--, j++)
    //    { // 4 in a row diagonally
    //        if (buttons[j, i].CssClass != text)
    //        {
    //            count = 0;
    //            break;
    //        }
    //        count++;
    //    }

    //    if (count >= wLength)
    //        return true;

    //    return false;
    //}
        */

        protected void Create_Board_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Create_Board_Click()");
            boardState = new char[int.Parse(XSIZE.Text), int.Parse(YSIZE.Text)];
            wLength = int.Parse(WINLENGTH.Text);
            click = reload = true;
            Win = false;
        }
    }
}


/*protected void Create_Board_Click(object sender, EventArgs e)
{
    //System.Diagnostics.Debug.WriteLine("Create_Board_Click()");
    buttons = new Button[int.Parse(XSIZE.Text), int.Parse(YSIZE.Text)];
    wLength = int.Parse(WINLENGTH.Text);
    click = reload = true;
    Win = false;
}*/