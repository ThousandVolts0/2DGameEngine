using Engine.Components;
using Engine.Content;
using Engine.Core;
using Engine.Data;
using Engine.Graphics;
using Microsoft.Extensions.Logging;
using Silk.NET.Windowing;
using System.Drawing;
using System.Numerics;

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
            IReadOnlyList<Camera> cameras = scene.GetComponentsOfTypeInEntities<Camera>();
            IReadOnlyList<Renderer> renderers = scene.GetComponentsOfTypeInEntities<Renderer>();

            foreach (Camera camera in cameras)
            {
                UpdateViewport(camera);

                foreach (Renderer renderer in renderers)
                    Render(renderer, camera);
            }
        }

        private Size GetWindowSize() => new Size(_window.Size.X, _window.Size.Y);

        private void Render(Renderer renderer, Camera camera)
        {
            Transform? entityTransform = renderer.Entity.GetComponent<Transform>();
            Transform? cameraTransform = camera.Entity.GetComponent<Transform>();

            Mesh mesh = renderer.Renderable.Mesh;
            Material material = renderer.Renderable.Material ?? _resources.Defaults.Materials.Unlit; // Fallback to default material if none specified

            Matrix4x4 world = entityTransform != null ? entityTransform.ModelMatrix : Matrix4x4.Identity;
            Matrix4x4 view = camera.GetViewMatrix(cameraTransform);
            Matrix4x4 proj = camera.GetProjectionMatrix(GetWindowSize());

            RenderInfo info = new(world, view, proj);
            _graphicsDevice.Draw(mesh, material, info);
        }

        private void UpdateViewport(Camera camera)
        {
            Viewport newViewport = camera.ViewRectangle * GetWindowSize();
            _graphicsDevice.SetViewport(newViewport);
        }
    }
}
