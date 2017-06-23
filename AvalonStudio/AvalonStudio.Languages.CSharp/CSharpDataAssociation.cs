﻿namespace AvalonStudio.Languages.CSharp
{
    using Avalonia.Input;
    using AvaloniaEdit.Document;
    using AvaloniaEdit.Rendering;
    using AvalonStudio.Extensibility.Editor;
    using AvalonStudio.Languages;
    using CPlusPlus;
    using Projects.OmniSharp;
    using System;
    using System.Collections.Generic;

    internal class CSharpDataAssociation
    {
        public CSharpDataAssociation(TextDocument textDocument)
        {
            BackgroundRenderers = new List<IBackgroundRenderer>();
            DocumentLineTransformers = new List<IVisualLineTransformer>();

            TextColorizer = new TextColoringTransformer(textDocument);
            TextMarkerService = new TextMarkerService(textDocument);

            BackgroundRenderers.Add(new BracketMatchingBackgroundRenderer());
            BackgroundRenderers.Add(TextMarkerService);

            DocumentLineTransformers.Add(TextColorizer);
        }

        public OmniSharpSolution Solution { get; set; }
        public TextColoringTransformer TextColorizer { get; }
        public TextMarkerService TextMarkerService { get; }
        public List<IBackgroundRenderer> BackgroundRenderers { get; }
        public List<IVisualLineTransformer> DocumentLineTransformers { get; }
        public EventHandler<TextInputEventArgs> TextInputHandler { get; set; }
    }
}