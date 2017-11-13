using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Markdown
{
    public class ToHtmlRenderer
    {
        public ToHtmlRenderer(string markdown)
        {
            Markdown = markdown;
            UsedStates = new List<State>();
        }

        internal string Markdown { get; set; }
        private State State { get; set; }
        internal List<State> UsedStates { get; }
        internal int Index { get; set; }

        public string Render()
        {
            State = new CommonState(Index);
            UsedStates.Add(State);
            for (; Index < Markdown.Length; Index++)
                CheckChar();
            SetTages();
            return RemoveEscapeChars(Markdown);
        }

        private static string RemoveEscapeChars(string str)
        {
            for (var i = 0; i < str.Length; i++)
                if (str[i] == '\\') str = str.Remove(i, 1);
            return str;
        }


        private int UnderilinesCount()
        {
            var index = Index;
            var count = 0;
            while (index < Markdown.Length && Markdown[index++] == '_' && count < 4)
                count++;
            return count;
        }

        private void SetTages()
        {
            var indexShift = 0;
            foreach (var state in UsedStates)
            {
                if (!state.IsClosed) continue;
                Markdown = state.SetTags(this, indexShift);
                indexShift += state.IndexShift;
            }
        }

        private void CheckChar()
        {
            if (Markdown[Index] == '\\') Index++;
            else if (Markdown[Index] == '_') CheckAndChangeState();
        }

        private void CheckAndChangeState()
        {
            var underlinesCount = UnderilinesCount();
            if (underlinesCount > 2)
            {
                Index += underlinesCount;
                return;
            }
            if (!State.IsNeedChangeState(this, underlinesCount)) return;
            State = State.ChangeState(this, underlinesCount);
            if (!UsedStates.Contains(State))
                UsedStates.Add(State);
        }

    }
}