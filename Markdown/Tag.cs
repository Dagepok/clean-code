namespace Markdown
{
    public class Tag
    {
        public Tag(bool isOpening)
            => IsOpening = isOpening;

        public virtual string OpenTag => "";
        public virtual string CloseTag => "";
        public virtual int UnderlinesCount => 0;

        public virtual bool IsOpening { get; }

        public override string ToString()
            => IsOpening ? OpenTag : CloseTag;
    }

    public class ItalicTag : Tag
    {
        public override string OpenTag => "<em>";
        public override string CloseTag => "</em>";
        public override int UnderlinesCount => 1;

        public ItalicTag(bool isOpening) : base(isOpening)
        {
        }
    }
    public class BoldTag : Tag
    {
        public override string OpenTag => "<strong>";
        public override string CloseTag => "</strong>";
        public override int UnderlinesCount => 2;

        public BoldTag(bool isOpening) : base(isOpening)
        {
        }
    }
}