using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    class Piece
    {
        public int player;
        public int type;
        public int X;
        public int Y;
        public int moved;

        public Piece(int type, int player, int X, int Y)
        {
            this.player = player;
            this.type = type;
            this.X = X;
            this.Y = Y;
        }

        public void Move(int X, int Y)
        {
            Program.lastmove[0] = this.X;
            Program.lastmove[1] = this.Y;
            Program.lastmove[2] = this.type;
            Program.lastmove[3] = this.player;
            if (moved == 0)
            {
                if (this.type == 2)
                {
                    if (this.X == 0)
                    {
                        Program.castled[this.player][0] = 1;
                    }
                    if (this.X == 7)
                    {
                        Program.castled[this.player][1] = 1;
                    }
                }
                if (this.type == 6)
                {
                        Program.castled[this.player][0] = 1;
                        Program.castled[this.player][1] = 1;
                }
            }
            this.X = X;
            this.Y = Y;
            this.moved = 1;
        }

        public void Destroy()
        {
            this.X = 9999;
            this.Y = 9999;
        }

        public bool CanMove(int X, int Y)
        {
            switch (this.type)
            {
                case 1:
                    {
                        if (this.player == 0)
                        {
                            if (this.X == X)
                            {
                                if (this.Y == 1 && Y == 3)
                                {
                                    if (Program.Empty(X, 2) && Program.Empty(X, 3))
                                    {
                                        return true;
                                    }
                                }
                                if (Y == this.Y + 1)
                                {
                                    if (Program.Empty(X, Y))
                                    {
                                        return true;
                                    }
                                }
                            }
                            if (X == this.X + 1 || X == this.X - 1)
                            {
                                if (Y == this.Y + 1)
                                {
                                    if (Program.Enemy(player, X, Y))
                                    {
                                        return true;
                                    }
                                    if (Program.lastmove[0] == X && Program.lastmove[1] == Y + 1)
                                    {
                                        if (Program.lastmove[2] == 1)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (this.X == X)
                            {
                                if (this.Y == 6 && Y == 4)
                                {
                                    if (Program.Empty(X, 5) && Program.Empty(X, 4))
                                    {
                                        return true;
                                    }
                                }
                                if (Y == this.Y - 1)
                                {
                                    if (Program.Empty(X, Y))
                                    {
                                        return true;
                                    }
                                }
                            }
                            if (X == this.X + 1 || X == this.X - 1)
                            {
                                if (Y == this.Y - 1)
                                {
                                    if (Program.Enemy(player, X, Y))
                                    {
                                        return true;
                                    }
                                    if (Program.lastmove[0] == X && Program.lastmove[1] == Y-1)
                                    {
                                        if (Program.lastmove[2] == 1)
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        if (this.X == X)
                        {
                            for (int y = this.Y + 1; y < 8; y++)
                            {
                                if (Program.Enemy(player, this.X, y))
                                {
                                    if (Y == y)
                                    {
                                        return true;
                                    }
                                    break;
                                }
                                if (Program.Empty(this.X, y))
                                {
                                    if (Y == y)
                                    {
                                        return true;
                                    }
                                }
                                else break;
                            }
                            for (int y = this.Y - 1; y >= 0; y--)
                            {
                                if (Program.Enemy(player, this.X, y))
                                {
                                    if (Y == y)
                                    {
                                        return true;
                                    }
                                    break;
                                }
                                if (Program.Empty(this.X, y))
                                {
                                    if (Y == y)
                                    {
                                        return true;
                                    }
                                }
                                else break;
                            }
                        }
                        if (this.Y == Y)
                        {
                            for (int x = this.X + 1; x < 8; x++)
                            {
                                if (Program.Enemy(player, x, this.Y))
                                {
                                    if (X == x)
                                    {
                                        return true;
                                    }
                                    break;
                                }
                                if (Program.Empty(x, this.Y))
                                {
                                    if (X == x)
                                    {
                                        return true;
                                    }
                                }
                                else break;
                            }
                            for (int x = this.X - 1; x >= 0; x--)
                            {
                                if (Program.Enemy(player, x, this.Y))
                                {
                                    if (X == x)
                                    {
                                        return true;
                                    }
                                    break;
                                }
                                if (Program.Empty(x, this.Y))
                                {
                                    if (X == x)
                                    {
                                        return true;
                                    }
                                }
                                else break;
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        if (X == this.X + 1 || X == this.X - 1)
                        {
                            if (Y == this.Y + 2 || Y == this.Y - 2)
                            {
                                if (Program.Enemy(player, X, Y) || Program.Empty(X, Y))
                                {
                                    return true;
                                }
                            }
                        }
                        if (Y == this.Y + 1 || Y == this.Y - 1)
                        {
                            if (X == this.X + 2 || X == this.X - 2)
                            {
                                if (Program.Enemy(player, X, Y) || Program.Empty(X, Y))
                                {
                                    return true;
                                }
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        for (int x = this.X + 1, y = this.Y + 1; x < 8 && y < 8; x++, y++)
                        {
                            if (Program.Enemy(player, x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                                break;
                            }
                            if (Program.Empty(x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                            }
                            else break;
                        }
                        for (int x = this.X - 1, y = this.Y + 1; x >= 0 && y < 8; x--, y++)
                        {
                            if (Program.Enemy(player, x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                                break;
                            }
                            if (Program.Empty(x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                            }
                            else break;
                        }
                        for (int x = this.X - 1, y = this.Y - 1; x >= 0 && y >= 0; x--, y--)
                        {
                            if (Program.Enemy(player, x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                                break;
                            }
                            if (Program.Empty(x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                            }
                            else break;
                        }
                        for (int x = this.X + 1, y = this.Y - 1; x < 8 && y >= 0; x++, y--)
                        {
                            if (Program.Enemy(player, x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                                break;
                            }
                            if (Program.Empty(x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                            }
                            else break;
                        }
                        break;
                    }
                case 5:
                    {
                        for (int x = this.X + 1, y = this.Y + 1; x < 8 && y < 8; x++, y++)
                        {
                            if (Program.Enemy(player, x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                                break;
                            }
                            if (Program.Empty(x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                            }
                            else break;
                        }
                        for (int x = this.X - 1, y = this.Y + 1; x >= 0 && y < 8; x--, y++)
                        {
                            if (Program.Enemy(player, x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                                break;
                            }
                            if (Program.Empty(x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                            }
                            else break;
                        }
                        for (int x = this.X - 1, y = this.Y - 1; x >= 0 && y >= 0; x--, y--)
                        {
                            if (Program.Enemy(player, x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                                break;
                            }
                            if (Program.Empty(x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                            }
                            else break;
                        }
                        for (int x = this.X + 1, y = this.Y - 1; x < 8 && y >= 0; x++, y--)
                        {
                            if (Program.Enemy(player, x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                                break;
                            }
                            if (Program.Empty(x, y))
                            {
                                if (X == x && Y == y)
                                {
                                    return true;
                                }
                            }
                            else break;
                        }
                        if (this.X == X)
                        {
                            for (int y = this.Y + 1; y < 8; y++)
                            {
                                if (Program.Enemy(player, this.X, y))
                                {
                                    if (Y == y)
                                    {
                                        return true;
                                    }
                                    break;
                                }
                                if (Program.Empty(this.X, y))
                                {
                                    if (Y == y)
                                    {
                                        return true;
                                    }
                                }
                                else break;
                            }
                            for (int y = this.Y - 1; y >= 0; y--)
                            {
                                if (Program.Enemy(player, this.X, y))
                                {
                                    if (Y == y)
                                    {
                                        return true;
                                    }
                                    break;
                                }
                                if (Program.Empty(this.X, y))
                                {
                                    if (Y == y)
                                    {
                                        return true;
                                    }
                                }
                                else break;
                            }
                        }
                        if (this.Y == Y)
                        {
                            for (int x = this.X + 1; x < 8; x++)
                            {
                                if (Program.Enemy(player, x, this.Y))
                                {
                                    if (X == x)
                                    {
                                        return true;
                                    }
                                    break;
                                }
                                if (Program.Empty(x, this.Y))
                                {
                                    if (X == x)
                                    {
                                        return true;
                                    }
                                }
                                else break;
                            }
                            for (int x = this.X - 1; x >= 0; x--)
                            {
                                if (Program.Enemy(player, x, this.Y))
                                {
                                    if (X == x)
                                    {
                                        return true;
                                    }
                                    break;
                                }
                                if (Program.Empty(x, this.Y))
                                {
                                    if (X == x)
                                    {
                                        return true;
                                    }
                                }
                                else break;
                            }
                        }
                        break;
                    }
                case 6:
                    {
                        if (X == this.X + 1 || X == this.X - 1 || X == this.X)
                        {
                            if (Y == this.Y + 1 || Y == this.Y - 1 || Y == this.Y)
                            {
                                if (Program.Enemy(player, X, Y))
                                {
                                    return true;
                                }
                                if (Program.Empty(X, Y))
                                {
                                    return true;
                                }
                            }
                        }
                        if (Y == this.Y)
                        {
                            if (X == this.X - 2)
                            {
                                if (Program.Empty(this.X-1, Y) && Program.Empty(this.X-2, Y) && Program.Empty(this.X-3, Y))
                                {
                                    if (Program.castled[player][0] == 0)
                                    {
                                        return true;
                                    }
                                }
                            }
                            if (X == this.X + 2)
                            {
                                if (Program.Empty(this.X + 1, Y) && Program.Empty(this.X + 2, Y))
                                {
                                    if (Program.castled[player][1] == 0)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                        break;
                    }
            }
            return false;
        }

        public bool IfMoveNoCheck(int X, int Y)
        {
            int tempx = this.X, tempy = this.Y;//Variables temporales
            this.X = X; // Temporalmente pongo a la pieza en una posición hipotética
            this.Y = Y;
            if (player == 1) //Empezamos con el jugador blanco
            {
                for (int a = 0; a < Program.pieces[0].Length; a++) //Para cada pieza negra pieces[0][0-15] = negras
                {
                        if (Program.pieces[0][a].CanMove(Program.pieces[1][15].X, Program.pieces[1][15].Y))//Checkeamos que pueda matar al rey
                        {
                            if (this.X == Program.pieces[0][a].X && this.Y == Program.pieces[0][a].Y)
                            {
                                this.X = tempx;
                                this.Y = tempy;
                                return true;
                            }
                            this.X = tempx;
                            this.Y = tempy;
                            return false;//Retornamos todo a su lugar y sabemos que no se puede hacer este movimiento
                        }
                }
            }
            else
            {
                for (int a = 0; a < Program.pieces[1].Length; a++)
                {
                        if (Program.pieces[1][a].CanMove(Program.pieces[0][15].X, Program.pieces[0][15].Y))
                        {
                            if (this.X == Program.pieces[1][a].X && this.Y == Program.pieces[1][a].Y)
                            {
                                this.X = tempx;
                                this.Y = tempy;
                                return true;
                            }
                            this.X = tempx;
                            this.Y = tempy;
                            return false;
                        }
                }
            }
            this.X = tempx;
            this.Y = tempy;
            return true;
        }
        }

}
