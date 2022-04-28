using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagementAPI.Dtos
{
    public class CreateUserRequestDto
    {
        [Required(ErrorMessage = "First Name is Required.")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
