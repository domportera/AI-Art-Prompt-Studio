using Godot;
using System;
using CustomDotNetExtensions;
using Object = Godot.Object;


namespace GodotExtensions
{
    public abstract class NodeExt : Node, IIdentifiedNamed
    {
        public ulong ID => GetInstanceId();

        [Export] DebugMode debugMode = DebugMode.Debug;
        public DebugMode DebugMode => debugMode;
        string IIdentifiedNamed.Name => Name;

        // Declare member variables here. Examples:
        // private int a = 2;
        // private string b = "text";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {

        }

        public virtual void HighlightInEditor()
        {
            //override this with however this object could be made more clear in editor
        }
    }
}