using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

namespace Chess
{
    class Program
    {
        static int[] selected = new int[] {-1,-1,-1};
        public static int[] lastmove = new int[] { -1, -1, 0, -1 };
        public static int[][] castled = new int[2][] { new int[] {0,0}, new int[] {0,0}};
        public static int check = -1;
        static int turn;
        //textures
        static Texture tone = new Texture("resources/black.png");
        static Texture ttwo = new Texture("resources/white.png");
        static Texture bb = new Texture("resources/bb.png");
        static Texture bw = new Texture("resources/bw.png");
        static Texture kb = new Texture("resources/kb.png");
        static Texture kw = new Texture("resources/kw.png");
        static Texture nb = new Texture("resources/nb.png");
        static Texture nw = new Texture("resources/nw.png");
        static Texture pb = new Texture("resources/pb.png");
        static Texture pw = new Texture("resources/pw.png");
        static Texture qb = new Texture("resources/qb.png");
        static Texture qw = new Texture("resources/qw.png");
        static Texture rb = new Texture("resources/rb.png");
        static Texture rw = new Texture("resources/rw.png");
        static Texture sel = new Texture("resources/selected.png");
        static Texture las = new Texture("resources/last.png");
        static Texture dot = new Texture("resources/dot.png");
        //Create Pieces
        public static Piece[][] pieces = new Piece[2][] { new Piece[] { new Piece(1, 0, 0, 1), new Piece(1, 0, 1, 1), new Piece(1, 0, 2, 1), new Piece(1, 0, 3, 1),
            new Piece(1,0,4,1), new Piece(1,0,5,1), new Piece(1,0,6,1), new Piece(1,0,7,1), new Piece(2,0,0,0), new Piece(2,0,7,0),
            new Piece(3,0,1,0), new Piece(3,0,6,0), new Piece(4,0,2,0), new Piece(4,0,5,0), new Piece(5,0,3,0), new Piece(6,0,4,0)}, new Piece[] { new Piece(1, 1, 0, 6), new Piece(1, 1, 1, 6), new Piece(1, 1, 2, 6), new Piece(1, 1, 3, 6),
            new Piece(1,1,4,6), new Piece(1,1,5,6), new Piece(1,1,6,6), new Piece(1,1,7,6), new Piece(2,1,0,7), new Piece(2,1,7,7),
            new Piece(3,1,1,7), new Piece(3,1,6,7), new Piece(4,1,2,7), new Piece(4,1,5,7), new Piece(5,1,3,7), new Piece(6,1,4,7)}};
        static RenderWindow window = new RenderWindow(new VideoMode(512, 512), "Chess");

