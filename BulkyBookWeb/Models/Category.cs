using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Category
    {
        // type [Key] + ctrl + . to select primary key: add the using statement in 1st line : YouTube 1:07:30
        [Key] //things below it is PK
     // hot key for property: prop
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
        //Change display name of DisplayOrder in interface
		[DisplayName("Display Order")]
        [Range(1,100, ErrorMessage = "Display Order must be between 1 and 100.")]
		public int DisplayOrder { get; set; }
        // assign default value
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
