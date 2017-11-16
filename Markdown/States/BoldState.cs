namespace Markdown
{
    public class BoldState : State
    {
        public BoldState(int startIndex) : base(startIndex)
        {
        }

        public override bool IsClosed
            => !(IsInsideItalic)
            && EndIndex > StartIndex;

        private bool IsInsideItalic => IsInner && Parent is ItalicState && Parent.IsClosed;

        public BoldState(int startIndex, State parent) : base(startIndex)
            => Parent = parent;

        public override bool IsNeedChangeState(HtmlRenderer renderer, int underlinesCount)
        {
            if (underlinesCount == 1)
                if (IsIndexBeforeWhiteSpace(renderer.Markdown, renderer.Index, underlinesCount)) return false;
            if (underlinesCount == 2)
                if (IsIndexAfterWhiteSpace(renderer.Markdown, renderer.Index)) return false;
            if (IsIndexNearNumbers(renderer.Markdown, renderer.Index, underlinesCount)) return false;
            return underlinesCount < 3;
        }

        public override Tag GetEndTag() => new BoldTag(false);

        public override Tag GetStartTag() => new BoldTag(true);


        public override Tag Tag => new BoldTag(true);

        public override State ChangeState(HtmlRenderer renderer, int underlinesCount)
        {
            if (underlinesCount == 1) return new ItalicState(renderer.Index, this);
            EndIndex = renderer.Index++;
            if (underlinesCount == 2)
                return IsInner
                    ? Parent
                    : new CommonState(renderer.Index);
            ;
            return this;
        }
    }
}