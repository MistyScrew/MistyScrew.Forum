using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using NitroBolt.Functional;
using NitroBolt.Immutable;
using System;
using System.Collections.Generic;
using System.Text;

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
        public Board(string name, string title, string description, User_Name[] moderators = null, BoardState state = null)
        {
            Name = name;
            Title = title;
            Description = description;
            Moderators = moderators ?? Moderators;
            State = state ?? State;
        }

        public Board With(string name = null, string title = null, string description = null, User_Name[] moderators = null, BoardState state = null)
        {
            return new Board(name ?? Name, title ?? Title, description ?? Description, moderators ?? Moderators, state ?? State);
        }

        public Board With_Moderators(params User_Name[] moderators)
        {
            return With(moderators: moderators);
        }
    }

    public static partial class BoardHelper
    {
        public static Board By(this IEnumerable<Board> items, string name = null, string title = null, string description = null, BoardState state = null)
        {
            if (name != null)
                return items.FirstOrDefault(_item => _item.Name == name);
            if (title != null)
                return items.FirstOrDefault(_item => _item.Title == title);
            if (description != null)
                return items.FirstOrDefault(_item => _item.Description == description);
            if (state != null)
                return items.FirstOrDefault(_item => _item.State == state);
            return null;
        }
    }

    partial class BoardState
    {
        public BoardState(bool ? isFlashed, int ? threadCount, int ? postCount, BoardStateLastPost lastPost = null)
        {
            IsFlashed = isFlashed;
            ThreadCount = threadCount;
            PostCount = postCount;
            LastPost = lastPost ?? LastPost;
        }

        public BoardState With(bool ? isFlashed = null, int ? threadCount = null, int ? postCount = null, BoardStateLastPost lastPost = null)
        {
            return new BoardState(isFlashed ?? IsFlashed, threadCount ?? ThreadCount, postCount ?? PostCount, lastPost ?? LastPost);
        }
    }

    public static partial class BoardStateHelper
    {
        public static BoardState By(this IEnumerable<BoardState> items, bool ? isFlashed = null, int ? threadCount = null, int ? postCount = null, BoardStateLastPost lastPost = null)
        {
            if (isFlashed != null)
                return items.FirstOrDefault(_item => _item.IsFlashed == isFlashed);
            if (threadCount != null)
                return items.FirstOrDefault(_item => _item.ThreadCount == threadCount);
            if (postCount != null)
                return items.FirstOrDefault(_item => _item.PostCount == postCount);
            if (lastPost != null)
                return items.FirstOrDefault(_item => _item.LastPost == lastPost);
            return null;
        }
    }

    partial class BoardStateLastPost
    {
        public BoardStateLastPost(string id, User_Name user, DateTime time)
        {
            Id = id;
            User = user;
            Time = time;
        }

        public BoardStateLastPost With(string id = null, User_Name user = null, DateTime? time = null)
        {
            return new BoardStateLastPost(id ?? Id, user ?? User, time ?? Time);
        }
    }

    public static partial class BoardStateLastPostHelper
    {
        public static BoardStateLastPost By(this IEnumerable<BoardStateLastPost> items, string id = null, User_Name user = null, DateTime? time = null)
        {
            if (id != null)
                return items.FirstOrDefault(_item => _item.Id == id);
            if (user != null)
                return items.FirstOrDefault(_item => _item.User == user);
            if (time != null)
                return items.FirstOrDefault(_item => _item.Time == time);
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