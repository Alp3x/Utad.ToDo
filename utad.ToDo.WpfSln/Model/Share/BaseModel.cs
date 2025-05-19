namespace utad.ToDo.Wpf.Models.Share
{
    public class BaseModel
    {
        public BaseModel() {
            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString(); 
            }
        }
        public string Id { get; set; } = string.Empty;  
    }
}
