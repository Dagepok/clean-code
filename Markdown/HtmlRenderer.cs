using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;

namespace Markdown
{
    public class HtmlRenderer
    {
        public HtmlRenderer(string markdown)
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
                TryRenderChar();
            SetTages();
            return RemoveEscapeChars(Markdown);
        }

        private static string RemoveEscapeChars(string str)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
                if (str[i] != '\\')
                    sb.Append(str[i]);
                else if (i != str.Length - 1)
                    sb.Append(str[++i]);
            return sb.ToString();
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
            var sb = new StringBuilder();
            var tags = GetTags();
            for (var i = 0; i < Markdown.Length; i++)
            {
                if (tags.ContainsKey(i))
                {
                    sb.Append(tags[i]);
                    i += tags[i].UnderlinesCount - 1;
                }
                else sb.Append(Markdown[i]);
            }
            Markdown = sb.ToString();
        }

        private Dictionary<int, Tag> GetTags()
        {
            var tags = new Dictionary<int, Tag>();
            foreach (var state in UsedStates)
                if (state.IsClosed)
                {
                    tags.Add(state.EndIndex, state.GetEndTag());
                    tags.Add(state.StartIndex, state.GetStartTag());
                }
            return tags;
        }

        private void TryRenderChar()
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
            if (UsedStates.Count <= 1 || UsedStates[UsedStates.Count - 1].Parent != State)
                UsedStates.Add(State);
        }
    }
}