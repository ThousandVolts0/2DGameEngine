namespace Engine.Core
{
    public class Canvas
    {
        public string Name { get; set; } = "Canvas";

        private Dictionary<int, Element> _elements = new Dictionary<int, Element>();
        private int _nextElementId = 0;

        public Canvas(string name = "Canvas")
        {
            Name = name;
        }

        public Canvas(List<Element> elements, string name = "Canvas")
        {
            Name = name;
            foreach (Element element in elements)
                AddElement(element);
        }

        public Element? GetElementByName(string name) => _elements.Values.FirstOrDefault(e => e.Name == name);
        public Element? GetElementById(int id) => _elements.ContainsKey(id) ? _elements[id] : null;
        public IReadOnlyList<Element> GetElements() => _elements.Values.ToList().AsReadOnly();

        public Canvas AddElement(Element element)
        {
            _elements.Add(_nextElementId++, element);
            return this;
        }

        public Canvas RemoveElement(Element element)
        {
            _elements.Remove(_elements.First(e => e.Value == element).Key);
            return this;
        }
    }
}
