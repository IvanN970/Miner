using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Miner
{
    class Program
    {
        static void CreateField(char[,] field)
        {
            List<string> list = new List<string>();
            string line;
            int counter = 0;
            while(counter<field.GetLength(0))
            {
                Console.WriteLine("Enter line {0} of the field with {1} symbols:",counter+1,field.GetLength(1));
                line = Console.ReadLine();
                list = ValidateLine(field, line);
                if(list!=null)
                {
                    FillRowOfTheField(field,list,counter);
                    counter++;
                }
            }
        }
        static void FillRowOfTheField(char[,] field,List<string> list,int counter)
        {
            for(int i=0;i<list.Count;i++)
            {
                char.TryParse(list[i], out field[counter, i]);
            }
        }
        static List<string> ValidateLine(char[,] field,string line)
        {
            List<string> list = line.Split(' ').ToList();
            if(list.Count==field.GetLength(1))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (IsValidSymbol(char.Parse(list[i])) == false)
                    {
                        Console.WriteLine("Symbol {0} is invalid,please enter the row once again",list[i]);
                        return null;
                    }
                }
                return list;
            }
            else
            {
                Console.WriteLine("Invalid input,row should contain {0} symbols", field.GetLength(1));
                return null;
            }
        }
        static bool IsValidSymbol(char s)
        {
            if(s=='*' || s=='s' || s=='e' || s=='c')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void PrintField(char[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.Write("{0} ", field[i, j]);
                }
                Console.WriteLine();
            }
        }
        static bool IsValidDirection(string command)
        {
            if(command=="up" || command=="down" || command=="left" || command=="right")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static List<string> EnterDirections()
        {
            Console.WriteLine("Enter directions to move the miner:");
            string str = Console.ReadLine();
            List<string> directions = str.Split(' ').ToList();
            return directions;
        }
        static bool IsInsideField(char[,] field,int row,int col)
        {
            if((row<field.GetLength(0) && row>=0) && (col<field.GetLength(1) && col>=0)) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void MoveMiner(char[,] field,List<string> directions,int row,int col)
        {
            int counter = 0;
            for (int i = 0; i < directions.Count; i++)
            {
                if (IsValidDirection(directions[i]) == true)
                {
                    switch (directions[i])
                    {
                        case "up":
                            if (IsInsideField(field, row - 1, col) == true) { row -= 1; }
                            else { continue;}
                            break;
                        case "down":
                            if (IsInsideField(field, row + 1, col) == true) { row += 1; }
                            else { continue;}
                            break;
                        case "left":
                            if (IsInsideField(field, row, col - 1) == true) { col -= 1;}
                            else { continue;}
                            break;
                        case "right":
                            if (IsInsideField(field, row, col + 1) == true) { col += 1; }
                            else { continue;}
                            break;
                        default:
                            break;
                    }
                    if (CheckSymbol(field, row, col, ref counter) == false)
                    {
                        Console.WriteLine("Game Over ({0},{1})", row, col);
                        return;
                    }
                    else
                    {
                        if(CountCoals(field)==0)
                        {
                            Console.WriteLine("You collected all coals ({0},{1})", row, col);
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid direction");
                }
                
            }
            if(CountCoals(field)!=0)
            {
                Console.WriteLine("{0} coals left! ({1},{2})",CountCoals(field),row,col);
            }
        }
        static int CountCoals(char[,] field)
        {
            int counter = 0;
            for(int i=0;i<field.GetLength(0);i++)
            {
                for(int j = 0; j < field.GetLength(1); j++)
                {
                    if(field[i,j]=='c')
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
        static bool CheckSymbol(char[,] field,int row,int col,ref int counter)
        {
            if(field[row,col]=='c')
            {
                field[row, col] = '*';
                counter++;
                return true;
            }
            else if(field[row,col]=='*')
            {
                return true;
            }
            else if(field[row,col]=='s')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static void GetStartIndex(char[,] field,out int row,out int col)
        {
            row = -1;
            col = -1;
            for(int i=0;i<field.GetLength(0);i++)
            {
                for(int j=0;j<field.GetLength(1);j++)
                {
                    if(field[i,j]=='s')
                    {
                        row = i;
                        col = j;
                        break;
                    }
                }
                if(row!=-1 && col!=-1)
                {
                    break;
                }
            }
        }
        static void Main()
        {
            int n, row,col;
            Console.WriteLine("Enter size of the field:");
            n = int.Parse(Console.ReadLine());
            char[,] field = new char[n, n];
            List<string> directions = EnterDirections();
            CreateField(field);
            PrintField(field);
            GetStartIndex(field, out row, out col);
            MoveMiner(field,directions,row,col);
        }
    }
}
