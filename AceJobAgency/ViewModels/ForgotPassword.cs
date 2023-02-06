using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace AceJobAgency.ViewModels
{
	public class ForgotPassword
	{
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
