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

    partial class Thread
    {
        public Thread(string id, string title, User_Name creator = null, int ? views = null, int ? replies = null, int ? rating = null)
        {
            Id = id;
            Title = title;
            Creator = creator ?? Creator;
            Views = views ?? Views;
            Replies = replies ?? Replies;
            Rating = rating ?? Rating;
        }

        public Thread With(string id = null, string title = null, User_Name creator = null, int ? views = null, int ? replies = null, int ? rating = null)
        {
            return new Thread(id ?? Id, title ?? Title, creator ?? Creator, views ?? Views, replies ?? Replies, rating ?? Rating);
        }
    }

    partial class Post
    {
        public Post(int id, string title, string body)
        {
            Id = id;
            Title = title;
            Body = body;
        }

        public Post With(int ? id = null, string title = null, string body = null)
        {
            return new Post(id ?? Id, title ?? Title, body ?? Body);
        }
    }
}