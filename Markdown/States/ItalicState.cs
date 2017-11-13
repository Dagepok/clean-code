﻿namespace Markdown
{
    public class ItalicState : State
    {
        public ItalicState(int startIndex) : base(startIndex)
        {
        }

        public ItalicState(int startIndex, State parent) : base(startIndex)
            => Parent = parent;

        public override string OpenTag => "<em>";
        public override string CloseTag => "</em>";


        public override bool IsNeedChangeState(ToHtmlRenderer renderer, int underlinesCount)
        {
            if (underlinesCount == 1)
                if (IsIndexAfterWhiteSpace(renderer.Markdown, renderer.Index)) return false;
            if (underlinesCount == 2)
                if (IsIndexBeforeWhiteSpace(renderer.Markdown, renderer.Index, underlinesCount)) return false;
            if (IsIndexNearNumbers(renderer.Markdown, renderer.Index, underlinesCount)) return false;
            return underlinesCount < 3;
        }


        public override State ChangeState(ToHtmlRenderer renderer, int underlinesCount)
        {
            if (underlinesCount == 2) return new BoldState(renderer.Index++, this);
            EndIndex = renderer.Index;
            if (underlinesCount == 1)
                return IsInner
                    ? Parent
                    : new CommonState(renderer.Index);
            return this;
        }

        public override string SetTags(ToHtmlRenderer renderer, int indexShift)
        {
            if (IsInner && Parent.IsClosed)
                indexShift = indexShift - Parent.CloseTag.Length - 2;

            return renderer.Markdown
                .Remove(EndIndex + indexShift, 1)
                .Insert(EndIndex + indexShift, CloseTag)
                .Remove(StartIndex + indexShift, 1)
                .Insert(StartIndex + indexShift, OpenTag);
        }
    }
}