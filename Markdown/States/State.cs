namespace Markdown
{
    public abstract class State
    {
        protected State(int startIndex)
        {
            StartIndex = startIndex;
            EndIndex = -1;
        }

        public abstract Tag Tag { get; }

        protected State Parent { get; set; }
        protected bool IsInner => Parent != null;
        public virtual bool IsClosed => EndIndex > StartIndex;
        public int StartIndex { get; protected set; }
        public int EndIndex { get; protected set; }
        public int IndexShift => Tag.OpenTag.Length + Tag.CloseTag.Length - Tag.UnderlinesCount;

        public abstract State ChangeState(HtmlRenderer renderer, int underlinesCount);


        public virtual bool IsNeedChangeState(HtmlRenderer renderer, int underlinesCount)
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
        {
            return index > 0 && char.IsWhiteSpace(str[index - 1]);
        }

        protected bool IsIndexBeforeWhiteSpace(string str, int index, int underlinsCount)
        {
            return index + underlinsCount < str.Length && char.IsWhiteSpace(str[index + underlinsCount]);
        }
       
        public abstract Tag GetEndTag();

        public abstract Tag GetStartTag();
    }
}