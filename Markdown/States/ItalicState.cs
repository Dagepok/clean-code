namespace Markdown
{
    public class ItalicState : State
    {
        public ItalicState(int startIndex) : base(startIndex)
        {
        }

        public ItalicState(int startIndex, State parent) : base(startIndex)
            => Parent = parent;


        public override bool IsNeedChangeState(HtmlRenderer renderer, int underlinesCount)
        {
            if (underlinesCount == 1)
                if (IsIndexAfterWhiteSpace(renderer.Markdown, renderer.Index)) return false;
            if (underlinesCount == 2)
                if (IsIndexBeforeWhiteSpace(renderer.Markdown, renderer.Index, underlinesCount)) return false;
            if (IsIndexNearNumbers(renderer.Markdown, renderer.Index, underlinesCount)) return false;
            return underlinesCount < 3;
        }

        public override Tag GetEndTag() => new ItalicTag(false);

        public override Tag GetStartTag() => new ItalicTag(true);


        public override Tag Tag => new ItalicTag(true);

        public override State ChangeState(HtmlRenderer renderer, int underlinesCount)
        {
            if (underlinesCount == 2) return new BoldState(renderer.Index++, this);
            EndIndex = renderer.Index;
            if (underlinesCount == 1)
                return IsInner
                    ? Parent
                    : new CommonState(renderer.Index);
            return this;
        }
    }
}