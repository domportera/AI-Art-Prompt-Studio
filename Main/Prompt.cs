using CustomDotNetExtensions;
using Godot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AiArtPromptStudio.Main
{

    [Serializable]
    internal class Prompt
    {
        [JsonProperty] string promptName;
        [JsonProperty] List<SubPrompt> subPrompts;

        /// <summary>
        /// Returns a COPY of our subPrompts list
        /// </summary>
        public List<SubPrompt> SubPrompts { get { return subPrompts.ToList(); } }
        public string PromptName { get { return promptName; } private set { promptName = value; } }

        internal event EventHandler<PromptEventArgs> Updated;

        const string DEFAULT_PROMPT_NAME = "Default Prompt Name";

        public Prompt()
        {
            subPrompts = new List<SubPrompt>();
            promptName = DEFAULT_PROMPT_NAME;
        }

        internal void Reorder(int[] indices)
        {
            subPrompts = indices.Select(index =>
            {
                if (index > subPrompts.LastIndex())
                {
                    Logger.Error(this, $"Attempting to reorder {GetType()} with indices that exceed the number of {typeof(SubPrompt)}s we have. Returning null.");
                    return null;
                }
                return subPrompts[index];
            }).ToList();
            UpdateAll();
        }

        internal void ChangeSubPrompt(int index, string newSubPrompt)
        {
            subPrompts[index].Text = newSubPrompt;
        }

        void UpdateAll()
        {
            Action<SubPrompt> subPromptAction = (SubPrompt s) => s.InvokeUpdateFromPrompt(this);
            subPrompts.ForEach(subPromptAction);

            Updated.Invoke(this, new PromptEventArgs(this));
        }

        public string GetFullPrompt(AITool toolType)
        {
            string textPrompt = string.Empty;
            for (int i = 0; i < subPrompts.Count; i++)
            {
                SubPrompt subPrompt = subPrompts[i];
                textPrompt += subPrompt.Text + subPrompt.Separator;
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