        static void Main(string[] args)
        {
            turn = 1;
            window.SetActive();
            window.MouseButtonPressed += window_MouseButtonPressed;
            while (window.IsOpen)
            {
                window.Clear();
                window.DispatchEvents();
                //draw board
                for (int x=0;x<8;x++)
                {
                    for (int y = 0; y < 8; y++)
                    {
                            Sprite temp;
                            if ((x + 1 + y + 1) % 2 == 0)
                            {
                                temp = new Sprite(tone);
                            }
                            else temp = new Sprite(ttwo);
                            temp.Position = new Vector2f(x * 64, y * 64);
                            window.Draw(temp);
                    }
                }
                //draw pieces
                for (int x = 0; x < pieces[0].Length; x++)
                {
                    Sprite temp;
                    switch (pieces[0][x].type)
                    {
                            case 1:
                            {
                                temp = new Sprite(pb);
                                break;
                            }
                            case 2:
                            {
                                temp = new Sprite(rb);
                                break;
                            }
                            case 3:
                            {
                                temp = new Sprite(nb);
                                break;
                            }
                            case 4:
                            {
                                temp = new Sprite(bb);
                                break;
                            }
                            case 5:
                            {
                                temp = new Sprite(qb);
                                break;
                            }
                            case 6:
                            {
                                temp = new Sprite(kb);
                                break;
                            }
                            default:
                            {
                                temp = null;
                                break;
                            }
                    }
                    temp.Position = new Vector2f(pieces[0][x].X * 64, pieces[0][x].Y * 64);
                    window.Draw(temp);
                }
                for (int x = 0; x < pieces[1].Length; x++)
                {
                    Sprite temp;
                    switch (pieces[1][x].type)
                    {
                        case 1:
                            {
                                temp = new Sprite(pw);
                                break;
                            }
                        case 2:
                            {
                                temp = new Sprite(rw);
                                break;
                            }
                        case 3:
                            {
                                temp = new Sprite(nw);
                                break;
                            }
                        case 4:
                            {
                                temp = new Sprite(bw);
                                break;
                            }
                        case 5:
                            {
                                temp = new Sprite(qw);
                                break;
                            }
                        case 6:
                            {
                                temp = new Sprite(kw);
                                break;
                            }
                        default:
                            {
                                temp = null;
                                break;
                            }
                    }
                    temp.Position = new Vector2f(pieces[1][x].X * 64, pieces[1][x].Y * 64);
                    window.Draw(temp);
                }
                if (selected[0] > -1)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            if (turn == 0)
                            {
                                if (pieces[0][selected[2]].CanMove(x, y) && pieces[0][selected[2]].IfMoveNoCheck(x, y))
                                {
                                    Sprite doc = new Sprite(dot);
                                    doc.Position = new Vector2f(x * 64, y * 64);
                                    window.Draw(doc);
                                }
                            }
                            else
                            {
                                if (pieces[1][selected[2]].CanMove(x, y) && pieces[1][selected[2]].IfMoveNoCheck(x, y))
                                {
                                    Sprite doc = new Sprite(dot);
                                    doc.Position = new Vector2f(x * 64, y * 64);
                                    window.Draw(doc);
                                }
                            }
                        }
                    }
                    Sprite temp;
                    temp = new Sprite(sel);
                    temp.Position = new Vector2f(selected[0] * 64, selected[1] * 64);
                    window.Draw(temp);
                }
                if (lastmove[0] > -1)
                {
                    Sprite temp;
                    temp = new Sprite(las);
                    temp.Position = new Vector2f(lastmove[0] * 64, lastmove[1] * 64);
                    window.Draw(temp);
                }
                window.Display();
            }
        }

        static void window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                if (selected[0] > -1)
                {
                    if (turn == 1)
                    {
                        if (pieces[1][selected[2]].CanMove(Mouse.GetPosition(window).X / 64, Mouse.GetPosition(window).Y / 64))
                        {
                            if (pieces[1][selected[2]].IfMoveNoCheck(Mouse.GetPosition(window).X / 64, Mouse.GetPosition(window).Y / 64))
                            {
                                if (Enemy(turn, Mouse.GetPosition(window).X / 64, Mouse.GetPosition(window).Y / 64))
                                {
                                    DelEnemy(turn, Mouse.GetPosition(window).X / 64, Mouse.GetPosition(window).Y / 64);
                                }
                                if (lastmove[0] == Mouse.GetPosition(window).X / 64 && lastmove[1] == (Mouse.GetPosition(window).Y / 64) - 1)
                                {
                                    if (lastmove[2] == 1)
                                    {
                                        DelEnemy(turn, Mouse.GetPosition(window).X / 64, (Mouse.GetPosition(window).Y / 64) + 1);
                                    }
                                }
                                if (pieces[1][selected[2]].type == 6)
                                {
                                    if (Mouse.GetPosition(window).X / 64 == pieces[1][selected[2]].X + 2)
                                    {
                                        pieces[1][9].Move(pieces[1][selected[2]].X + 1, pieces[1][selected[2]].Y); //id 9 = right rook
                                    }
                                    if (Mouse.GetPosition(window).X / 64 == pieces[1][selected[2]].X - 2)
                                    {
                                        pieces[1][8].Move(pieces[1][selected[2]].X - 1, pieces[1][selected[2]].Y); //id 8 = left rook
                                    }
                                }
                                pieces[1][selected[2]].Move(Mouse.GetPosition(window).X / 64, Mouse.GetPosition(window).Y / 64);
                                Check(turn);
                                turn = 0;
                            }
                        }
                    }
                    else
                    {
                        if (pieces[0][selected[2]].CanMove(Mouse.GetPosition(window).X / 64, Mouse.GetPosition(window).Y / 64))
                        {
                            if (pieces[0][selected[2]].IfMoveNoCheck(Mouse.GetPosition(window).X / 64, Mouse.GetPosition(window).Y / 64))
                            {
                                if (Enemy(turn, Mouse.GetPosition(window).X / 64, Mouse.GetPosition(window).Y / 64))
                                {
                                    DelEnemy(turn, Mouse.GetPosition(window).X / 64, Mouse.GetPosition(window).Y / 64);
                                }
                                if (lastmove[0] == Mouse.GetPosition(window).X / 64 && lastmove[1] == (Mouse.GetPosition(window).Y / 64) + 1)
                                {
                                    if (lastmove[2] == 1)
                                    {
                                        DelEnemy(turn, Mouse.GetPosition(window).X / 64, (Mouse.GetPosition(window).Y / 64) - 1);
                                    }
                                }
                                if (pieces[0][selected[2]].type == 6)
                                {
                                    if (Mouse.GetPosition(window).X / 64 == pieces[0][selected[2]].X + 2)
                                    {
                                        pieces[0][9].Move(pieces[0][selected[2]].X + 1, pieces[0][selected[2]].Y); //id 9 = right rook
                                    }
                                    if (Mouse.GetPosition(window).X / 64 == pieces[0][selected[2]].X - 2)
                                    {
                                        pieces[0][8].Move(pieces[0][selected[2]].X - 1, pieces[0][selected[2]].Y); //id 8 = left rook
                                    }
                                }
                                pieces[0][selected[2]].Move(Mouse.GetPosition(window).X / 64, Mouse.GetPosition(window).Y / 64);
                                Check(turn);
                                turn = 1;
                            }
                        }
                    }
                    selected[0] = -1;
                    selected[1] = -1;
                    selected[2] = -1;
                }
                else
                {
                    if (turn == 1)
                    {
                        for (int x = 0; x < pieces[1].Length; x++)
                        {
                            if (pieces[1][x].X == Mouse.GetPosition(window).X / 64 && pieces[1][x].Y == Mouse.GetPosition(window).Y / 64)
                            {
                                selected[0] = (Mouse.GetPosition(window).X / 64);
                                selected[1] = (Mouse.GetPosition(window).Y / 64);
                                selected[2] = x;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int x = 0; x < pieces[0].Length; x++)
                        {
                            if (pieces[0][x].X == Mouse.GetPosition(window).X / 64 && pieces[0][x].Y == Mouse.GetPosition(window).Y / 64)
                            {
                                selected[0] = (Mouse.GetPosition(window).X / 64);
                                selected[1] = (Mouse.GetPosition(window).Y / 64);
                                selected[2] = x;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static bool Empty(int X, int Y)
        {
            for (int x = 0; x < pieces[0].Length; x++)
            {
                if (pieces[0][x].X == X && pieces[0][x].Y == Y)
                {
                    return false;
                }
            }
            for (int x = 0; x < pieces[1].Length; x++)
            {
                if (pieces[1][x].X == X && pieces[1][x].Y == Y)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Enemy(int player, int X, int Y)
        {
            if (player == 1)
            {
                for (int x = 0; x < pieces[0].Length; x++)
                {
                    if (pieces[0][x].X == X && pieces[0][x].Y == Y)
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (int x = 0; x < pieces[1].Length; x++)
                {
                    if (pieces[1][x].X == X && pieces[1][x].Y == Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void DelEnemy(int player, int X, int Y)
        {
            if (player == 1)
            {
                for (int x = 0; x < pieces[0].Length; x++)
                {
                    if (pieces[0][x].X == X && pieces[0][x].Y == Y)
                    {
                        pieces[0][x].Destroy();
                    }
                }
            }
            else
            {
                for (int x = 0; x < pieces[1].Length; x++)
                {
                    if (pieces[1][x].X == X && pieces[1][x].Y == Y)
                    {
                        pieces[1][x].Destroy();
                    }
                }
            }
        }

        public static void Check(int player)
        {
            int MovePosible = 0;
            check = -1;
            for (int i = 0; i < pieces[player].Length; i++)
            {
                if (player == 1)
                {
                        if (pieces[player][i].CanMove(pieces[0][15].X, pieces[0][15].Y))
                        {
                            check = 0;
                            Console.WriteLine("Checked");
                        }
                }
                else
                {
                        if (pieces[player][i].CanMove(pieces[1][15].X, pieces[1][15].Y))
                        {
                            check = 1;
                            Console.WriteLine("Checked");
                        }
                }
            }
            if (check > -1)
            {
                for (int i = 0; i < pieces[check].Length; i++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            if (pieces[check][i].CanMove(x, y) && pieces[check][i].IfMoveNoCheck(x, y))
                            {
                                MovePosible++;
                            }
                        }
                    }
                }
                if (MovePosible == 0)
                {
                    FinGame(player);
                }
            }
        }

        public static void FinGame(int winner)
        {
            if(winner == 0)
            {
                Console.WriteLine("Winner: Black");
                Console.WriteLine("Loser: White");
            }
            else
            {
                Console.WriteLine("Winner: White");
                Console.WriteLine("Loser: Black");
            }
        }
    }
}
