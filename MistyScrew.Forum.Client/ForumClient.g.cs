using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using NitroBolt.Functional;
using NitroBolt.Immutable;
using NitroBolt.Functional;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MistyScrew.Forum
{
    partial class Area
    {
        public Area(string name, Board[] boards = null)
        {
            Name = name;
            Boards = boards ?? Boards;
        }

        public Area With(string name = null, Board[] boards = null)
        {
            return new Area(name ?? Name, boards ?? Boards);
        }

        public Area With_Boards(params Board[] boards)
        {
            return With(boards: boards);
        }
    }

    public static partial class AreaHelper
    {
        public static Area By(this IEnumerable<Area> items, string name = null)
        {
            if (name != null)
                return items.FirstOrDefault(_item => _item.Name == name);
            return null;
        }
    }

    partial class Board
    {
        public Board(string name, string title, string description, User_Name[] moderators = null)
        {
            Name = name;
            Title = title;
            Description = description;
            Moderators = moderators ?? Moderators;
        }

        public Board With(string name = null, string title = null, string description = null, User_Name[] moderators = null)
        {
            return new Board(name ?? Name, title ?? Title, description ?? Description, moderators ?? Moderators);
        }

        public Board With_Moderators(params User_Name[] moderators)
        {
            return With(moderators: moderators);
        }
    }

    public static partial class BoardHelper
    {
        public static Board By(this IEnumerable<Board> items, string name = null, string title = null, string description = null)
        {
            if (name != null)
                return items.FirstOrDefault(_item => _item.Name == name);
            if (title != null)
                return items.FirstOrDefault(_item => _item.Title == title);
            if (description != null)
                return items.FirstOrDefault(_item => _item.Description == description);
            return null;
        }
    }

    partial class BoardState
    {
        public BoardState(int ? threadCount, int ? postCount, BoardStateLastPost lastPost)
        {
            ThreadCount = threadCount;
            PostCount = postCount;
            LastPost = lastPost;
        }

        public BoardState With(int ? threadCount = null, int ? postCount = null, BoardStateLastPost lastPost = null)
        {
            return new BoardState(threadCount ?? ThreadCount, postCount ?? PostCount, lastPost ?? LastPost);
        }
    }

    public static partial class BoardStateHelper
    {
        public static BoardState By(this IEnumerable<BoardState> items, int ? threadCount = null, int ? postCount = null, BoardStateLastPost lastPost = null)
        {
            if (threadCount != null)
                return items.FirstOrDefault(_item => _item.ThreadCount == threadCount);
            if (postCount != null)
                return items.FirstOrDefault(_item => _item.PostCount == postCount);
            if (lastPost != null)
                return items.FirstOrDefault(_item => _item.LastPost == lastPost);
            return null;
        }
    }

    partial class User_Name
    {
        public User_Name(string name)
        {
            Name = name;
        }

        public User_Name With(string name = null)
        {
            return new User_Name(name ?? Name);
        }
    }

    public static partial class User_NameHelper
    {
        public static User_Name By(this IEnumerable<User_Name> items, string name = null)
        {
            if (name != null)
                return items.FirstOrDefault(_item => _item.Name == name);
            return null;
        }
    }
}