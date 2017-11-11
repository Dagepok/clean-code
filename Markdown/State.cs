using System;

namespace Markdown
{
    public abstract class State
    {
        protected State(int startIndex) => StartIndex = startIndex;

        public bool IsClosed => EndIndex > StartIndex;
        private int StartIndex { get; }
        private int EndIndex { get; set; }
        public abstract string OpenTag { get; }
        public abstract string CloseTag { get; }


        public abstract bool checkPlace(string str, int index);
        public abstract State ChangeState();
        /// <summary>
        /// Set tag at index in str 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="index"></param>
        /// <returns name="new index"> Index after setting tag </returns>
        public abstract int SetTag(string str, int index);
    }

    public class CommonState : State
    {
        public new bool IsClosed => true;
        public CommonState(int startIndex) : base(startIndex)
        {
        }
        public override string OpenTag => "";

        public override string CloseTag => "";

        public override State ChangeState()
        {
            throw new NotImplementedException();
        }

        public override bool checkPlace(string str, int index)
        {
            throw new NotImplementedException();
        }

        public override int SetTag(string str, int index)
        {
            throw new NotImplementedException();
        }

    }
    public class ItalicState : State
    {
        public ItalicState(int startIndex) : base(startIndex)
        {
        }
        public override string OpenTag => "<em>";
        public override string CloseTag => "</em>";


        public override bool checkPlace(string str, int index)
        {
            throw new NotImplementedException();
        }

        public override State ChangeState()
        {
            throw new NotImplementedException();
        }

        public override int SetTag(string str, int index)
        {
            throw new NotImplementedException();
        }

    }

    public class BoldState : State
    {
        public BoldState(int startIndex) : base(startIndex)
        {
        }

        public override string OpenTag => "<strong>";
        public override string CloseTag => "</strong>";

        public override bool checkPlace(string str, int index)
        {
            throw new NotImplementedException();
        }

        public override State ChangeState()
        {
            throw new NotImplementedException();
        }

        public override int SetTag(string str, int index)
        {
            throw new NotImplementedException();
        }


    }
}