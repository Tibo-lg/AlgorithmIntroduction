using System;
using System.Collections.Generic;

namespace Trees
{
    class Program
    {
        static List<string> Menu = new List<string>()
        {
            "Menu",
            "'a' --> add to tree",
            "'p' --> print tree",
            "'b' --> print bfs",
            "'d' --> print dfs",
            "'q' --> exit",
        };

        static public void PrintMenu()
        {
            foreach(string s in Menu)
            {
                Console.WriteLine(s);
            }
        }

        static bool TryGetChar(out char c)
        {
            string s = Console.ReadLine();
            return char.TryParse(s, out c);
        }

        static bool TryGetInt(out int i, string display)
        {
            Console.WriteLine(display);
            string s = Console.ReadLine();
            return int.TryParse(s, out i);
        }

        static void BadInput()
        {
            Console.WriteLine("Unrecognized input.");
        }

        static void Main(string[] args)
        {
            var bst = new BinarySearchTree();
            char c;

            do
            {
                PrintMenu();
                if (!TryGetChar(out c))
                {
                    BadInput();
                }
                else
                {
                    switch (c)
                    {
                        case 'a':
                            if (!TryGetInt(out int i, "Enter the value to add"))
                            {
                                BadInput();
                            }
                            else
                            {
                                bst.Insert(i);
                            }
                            break;
                        case 'p':
                            BTreePrinter.Print(bst.Root);
                            break;
                        case 'b':
                            bst.PrintBfs();
                            break;
                        case 'd':
                            bst.PrintDfs();
                            break;
                    }
                }
            } while (c != 'q');
        }
    }
}
