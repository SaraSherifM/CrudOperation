using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CrudOperation.Models
{
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class EmployeeInfo
    {
    }
    public class EmployeeMetaData
    {
        [Required(AllowEmptyStrings = false,ErrorMessage ="Please provide a valid Name")]
        public String Name { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Specify the Office")]
        public String Office { get; set; }

    }
}