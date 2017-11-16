namespace Markdown
{
    public class CommonState : State
    {
        public CommonState(int startIndex) : base(startIndex)
        {
        }

        public override Tag Tag => new Tag(false);

        public override bool IsClosed => false;

        public override State ChangeState(HtmlRenderer renderer, int underlinesCount)
        {
            EndIndex = renderer.Index;
            if (underlinesCount == 1) return new ItalicState(renderer.Index);
            if (underlinesCount == 2) return new BoldState(renderer.Index++);
            return this;
        }

        public override Tag GetEndTag() => new Tag(false);

        public override Tag GetStartTag() => new Tag(true);
    }
}