using System.Runtime.Remoting.Messaging;

namespace Markdown
{
    public abstract class State
    {
        protected State(int startIndex)
        {
            StartIndex = startIndex;
            EndIndex = -1;
        }

        protected State Parent { get; set; }
        protected bool IsInner => Parent != null;
        public bool IsClosed => EndIndex > StartIndex;
        protected int StartIndex { get; }
        protected int EndIndex { get; set; }
        public abstract string OpenTag { get; }
        public abstract string CloseTag { get; }
        public int IndexShift => OpenTag.Length + CloseTag.Length - DeletedUnderlines;
        public abstract int DeletedUnderlines { get; }

        public abstract State ChangeState(ToHtmlRenderer renderer, int underlinesCount);
        public abstract string SetTags(ToHtmlRenderer renderer, int indexShift);


        public virtual bool IsNeedChangeState(ToHtmlRenderer renderer, int underlinesCount)
        {
            if (!IsIndexNearNumbers(renderer.Markdown, renderer.Index, underlinesCount) &&
                !IsIndexBeforeWhiteSpace(renderer.Markdown, renderer.Index, underlinesCount))
                return underlinesCount < 3;
            renderer.Index += underlinesCount - 1;
            return false;
        }

        protected bool IsIndexNearNumbers(string str, int index, int underlinsCount)
        {
            var isLeftNumber = index > 0 && char.IsNumber(str, index - 1);
            var isRightNumber = index + underlinsCount < str.Length && char.IsNumber(str, index + underlinsCount);
            return isRightNumber || isLeftNumber;
        }

        protected bool IsIndexAfterWhiteSpace(string str, int index)
            => index > 0 && char.IsWhiteSpace(str[index - 1]);

        protected bool IsIndexBeforeWhiteSpace(string str, int index, int underlinsCount)
            => index + underlinsCount < str.Length && char.IsWhiteSpace(str[index + underlinsCount]);
    }
}