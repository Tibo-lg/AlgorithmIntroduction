using System;
using System.Collections.Generic;

namespace Trees
{
    // https://stackoverflow.com/questions/36311991/c-sharp-display-a-binary-search-tree-in-console/36313190
    public static class BTreePrinter
    {
        class NodeInfo
        {
            public BinaryTreeNode Node;
            public string Text;
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo Parent, Left, Right;
        }

        public static void Print(this BinaryTreeNode root, int topMargin = 2, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var Value = new NodeInfo { Node = next, Text = next.Value.ToString(" 0 ") };
                if (level < last.Count)
                {
                    Value.StartPos = last[level].EndPos + 1;
                    last[level] = Value;
                }
                else
                {
                    Value.StartPos = leftMargin;
                    last.Add(Value);
                }
                if (level > 0)
                {
                    Value.Parent = last[level - 1];
                    if (next == Value.Parent.Node.LeftChild)
                    {
                        Value.Parent.Left = Value;
                        Value.EndPos = Math.Max(Value.EndPos, Value.Parent.StartPos);
                    }
                    else
                    {
                        Value.Parent.Right = Value;
                        Value.StartPos = Math.Max(Value.StartPos, Value.Parent.EndPos);
                    }
                }
                next = next.LeftChild ?? next.RightChild;
                for (; next == null; Value = Value.Parent)
                {
                    Print(Value, rootTop + 2 * level);
                    if (--level < 0) break;
                    if (Value == Value.Parent.Left)
                    {
                        Value.Parent.StartPos = Value.EndPos;
                        next = Value.Parent.Node.RightChild;
                    }
                    else
                    {
                        if (Value.Parent.Left == null)
                            Value.Parent.EndPos = Value.StartPos;
                        else
                            Value.Parent.StartPos += (Value.StartPos - Value.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print(NodeInfo Value, int top)
        {
            SwapColors();
            Print(Value.Text, top, Value.StartPos);
            SwapColors();
            if (Value.Left != null)
                PrintLink(top + 1, "┌", "┘", Value.Left.StartPos + Value.Left.Size / 2, Value.StartPos);
            if (Value.Right != null)
                PrintLink(top + 1, "└", "┐", Value.EndPos - 1, Value.Right.StartPos + Value.Right.Size / 2);
        }

        private static void PrintLink(int top, string start, string end, int startPos, int endPos)
        {
            Print(start, top, startPos);
            Print("─", top, startPos + 1, endPos);
            Print(end, top, endPos);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }

        private static void SwapColors()
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
        }
    }
}
