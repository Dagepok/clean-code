namespace Markdown
{
    public class CommonState : State
    {
        public CommonState(int startIndex) : base(startIndex)
        {
        }

        public new bool IsClosed => true;

        public override string OpenTag => "";

        public override string CloseTag => "";

        public override State ChangeState(ToHtmlRenderer renderer, int underlinesCount)
        {
            EndIndex = renderer.Index;
            if (underlinesCount == 1) return new ItalicState(renderer.Index);
            if (underlinesCount == 2) return new BoldState(renderer.Index++);
            return this;
        }

        public override string SetTags(ToHtmlRenderer renderer, int indexShift) 
            => renderer.Markdown;
    }
}