using Newtonsoft.Json;
using System;

namespace AiArtPromptStudio.Main
{
    [Serializable]
    internal class SubPrompt
    {
        [JsonProperty] string _text;
        public virtual string Text {
            get {
                return _text;
            }
            set {
                _text = value;
                Updated(this, NewEventArgs);
            }
        }

        [JsonProperty] string _separator;
        public virtual string Separator { //should override this in child classes, like SubpromptMidjourney
            get {
                return _separator;
            }
            set {
                _separator = value;
                Updated(this, NewEventArgs);
            }
        }

        private SubPromptEventArgs NewEventArgs => new SubPromptEventArgs(this);

        internal event EventHandler<SubPromptEventArgs> Updated;

        public SubPrompt(string thistext, string separator)
        {
            _text = thistext;
            _separator = separator;
        }

        internal void InvokeUpdateFromPrompt(Prompt sender)
        {
            Updated(sender, NewEventArgs);
        }
    }


    internal class SubPromptEventArgs : EventArgs
    {
        internal SubPromptEventArgs(SubPrompt subPrompt)
        {
            this.subPrompt = subPrompt;
        }

        internal SubPrompt subPrompt { get; private set; }
    }
}
