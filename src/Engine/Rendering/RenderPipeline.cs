using Engine.Components;
using Engine.Core;
using Engine.Graphics;
using Silk.NET.Windowing;
using System.Drawing;

namespace Engine.Rendering
{
    public class RenderPipeline
    {
        private IGraphicsDevice _device;
        private IWindow _window;

        public RenderPipeline(IGraphicsDevice device, IWindow window)
        {
            _device = device;
            _window = window;
        }

        public void Render(Canvas world)
        {
            foreach (Camera camera in world.GetEntities().Where(e => e.HasComponent<Camera>()).Select(e => e.GetComponent<Camera>()!))
            {
                Size windowSize = new Size(_window.Size.X, _window.Size.Y);
                _device.SetViewport(camera.ViewportRect * windowSize);

                foreach (Element entity in world.GetEntities().Where(e => e.HasComponent<Renderer>()))
                {
                    Renderer renderer = entity.GetComponent<Renderer>()!;
                    _device.Draw(renderer.MeshData, renderer.Material, camera.GetProjectionMatrix(windowSize));
                }
            }
        }
    }
}
