using System.ComponentModel.DataAnnotations;

namespace YourProjectName.ViewModels.MyData
{
    public class MyDataViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        [MaxLength(150)]
        public string Contact { get; set; }
        public byte[]? Signature { get; set; }
        public IFormFile? PostedFile { get; set; }


    }
}
