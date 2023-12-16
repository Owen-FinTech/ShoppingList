namespace ShoppingList
{
    public class GroceryItem
    {
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public string Annotation { get; set; }

        public GroceryItem()
        {
            Name = string.Empty;     // Initialize Name to an empty string.
            Annotation = string.Empty;  // Initialize Annotation to an empty string.
        }
    }  
}
