using Newtonsoft.Json;
using System;

namespace AiArtPromptStudio.Main
{
    [Serializable]
    internal class SubPrompt
    {
        [JsonProperty] protected string text;
        public virtual string Text {
            get {
                return text;
            }
            set {
                text = value;
                Updated(this, NewEventArgs);
            }
        }

        [JsonProperty] protected string separator;
        public virtual string Separator {
            get {
                return separator;
            }
            set {
                separator = value;
                Updated(this, NewEventArgs);
            }
        }

        private SubPromptEventArgs NewEventArgs => new SubPromptEventArgs(this);

        internal event EventHandler<SubPromptEventArgs> Updated;

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
