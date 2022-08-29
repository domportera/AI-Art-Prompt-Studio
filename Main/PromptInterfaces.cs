using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiArtPromptStudio.Main
{
    internal interface ISubPromptManipulator : IDisposable
    {
        protected SubPrompt MySubPrompt { get; set; }
        public void ChangeSubPrompt(string subPrompt)
        {
            MySubPrompt.Text = subPrompt;
        }
    }

    internal interface ISubPromptVisualEditor
    {

    }

    internal interface IPromptVisualEditor
    {

    }
}
