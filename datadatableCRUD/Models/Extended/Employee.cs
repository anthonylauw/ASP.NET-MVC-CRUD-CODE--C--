﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace datadatableCRUD.Models
{
    [MetadataType(typeof(EmployeeMetadata))]
    public partial class employee
    {

    }

    public class EmployeeMetadata
    {
        [Required (AllowEmptyStrings=false, ErrorMessage ="Please Provide the first name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Provide the Last name")]
        public string LastName { get; set; }
    }
}