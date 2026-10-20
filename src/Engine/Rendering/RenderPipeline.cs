using Engine.Components;
using Engine.Core;
using Engine.Graphics;
using Silk.NET.Windowing;
using System.Drawing;

namespace Engine.Rendering
{
    public class RenderPipeline
    {
        private readonly IGraphicsDevice _graphicsDevice;
        private readonly IWindow _window;

        public RenderPipeline(IGraphicsDevice graphicsDevice, IWindow window)
        {
            _graphicsDevice = graphicsDevice;
            _window = window;
        }

        public void Render(Scene scene)
        {
            foreach (Camera camera in scene.GetComponentsOfTypeInEntities<Camera>())
            {
                Size windowSize = new Size(_window.Size.X, _window.Size.Y);
                Viewport newViewport = camera.ViewRectangle * windowSize;
                _graphicsDevice.SetViewport(newViewport);

                foreach (Renderer renderer in scene.GetComponentsOfTypeInEntities<Renderer>())
                    _graphicsDevice.Draw(renderer.Info, camera.GetProjectionMatrix(windowSize));
            }
        }
    }
}
