namespace Markdown
{
    public class BoldState : State
    {
        public BoldState(int startIndex) : base(startIndex)
        {
        }

        public BoldState(int startIndex, State parent) : base(startIndex)
            => Parent = parent;


        public override int DeletedUnderlines => 4;
        public override string OpenTag => "<strong>";
        public override string CloseTag => "</strong>";

        public override bool IsNeedChangeState(ToHtmlRenderer renderer, int underlinesCount)
        {
            if (underlinesCount == 1)
                if (IsIndexBeforeWhiteSpace(renderer.Markdown, renderer.Index, underlinesCount)) return false;
            if (underlinesCount == 2)
                if (IsIndexAfterWhiteSpace(renderer.Markdown, renderer.Index)) return false;
            if (IsIndexNearNumbers(renderer.Markdown, renderer.Index, underlinesCount)) return false;
            return underlinesCount < 3;
        }


        public override State ChangeState(ToHtmlRenderer renderer, int underlinesCount)
        {

            if (underlinesCount == 1) return new ItalicState(renderer.Index, this);
            EndIndex = renderer.Index++;
            if (underlinesCount == 2) return IsInner
               ? Parent
               : new CommonState(renderer.Index); ;
            return this;
        }

        public override string SetTags(ToHtmlRenderer renderer, int indexShift)
        {
            if (IsInner && Parent.IsClosed) return renderer.Markdown;
            return renderer.Markdown
                .Remove(EndIndex + indexShift, 2)
                .Insert(EndIndex + indexShift, CloseTag)
                .Remove(StartIndex + indexShift, 2)
                .Insert(StartIndex + indexShift, OpenTag);
        }
    }
}