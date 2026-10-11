using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Core
{
    public class CanvasRegistry
    {
        private List<Canvas> _canvas = new List<Canvas>();
        public Canvas? ActiveCanvas { get; private set; } = null;
        public void SetActiveCanvas(Canvas canvas)
        {
            if (!_canvas.Contains(canvas))
                throw new InvalidOperationException("Canvas is not registered in the registry.");

            ActiveCanvas = canvas;
        }

        public IReadOnlyList<Canvas> GetCanvases() => _canvas.AsReadOnly();
        public void AddCanvas(Canvas canvas) => _canvas.Add(canvas);
        public void RemoveCanvas(Canvas canvas)
        {
            bool removed = _canvas.Remove(canvas);
            if (!removed)
                throw new InvalidOperationException("Canvas is not registered in the registry.");

            if (ActiveCanvas == canvas)
                ActiveCanvas = null;
        }
        public void ClearCanvases() => _canvas.Clear(); 
    }
}
