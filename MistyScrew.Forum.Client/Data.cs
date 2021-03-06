﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MistyScrew.Forum
{
    public partial class Area
    {
        public readonly string Name;
        public readonly Board[] Boards;
    }

    public partial class Board
    {
        public readonly string Name;
        public readonly string Title;
        public readonly string Description;
        public readonly User_Name[] Moderators;

        public readonly BoardState State = null;
    }
    public partial class BoardState
    {
        public readonly bool? IsFlashed;
        public readonly int? ThreadCount;
        public readonly int? PostCount;
        public readonly BoardStateLastPost LastPost = null;
    }
    public partial class BoardStateLastPost
    {
        public readonly string Id;
        public readonly User_Name User;
        public readonly DateTime Time;
    }
    public partial class User_Name
    {
        public readonly string Name;
    }

    public partial class Thread
    {
        public readonly string Id;
        public readonly string Title;
        public readonly User_Name Creator = null;
        public readonly int Views = 0;
        public readonly int Replies = 0;
        public readonly int Rating = 0;
    }

    public partial class Post
    {
        public readonly int Id;
        public readonly string Title;
        public readonly string Body;
    }
}
