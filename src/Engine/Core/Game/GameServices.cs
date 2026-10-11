using Engine.Graphics;
using Engine.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Silk.NET.Windowing;

namespace Engine.Core.Game
{
    public class GameServices
    {
        public required IGraphicsDevice Graphics { get; init; }
        public required RenderPipeline RenderPipeline { get; init; }
        public required CanvasRegistry Canvases { get;init; }
        public required IWindow Window { get; init; }
    }
}
