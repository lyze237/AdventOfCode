//-----------------------------------------------------------------------
// <copyright>
// MIT License
//
// Copyright (c) 2018 Michael Weinberger lyze@owl.sh
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Day24
{
    public class Node
    {
        public Component Component { get; set; }

        public Node Parent { get; set; }
        public List<Node> Childs { get; set; }

        public Node(Component component, Node parent)
        {
            Component = component;
            Childs = new List<Node>();
            Parent = parent;

            if (GetParentSide() == -1)
                throw new ArgumentException("Can't connect parent with child");
        }

        public int GetChildSide()
        {
            var parentSide = GetParentSide();
            
            return parentSide == Component.Left ? Component.Right : Component.Left;
        }

        public int GetParentSide()
        {
            if (Parent == null)
                return 0;
            
            if (Component.Left == Parent.Component.Left)
            {
                return Component.Left;
            }
            if (Component.Left == Parent.Component.Right)
            {
                return Component.Left;
            }
            if (Component.Right == Parent.Component.Left)
            {
                return Component.Right;
            }
            if (Component.Right == Parent.Component.Right)
            {
                return Component.Right;
            }
            return -1;
        }
    }
}
