using CustomDotNetExtensions;
using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AiArtPromptStudio.Main
{

    [Serializable]
    internal class Prompt : IIdentifiedNamed
    {
        [JsonProperty] string _promptName;
        [JsonProperty] List<SubPrompt> _subPrompts;

        /// <summary>
        /// Returns a COPY of our subPrompts list
        /// </summary>
        public List<SubPrompt> SubPrompts { get { return _subPrompts.ToList(); } }

        #region IIdentifiedNamed implementation
        public string Name => _promptName;

        public ulong ID => 0;

        public DebugMode DebugMode => DebugMode.Debug;

        public void HighlightInEditor() { }
        #endregion IIdentifiedNamed implementation

        internal event EventHandler<PromptEventArgs> Updated;

        const string DEFAULT_PROMPT_NAME = "Default Prompt Name";
        const string DEFAULT_SEPARATOR = ", ";

        public Prompt()
        {
            _subPrompts = new List<SubPrompt>();
            _promptName = DEFAULT_PROMPT_NAME;
        }

        #region Subprompt Order Modification
        internal void MoveToFront(SubPrompt subPrompt) => _subPrompts.MoveItemToFront(subPrompt);
        internal void MoveToEnd(SubPrompt subPrompt) => _subPrompts.MoveItemToEnd(subPrompt);
        internal void MoveTo(SubPrompt subPrompt, int newIndex) => _subPrompts.MoveItemTo(subPrompt, newIndex);
        internal void Swap(SubPrompt subToSwap1, SubPrompt subToSwap2) => _subPrompts.Swap(subToSwap1, subToSwap2);

        /// <summary>
        /// Reorders the 
        /// </summary>
        /// <param name="newPromptOrder"></param>
        internal void Reorder(List<SubPrompt> newPromptOrder)
        {
            bool providedListIsCorrect = newPromptOrder.All(_subPrompts.Contains) && _subPrompts.All(newPromptOrder.Contains);
            
            if (!providedListIsCorrect)
            {
                Logger.Error(this, $"Received an incorrect number of {typeof(SubPrompt)}s for the Reorder function." +
                    $"Make sure you use {GetType()}'s methods for SubPrompt deletion.");
            }

            _subPrompts = newPromptOrder.ToList();
            UpdateAll();
        }
        #endregion Subprompt Order Modification

        internal SubPrompt CreateNewSubPromptAtIndex(int index)
        {
            SubPrompt subPrompt = new SubPrompt(string.Empty, DEFAULT_SEPARATOR);
            _subPrompts.Insert(index, subPrompt);
            return subPrompt;
        }

        internal void ChangeSubPrompt(int index, string newSubPrompt)
        {
            _subPrompts[index].Text = newSubPrompt;
        }

        void UpdateAll()
        {
            Action<SubPrompt> subPromptAction = (SubPrompt s) => s.InvokeUpdateFromPrompt(this);
            _subPrompts.ForEach(subPromptAction);

            Updated.Invoke(this, new PromptEventArgs(this));
        }

        public string GetFullPrompt(AITool toolType)
        {
            string textPrompt = string.Empty;
            for (int i = 0; i < _subPrompts.Count; i++)
            {
                SubPrompt subPrompt = _subPrompts[i];                
                textPrompt += subPrompt.Text + subPrompt.Separator;
                Logger.Log(this, "Warning: proper separator logic not implemented");
            }

            return textPrompt;
        }
    }


    internal class PromptEventArgs : EventArgs
    {
        //consider just changing this to a collection of sub prompts
        internal Prompt prompt { get; private set; }

        internal PromptEventArgs(Prompt prompt)
        {
            this.prompt = prompt;
        }
    }
}