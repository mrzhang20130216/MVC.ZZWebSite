using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.ZZAdminModels
{
    [MetadataType(typeof(productMetadata))]
    public partial class product
    {

        private Guid _tempID;
        public Guid TempID { get { if (_tempID == null) _tempID = System.Guid.NewGuid(); return _tempID; } }
    }
    public class productMetadata
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Number")]
        public string Number { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; } 
    }
}
