using Engine.Components;
using Engine.Content;
using Engine.Core;
using Engine.Data;
using Engine.Graphics;
using Microsoft.Extensions.Logging;
using Silk.NET.Windowing;
using System.ComponentModel;
using System.Drawing;

namespace Engine.Rendering
{
    public class RenderPipeline
    {
        private readonly IGraphicsDevice _graphicsDevice;
        private readonly IWindow _window;
        private readonly IResourceProvider _resources;

        private readonly ILogger<RenderPipeline> _logger;
        public RenderPipeline(
            IGraphicsDevice graphicsDevice, 
            IWindow window,
            ILogger<RenderPipeline> logger,
            IResourceProvider resources)
        {
            _graphicsDevice = graphicsDevice;
            _window = window;
            _logger = logger;
            _resources = resources;
        }

        public void Render(Scene scene)
        {
            foreach (Camera camera in scene.GetComponentsOfTypeInEntities<Camera>())
            {
                Size windowSize = new Size(_window.Size.X, _window.Size.Y);
                Viewport newViewport = camera.ViewRectangle * windowSize;
                _graphicsDevice.SetViewport(newViewport);

                foreach (Renderer renderer in scene.GetComponentsOfTypeInEntities<Renderer>())
                {
                    Mesh mesh = renderer.Renderable.Mesh;
                    Material material = renderer.Renderable.Material ?? _resources.Defaults.Materials.Unlit;
                    _graphicsDevice.Draw(mesh, material, camera.GetProjectionMatrix(windowSize));
                }
                   
            }
        }
    }
}
