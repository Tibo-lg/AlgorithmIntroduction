using System;
using System.Collections.Generic;

namespace LinkedList
{
    class Program
    {
        static List<string> Menu = new List<string>()
        {
            "Menu",
            "'a' --> append to list",
            "'i' --> insert at index into list",
            "'p' --> print list",
            "'q' --> exit",
            "'r' --> remove first element with value from list",
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
            LinkedList<int> linkedList = new LinkedList<int>();
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
                            if (!TryGetInt(out int i, "Enter the value to append"))
                            {
                                BadInput();
                            }
                            else
                            {
                                linkedList.AppendNode(new Node<int>(i));
                            }
                            break;
                        case 'p':
                            linkedList.Print();
                            break;
                        case 'r':
                            if (!TryGetInt(out i, "Enter the value to remove"))
                            {
                                BadInput();
                            }
                            else
                            {
                                linkedList.RemoveFirst(i);
                            }
                            break;
                        case 'i':
                            Console.WriteLine();
                            if (!TryGetInt(out i, "Enter the value to insert") ||
                                !TryGetInt(out int ind, "Enter the index"))
                            {
                                BadInput();
                            }
                            else
                            {
                                linkedList.InsertAt(new Node<int>(i), ind);
                            }
                            break;
                    }
                }
            } while (c != 'q');
        }
    }
}
